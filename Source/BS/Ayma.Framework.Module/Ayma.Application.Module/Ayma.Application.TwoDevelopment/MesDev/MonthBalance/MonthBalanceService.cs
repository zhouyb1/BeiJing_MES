﻿using Dapper;
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
            try
            {
                DateTime dt = DateTime.Parse(month);//月结日期
                string lastDate = dt.AddMonths(-1).ToString("yyyy-MM");


                #region 月结库存、各单据数量
                List<Mes_MonthBalanceDetailEntity> listMonthBalanceDetail = new List<Mes_MonthBalanceDetailEntity>();//各项数据

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
                        listLastQty = rows.ToList();
                }
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
                        entity.M_LastQty += entity.M_LastQty;
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
                        listNowQty = rows.ToList();
                }
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
                        entity.M_StockQty += entity.M_StockQty;
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
                        listInQty = rows.ToList();
                }
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
                        entity.M_InQty += entity.M_InQty;
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
                        listBackSupplyQty = rows.ToList();
                }
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
                        entity.M_BackSupplyQty += entity.M_BackSupplyQty;
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
                        listOutQty = rows.ToList();
                }
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
                        entity.M_OutQty += entity.M_OutQty;
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
                        listBackStockQty = rows.ToList();
                }
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
                        entity.M_BackStockQty += entity.M_BackStockQty;
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
                        listScrapQty = rows.ToList();
                }
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
                        entity.M_ScrapQty += entity.M_ScrapQty;
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
                        listOtherInQty = rows.ToList();
                }
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
                        entity.M_OtherInQty += entity.M_OtherInQty;
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
                        listOtherOutQty = rows.ToList();
                }
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
                        entity.M_OtherOutQty += entity.M_OtherOutQty;
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
                        listSaleQty = rows.ToList();
                }
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
                        entity.M_SaleQty += entity.M_SaleQty;
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
                        listExpendQty = rows.ToList();
                }
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
                        entity.M_ExpendQty += entity.M_ExpendQty;
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
                        listRequistInQty = rows.ToList();
                }
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
                        entity.M_RequistQty += entity.M_RequistQty;
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
                        listRequistOutQty = rows.ToList();
                }
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
                        entity.M_RequistQty -= entity.M_RequistQty;
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
                        listInspectQty = rows.ToList();
                }
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
                        entity.M_InspectQty += entity.M_InspectQty;
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
                        listInWorkShopQty = rows.ToList();
                }
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
                        entity.M_InWorkShopQty += entity.M_InWorkShopQty;
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
                        listOrgresOutQty = rows.ToList();
                }
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
                        entity.M_OrgresOutQty += entity.M_OrgresOutQty;
                    }
                }
                #endregion

                #region 月结成本价

                #endregion

                return success;
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

            try
            {
                UserInfo userinfo = LoginUserInfo.Get();
                var dp = new DynamicParameters(new { });
                dp.Add("@BalanceMonth", month);
                dp.Add("@BalanceBy", userinfo.realName);
                dp.Add("@errcode", "", DbType.Int32, ParameterDirection.Output);
                dp.Add("@errtxt", "", DbType.String, ParameterDirection.Output);
                this.BaseRepository().ExecuteByProc("sp_MonthBalance_Cancel", dp);

                int errcode = dp.Get<int>("@errcode");//返回的错误代码 0：成功
                string errMsg = dp.Get<string>("@errtxt");//存储过程返回的错误消息

                if (errcode != 0)
                {
                    msg = errMsg;
                    return false;
                }
                else
                {
                    return true;
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
