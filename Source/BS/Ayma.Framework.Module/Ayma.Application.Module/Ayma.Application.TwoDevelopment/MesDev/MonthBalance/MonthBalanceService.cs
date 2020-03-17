using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-08 14:02
    /// 描 述：财务月结
    /// </summary>
    public partial class MonthBalanceService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_MonthBalanceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.M_Months,
                t.M_MonthBalanceTime,
                t.M_MonthBalanceBy,
                t.M_Status,
                t.M_Remark
                ");
                strSql.Append("  FROM Mes_MonthBalance t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["M_Months"].IsEmpty())
                {
                    dp.Add("M_Months", "%" + queryParam["M_Months"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_Months Like @M_Months ");
                }
                return this.BaseRepository().FindList<Mes_MonthBalanceEntity>(strSql.ToString(),dp, pagination);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取Mes_MonthBalance表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_MonthBalanceEntity GetMes_MonthBalanceEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_MonthBalanceEntity>(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        #endregion

        #region 提交数据


        /// <summary>
        /// 月结、反月结
        /// </summary>
        /// <param name="month"></param>
        /// <param name="type"></param>
        /// <param name="msg"></param>
        public void PostOrCancel(string month, int type, out string msg)
        {
            try
            {
                DateTime dt=DateTime.Parse(month);//月结日期
                string lastDate = dt.AddMonths(-1).ToString("yyyy-MM");
                string nextDate= dt.AddMonths(1).ToString("yyyy-MM");
                msg = "";
                var entity = this.BaseRepository().FindEntity<Mes_MonthBalanceEntity>(r => r.M_Months == month);

                if (entity != null)
                {
                    #region 月结
                    if (type == 1)
                    {
                        if (entity.M_Status == 1)
                        {
                            msg = "已月结凭证，不能重复月结！";
                        }
                        else
                        {
                            string sql =
                                 @"SELECT [ID]
      ,[M_Months]
      ,[M_MonthBalanceTime]
      ,[M_MonthBalanceBy]
      ,[M_Status]
      ,[M_Remark]
  FROM [Mes_MonthBalance]
  WHERE LEFT(M_Months,7)='"+ lastDate + "'";

                            var rows=this.BaseRepository().FindList<Mes_MonthBalanceEntity>(sql);
                            if (rows == null || rows.Count() < 1)
                            {
                                msg = "上月月结的凭证不存在！";
                            }
                            else
                            {
                               var entityTemp= rows.First();
                               if (entityTemp.M_Status != 1)
                               {
                                   msg = "上月月结的凭证未月结，请先月结上月月结凭证！";
                               }
                               else
                               {
                                   PostMonthBalance(month, out msg);//月结
                               }
                            }
                        }
                    }
                    #endregion

                    #region 反月结

                    else
                    {
                        if (entity.M_Status != 1)
                        {
                            msg = "未月结凭证，不能反月结！";
                        }
                        else
                        {
                            string sql =
                                @"SELECT [ID]
      ,[M_Months]
      ,[M_MonthBalanceTime]
      ,[M_MonthBalanceBy]
      ,[M_Status]
      ,[M_Remark]
  FROM [Mes_MonthBalance]
  WHERE LEFT(M_Months,7)='" + nextDate + "'";

                            var rows = this.BaseRepository().FindList<Mes_MonthBalanceEntity>(sql);
                            bool success = true;
                            if (rows != null || rows.Count()>= 1)
                            {
                                var entityTemp = rows.First();
                                if (entityTemp.M_Status != 2)
                                {
                                    success = false;
                                    msg = "下月月结的凭证未反月结，请先反月结下月月结凭证！";
                                }
                            }

                            if (success)
                            {
                                CancelMonthBalance(month, out msg);//反月结
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    msg = "月结的凭证不存在！";
                }


            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }


        /// <summary>
        /// 月结
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool PostMonthBalance(string month, out string msg)
        {
            msg = "";

            bool success = true;
            var db = this.BaseRepository().BeginTrans();
            UserInfo user = LoginUserInfo.Get();

            try
            {
                DateTime dt = DateTime.Parse(month);//月结日期
                string lastDate = dt.AddMonths(-1).ToString("yyyy-MM");


                #region 月结库存、各单据数量

                //各项月结库存数据
                List<Mes_MonthBalanceDetailEntity> listMonthBalanceDetail = new List<Mes_MonthBalanceDetailEntity>();

                //加载上月月结凭证
                Mes_MonthBalanceEntity lastMonthBalanceEntity = new Mes_MonthBalanceEntity();
                if (success)
                {
                    string sql =
                        @"SELECT [ID]
      ,[M_Months]
      ,[M_MonthBalanceTime]
      ,[M_MonthBalanceBy]
      ,[M_Status]
      ,[M_Remark]
  FROM [Mes_MonthBalance]
  WHERE LEFT(M_Months,7)='" + lastDate + "'";

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceEntity>(sql);
                    if (rows != null && rows.Count() > 0)
                    {
                        lastMonthBalanceEntity = rows.First();
                    }
                }

                //加载上月月结库存
                List<Mes_MonthBalanceDetailEntity> listLastQty = new List<Mes_MonthBalanceDetailEntity>();
                if (success)
                {
                    string sql =
                        @"
SELECT 
M_StockCode,
M_StockName,
M_GoodsCode,
M_GoodsName,
M_StockQty M_LastQty
FROM 
Mes_MonthBalanceDetail
WHERE LEFT(M_Months,7)='" + lastDate + "'";
                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql);

                    if (rows != null && rows.Count() > 0)
                    {
                        listLastQty = rows.ToList();

                        foreach (var row in listLastQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);

                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_LastQty += row.M_LastQty;
                            }
                        }
                    }
                }

                //加载当月月结库存
                List<Mes_MonthBalanceDetailEntity> listNowQty = new List<Mes_MonthBalanceDetailEntity>();
                if (success)
                {
                    string sql =
                        @"
SELECT I_StockCode M_StockCode,
       I_StockName M_StockName,
       I_GoodsCode M_GoodsCode,
       I_GoodsName M_GoodsName,
       I_Qty M_StockQty
FROM Mes_Inventory";
                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql);

                    if (rows != null && rows.Count() > 0)
                    {
                        listNowQty = rows.ToList();

                        foreach (var row in listNowQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_StockQty += row.M_StockQty;
                            }
                        }
                    }
                }
                
                //入库数量
                List<Mes_MonthBalanceDetailEntity> listInQty = new List<Mes_MonthBalanceDetailEntity>(); 
                if (success)
                {
                    string sql =
                        @"
SELECT D.M_StockCode,
       D.M_StockName,
       D.M_GoodsCode,
       D.M_GoodsName,
       SUM(D.M_Qty) M_InQty
FROM Mes_MaterInHead H
    LEFT JOIN Mes_MaterInDetail D
        ON H.M_MaterInNo = D.M_MaterInNo
WHERE H.M_Status=3
AND (H.M_CreateDate>@starDate AND H.M_CreateDate<=@endDate)
GROUP BY 
       D.M_StockCode,
       D.M_StockName,
       D.M_GoodsCode,
       D.M_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate",  month.ToDate(),DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listInQty = rows.ToList();

                        foreach (var row in listInQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_InQty += row.M_InQty;
                            }
                        }
                    }
                }
                
                //退供应商数量
                List<Mes_MonthBalanceDetailEntity> listBackSupplyQty = new List<Mes_MonthBalanceDetailEntity>();
                if (success)
                {
                    string sql =
                        @"
SELECT H.B_StockCode M_StockCode,
       H.B_StockName M_StockName,
       D.B_GoodsCode M_GoodsCode,
       D.B_GoodsName M_GoodsName,
       SUM(D.B_Qty) M_BackSupplyQty
FROM Mes_BackSupplyHead H
    LEFT JOIN Mes_BackSupplyDetail D ON H.B_BackSupplyNo = D.B_BackSupplyNo
WHERE H.B_Status = 3
      AND (H.B_CreateDate >@starDate AND H.B_CreateDate <=@endDate)
GROUP BY H.B_StockCode,
		 H.B_StockName,
		 D.B_GoodsCode,
		 D.B_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listBackSupplyQty = rows.ToList();

                        foreach (var row in listBackSupplyQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_BackSupplyQty += row.M_BackSupplyQty;
                            }
                        }
                    }
                }


                //领料单数量
                List<Mes_MonthBalanceDetailEntity> listOutQty = new List<Mes_MonthBalanceDetailEntity>();
                if (success)
                {
                    string sql =
                        @"
SELECT D.C_StockCode M_StockCode,
       D.C_StockName M_StockName,
       D.C_GoodsCode M_GoodsCode,
       D.C_GoodsName M_GoodsName,
       SUM(D.C_Qty) M_OutQty
FROM Mes_CollarHead H
    LEFT JOIN Mes_CollarDetail D
        ON H.C_CollarNo = D.C_CollarNo
WHERE H.P_Status=3
      AND (H.C_CreateDate>@starDate AND H.C_CreateDate<=@endDate)
GROUP BY D.C_StockCode,
         D.C_StockName ,
         D.C_GoodsCode,
         D.C_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listOutQty = rows.ToList();

                        foreach (var row in listOutQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_OutQty += row.M_OutQty;
                            }
                        }
                    }
                }
               

                //退库单数量
                List<Mes_MonthBalanceDetailEntity> listBackStockQty = new List<Mes_MonthBalanceDetailEntity>(); 
                if (success)
                {
                    string sql =
                        @"
SELECT H.B_StockCode M_StockCode,
       H.B_StockName M_StockName,
       D.B_GoodsCode M_GoodsCode,
       D.B_GoodsName M_GoodsName,
       SUM(D.B_Qty) M_BackStockQty
FROM Mes_BackStockHead H
    LEFT JOIN Mes_BackStockDetail D
        ON H.B_BackStockNo = D.B_BackStockNo
WHERE H.B_Status=3
     AND (H.B_CreateDate>@starDate AND H.B_CreateDate<=@endDate)
GROUP BY H.B_StockCode,
         H.B_StockName ,
         D.B_GoodsCode,
         D.B_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listBackStockQty = rows.ToList();
                        foreach (var row in listBackStockQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_BackStockQty += row.M_BackStockQty;
                            }
                        }
                    }
                }


                //报废单数量
                List<Mes_MonthBalanceDetailEntity> listScrapQty = new List<Mes_MonthBalanceDetailEntity>();
                if (success)
                {
                    string sql =
                        @"
SELECT H.S_StockCode M_StockCode,
       H.S_StockName M_StockName,
       D.S_GoodsCode M_GoodsCode,
       D.S_GoodsName M_GoodsName,
       SUM(D.S_Qty) M_ScrapQty
FROM Mes_ScrapHead H
    LEFT JOIN Mes_ScrapDetail D
        ON H.S_ScrapNo = D.S_ScrapNo
WHERE H.S_Status=3
     AND (H.S_CreateDate>@starDate AND H.S_CreateDate<=@endDate)
GROUP BY H.S_StockCode,
         H.S_StockName ,
         D.S_GoodsCode,
         D.S_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listScrapQty = rows.ToList();

                        foreach (var row in listScrapQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_ScrapQty += row.M_ScrapQty;
                            }
                        }
                    }
                }


                //其他入库单数量
                List<Mes_MonthBalanceDetailEntity> listOtherInQty = new List<Mes_MonthBalanceDetailEntity>();
                if (success)
                {
                    string sql =
                        @"
SELECT H.O_StockCode M_StockCode,
       H.O_StockName M_StockName,
       D.O_GoodsCode M_GoodsCode,
       D.O_GoodsName M_GoodsName,
       SUM(D.O_Qty) M_OtherInQty
FROM Mes_OtherInHead H
    LEFT JOIN Mes_OtherInDetail D
        ON H.O_OtherInNo = D.O_OtherInNo
WHERE H.O_Status=3 
    AND (H.O_CreateDate>@starDate AND H.O_CreateDate<=@endDate)
GROUP BY H.O_StockCode,
         H.O_StockName ,
         D.O_GoodsCode,
         D.O_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listOtherInQty = rows.ToList();

                        foreach (var row in listOtherInQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_OtherInQty += row.M_OtherInQty;
                            }
                        }
                    }
                }


                //其他出库单数量
                List<Mes_MonthBalanceDetailEntity> listOtherOutQty = new List<Mes_MonthBalanceDetailEntity>(); 
                if (success)
                {
                    string sql =
                        @"
SELECT H.O_StockCode M_StockCode,
       H.O_StockName M_StockName,
       D.O_GoodsCode M_GoodsCode,
       D.O_GoodsName M_GoodsName,
       SUM(D.O_Qty) M_OtherOutQty
FROM Mes_OtherOutHead H
    LEFT JOIN Mes_OtherOutDetail D
        ON H.O_OtherOutNo = D.O_OtherOutNo
WHERE H.O_Status=3 
    AND (H.O_CreateDate>@starDate AND H.O_CreateDate<=@endDate)
GROUP BY H.O_StockCode,
         H.O_StockName ,
         D.O_GoodsCode,
         D.O_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listOtherOutQty = rows.ToList();

                        foreach (var row in listOtherOutQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_OtherOutQty += row.M_OtherOutQty;
                            }
                        }
                    }
                }
               

                //销售单数量
                List<Mes_MonthBalanceDetailEntity> listSaleQty = new List<Mes_MonthBalanceDetailEntity>(); 
                if (success)
                {
                    string sql =
                        @"
SELECT H.S_StockCode M_StockCode,
       H.S_StockName M_StockName,
       D.S_GoodsCode M_GoodsCode,
       D.S_GoodsName M_GoodsName,
       SUM(D.S_Qty) M_SaleQty
FROM Mes_SaleHead H
    LEFT JOIN Mes_SaleDetail D
        ON H.S_SaleNo = D.S_SaleNo
WHERE H.S_Status=3 
     AND (H.S_CreateDate>@starDate AND H.S_CreateDate<=@endDate)
GROUP BY H.S_StockCode,
         H.S_StockName ,
         D.S_GoodsCode,
         D.S_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listSaleQty = rows.ToList();

                        foreach (var row in listSaleQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_SaleQty += row.M_SaleQty;
                            }
                        }
                    }
                }
               

                //消耗单数量
                List<Mes_MonthBalanceDetailEntity> listExpendQty = new List<Mes_MonthBalanceDetailEntity>();
                if (success)
                {
                    string sql =
                        @"
SELECT H.E_StockCode M_StockCode,
       H.E_StockName M_StockName,
       D.E_GoodsCode M_GoodsCode,
       D.E_GoodsName M_GoodsName,
       SUM(D.E_Qty) M_ExpendQty
FROM Mes_ExpendHead H
    LEFT JOIN Mes_ExpendDetail D
        ON H.E_ExpendNo = D.E_ExpendNo
WHERE H.E_Status=3
   AND (H.E_CreateDate>@starDate AND H.E_CreateDate<=@endDate)
GROUP BY H.E_StockCode,
         H.E_StockName ,
         D.E_GoodsCode,
         D.E_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listExpendQty = rows.ToList();

                        foreach (var row in listExpendQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_ExpendQty += row.M_ExpendQty;
                            }
                        }
                    }
                }


                //调拨入数量
                List<Mes_MonthBalanceDetailEntity> listRequistInQty = new List<Mes_MonthBalanceDetailEntity>();
                if (success)
                {
                    string sql =
                        @"
SELECT H.R_StockToCode M_StockCode,
       H.R_StockToName M_StockName,
       D.R_GoodsCode M_GoodsCode,
       D.R_GoodsName M_GoodsName,
       SUM(D.R_Qty) M_RequistQty
FROM Mes_RequistHead H
    LEFT JOIN Mes_RequistDetail D
        ON H.R_RequistNo = D.R_RequistNo
WHERE H.R_Status=3 
  AND (H.R_CreateDate>@starDate AND H.R_CreateDate<=@endDate)
GROUP BY H.R_StockToCode,
         H.R_StockToName ,
         D.R_GoodsCode,
         D.R_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listRequistInQty = rows.ToList();

                        foreach (var row in listRequistInQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_RequistQty += row.M_RequistQty;
                            }
                        }
                    }
                }
                

                //调拨出数量
                List<Mes_MonthBalanceDetailEntity> listRequistOutQty = new List<Mes_MonthBalanceDetailEntity>(); 
                if (success)
                {
                    string sql =
                        @"
SELECT H.R_StockCode M_StockCode,
       H.R_StockName M_StockName,
       D.R_GoodsCode M_GoodsCode,
       D.R_GoodsName M_GoodsName,
       SUM(D.R_Qty) M_RequistQty
FROM Mes_RequistHead H
    LEFT JOIN Mes_RequistDetail D
        ON H.R_RequistNo = D.R_RequistNo
WHERE H.R_Status=3 
     AND (H.R_CreateDate>@starDate AND H.R_CreateDate<=@endDate)
GROUP BY H.R_StockCode,
         H.R_StockName ,
         D.R_GoodsCode,
         D.R_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listRequistOutQty = rows.ToList();

                        foreach (var row in listRequistOutQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_RequistQty -= row.M_RequistQty;
                            }
                        }
                    }
                }
               

                //抽检单数量
                List<Mes_MonthBalanceDetailEntity> listInspectQty = new List<Mes_MonthBalanceDetailEntity>();
                if (success)
                {
                    string sql =
                        @"
SELECT H.I_StockCode M_StockCode,
       H.I_StockName M_StockName,
       H.I_GoodsCode M_GoodsCode,
       H.I_GoodsName M_GoodsName,
       SUM(H.I_GoodsQty) M_InspectQty
FROM Mes_Inspect H
WHERE H.I_Status=2 
  AND (H.I_CreateDate>@starDate AND H.I_CreateDate<=@endDate)
GROUP BY H.I_StockCode,
         H.I_StockName ,
         H.I_GoodsCode,
         H.I_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listInspectQty = rows.ToList();

                        foreach (var row in listInspectQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_InspectQty += row.M_InspectQty;
                            }
                        }
                    }
                }


                //半成品入库数量
                List<Mes_MonthBalanceDetailEntity> listInWorkShopQty = new List<Mes_MonthBalanceDetailEntity>();
                if (success)
                {
                    string sql =
                        @"
               SELECT             
			        H.I_StockCode M_StockCode,
                    H.I_StockName M_StockName,
                    D.I_GoodsCode M_GoodsCode,
                    D.I_GoodsName M_GoodsName,
                    SUM(D.I_Qty) M_InWorkShopQty
                FROM Mes_InWorkShopHead H
                    LEFT JOIN Mes_InWorkShopDetail D
                    ON H.I_InNo = D.I_InNo
                WHERE H.I_Status = 3 
				    AND(H.I_CreateDate > @starDate AND H.I_CreateDate <= @endDate)
                GROUP BY H.I_StockCode,
                         H.I_StockName ,
                         D.I_GoodsCode,
                         D.I_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listInWorkShopQty = rows.ToList();

                        foreach (var row in listInWorkShopQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_InWorkShopQty += row.M_InWorkShopQty;
                            }
                        }
                    }
                }


                //半成品出库数量
                List<Mes_MonthBalanceDetailEntity> listOrgresOutQty = new List<Mes_MonthBalanceDetailEntity>(); 
                if (success)
                {
                    string sql =
                        @"
 SELECT H.O_StockCode M_StockCode,
       H.O_StockName M_StockName,
       D.O_GoodsCode M_GoodsCode,
       D.O_GoodsName M_GoodsName,
       SUM(D.O_Qty) M_OrgresOutQty
FROM Mes_OrgResHead H
    LEFT JOIN Mes_OrgResDetail D
        ON H.O_OrgResNo = D.O_OrgResNo
WHERE H.O_Status=3 
  AND (H.O_CreateDate>@starDate AND H.O_CreateDate<=@endDate)
GROUP BY H.O_StockCode,
         H.O_StockName ,
         D.O_GoodsCode,
         D.O_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceDetailEntity>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listOrgresOutQty = rows.ToList();

                        foreach (var row in listOrgresOutQty)
                        {
                            var entity = listMonthBalanceDetail.Find(r => r.M_StockCode == row.M_StockCode && r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                row.Create();
                                row.M_Months = month;
                                listMonthBalanceDetail.Add(row);
                            }
                            else
                            {
                                entity.M_OrgresOutQty += row.M_OrgresOutQty;
                            }
                        }
                    }
                }
                #endregion

                #region 成本价月结

                #region 原料成本价
                //成本价
                List<Mes_MonthBalancePrice> listCostPrice = new List<Mes_MonthBalancePrice>();

                //采购入库单统计
                List<Mes_MonthBalancePrice> listMaterInPrice = new List<Mes_MonthBalancePrice>();
                if (success)
                {
                    string sql =
                        @"
SELECT
       D.M_GoodsCode,
       D.M_GoodsName,
	   SUM(D.M_Qty*D.M_Price) M_TotalAmount,
       SUM(D.M_Qty) M_StockQty,
       1 M_Kind
FROM Mes_MaterInHead H
    LEFT JOIN Mes_MaterInDetail D
        ON H.M_MaterInNo = D.M_MaterInNo
WHERE H.M_Status=3
    AND (H.M_CreateDate>@starDate AND H.M_CreateDate<=@endDate)
GROUP BY 
       D.M_GoodsCode,
       D.M_GoodsName";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);

                    var rows = this.BaseRepository().FindList<Mes_MonthBalancePrice>(sql, dp);

                    if (rows != null && rows.Count() > 0)
                    {
                        listMaterInPrice = rows.ToList();

                        foreach (var row in listMaterInPrice)
                        {
                            var entity = listCostPrice.Find(r => r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                listCostPrice.Add(row);
                            }
                            else
                            {
                                entity.M_StockQty = row.M_StockQty;
                                entity.M_GoodsPrice = 0;
                                entity.M_TotalAmount = row.M_TotalAmount;
                                entity.M_Kind = row.M_Kind;
                            }
                        }
                    }
                }

                //上月成本价统计
                List<Mes_MonthBalancePrice> listLastCostPrice = new List<Mes_MonthBalancePrice>();
                if (success)
                {
                    string sql =
                        @"
SELECT
       P.M_GoodsCode
      ,P.M_GoodsName
      ,P.M_GoodsPrice M_LastPrice
      ,P.M_StockQty M_LastQty
	  ,G.G_Kind M_Kind
  FROM Mes_MonthBalancePrice P
  LEFT JOIN Mes_Goods G ON P.M_GoodsCode=G.G_Code
  WHERE LEFT(P.M_Months,7)='" + lastDate + "'";
                    var rows = this.BaseRepository().FindList<Mes_MonthBalancePrice>(sql);
                    if (rows != null && rows.Count() > 0)
                    {
                        listLastCostPrice = rows.ToList();

                        var filters = listLastCostPrice.Where(r => r.M_Kind == 1);

                        foreach (var row in filters)
                        {
                            var entity = listCostPrice.Find(r => r.M_GoodsCode == row.M_GoodsCode);
                            if (entity == null)
                            {
                                listCostPrice.Add(row);
                            }
                            else
                            {
                                entity.M_LastQty = row.M_LastQty;
                                entity.M_LastPrice = row.M_LastPrice;
                                entity.M_Kind = row.M_Kind;
                            }
                        }
                    }
                }


                //计算原料成本价
                List<Mes_MonthBalancePriceEntity> listMonthBalancePrice = new List<Mes_MonthBalancePriceEntity>();
                foreach (var row in listCostPrice)
                {
                    Mes_MonthBalancePriceEntity entity = new Mes_MonthBalancePriceEntity();
                    entity.Create();
                    entity.M_Months = month;
                    entity.M_GoodsCode = row.M_GoodsCode;
                    entity.M_GoodsName = row.M_GoodsName;

                    entity.M_LastQty = row.M_LastQty.HasValue? row.M_LastQty.Value:0;
                    entity.M_LastPrice = row.M_LastPrice.HasValue ? row.M_LastPrice.Value : 0;

                    decimal M_StockQty=row.M_StockQty.HasValue ? row.M_StockQty.Value : 0;
                    decimal M_TotalAmount=row.M_TotalAmount.HasValue ? row.M_TotalAmount.Value : 0;

                    if ((entity.M_LastQty + M_StockQty) == 0)
                    {
                        entity.M_GoodsPrice = 0;
                    }
                    else
                    {
                        entity.M_GoodsPrice = ((entity.M_LastQty * entity.M_LastPrice) + M_TotalAmount) / (entity.M_LastQty + M_StockQty);
                    }


                    var stocks = listOutQty.Where(r => r.M_GoodsCode == row.M_GoodsCode);
                    if (stocks != null && stocks.Count() > 0)
                    {
                        entity.M_StockQty = stocks.Sum(r => r.M_StockQty);
                    }
                    else
                    {
                        entity.M_StockQty = 0;
                    }

                    listMonthBalancePrice.Add(entity);
                }
                #endregion

                #region 半成品成本价

                //物料转换关系
                List<Mes_Product> listConvert = new List<Mes_Product>();
                if (success)
                {
                    string sql = @"SELECT C_Code M_GoodsCode,
       C_Name M_GoodsName,
       C_SecCode M_SecGoodsCode,
       C_SecName M_SecGoodsName
FROM Mes_Convert";
                    var rows = this.BaseRepository().FindList<Mes_Product>(sql);
                    if (rows != null && rows.Count() > 0)
                    {
                        listConvert = rows.ToList();
                    }
                    else
                    {
                        success = false;
                        msg = "物料转换信息不完整";
                    }
                }


                //本次月使用原料
                List<Mes_Product> listUseProducts = new List<Mes_Product>();
                if (success)
                {
                    string sql = @"SELECT DISTINCT
           D.O_GoodsCode M_GoodsCode,
           D.O_GoodsName M_GoodsName
    FROM Mes_OrgResHead H
        LEFT JOIN Mes_OrgResDetail D ON D.O_OrgResNo = H.O_OrgResNo
        LEFT JOIN Mes_Goods G ON G.G_Code = D.O_GoodsCode
    WHERE G.G_Kind = 1
          AND H.O_Status = 3
		  AND (H.O_CreateDate>@starDate AND H.O_CreateDate<=@endDate)";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);
                    var rows = this.BaseRepository().FindList<Mes_Product>(sql, dp);
                    if (rows != null && rows.Count() > 0)
                    {
                        listUseProducts = rows.ToList();
                    }
                    else
                    {
                        success = false;
                        msg = "物料转换信息不完整";
                    }
                }


                List<Mes_Product> listConvertDatas = new List<Mes_Product>();
                //循环从数据库读取本月数据
                if (success)
                {
                    string sql = @"SELECT    
           D.O_GoodsCode M_GoodsCode,
           D.O_GoodsName M_GoodsName,
		   D.O_Qty M_Qty,
		   D.O_SecGoodsCode M_SecGoodsCode,
		   D.O_SecGoodsName M_SecGoodsName,
		   D.O_SecQty M_SecQty
    FROM Mes_OrgResHead H
        LEFT JOIN Mes_OrgResDetail D ON D.O_OrgResNo = H.O_OrgResNo
        LEFT JOIN Mes_Goods G ON G.G_Code = D.O_GoodsCode
    WHERE G.G_Kind = 1
          AND H.O_Status = 3
		  AND (H.O_CreateDate>@starDate AND H.O_CreateDate<=@endDate)";

                    var dp = new DynamicParameters(new { });
                    dp.Add("@starDate", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endDate", month.ToDate(), DbType.DateTime);
                    var rows = this.BaseRepository().FindList<Mes_Product>(sql, dp);
                    if (rows != null && rows.Count() > 0)
                    {
                        listConvertDatas = rows.ToList();
                    }
                    else
                    {
                        success = false;
                        msg = "转换单数据不完整";
                    }
                }


                //根据使用原料获得最终bom表
                List<Mes_UseProduct> listBomProducts = new List<Mes_UseProduct>();
                if (success)
                {
                    foreach (var row in listUseProducts)
                    {
                        GetBomProducts(row.M_GoodsCode, 0, listConvert, listBomProducts);
                    }
                }


                //计算半成品成本价
                if (success)
                {
                    int maxLevel=listBomProducts.Max(r => r.M_Level);
                    for (int i = 1; i <= maxLevel; i++)
                    {
                        var rows=listBomProducts.Where(r => r.M_Level == i);


                        foreach (var row in rows)
                        {

                            var childs = row.ChildUseProducts;
                            if (childs == null || childs.Count < 1)
                            {
                                success = false;
                                msg = "转换关系存在错误";
                                break;
                            }

                            //转换后数据
                            var secDatas = listConvertDatas.Where(r => r.M_SecGoodsCode == row.M_GoodsCode);
                            if (secDatas.Count() < 1)
                                continue;

                            //转换后数量
                            decimal? secQty = secDatas.Sum(r => r.M_SecQty) / childs.Count;

                            //原料总价值
                            decimal? sumAmount = 0;
                            foreach (var child in childs)
                            {
                                //上级用料数据
                                var datas=secDatas.Where(r => r.M_GoodsCode == child.M_GoodsCode);

                                //上级用料数量
                                var qty = datas.Sum(r => r.M_Qty);

                                //上级成本价
                                decimal price = 0;
                                var goodsprice= listMonthBalancePrice.Find(r => r.M_GoodsCode == child.M_GoodsCode);
                                if (goodsprice != null)
                                {
                                    price = goodsprice.M_GoodsPrice.HasValue ? goodsprice.M_GoodsPrice.Value : 0;
                                }

                                sumAmount += (price * qty);
                            }

                            #region 添加到成本价表

                            //上次月结数
                            var lastPrice=listLastCostPrice.Find(r => r.M_GoodsCode == row.M_GoodsCode);

                            Mes_MonthBalancePriceEntity entity = new Mes_MonthBalancePriceEntity();
                            entity.Create();
                            entity.M_Months = month;
                            entity.M_GoodsCode = row.M_GoodsCode;
                            entity.M_GoodsName = row.M_GoodsName;
                            entity.M_GoodsPrice = sumAmount / secQty;
                            entity.M_StockQty = secQty;

                            if (lastPrice != null)
                            {
                                entity.M_LastQty = lastPrice.M_LastQty;
                                entity.M_LastPrice = lastPrice.M_LastPrice;
                            }
                            else
                            {
                                entity.M_LastQty = 0;
                                entity.M_LastPrice = 0;
                            }

                            listMonthBalancePrice.Add(entity);

                            #endregion
                        }

                        if (!success)
                            break;
                    }
                }

                #endregion

                #endregion

                #region 保存数据

                if (success)
                {
                    //保存月结库存明细
                    if (listMonthBalanceDetail.Count > 0)
                    {
                        db.Insert(listMonthBalanceDetail);
                    }

                    //保存月结价格明细
                    if (listMonthBalancePrice.Count > 0)
                    {
                        db.Insert(listMonthBalancePrice);
                    }

                    var dp = new DynamicParameters(new { });
                    dp.Add("@beginTime", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endTime", month.ToDate(), DbType.DateTime);
                       
                    //--原料入库单
                    db.ExecuteBySql("UPDATE Mes_MaterInHead SET MonthBalance='月结' WHERE M_CreateDate>@beginTime AND M_CreateDate<=@endTime", dp);

                    //退供应商单
                    db.ExecuteBySql("UPDATE Mes_BackSupplyHead SET MonthBalance='月结' WHERE B_CreateDate>@beginTime AND B_CreateDate<=@endTime", dp);

                    //领料单
                    db.ExecuteBySql("UPDATE Mes_CollarHead SET MonthBalance='月结' WHERE C_CreateDate>@beginTime AND C_CreateDate<=@endTime", dp);

                    //退库单
                    db.ExecuteBySql("UPDATE Mes_BackStockHead SET MonthBalance='月结' WHERE B_CreateDate>@beginTime AND B_CreateDate<=@endTime", dp);

                    //报废单
                    db.ExecuteBySql("UPDATE Mes_ScrapHead SET MonthBalance='月结' WHERE S_CreateDate>@beginTime AND S_CreateDate<=@endTime", dp);

                    //组装与拆分单
                    db.ExecuteBySql("UPDATE Mes_OrgResHead SET MonthBalance='月结' WHERE O_CreateDate>@beginTime AND O_CreateDate<=@endTime", dp);

                    //车间入库到线边仓单
                    db.ExecuteBySql("UPDATE Mes_InWorkShopHead SET MonthBalance='月结' WHERE I_CreateDate>@beginTime AND I_CreateDate<=@endTime", dp);

                    //调拨单
                    db.ExecuteBySql("UPDATE Mes_RequistHead SET MonthBalance='月结' WHERE R_CreateDate>@beginTime AND R_CreateDate<=@endTime", dp);

                    //其他出库单
                    db.ExecuteBySql("UPDATE Mes_OtherOutHead SET MonthBalance='月结' WHERE O_CreateDate>@beginTime AND O_CreateDate<=@endTime", dp);

                    //其他入库单
                    db.ExecuteBySql("UPDATE Mes_OtherInHead SET MonthBalance='月结' WHERE O_CreateDate>@beginTime AND O_CreateDate<=@endTime", dp);

                    //原物料销售单
                    db.ExecuteBySql("UPDATE Mes_SaleHead SET MonthBalance='月结' WHERE S_CreateDate>@beginTime AND S_CreateDate<=@endTime", dp);

                    //消耗单
                    db.ExecuteBySql("UPDATE Mes_ExpendHead SET MonthBalance='月结' WHERE E_CreateDate>@beginTime AND E_CreateDate<=@endTime", dp);

                    //强制使用记录单
                    db.ExecuteBySql("UPDATE Mes_CompUseHead SET MonthBalance='月结' WHERE C_CreateDate>@beginTime AND C_CreateDate<=@endTime", dp);

                    //抽检单
                    db.ExecuteBySql("UPDATE Mes_Inspect SET MonthBalance='月结' WHERE I_CreateDate>@beginTime AND I_CreateDate<=@endTime", dp);

                    //抽检单
                    db.ExecuteBySql("UPDATE Mes_Inspect SET MonthBalance='月结' WHERE I_CreateDate>@beginTime AND I_CreateDate<=@endTime", dp);


                    var dpUpdate = new DynamicParameters(new { });
                    dpUpdate.Add("@BalanceBy", user.account, DbType.String);
                    dpUpdate.Add("@BalanceMonth", month, DbType.String);
                    
                    //更新状态
                    db.ExecuteBySql(" UPDATE Mes_MonthBalance SET M_Status=1,M_MonthBalanceBy=@BalanceBy,M_MonthBalanceTime=GETDATE() WHERE M_Months=@BalanceMonth", dpUpdate);

                    db.Commit();
                }
                #endregion
                return success;
            }
            catch (Exception ex)
            {
                db.Rollback();//回滚事物

                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        private List<Mes_UseProduct> GetBomProducts(string goodcode,int level, List<Mes_Product> converts, List<Mes_UseProduct> products)
        {
            try
            {
                var rows = converts.Where(r => r.M_GoodsCode == goodcode);

                if (rows == null || rows.Count() < 1)
                {
                    return products;
                }
                else
                {
                    foreach (var row in rows)
                    {
                        var current = products.Find(r => r.M_GoodsCode == row.M_GoodsCode);
                        if (current != null)
                        {
                            return GetBomProducts(row.M_SecGoodsCode, level + 1, converts, products);
                        }
                        else
                        {
                            Mes_UseProduct product = new Mes_UseProduct();
                            product.M_Level = level;
                            product.M_GoodsCode = row.M_GoodsCode;
                            product.M_GoodsName = row.M_GoodsName;
                            product.M_GoodsPrice = 0;
                            product.M_Qty = 0;
                            product.ChildUseProducts = new List<Mes_UseProduct>();

                            var childs = converts.Where(r => r.M_SecGoodsCode == row.M_GoodsCode);//寻找子元素
                            if (childs != null && childs.Count() > 0)
                            {
                                foreach (var child in childs)
                                {
                                    Mes_UseProduct childproduct = new Mes_UseProduct();
                                    childproduct.M_Level = level-1;
                                    childproduct.M_GoodsCode = child.M_GoodsCode;
                                    childproduct.M_GoodsName = child.M_GoodsName;
                                    childproduct.M_GoodsPrice = 0;
                                    childproduct.M_Qty = 0;
                                    childproduct.ChildUseProducts = new List<Mes_UseProduct>();

                                    product.ChildUseProducts.Add(childproduct);
                                }
                            }
                            products.Add(product);

                            return GetBomProducts(row.M_SecGoodsCode, level + 1, converts, products);
                        }
                    }
                }

                return products;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 反月结
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool CancelMonthBalance(string month, out string msg)
        {
            msg = "";

            var db = this.BaseRepository().BeginTrans();
            UserInfo user = LoginUserInfo.Get();
            try
            {
                DateTime dt = DateTime.Parse(month);//月结日期
                string lastDate = dt.AddMonths(-1).ToString("yyyy-MM");

                //加载上月月结凭证
                Mes_MonthBalanceEntity lastMonthBalanceEntity = new Mes_MonthBalanceEntity();
                if (true)
                {
                    string sql =
                        @"SELECT [ID]
      ,[M_Months]
      ,[M_MonthBalanceTime]
      ,[M_MonthBalanceBy]
      ,[M_Status]
      ,[M_Remark]
  FROM [Mes_MonthBalance]
  WHERE LEFT(M_Months,7)='" + lastDate + "'";

                    var rows = this.BaseRepository().FindList<Mes_MonthBalanceEntity>(sql);
                    if (rows != null && rows.Count() > 0)
                    {
                        lastMonthBalanceEntity = rows.First();
                    }
                }

                //更新单据状态
                if (true)
                {
                    var dp = new DynamicParameters(new { });
                    dp.Add("@beginTime", lastMonthBalanceEntity.M_Months.ToDate(), DbType.DateTime);
                    dp.Add("@endTime", month.ToDate(), DbType.DateTime);

                    //--原料入库单
                    db.ExecuteBySql("UPDATE Mes_MaterInHead SET MonthBalance=NULL WHERE M_CreateDate>@beginTime AND M_CreateDate<=@endTime", dp);

                    //退供应商单
                    db.ExecuteBySql("UPDATE Mes_BackSupplyHead SET MonthBalance=NULL WHERE B_CreateDate>@beginTime AND B_CreateDate<=@endTime", dp);

                    //领料单
                    db.ExecuteBySql("UPDATE Mes_CollarHead SET MonthBalance=NULL WHERE C_CreateDate>@beginTime AND C_CreateDate<=@endTime", dp);

                    //退库单
                    db.ExecuteBySql("UPDATE Mes_BackStockHead SET MonthBalance=NULL WHERE B_CreateDate>@beginTime AND B_CreateDate<=@endTime", dp);

                    //报废单
                    db.ExecuteBySql("UPDATE Mes_ScrapHead SET MonthBalance=NULL WHERE S_CreateDate>@beginTime AND S_CreateDate<=@endTime", dp);

                    //组装与拆分单
                    db.ExecuteBySql("UPDATE Mes_OrgResHead SET MonthBalance=NULL WHERE O_CreateDate>@beginTime AND O_CreateDate<=@endTime", dp);

                    //车间入库到线边仓单
                    db.ExecuteBySql("UPDATE Mes_InWorkShopHead SET MonthBalance=NULL WHERE I_CreateDate>@beginTime AND I_CreateDate<=@endTime", dp);

                    //调拨单
                    db.ExecuteBySql("UPDATE Mes_RequistHead SET MonthBalance=NULL WHERE R_CreateDate>@beginTime AND R_CreateDate<=@endTime", dp);

                    //其他出库单
                    db.ExecuteBySql("UPDATE Mes_OtherOutHead SET MonthBalance=NULL WHERE O_CreateDate>@beginTime AND O_CreateDate<=@endTime", dp);

                    //其他入库单
                    db.ExecuteBySql("UPDATE Mes_OtherInHead SET MonthBalance=NULL WHERE O_CreateDate>@beginTime AND O_CreateDate<=@endTime", dp);

                    //原物料销售单
                    db.ExecuteBySql("UPDATE Mes_SaleHead SET MonthBalance=NULL WHERE S_CreateDate>@beginTime AND S_CreateDate<=@endTime", dp);

                    //消耗单
                    db.ExecuteBySql("UPDATE Mes_ExpendHead SET MonthBalance=NULL WHERE E_CreateDate>@beginTime AND E_CreateDate<=@endTime", dp);

                    //强制使用记录单
                    db.ExecuteBySql("UPDATE Mes_CompUseHead SET MonthBalance=NULL WHERE C_CreateDate>@beginTime AND C_CreateDate<=@endTime", dp);

                    //抽检单
                    db.ExecuteBySql("UPDATE Mes_Inspect SET MonthBalance=NULL WHERE I_CreateDate>@beginTime AND I_CreateDate<=@endTime", dp);

                    //抽检单
                    db.ExecuteBySql("UPDATE Mes_Inspect SET MonthBalance=NULL WHERE I_CreateDate>@beginTime AND I_CreateDate<=@endTime", dp);
                }
                
                //更新凭证状态
                if (true)
                {
                    var dp = new DynamicParameters(new { });
                    dp.Add("@BalanceBy", user.account, DbType.String);
                    dp.Add("@BalanceMonth", month, DbType.String);

                    //更新状态
                    db.ExecuteBySql(" UPDATE Mes_MonthBalance SET M_Status=2,M_MonthBalanceBy=@BalanceBy,M_MonthBalanceTime=GETDATE() WHERE M_Months=@BalanceMonth", dp);
                }

                //删除数据
                if (true)
                {
                    var dp = new DynamicParameters(new { });
                    dp.Add("@BalanceMonth", month, DbType.String);

                    db.ExecuteBySql("DELETE Mes_MonthBalanceDetail WHERE M_Months=@BalanceMonth", dp);
                    db.ExecuteBySql("DELETE Mes_MonthBalancePrice WHERE M_Months=@BalanceMonth", dp);
                }

                db.Commit();

                return true;
            }
            catch (Exception ex)
            {
                db.Rollback();

                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
           
        }

        /// <summary>
        /// 新增月结凭证
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, Mes_MonthBalanceEntity entity,out string msg)
        {
            try
            {
                UserInfo userinfo = LoginUserInfo.Get();

                entity.Create();
                entity.M_Status = 2;
                entity.M_MonthBalanceBy = userinfo.account;
                entity.M_MonthBalanceTime=DateTime.Now;
                msg = "";

                string sql=@"SELECT M_MonthsFROM Mes_MonthBalance WHERE LEFT(M_Months, 7) = LEFT('"+entity.M_Months+"', 7)";
                var entityTemp = this.BaseRepository().FindEntity<Mes_MonthBalanceEntity>(r => r.M_Months.Substring(0, 7) == entity.M_Months.Substring(0,7));

                if (entityTemp == null)
                {
                    this.BaseRepository().Insert(entity);
                }
                else
                {
                    msg = "【" + entity.M_Months + "】该月已存在月结凭证，添加失败";
                }

            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }


        /// <summary>
        /// 删除月结凭证
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue, out string msg)
        {
            try
            {
                msg = "";
                var entityTemp = this.BaseRepository().FindEntity<Mes_MonthBalanceEntity>(r => r.ID == keyValue);

                if (entityTemp != null)
                {
                    if (entityTemp.M_Status == 2)
                    {
                        this.BaseRepository().Delete<Mes_MonthBalanceEntity>(t => t.ID == keyValue);
                    }
                    else
                    {
                        msg = "已月结的凭证无法删除！";
                    }
                }
                else
                {
                    msg = "月结的凭证不存在！";
                }


            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        #endregion

    }
}
