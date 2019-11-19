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
        /// 获取选取的时间原物料入库详细
        /// </summary>
        /// <returns></returns>
        public DataTable GetMaterialDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"
                             select  m.M_MaterInNo
							        ,m.M_SupplyCode
									,m.M_SupplyName
									,m.M_GoodsCode
									,m.M_GoodsName
									,m.M_Qty
									,m.M_Batch
									,m.M_GoodsItax
									,m.M_Remark  
									,t.M_StockName    
									,t.M_CreateDate
                                    ,t.M_CreateBy     
									 from  Mes_MaterInDetail m left join Mes_MaterInHead t on (m.M_MaterInNo=t.M_MaterInNo) 
                            where m.M_MaterInNo in (select b.M_MaterInNo from Mes_MaterInHead b where b.M_CreateDate>=@StartTime and b.M_CreateDate<=@EndTime and b.M_Status=3)
                             and m.M_GoodsCode=@M_GoodsCode

                             ");

                var queryParam = queryJson.ToJObject();
                //虚拟参数
                var dp = new DynamicParameters(new { });
                DateTime StartTime = queryParam["StartTime"].ToDate();
                DateTime EndTime = queryParam["EndTime"].ToDate();
                dp.Add("StartTime", StartTime, DbType.DateTime);
                dp.Add("EndTime", EndTime, DbType.DateTime);
                dp.Add("M_GoodsCode", M_GoodsCode, DbType.String);
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
        /// 获取选取的时间原物料出库详细
        /// </summary>
        /// <returns></returns>
        public DataTable GetMaterialOutDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"
                             select                    
                            t.C_StockCode
						   ,t.C_StockName
                           ,t.C_StockToCode
						   ,t.C_StockToName
						   ,t.C_CreateDate
						   ,m.C_CollarNo
						   ,m.C_SupplyCode
						   ,m.C_SupplyName
						   ,m.C_GoodsCode
						   ,m.C_GoodsName
						   ,m.C_Unit
						   ,m.C_Qty
						   ,m.C_Batch
						   ,m.C_Price
						   ,m.C_Remark
                           ,t.C_CreateBy  
                            from  Mes_CollarDetail m left join Mes_CollarHead t on (m.C_CollarNo=t.C_CollarNo) 
                            where m.C_CollarNo in (select C_CollarNo from Mes_CollarHead where C_CreateDate>=@StartTime and C_CreateDate<=@EndTime and P_Status=3)
                             and m.C_GoodsCode=@M_GoodsCode
                             ");

                var queryParam = queryJson.ToJObject();
                //虚拟参数
                var dp = new DynamicParameters(new { });
                DateTime StartTime = queryParam["StartTime"].ToDate();
                DateTime EndTime = queryParam["EndTime"].ToDate();
                dp.Add("StartTime", StartTime, DbType.DateTime);
                dp.Add("EndTime", EndTime, DbType.DateTime);
                dp.Add("M_GoodsCode", M_GoodsCode, DbType.String);
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
        /// 获取选取的时间原物料退库详细
        /// </summary>
        /// <returns></returns>
        public DataTable GetMaterialBackDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"
                            select  
                            t.B_StockCode,
                            t.B_StockName,
                            t.B_StockToCode,
                            t.B_StockToName,
                            t.B_CreateDate,
                            m.B_BackStockNo,
                            m.B_GoodsCode,
                            m.B_GoodsName,
                            m.B_Unit,
                            m.B_Qty,
                            m.B_Batch,
                            m.B_Price,
                            m.B_Remark,
                            t.B_CreateBy  
                            from Mes_BackStockDetail m left join Mes_BackStockHead t on(m.B_BackStockNo=t.B_BackStockNo) where B_GoodsCode=@M_GoodsCode and m.B_BackStockNo 
                            in(select B_BackStockNo from Mes_BackStockHead where (B_CreateDate >=@StartTime and B_CreateDate <=@EndTime)and B_Status=3)
                             ");

                var queryParam = queryJson.ToJObject();
                //虚拟参数
                var dp = new DynamicParameters(new { });
                DateTime StartTime = queryParam["StartTime"].ToDate();
                DateTime EndTime = queryParam["EndTime"].ToDate();
                dp.Add("StartTime", StartTime, DbType.DateTime);
                dp.Add("EndTime", EndTime, DbType.DateTime);
                dp.Add("M_GoodsCode", M_GoodsCode, DbType.String);
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
        /// 获取选取的时间原物料销售详细
        /// </summary>
        /// <returns></returns>
        public DataTable GetMaterialSaleDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"
                            select  
                            t.S_StockCode,
                            t.S_StockName,
                            t.S_CostomCode,
                            t.S_CostomName,
                            t.S_CreateDate,
                            t.S_CreateBy,
                            m.S_SaleNo,
                            m.S_GoodsCode,
                            m.S_GoodsName,
                            m.S_Otax,
                            m.S_Unit,
                            m.S_Qty,
                            m.S_Price,
                            m.S_Batch,
                            m.S_Remark
                             from Mes_SaleDetail m left join Mes_SaleHead t on(m.S_SaleNo=t.S_SaleNo) where S_GoodsCode=@M_GoodsCode and m.S_SaleNo 
                             in(select S_SaleNo from Mes_SaleHead where (S_CreateDate >=@StartTime and S_CreateDate <=@EndTime)and S_Status=3) 
                             ");

                var queryParam = queryJson.ToJObject();
                //虚拟参数
                var dp = new DynamicParameters(new { });
                DateTime StartTime = queryParam["StartTime"].ToDate();
                DateTime EndTime = queryParam["EndTime"].ToDate();
                dp.Add("StartTime", StartTime, DbType.DateTime);
                dp.Add("EndTime", EndTime, DbType.DateTime);
                dp.Add("M_GoodsCode", M_GoodsCode, DbType.String);
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
        /// 获取期初期末页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetMaterialSumListByDate(Pagination pagination, string queryJson)
       {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"   	 select 
									 m.M_GoodsCode 
									,m.M_GoodsName
			                	   	,(select  ISNULL(SUM(B_Qty),0) from Mes_BackStockDetail where B_GoodsCode=M_GoodsCode and B_BackStockNo in(select B_BackStockNo from Mes_BackStockHead where (B_CreateDate >=@StartTime and B_CreateDate <=@EndTime)and B_Status=3))as withdrawingnumber
									,(select  ISNULL(SUM(S_Qty),0) from Mes_SaleDetail where S_GoodsCode=M_GoodsCode and S_SaleNo in(select S_SaleNo from Mes_SaleHead where (S_CreateDate >=@StartTime and S_CreateDate <=@EndTime)and S_Status=3)) as materialssales 
									,(select ISNULL(SUM(S_Qty),0) from Mes_ScrapDetail where S_GoodsCode=M_GoodsCode and S_ScrapNo in(select S_ScrapNo from Mes_ScrapHead where (S_CreateDate >=@StartTime and S_CreateDate <=@EndTime)and S_Status=3)) as scrapist  
									,(select  ISNULL(SUM(O_Qty),0) from Mes_OtherInDetail where O_GoodsCode=M_GoodsCode and O_OtherInNo in(select O_OtherInNo from Mes_OtherInHead where (O_CreateDate >=@StartTime and O_CreateDate <=@EndTime)and O_Status=3))  as otherwarehouse  
									,(select  ISNULL(SUM(O_Qty),0) from Mes_OtherOutDetail where O_GoodsCode=M_GoodsCode and O_OtherOutNo in(select O_OtherOutNo from Mes_OtherOutHead where (O_CreateDate >=@StartTime and O_CreateDate <=@EndTime)and O_Status=3)) as otheroutbound  								
									,(select  ISNULL(SUM(B_Qty),0) from Mes_BackSupplyDetail where B_GoodsCode=M_GoodsCode and B_BackSupplyNo in(select B_BackSupplyNo from Mes_BackSupplyHead where (B_CreateDate >=@StartTime and B_CreateDate <=@EndTime)and B_Status=3)) as supplierback                                    
								    ,(select ISNULL(SUM(I_Qty),0) from Mes_InventoryLS where I_GoodsCode=m.M_GoodsCode  and I_Date=@Time) as Initialinventory													
									,ISNULL(SUM(m.M_Qty),0) as Inventoryquantity								
									,(select G_Price from Mes_Goods where G_Code=M_GoodsCode) as Price								  
								    ,(select  ISNULL(SUM(B_Qty),0) from Mes_BackStockDetail b where  b.B_BackStockNo in(select B_BackStockNo from Mes_BackStockHead h 
									where (h.B_CreateDate>=@StartTime and h.B_CreateDate<=@EndTime and B_Kind=1 and B_Status=3 )) AND B_GoodsCode=m.M_GoodsCode) as Back_Qty							
									,(select G_Price from Mes_Goods where G_Code=m.M_GoodsCode)*(select ISNULL(SUM(I_Qty),0) from Mes_InventoryLS where I_GoodsCode=m.M_GoodsCode  and I_Date=@Time) as initialamount
						      	    ,(select ISNULL(SUM(C_Qty),0) from Mes_CollarDetail where C_CollarNo in(select C_CollarNo from Mes_CollarHead  where (C_CreateDate>=@StartTime and C_CreateDate<=@EndTime) and C_GoodsCode=m.M_GoodsCode and P_Status=3)) as delivery
									,((select ISNULL(SUM(I_Qty),0) from Mes_InventoryLS where I_GoodsCode=m.M_GoodsCode  and I_Date=@Time)+ISNULL(SUM(m.M_Qty),0)-
									(select ISNULL(SUM(C_Qty),0) from Mes_CollarDetail where C_CollarNo in(select C_CollarNo from Mes_CollarHead  where (C_CreateDate>=@StartTime and C_CreateDate<=@EndTime) and C_GoodsCode=m.M_GoodsCode and P_Status=3))
									+(select  ISNULL(SUM(B_Qty),0) from Mes_BackStockDetail where B_GoodsCode=m.M_GoodsCode and B_BackStockNo in(select B_BackStockNo from Mes_BackStockHead where (B_CreateDate >=@StartTime and B_CreateDate <=@EndTime)and B_Status=3))
									-(select  ISNULL(SUM(S_Qty),0) from Mes_SaleDetail where S_GoodsCode=m.M_GoodsCode and S_SaleNo in(select S_SaleNo from Mes_SaleHead where (S_CreateDate >=@StartTime and S_CreateDate <=@EndTime)and S_Status=3))
									-(select ISNULL(SUM(S_Qty),0) from Mes_ScrapDetail where S_GoodsCode=m.M_GoodsCode and S_ScrapNo in(select S_ScrapNo from Mes_ScrapHead where (S_CreateDate >=@StartTime and S_CreateDate <=@EndTime)and S_Status=3))
									+(select  ISNULL(SUM(O_Qty),0) from Mes_OtherInDetail where O_GoodsCode=m.M_GoodsCode and O_OtherInNo in(select O_OtherInNo from Mes_OtherInHead where (O_CreateDate >=@StartTime and O_CreateDate <=@EndTime)and O_Status=3))
									-(select  ISNULL(SUM(O_Qty),0) from Mes_OtherOutDetail where O_GoodsCode=m.M_GoodsCode and O_OtherOutNo in(select O_OtherOutNo from Mes_OtherOutHead where (O_CreateDate >=@StartTime and O_CreateDate <=@EndTime)and O_Status=3))
									-(select  ISNULL(SUM(B_Qty),0) from Mes_BackSupplyDetail where B_GoodsCode=m.M_GoodsCode and B_BackSupplyNo in(select B_BackSupplyNo from Mes_BackSupplyHead where (B_CreateDate >=@StartTime and B_CreateDate <=@EndTime)and B_Status=3)))	 as Endinginventory																	   								   
								    ,((select ISNULL(SUM(I_Qty),0) from Mes_InventoryLS where I_GoodsCode=m.M_GoodsCode  and I_Date=@Time)+ISNULL(SUM(m.M_Qty),0)-
									(select ISNULL(SUM(C_Qty),0) from Mes_CollarDetail where C_CollarNo in(select C_CollarNo from Mes_CollarHead  where (C_CreateDate>=@StartTime and C_CreateDate<=@EndTime)and C_GoodsCode=m.M_GoodsCode and P_Status=3))
									+(select  ISNULL(SUM(B_Qty),0) from Mes_BackStockDetail where B_GoodsCode=m.M_GoodsCode and B_BackStockNo in(select B_BackStockNo from Mes_BackStockHead where (B_CreateDate >=@StartTime and B_CreateDate <=@EndTime)and B_Status=3))
									-(select  ISNULL(SUM(S_Qty),0) from Mes_SaleDetail where S_GoodsCode=m.M_GoodsCode and S_SaleNo in(select S_SaleNo from Mes_SaleHead where (S_CreateDate >=@StartTime and S_CreateDate <=@EndTime)and S_Status=3))
									-(select ISNULL(SUM(S_Qty),0) from Mes_ScrapDetail where S_GoodsCode=m.M_GoodsCode and S_ScrapNo in(select S_ScrapNo from Mes_ScrapHead where (S_CreateDate >=@StartTime and S_CreateDate <=@EndTime)and S_Status=3))
									+(select  ISNULL(SUM(O_Qty),0) from Mes_OtherInDetail where O_GoodsCode=m.M_GoodsCode and O_OtherInNo in(select O_OtherInNo from Mes_OtherInHead where (O_CreateDate >=@StartTime and O_CreateDate <=@EndTime)and O_Status=3))
									-(select  ISNULL(SUM(O_Qty),0) from Mes_OtherOutDetail where O_GoodsCode=m.M_GoodsCode and O_OtherOutNo in(select O_OtherOutNo from Mes_OtherOutHead where (O_CreateDate >=@StartTime and O_CreateDate <=@EndTime)and O_Status=3))
									-(select  ISNULL(SUM(B_Qty),0) from Mes_BackSupplyDetail where B_GoodsCode=m.M_GoodsCode and B_BackSupplyNo in(select B_BackSupplyNo from Mes_BackSupplyHead where (B_CreateDate >=@StartTime and B_CreateDate <=@EndTime)and B_Status=3)))*(select G_Price from Mes_Goods where G_Code=M_GoodsCode) as finalamount																										
									from Mes_MaterInHead t left join Mes_MaterInDetail m on (t.M_MaterInNo=m.M_MaterInNo)  where (t.M_CreateDate>=@StartTime and t.M_CreateDate<=@EndTime) and  m.M_Kind=1	and t.M_Status=3									
									 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                DateTime StartTime = queryParam["StartTime"].ToDate();
                DateTime EndTime = queryParam["EndTime"].ToDate();
                string Time = StartTime.AddDays(-1).ToShortDateString();
                dp.Add("StartTime", StartTime, DbType.DateTime);
                dp.Add("EndTime", EndTime, DbType.DateTime);
                dp.Add("Time", Time, DbType.DateTime);
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
                strSql.Append("Group by m.M_GoodsCode,m.M_GoodsName  ");
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
                strSql.Append(@"SELECT 
                                        g.G_Name,
                                        g.G_Code,
                                        g.G_Unit,
                                        ( SELECT ISNULL(SUM(md.M_Qty), 0)FROM dbo.Mes_MaterInHead mh LEFT JOIN dbo.Mes_MaterInDetail md ON mh.M_MaterInNo = md.M_MaterInNo WHERE mh.M_OrderKind = 0 AND mh.M_Status = 3 AND md.M_GoodsCode = g.G_Code AND mh.M_CreateDate>=@startTime  AND mh.M_CreateDate<=@endTime) In_Qty,
                                        ( SELECT ISNULL(SUM(bd.B_Qty),0) FROM dbo.Mes_BackStockHead bh LEFT JOIN dbo.Mes_BackStockDetail bd ON bd.B_BackStockNo = bh.B_BackStockNo WHERE bd.B_GoodsCode=g.G_Code AND bh.B_Status=3 AND B_Kind=1 and bh.B_CreateDate>=@startTime  AND bh.B_CreateDate<=@endTime) Back_Qty,
                                        ( SELECT ISNULL(SUM(cd.C_Qty),0) FROM dbo.Mes_CollarHead ch LEFT JOIN dbo.Mes_CollarDetail cd ON cd.C_CollarNo = ch.C_CollarNo WHERE ch.P_Status=3   AND cd.C_GoodsCode=g.G_Code and ch.C_CreateDate>=@startTime  AND ch.C_CreateDate<=@endTime)Out_Qty
       
                                FROM    dbo.Mes_Goods g WHERE G_Kind=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                }
                if (!queryParam["G_Code"].IsEmpty())
                {
                    dp.Add("G_Code", "%" + queryParam["G_Code"].ToString() + "%", DbType.String);
                    strSql.Append(" AND g.G_Code Like @G_Code ");
                }
                if (!queryParam["G_Name"].IsEmpty())
                {
                    dp.Add("G_Name", "%" + queryParam["G_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND g.G_Name Like @G_Name ");
                }
              
                strSql.Append(" GROUP BY g.G_Code,g.G_Name ,g.G_Unit");
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
//                strSql.Append(@"SELECT  t.ID ,
//                                        G_Code ,
//                                        G_Name ,
//                                        G_Kind,
//                                        G_Unit,
//                                        d.M_Price,
//                                        d.M_SupplyName,
//                                        d.M_SupplyCode,
//                                        d.M_Batch,
//                                        ISNULL(SUM(d.M_Qty),0) In_Qty ,
//                                       (SELECT ISNULL(sum(c.C_Qty),0) FROM dbo.Mes_CollarDetail c WHERE c.C_GoodsCode=G_Code) Out_Qty ,
//                                       (SELECT ISNULL(SUM(B_Qty),0) FROM dbo.Mes_BackStockHead INNER JOIN dbo.Mes_BackStockDetail ON Mes_BackStockDetail.B_BackStockNo = Mes_BackStockHead.B_BackStockNo WHERE B_Kind=1 AND B_GoodsCode=G_Code) Back_Qty
//                                FROM    dbo.Mes_Goods t
//                                        LEFT JOIN dbo.Mes_MaterInDetail d ON t.g_code = d.M_GoodsCode ");

//                strSql.Append("  WHERE  t.G_Kind=1 ");
                strSql.Append(@"SELECT  t.G_SupplyName ,
                                        dt1.In_Qty ,
                                        dt1.M_Batch ,
                                        dt2.Back_Qty ,
                                        dt3.Out_Qty ,
                                        t.G_Name ,
                                        t.G_Code ,
                                        t.G_Price ,
                                        t.G_Unit
                                FROM    dbo.Mes_Goods t
                                        LEFT JOIN ( SELECT  ISNULL(md.M_Qty, 0) In_Qty ,
                                                            md.M_Price ,
                                                            md.M_Batch ,
                                                            mh.M_SupplyName ,
                                                            md.M_GoodsCode
                                                    FROM    dbo.Mes_MaterInHead mh
                                                            LEFT JOIN dbo.Mes_MaterInDetail md ON md.M_MaterInNo = mh.M_MaterInNo
                                                    WHERE   mh.M_OrderKind = 0
                                                            AND mh.M_Status = 3
                                                            AND mh.M_CreateDate >= @startTime
                                                            AND mh.M_CreateDate <= @endTime
                                                  ) dt1 ON dt1.M_GoodsCode = t.G_Code
                                        LEFT JOIN ( SELECT  ISNULL(bd.B_Qty, 0) Back_Qty ,
                                                            bd.B_Price ,
                                                            bd.B_GoodsCode
                                                    FROM    dbo.Mes_BackStockHead bh
                                                            LEFT JOIN dbo.Mes_BackStockDetail bd ON bh.B_BackStockNo = bd.B_BackStockNo
                                                    WHERE   bh.B_Status = 3 AND bh.B_Kind=1
                                                            AND bh.B_CreateDate >= @startTime
                                                            AND bh.b_CreateDate <= @endTime
                                                  ) dt2 ON dt2.b_GoodsCode = t.G_Code
                                        LEFT JOIN ( SELECT  ISNULL(cd.C_Qty, 0) Out_Qty ,
                                                            cd.C_GoodsCode
                                                    FROM    dbo.Mes_CollarHead ch
                                                            LEFT JOIN dbo.Mes_CollarDetail cd ON ch.C_CollarNo = cd.C_CollarNo
                                                    WHERE   ch.P_Status = 3
                                                            AND ch.C_CreateDate >= @startTime
                                                            AND ch.C_CreateDate <= @endTime
                                                  ) dt3 ON dt3.c_GoodsCode = t.G_Code   WHERE G_Kind=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
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
                    strSql.Append(" AND t.G_SupplyCode Like @G_SupplyCode ");

                }
               
                //strSql.Append(" GROUP BY G_Code,G_Name ,G_Unit,d.M_Batch,t.ID,d.M_SupplyName,d.M_SupplyCode,d.M_Price ");
                var dt = this.BaseRepository().FindTable(strSql.ToString(), dp, pagination);
                dt.Columns.Add("in_amount", typeof (decimal));
                dt.Columns.Add("out_amount", typeof (decimal));
                dt.Columns.Add("back_amount", typeof (decimal));
                foreach (DataRow dr in dt.Rows)
                {
                    dr["in_amount"] = dr["g_price"].ToDouble(2) * dr["in_qty"].ToDouble(2);
                    dr["out_amount"] = dr["g_price"].ToDouble(2) * dr["out_qty"].ToDouble(2);
                    dr["back_amount"] = dr["g_price"].ToDouble(2) * dr["back_qty"].ToDouble(2);
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
