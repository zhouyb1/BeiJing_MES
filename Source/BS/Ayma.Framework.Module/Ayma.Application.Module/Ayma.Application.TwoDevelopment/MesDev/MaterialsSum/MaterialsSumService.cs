using System.Globalization;
using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-09-16 10:59
    /// 描 述：原物料统计(入库、出库、次品)
    /// </summary>
    public partial class MaterialsSumService : RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 获取选取的时间原物料库存详细
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_MaterInDetailEntity> GetMaterialDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode, string M_Batch, DateTime ToDate)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"
                            select m.*,t.M_CreateDate from  Mes_MaterInDetail m left join Mes_MaterInHead t on (m.M_MaterInNo=t.M_MaterInNo) 
                            where m.M_MaterInNo in (select M_MaterInNo from Mes_MaterInHead where M_CreateDate>=@ToDate and M_CreateDate<=@AfterDate)
                             and m.M_GoodsCode=@M_GoodsCode
                             and M_Batch=@M_Batch
                             ");

                var queryParam = queryJson.ToJObject();
                //虚拟参数
                var dp = new DynamicParameters(new { });
                DateTime date = queryParam["data"].ToDate();
                DateTime AfterDate = ToDate.AddDays(1).Date;
                dp.Add("ToDate", ToDate, DbType.DateTime);
                dp.Add("AfterDate", AfterDate, DbType.DateTime);
                dp.Add("M_GoodsCode", M_GoodsCode, DbType.String);
                dp.Add("M_Batch", M_Batch, DbType.String);
                return this.BaseRepository().FindList<Mes_MaterInDetailEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取期初期末页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetMaterialSumListByDate(Pagination pagination, string queryJson)
       {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"    select 
									convert(datetime,convert(varchar(10),t.M_CreateDate,120)) as M_CreateDate
									,m.M_GoodsCode,m.M_GoodsName,sum(m.M_Qty) as Inventoryquantity
									,m.M_Batch
									,(select G_Price from Mes_Goods where G_Code=M_GoodsCode) as Price
                                    ,(select  ISNULL(SUM(B_Qty),0) from Mes_BackStockDetail b where  b.B_BackStockNo in(select B_BackStockNo from Mes_BackStockHead h where (h.B_CreateDate>=convert(datetime,convert(varchar(10),t.M_CreateDate,120)) and h.B_CreateDate<=DATEADD(day, 1, convert(datetime,convert(varchar(10),t.M_CreateDate,120))) and B_Kind=1))  AND B_GoodsCode=m.M_GoodsCode) as Back_Qty
                                    ,(select I_Qty from Mes_InventoryLS where I_GoodsCode=M_GoodsCode and I_Batch=m.M_Batch and I_Date=DATEADD(day, -1, convert(datetime,convert(varchar(10),t.M_CreateDate,120)))) as Initialinventory
									,(select G_Price from Mes_Goods where G_Code=m.M_GoodsCode)*(select I_Qty from Mes_InventoryLS where I_GoodsCode=M_GoodsCode and I_Batch=M_Batch and I_Date=DATEADD(day, -1, convert(datetime,convert(varchar(10),t.M_CreateDate,120)))) Initialamount
									,(select sum(C_Qty) from Mes_CollarDetail where  C_CollarNo in(select C_CollarNo from Mes_CollarHead  where C_CreateDate>=@StartTime and C_CreateDate<=@EndTime ) and C_GoodsCode=m.M_GoodsCode and C_Batch=m.M_Batch) delivery
							        ,((select I_Qty from Mes_InventoryLS where I_GoodsCode=M_GoodsCode and I_Batch=M_Batch and I_Date=DATEADD(day, -1, convert(datetime,convert(varchar(10),t.M_CreateDate,120))))+sum(m.M_Qty))-(select sum(C_Qty) from Mes_CollarDetail where  C_CollarNo in(select C_CollarNo from Mes_CollarHead  where C_CreateDate>=@StartTime and C_CreateDate<=@EndTime) and C_GoodsCode=m.M_GoodsCode and C_Batch=m.M_Batch) as Endinginventory
								   ,(select G_Price from Mes_Goods where G_Code=m.M_GoodsCode)*((select I_Qty from Mes_InventoryLS where I_GoodsCode=m.M_GoodsCode and I_Batch=m.M_Batch and I_Date=DATEADD(day, -1, convert(datetime,convert(varchar(10),t.M_CreateDate,120))))+sum(m.M_Qty))-(select sum(C_Qty) from Mes_CollarDetail where  C_CollarNo in(select C_CollarNo from Mes_CollarHead  where C_CreateDate>=@StartTime and C_CreateDate<=@EndTime ) and C_GoodsCode=m.M_GoodsCode and C_Batch=m.M_Batch) as finalamount
									from Mes_MaterInHead t left join Mes_MaterInDetail m on (t.M_MaterInNo=m.M_MaterInNo)  where (t.M_CreateDate>=@StartTime and t.M_CreateDate<=@EndTime) and  m.M_Kind=1
									 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                DateTime StartTime = queryParam["StartTime"].ToDate();
                DateTime EndTime = queryParam["EndTime"].ToDate();
                dp.Add("StartTime", StartTime, DbType.DateTime);
                dp.Add("EndTime", EndTime, DbType.DateTime);
                if (!queryParam["M_GoodsCode"].IsEmpty())
                {
                    dp.Add("M_GoodsCode", "%" + queryParam["M_GoodsCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND m.M_GoodsCode Like @M_GoodsCode ");
                }
                if (!queryParam["M_GoodsName"].IsEmpty())
                {
                    dp.Add("M_GoodsName", "%" + queryParam["M_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND m.M_GoodsName Like @M_GoodsName ");
                }
                strSql.Append("   Group by m.M_GoodsCode,m.M_GoodsName ,convert(datetime,convert(varchar(10),t.M_CreateDate,120)),m.M_Batch");
                return this.BaseRepository().FindTable(strSql.ToString(), dp, pagination);
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
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetMaterialSumList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT  t.ID ,
                                        G_Code ,
                                        G_Name ,
                                        G_Unit,
                                        t.G_CreateDate,
                                       (SELECT ISNULL(SUM(M_Qty),0) FROM dbo.Mes_MaterInDetail d WHERE d.M_GoodsCode=G_Code ) In_Qty ,
                                       (SELECT ISNULL(sum(c.C_Qty),0) FROM dbo.Mes_CollarDetail c WHERE c.C_GoodsCode=G_Code) Out_Qty ,
                                       (SELECT ISNULL(SUM(B_Qty),0) FROM dbo.Mes_BackStockHead INNER JOIN dbo.Mes_BackStockDetail ON Mes_BackStockDetail.B_BackStockNo = Mes_BackStockHead.B_BackStockNo WHERE B_Kind=1 AND B_GoodsCode=G_Code) Back_Qty
                                FROM    dbo.Mes_Goods t ");
                strSql.Append("  WHERE  t.G_Kind=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.G_CreateDate >= @startTime AND t.G_CreateDate <= @endTime ) ");
                }
                if (!queryParam["G_Code"].IsEmpty())
                {
                    dp.Add("G_Code", "%" + queryParam["G_Code"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.G_Code Like @G_Code ");
                }
                if (!queryParam["G_Name"].IsEmpty())
                {
                    dp.Add("G_Name", "%" + queryParam["G_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.G_Name Like @G_Name ");
                }
                //if (!queryParam["G_CreateDate"].IsEmpty() && !queryParam["G_CreateDate1"].IsEmpty())
                //{
                //    dp.Add("G_CreateDate", queryParam["G_CreateDate"].ToDate(), DbType.DateTime);
                //    dp.Add("G_CreateDate1", queryParam["G_CreateDate1"].ToDate(), DbType.DateTime);
                //    strSql.Append(" AND ( t.G_CreateDate >= @G_CreateDate AND t.G_CreateDate <= @G_CreateDate1 ) ");
                //}
                strSql.Append(" GROUP BY t.G_Code,t.G_Name ,t.G_Unit,t.ID,t.G_CreateDate");
                return this.BaseRepository().FindTable(strSql.ToString(), dp,pagination);
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
        /// 获取Mes_Goods表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_GoodsEntity GetMes_GoodsEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_GoodsEntity>(keyValue);
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
        /// 获取Mes_MaterInDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public DataTable GetMes_MaterInDetailList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT  t.ID ,
                                        G_Code ,
                                        G_Name ,
                                        G_Kind,
                                        G_Unit,
                                        d.M_Price,
                                        d.M_SupplyName,
                                        d.M_SupplyCode,
                                        d.M_Batch,
                                        ISNULL(SUM(d.M_Qty),0) In_Qty ,
                                       (SELECT ISNULL(sum(c.C_Qty),0) FROM dbo.Mes_CollarDetail c WHERE c.C_GoodsCode=G_Code) Out_Qty ,
                                       (SELECT ISNULL(SUM(B_Qty),0) FROM dbo.Mes_BackStockHead INNER JOIN dbo.Mes_BackStockDetail ON Mes_BackStockDetail.B_BackStockNo = Mes_BackStockHead.B_BackStockNo WHERE B_Kind=1 AND B_GoodsCode=G_Code) Back_Qty
                                FROM    dbo.Mes_Goods t
                                        LEFT JOIN dbo.Mes_MaterInDetail d ON t.g_code = d.M_GoodsCode ");

                strSql.Append("  WHERE  t.G_Kind=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.G_CreateDate >= @startTime AND t.G_CreateDate <= @endTime ) ");
                }
                if (!queryParam["G_Code"].IsEmpty())
                {
                    dp.Add("G_Code", "%" + queryParam["G_Code"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.G_Code Like @G_Code ");
                }
                if (!queryParam["G_Name"].IsEmpty())
                {
                    dp.Add("G_Name", "%" + queryParam["G_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.G_Name Like @G_Name ");
                }
                if (!queryParam["G_SupplyCode"].IsEmpty())
                {
                    dp.Add("G_SupplyCode", "%" + queryParam["G_SupplyCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND d.M_SupplyCode Like @G_SupplyCode ");

                }
                //if (!queryParam["G_CreateDate"].IsEmpty() && !queryParam["G_CreateDate1"].IsEmpty())
                //{
                //    dp.Add("G_CreateDate", queryParam["G_CreateDate"].ToDate(), DbType.DateTime);
                //    dp.Add("G_CreateDate1", queryParam["G_CreateDate1"].ToDate(), DbType.DateTime);
                //    strSql.Append(" AND ( t.G_CreateDate >= @G_CreateDate AND t.G_CreateDate <= @G_CreateDate1 ) ");
                //}
                strSql.Append(" GROUP BY G_Code,G_Name ,G_Kind,G_Unit,d.M_Batch,t.ID,d.M_SupplyName,d.M_SupplyCode,d.M_Price ");
                var dt = this.BaseRepository().FindTable(strSql.ToString(), dp, pagination);
                dt.Columns.Add("in_amount", typeof (decimal));
                dt.Columns.Add("out_amount", typeof (decimal));
                dt.Columns.Add("back_amount", typeof (decimal));
                foreach (DataRow dr in dt.Rows)
                {
                    dr["in_amount"] = dr["m_price"].ToDouble(2) * dr["in_qty"].ToDouble(2);
                    dr["out_amount"] = dr["m_price"].ToDouble(2) * dr["out_qty"].ToDouble(2);
                    dr["back_amount"] = dr["m_price"].ToDouble(2) * dr["back_qty"].ToDouble(2);
                }
                return dt;
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
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                var mes_GoodsEntity = GetMes_GoodsEntity(keyValue); 
                db.Delete<Mes_GoodsEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_MaterInDetailEntity>(t=>t.M_GoodsCode == mes_GoodsEntity.G_Code);
                db.Commit();
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
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, Mes_GoodsEntity entity,Mes_MaterInDetailEntity mes_MaterInDetailEntity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_GoodsEntityTmp = GetMes_GoodsEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_MaterInDetailEntity>(t=>t.M_GoodsCode == mes_GoodsEntityTmp.G_Code);
                    mes_MaterInDetailEntity.Create();
                    mes_MaterInDetailEntity.M_GoodsCode = mes_GoodsEntityTmp.G_Code;
                    db.Insert(mes_MaterInDetailEntity);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    mes_MaterInDetailEntity.Create();
                    mes_MaterInDetailEntity.M_GoodsCode = entity.G_Code;
                    db.Insert(mes_MaterInDetailEntity);
                }
                db.Commit();
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

        #endregion

    }
}
