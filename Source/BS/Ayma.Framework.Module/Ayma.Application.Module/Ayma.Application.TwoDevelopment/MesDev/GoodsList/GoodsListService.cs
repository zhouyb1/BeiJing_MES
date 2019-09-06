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
    /// 日 期：2019-01-07 13:55
    /// 描 述：物料列表
    /// </summary>
    public partial class GoodsListService : RepositoryFactory
    {
        
       # region 获取数据

        /// <summary>
        /// 获取毛到净出成率
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></return
        public DataTable GetYieldRatePageList(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"
                 
      WITH cte AS (
				 SELECT  o.O_GoodsName ,
				( SELECT    CONVERT(VARCHAR, C_Min * 100) + '-'
							+ CONVERT(VARCHAR, C_Max * 100) AS rate
				  FROM      dbo.Mes_Convert c
				  WHERE     c.C_ProNo = h.O_ProCode
				) rate ,
				( SELECT    b.B_FormulaName
				  FROM      dbo.Mes_BomRecord b
				  WHERE     b.B_FormulaCode = h.O_Record
				) FormulaName ,
                CONVERT(DECIMAL(18,2),SUM(o.O_Qty))requireQty,
			    CONVERT(DECIMAL(18,2),SUM(o.O_SecQty)) productQty,
				( CASE WHEN MONTH(O_CreateDate) = 1
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) January ,
				( CASE WHEN MONTH(O_CreateDate) = 2
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) February ,
				( CASE WHEN MONTH(O_CreateDate) = 3
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) March ,
				( CASE WHEN MONTH(O_CreateDate) = 4
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) April ,
				( CASE WHEN MONTH(O_CreateDate) = 5
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) May ,
				( CASE WHEN MONTH(O_CreateDate) = 6
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) June ,
				( CASE WHEN MONTH(O_CreateDate) = 7
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) July ,
				( CASE WHEN MONTH(O_CreateDate) = 8
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty)* 100))
					   ELSE 0
				  END ) August ,
				( CASE WHEN MONTH(O_CreateDate) = 9
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) September ,
				( CASE WHEN MONTH(O_CreateDate) = 10
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) October ,
				( CASE WHEN MONTH(O_CreateDate) = 11
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) November ,
				( CASE WHEN MONTH(O_CreateDate) = 12
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) December
		FROM    dbo.Mes_OrgResDetail o
				JOIN dbo.Mes_OrgResHead h ON h.O_OrgResNo = o.O_OrgResNo
				
		WHERE   h.O_ProCode = '01' AND o.O_GoodsCode IN ( SELECT   g.G_Code
                               FROM     dbo.Mes_Goods g
                               WHERE    g.G_TKind = '01' )
		       
		GROUP BY O_CreateDate ,
				O_GoodsName ,
				h.O_Record ,
				O_ProCode ) 
        SELECT *,
        cte.[January]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'JanDiff',
        cte.[February]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'FebDiff',
        cte.[March]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'MarDiff',
        cte.[April]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'AprDiff',
        cte.[May]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'MayDiff',
        cte.[June]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'JunDiff',
        cte.[July]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'JulyDiff',
        cte.[August]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'AugDiff',
        cte.[September]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'SeptDiff',
        cte.[October]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'OctDiff',
        cte.November-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'NovDiff',
        cte.[December]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'DecDiff'
        FROM cte where 1=1 
         ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
               
                if (!queryParam["G_Name"].IsEmpty())
                {
                    dp.Add("G_Name", "%" + queryParam["G_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND cte.O_GoodsName Like @G_Name ");
                }
                if (!queryParam["FormulaName"].IsEmpty())
                {
                    dp.Add("FormulaName", "%" + queryParam["FormulaName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND cte.FormulaName Like @FormulaName ");
                }
                
                return this.BaseRepository().FindTable(strSql.ToString(),dp);
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
        /// 获取粗加工出成率
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetLivingToCookPageList(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"
                 
      WITH cte AS (
				 SELECT  o.O_GoodsName ,
				( SELECT    CONVERT(VARCHAR, C_Min * 100) + '-'
							+ CONVERT(VARCHAR, C_Max * 100) AS rate
				  FROM      dbo.Mes_Convert c
				  WHERE     c.C_ProNo = h.O_ProCode
				) rate ,
				( SELECT    b.B_FormulaName
				  FROM      dbo.Mes_BomRecord b
				  WHERE     b.B_FormulaCode = h.O_Record
				) FormulaName ,
                CONVERT(DECIMAL(18,2),SUM(o.O_Qty))requireQty,
			    CONVERT(DECIMAL(18,2),SUM(o.O_SecQty)) productQty,
				( CASE WHEN MONTH(O_CreateDate) = 1
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) January ,
				( CASE WHEN MONTH(O_CreateDate) = 2
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) February ,
				( CASE WHEN MONTH(O_CreateDate) = 3
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) March ,
				( CASE WHEN MONTH(O_CreateDate) = 4
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) April ,
				( CASE WHEN MONTH(O_CreateDate) = 5
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) May ,
				( CASE WHEN MONTH(O_CreateDate) = 6
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) June ,
				( CASE WHEN MONTH(O_CreateDate) = 7
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) July ,
				( CASE WHEN MONTH(O_CreateDate) = 8
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty)* 100))
					   ELSE 0
				  END ) August ,
				( CASE WHEN MONTH(O_CreateDate) = 9
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) September ,
				( CASE WHEN MONTH(O_CreateDate) = 10
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) October ,
				( CASE WHEN MONTH(O_CreateDate) = 11
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) November ,
				( CASE WHEN MONTH(O_CreateDate) = 12
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) December
		FROM    dbo.Mes_OrgResDetail o
				JOIN dbo.Mes_OrgResHead h ON h.O_OrgResNo = o.O_OrgResNo
				
		WHERE   h.O_ProCode = '02' AND o.O_GoodsCode IN ( SELECT   g.G_Code
                               FROM     dbo.Mes_Goods g
                               WHERE    g.G_TKind = '01' )
		       
		GROUP BY O_CreateDate ,
				O_GoodsName ,
				h.O_Record ,
				O_ProCode ) 
        SELECT *,
        cte.[January]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'JanDiff',
        cte.[February]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'FebDiff',
        cte.[March]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'MarDiff',
        cte.[April]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'AprDiff',
        cte.[May]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'MayDiff',
        cte.[June]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'JunDiff',
        cte.[July]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'JulyDiff',
        cte.[August]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'AugDiff',
        cte.[September]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'SeptDiff',
        cte.[October]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'OctDiff',
        cte.November-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'NovDiff',
        cte.[December]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'DecDiff'
        FROM cte where 1=1 
         ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
               
                if (!queryParam["G_Name"].IsEmpty())
                {
                    dp.Add("G_Name", "%" + queryParam["G_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND cte.O_GoodsName Like @G_Name ");
                }
                if (!queryParam["FormulaName"].IsEmpty())
                {
                    dp.Add("FormulaName", "%" + queryParam["FormulaName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND cte.FormulaName Like @FormulaName ");
                }
                
                return this.BaseRepository().FindTable(strSql.ToString(),dp);
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
        /// 获取细加工出成率
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetLivingToCookDetail(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"
                 
      WITH cte AS (
				 SELECT  o.O_GoodsName ,
				( SELECT    CONVERT(VARCHAR, C_Min * 100) + '-'
							+ CONVERT(VARCHAR, C_Max * 100) AS rate
				  FROM      dbo.Mes_Convert c
				  WHERE     c.C_ProNo = h.O_ProCode
				) rate ,
				( SELECT    b.B_FormulaName
				  FROM      dbo.Mes_BomRecord b
				  WHERE     b.B_FormulaCode = h.O_Record
				) FormulaName ,
                CONVERT(DECIMAL(18,2),SUM(o.O_Qty))requireQty,
			    CONVERT(DECIMAL(18,2),SUM(o.O_SecQty)) productQty,
				( CASE WHEN MONTH(O_CreateDate) = 1
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) January ,
				( CASE WHEN MONTH(O_CreateDate) = 2
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) February ,
				( CASE WHEN MONTH(O_CreateDate) = 3
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) March ,
				( CASE WHEN MONTH(O_CreateDate) = 4
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) April ,
				( CASE WHEN MONTH(O_CreateDate) = 5
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) May ,
				( CASE WHEN MONTH(O_CreateDate) = 6
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) June ,
				( CASE WHEN MONTH(O_CreateDate) = 7
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) July ,
				( CASE WHEN MONTH(O_CreateDate) = 8
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty)* 100))
					   ELSE 0
				  END ) August ,
				( CASE WHEN MONTH(O_CreateDate) = 9
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) September ,
				( CASE WHEN MONTH(O_CreateDate) = 10
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) October ,
				( CASE WHEN MONTH(O_CreateDate) = 11
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) November ,
				( CASE WHEN MONTH(O_CreateDate) = 12
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) December
		FROM    dbo.Mes_OrgResDetail o
				JOIN dbo.Mes_OrgResHead h ON h.O_OrgResNo = o.O_OrgResNo
				
		WHERE   h.O_ProCode = '03' AND o.O_GoodsCode IN ( SELECT   g.G_Code
                               FROM     dbo.Mes_Goods g
                               WHERE    g.G_TKind = '01' )
		       
		GROUP BY O_CreateDate ,
				O_GoodsName ,
				h.O_Record ,
				O_ProCode ) 
        SELECT *,
        cte.[January]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'JanDiff',
        cte.[February]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'FebDiff',
        cte.[March]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'MarDiff',
        cte.[April]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'AprDiff',
        cte.[May]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'MayDiff',
        cte.[June]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'JunDiff',
        cte.[July]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'JulyDiff',
        cte.[August]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'AugDiff',
        cte.[September]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'SeptDiff',
        cte.[October]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'OctDiff',
        cte.November-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'NovDiff',
        cte.[December]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'DecDiff'
        FROM cte where 1=1 
         ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
               
                if (!queryParam["G_Name"].IsEmpty())
                {
                    dp.Add("G_Name", "%" + queryParam["G_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND cte.O_GoodsName Like @G_Name ");
                }
                if (!queryParam["FormulaName"].IsEmpty())
                {
                    dp.Add("FormulaName", "%" + queryParam["FormulaName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND cte.FormulaName Like @FormulaName ");
                }
                
                return this.BaseRepository().FindTable(strSql.ToString(),dp);
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
        /// 获取包装偏差率
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetPackingRatePageList(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"
                 
      WITH cte AS (
				 SELECT  o.O_GoodsName ,
				( SELECT    CONVERT(VARCHAR, C_Min * 100) + '-'
							+ CONVERT(VARCHAR, C_Max * 100) AS rate
				  FROM      dbo.Mes_Convert c
				  WHERE     c.C_ProNo = h.O_ProCode
				) rate ,
				( SELECT    b.B_FormulaName
				  FROM      dbo.Mes_BomRecord b
				  WHERE     b.B_FormulaCode = h.O_Record
				) FormulaName ,
				( CASE WHEN MONTH(O_CreateDate) = 1
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) January ,
				( CASE WHEN MONTH(O_CreateDate) = 2
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) February ,
				( CASE WHEN MONTH(O_CreateDate) = 3
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) March ,
				( CASE WHEN MONTH(O_CreateDate) = 4
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) April ,
				( CASE WHEN MONTH(O_CreateDate) = 5
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) May ,
				( CASE WHEN MONTH(O_CreateDate) = 6
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) June ,
				( CASE WHEN MONTH(O_CreateDate) = 7
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) July ,
				( CASE WHEN MONTH(O_CreateDate) = 8
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty)* 100))
					   ELSE 0
				  END ) August ,
				( CASE WHEN MONTH(O_CreateDate) = 9
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) September ,
				( CASE WHEN MONTH(O_CreateDate) = 10
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) October ,
				( CASE WHEN MONTH(O_CreateDate) = 11
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) November ,
				( CASE WHEN MONTH(O_CreateDate) = 12
					   THEN CONVERT(DECIMAL(10, 2), ( SUM(o.O_SecQty) / SUM(o.O_Qty) )
							* 100)
					   ELSE 0
				  END ) December
		FROM    dbo.Mes_OrgResDetail o
				JOIN dbo.Mes_OrgResHead h ON h.O_OrgResNo = o.O_OrgResNo
				
		WHERE   h.O_ProCode = '04' AND o.O_GoodsCode IN ( SELECT   g.G_Code
                               FROM     dbo.Mes_Goods g
                               WHERE    g.G_TKind = '01' )
		       
		GROUP BY O_CreateDate ,
				O_GoodsName ,
				h.O_Record ,
				O_ProCode ) 
        SELECT *,
        cte.[January]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'JanDiff',
        cte.[February]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'FebDiff',
        cte.[March]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'MarDiff',
        cte.[April]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'AprDiff',
        cte.[May]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'MayDiff',
        cte.[June]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'JunDiff',
        cte.[July]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'JulyDiff',
        cte.[August]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'AugDiff',
        cte.[September]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'SeptDiff',
        cte.[October]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'OctDiff',
        cte.November-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'NovDiff',
        cte.[December]-CONVERT(DECIMAL(10,2),SUBSTRING(cte.rate,1,2))'DecDiff'
        FROM cte where 1=1 
         ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
               
                if (!queryParam["G_Name"].IsEmpty())
                {
                    dp.Add("G_Name", "%" + queryParam["G_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND cte.O_GoodsName Like @G_Name ");
                }
                if (!queryParam["FormulaName"].IsEmpty())
                {
                    dp.Add("FormulaName", "%" + queryParam["FormulaName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND cte.FormulaName Like @FormulaName ");
                }
                
                return this.BaseRepository().FindTable(strSql.ToString(),dp);
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
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                      g.[ID]
                      ,g.[G_Code]
                      ,g.[G_Name]
                      ,g.[G_SupplyName]
                      ,g.[G_Kind]
                      ,g.[G_Period]
                      ,g.[G_Price]
                      ,g.[G_Unit]
                      ,g.[G_UnitWeight]
                      ,g.[G_Super]
                      ,g.[G_Lower]
                      ,g.[G_CreateBy]
                      ,g.[G_CreateDate]
                      ,g.[G_UpdateBy]
                      ,g.[G_UpdateDate]
                      ,g.[G_Remark]
                      ,g.[G_Erpcode]
                      ,g.[G_TKind]
                      ,g.[G_UnitQty]
                      ,g.[G_Unit2]
                      ,g.[G_Self]
                      ,g.[G_Online]
                      ,g.[G_Prepareday]
                      ,g.[G_Otax]
                      ,g.[G_Itax]
                      ,g.[G_TeamCode]
                      ,k.G_Name KindName
                       ,c.T_Name T_Name
                ");
                strSql.Append("FROM Mes_Goods g LEFT JOIN dbo.Mes_GoodKind k ON(g.G_TKind=k.G_Code)  left join  Mes_Team c on(g.G_TeamCode=c.T_Code)");
                strSql.Append("WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
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
                if (!queryParam["G_Supply"].IsEmpty())
                {
                    dp.Add("G_Supply", "%" + queryParam["G_Supply"].ToString() + "%", DbType.String);
                    strSql.Append(" AND g.G_Supply Like @G_Supply ");
                }
                if (!queryParam["G_Kind"].IsEmpty())
                {
                    dp.Add("G_Kind", "%" + queryParam["G_Kind"].ToString() + "%", DbType.String);
                    strSql.Append(" AND g.G_Kind Like @G_Kind ");
                }
                return this.BaseRepository().FindTable(strSql.ToString(),dp, pagination);
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

        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete<Mes_GoodsEntity>(t=>t.ID == keyValue);
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
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, Mes_GoodsEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
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
