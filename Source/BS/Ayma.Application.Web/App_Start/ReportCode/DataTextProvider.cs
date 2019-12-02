///下面两个编译条件参数指定产生报表数据的格式。如果都不定义，则产生 XML 形式的报表数据
///编译条件参数定义在项目属性的“生成->条件编译符号”里更合适，这样可以为整个项目使用
///_XML_REPORT_DATA：指定产生 XML 形式的报表数据
///_JSON_REPORT_DATA：指定产生 JSON 形式的报表数据。
//#define _XML_REPORT_DATA
#define _JSON_REPORT_DATA

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Diagnostics;
using Ayma.Application.TwoDevelopment;
using Ayma.Util;
using Newtonsoft.Json;

namespace Ayma.Application.Web
{

#if _JSON_REPORT_DATA
    using MyDbReportData = DatabaseJsonReportData;

#else
using MyDbReportData = DatabaseXmlReportData;
#endif

    /// <summary>
    /// 在这里集中产生整个项目的所有报表需要的 XML 或 JSON 文本数据 
    /// </summary>
    public class DataTextProvider
    {
        /// <summary>
        /// 根据查询SQL语句产生报表数据
        /// </summary>
        public static string Build(string QuerySQL)
        {
            return MyDbReportData.TextFromOneSQL(QuerySQL);
        }

        /// <summary>
        /// 根据多条查询SQL语句产生报表数据，数据对应多记录集
        /// </summary>
        public static string Build(ArrayList QueryList)
        {
            return MyDbReportData.TextFromMultiSQL(QueryList);
        }

        #region 实际业务
        /// <summary>
        /// 领料单
        /// </summary>
        /// <returns></returns>
        public static string Picking(string doucno)
        {
            string sql = @"SELECT  
                        t.P_Status ,
                        t.C_CollarNo ,
                        t.C_StockName ,
                        t.C_StockToName ,
                        t.C_CreateDate ,
                        t.C_CreateBy,
                        d.C_Unit,
                        d.C_Qty,
                        d.C_GoodsCode,
                        d.C_GoodsName,
                        t.C_Remark,
		                d.C_Price,
		                (d.C_Price*d.C_Qty) as aoumnt
            FROM    Mes_CollarHead t
                    LEFT JOIN dbo.Mes_CollarDetail d ON t.C_CollarNo = d.C_CollarNo
            WHERE   t.C_CollarNo ='{0}'
            ORDER BY M_UploadDate DESC";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(sql, doucno), "Picking"));

            return MyDbReportData.TextFromMultiSQL(QueryList);
        }
        /// <summary>
        /// 退供应商单
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string BackSupply(string doucno)
        {
            string sql = @"SELECT  
                    a.B_BackSupplyNo ,
                    a.B_StockName ,
                    a.B_OrderDate ,
                    b.B_GoodsCode ,
                    b.B_GoodsName ,
                    b.B_Unit ,
                    b.B_Qty,
                    b.B_Batch,
                    b.B_Remark
            FROM    dbo.Mes_BackSupplyHead a
                    LEFT JOIN dbo.Mes_BackSupplyDetail b ON a.B_BackSupplyNo=b.B_BackSupplyNo
            WHERE   a.B_BackSupplyNo ='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(sql, doucno), "BackSupply"));

            return MyDbReportData.TextFromMultiSQL(QueryList);
        }
        /// <summary>
        /// 报废打印
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string Scrap(string doucno)
        {
            string sql = @"SELECT  h.S_ScrapNo ,
                                    h.S_OrderDate ,
                                    h.S_Remark ,
                                    d.S_GoodsCode ,
                                    d.S_GoodsName ,
                                    d.S_Unit ,
                                    d.S_Qty ,
                                    d.S_Batch
                            FROM    dbo.Mes_ScrapHead h
                                    LEFT JOIN dbo.Mes_ScrapDetail d ON h.S_ScrapNo = d.S_ScrapNo 
                            WHERE   h.S_ScrapNo ='{0}'"; 
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(sql, doucno), "Scrap"));

            return MyDbReportData.TextFromMultiSQL(QueryList);
        }

        /// <summary>
        /// 退库打印
        /// </summary>
        /// <returns></returns>
        public static string BackStock(string doucno)
        {
            var strsql = @"SELECT  h.B_BackStockNo ,
                                    h.B_OrderDate ,
                                    h.B_StockName ,
                                    h.B_StockToName ,
                                    d.B_GoodsCode ,
                                    d.B_GoodsName ,
                                    d.B_Qty ,
                                    d.B_Unit
                            FROM    dbo.Mes_BackStockHead h
                                    LEFT JOIN dbo.Mes_BackStockDetail d ON h.B_BackStockNo = d.B_BackStockNo
                            WHERE   h.B_BackStockNo ='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strsql, doucno), "BackStock"));

            return MyDbReportData.TextFromMultiSQL(QueryList);
        }

        /// <summary>
        /// 物料组装
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string OrgRes(string doucno)
        {
            var strSql = @"    SELECT  h.O_OrgResNo ,
                                    h.O_OrderNo,
                                    h.O_WorkShopName,
                                    d.O_GoodsName ,
                                    d.O_Qty,
                                    d.O_Unit,
                                    d.O_SecGoodsName ,
                                    d.O_SecQty ,
                                    d.O_SecUnit
                            FROM    dbo.Mes_OrgResHead h
                                    LEFT JOIN dbo.Mes_OrgResDetail D ON h.O_OrgResNo = d.O_OrgResNo
                            WHERE   1=1 AND h.O_OrgResNo ='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strSql, doucno), "OrgRes"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }
        /// <summary>
        /// 原物料销售单制作打印
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string SaleManager(string doucno)
        {
            var strSql = @"                             
                        SELECT  h.S_SaleNo ,
                                h.S_StockName ,
                                h.S_CostomName ,
                                h.S_CreateBy ,
		                        h.S_CreateDate,
		                        h.MonthBalance,
                                d.S_GoodsCode ,
                                d.S_GoodsName ,
                                d.S_Qty ,
                                d.S_Unit,
		                        d.S_Remark,
		                        d.S_Batch,
		                        d.S_Otax,
		                        d.S_Price
                        FROM    dbo.Mes_SaleHead h
                                LEFT JOIN dbo.Mes_SaleDetail d ON h.S_SaleNo = d.S_SaleNo
                            WHERE   h.S_SaleNo='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strSql, doucno), "SaleManager"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }
        /// <summary>
        /// 入库单打印
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string MaterIn(string doucno)
        {
            var strSql = @" SELECT  h.M_MaterInNo ,
                                    h.M_StockName ,
                                    h.M_OrderNo ,
                                    h.M_OrderDate ,
                                    h.M_CreateBy ,
                                    d.M_GoodsCode ,
                                    d.M_GoodsName ,
                                    d.M_Kind,
                                    d.M_Unit,
                                    d.M_Qty,
                                    d.M_Batch,
                                    d.M_Remark
                            FROM    dbo.Mes_MaterInHead h
                                    LEFT JOIN dbo.Mes_MaterInDetail d ON ( h.M_MaterInNo = d.M_MaterInNo )
                            WHERE   h.M_MaterInNo='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strSql, doucno), "MaterIn"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }
        /// <summary>
        /// 日耗品消耗单打印
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string ExpendManager(string doucno)
        {
            var strSql = @" 
                            SELECT  h.E_ExpendNo ,
                                    h.E_StockName ,
                                    h.E_CreateBy ,
                                    h.E_CreateDate ,
		                            h.MonthBalance,
                                    d.E_GoodsCode ,
                                    d.E_GoodsName ,
                                    d.E_Unit ,
                                    d.E_Qty,
		                            d.E_Batch,
		                            d.E_Price,
		                            h.E_Remark
                            FROM    dbo.Mes_ExpendHead h
                                    LEFT JOIN dbo.Mes_ExpendDetail d ON h.E_ExpendNo = d.E_ExpendNo
                            WHERE   h.E_ExpendNo='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strSql, doucno), "ExpendManager"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }
        /// <summary>
        /// 其它入库单打印
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string Other(string doucno)
        {
            var strSql = @" 
                            SELECT  h.O_OtherInNo ,
                                    h.O_StockName ,
                                    h.O_CreateDate ,
                                    h.O_CreateBy ,
		                            h.MonthBalance,
                                    d.O_GoodsCode ,
                                    d.O_GoodsName ,
                                    d.O_Qty ,
                                    d.O_Unit,
		                            d.O_Remark,
		                            d.O_Batch
                            FROM    dbo.Mes_OtherInHead h
                                    LEFT JOIN dbo.Mes_OtherInDetail d ON (h.O_OtherInNo = d.O_OtherInNo)
                            WHERE   h.O_OtherInNo='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strSql, doucno), "Other"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }
        /// <summary>
        /// 成品入库单打印
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string MaterInProject(string doucno)
        {
            var strSql = @" SELECT  h.M_MaterInNo ,
                                    h.M_StockName ,
                                    h.M_OrderNo ,
                                    h.M_OrderDate ,
                                    h.M_CreateBy ,
                                    d.M_GoodsCode ,
                                    d.M_GoodsName ,
                                    d.M_Kind,
                                    d.M_Unit,
                                    d.M_Qty,
                                    d.M_Batch,
                                    d.M_Remark
                            FROM    dbo.Mes_MaterInHead h
                                    LEFT JOIN dbo.Mes_MaterInDetail d ON ( h.M_MaterInNo = d.M_MaterInNo )
                            WHERE   h.M_MaterInNo='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strSql, doucno), "MaterInProject"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }
        /// <summary>
        /// 调拨单打印
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string Requist(string doucno)
        {
            var strSql = @" SELECT  h.R_RequistNo ,
                                    h.P_OrderNo ,
                                    h.P_OrderDate ,
                                    h.R_StockName,
                                    h.R_StockToName,
                                    h.R_Remark,
                                    d.R_GoodsCode ,
                                    d.R_GoodsName ,
                                    d.R_Unit ,
                                    d.R_Qty ,
                                    d.R_Batch 
                            FROM    dbo.Mes_RequistHead h
                                    LEFT JOIN dbo.Mes_RequistDetail d ON h.R_RequistNo = d.R_RequistNo
                            WHERE   h.R_RequistNo='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strSql, doucno), "Requist"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }
        /// <summary>
        /// 生产订单打印
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string ProductOrder(string doucno)
        {
            var strSql = @"SELECT   h.P_OrderNo ,
                                    h.P_OrderDate ,
                                    h.P_OrderStationName,
                                    d.P_GoodsCode ,
                                    d.P_GoodsName ,
                                    d.P_Qty ,
                                    d.P_Unit 
                            FROM    dbo.Mes_ProductOrderHead h
                                    LEFT JOIN dbo.Mes_ProductOrderDetail d ON h.P_OrderNo = d.P_OrderNo
                            WHERE   h.P_OrderNo='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strSql, doucno), "ProductOrder"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }
        /// <summary>
        /// 毛到净出成率打印
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string YieldRate(string name)
        {
            var strSql = @"
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
        ";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(strSql, "YieldRate"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        } 
        /// <summary>
        /// 粗加工出成率打印
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string LivingToCook(string name)
        {
            var strSql = @"
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
        ";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(strSql, "YieldRate"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        } 
        /// <summary>
        /// 细加工出成率打印
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string LivingToCookDetail(string name)
        {
            var strSql = @"
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
        ";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(strSql, "YieldRate"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }
        /// <summary>
        /// 包装偏差率打印
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string PackingRate(string name)
        {
            var strSql = @"
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
        ";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(strSql, "YieldRate"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }

        /// <summary>
        /// 线边仓出库
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string OutWorkShop(string doucno)
        {
            var sql = @"SELECT  h.O_OutNo ,
                                h.O_OrderNo ,
                                h.O_OrderDate ,
                                d.O_GoodsCode ,
                                d.O_GoodsName ,
                                d.O_Unit ,
                                d.O_Qty ,
                                d.O_Batch,
                                h.O_StockName
                        FROM    dbo.Mes_OutWorkShopHead h
                                LEFT JOIN dbo.Mes_OutWorkShopDetail d ON h.O_OutNo = d.O_OutNo
                        WHERE   h.O_OutNo ='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(sql, doucno), "OrgRes"));
            return MyDbReportData.TextFromMultiSQL(QueryList);
        } 
        /// <summary>
        /// 车间入库到线边仓
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string InWorkShop(string doucno)
        {
            var sql = @"SELECT  h.I_InNo ,
                                h.I_OrderNo ,
                                h.I_OrderDate ,
                                d.I_GoodsCode ,
                                d.I_GoodsName ,
                                d.I_Unit ,
                                d.I_Qty ,
                                d.I_Batch,
                                h.I_StockName
                        FROM    dbo.Mes_InWorkShopHead h
                                LEFT JOIN dbo.Mes_InWorkShopDetail d ON h.I_InNo = d.I_InNo
                        WHERE   h.I_InNo ='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(sql, doucno), "OrgRes"));
            return MyDbReportData.TextFromMultiSQL(QueryList);
        }
        /// <summary>
        /// 成品出库
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string ProOutMake(string doucno)
        {
            var sql = @"SELECT  h.P_ProOutNo ,
                                h.P_OrderNo ,
                                h.P_OrderDate ,
                                d.P_GoodsCode ,
                                d.P_GoodsName ,
                                d.P_Unit ,
                                d.P_Qty ,
                                d.P_Batch ,
                                h.P_StockName
                        FROM    dbo.Mes_ProOutHead h
                                LEFT JOIN dbo.Mes_ProOutDetail d ON h.P_ProOutNo = d.P_ProOutNo
                        WHERE   h.P_Status = 2 AND h.P_ProOutNo ='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(sql, doucno), "ProOutMake"));
            return MyDbReportData.TextFromMultiSQL(QueryList);
        }
        /// <summary>
        /// 其它出库
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string OtherOut(string doucno)
        {
            var sql = @"SELECT  h.O_OtherOutNo ,
                                h.O_StockName ,
                                h.O_DepartName ,
                                h.O_CreateBy ,
		                        h.O_CreateDate,
		                        h.MonthBalance,
                                d.O_GoodsCode ,
                                d.O_GoodsName ,
                                d.O_Qty ,
                                d.O_Unit,
		                        d.O_Remark,
		                        d.O_Batch
                        FROM    dbo.Mes_OtherOutHead h
                                LEFT JOIN dbo.Mes_OtherOutDetail d ON h.O_OtherOutNo = d.O_OtherOutNo
                        WHERE    h.O_OtherOutNo ='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(sql, doucno), "OtherOut"));
            return MyDbReportData.TextFromMultiSQL(QueryList);
        }
        #endregion

        #region 根据 HTTP 请求中的参数生成报表数据，主要是为例子报表自动分配合适的数据生成函数

        /// <summary>
        /// 为了避免 switch 语句的使用，建立数据名称与数据函数的映射(map)
        /// 在 Global.asax 中创建映射，即在WEB服务启动时初始化映射数据
        /// </summary>

        //简单无参数报表数据的名称与函数映射表
        private delegate string SimpleDataFun();

        //private static Dictionary<string, SimpleDataFun> SimpleDataFunMap = new Dictionary<string, SimpleDataFun>();

        //有参数报表数据的名称与函数映射表，参数来自 HttpRequest
        private delegate string SpecialDataFun(HttpRequest Request);

        private static Dictionary<string, SpecialDataFun> SpecialDataFunMap = new Dictionary<string, SpecialDataFun>();
 
        public static string BuildByHttpRequest(HttpRequest Request)
        {
            string DataText;
            string DataName = Request.QueryString["data"];

            //Trace.Assert(SimpleDataFunMap.Count > 0, "DataFunMap isn't initialized!");

            if (DataName != null) //if (DataName != "")
            {
                //根据数据名称查找映射表，如果找到，执行对应的报表数据函数获取数据
                //SimpleDataFun simpleFun;
                SpecialDataFun specialFun;
                //if (SimpleDataFunMap.TryGetValue(DataName, out simpleFun))
                //{
                //    DataText = simpleFun();
                //}
                //else 
                if (SpecialDataFunMap.TryGetValue(DataName, out specialFun))
                {
                    DataText = specialFun(Request);
                }
                else
                {
                    throw new Exception(string.Format("没有为报表数据 '{0}' 分配处理程序！", DataName));
                }
            }
            else
            {
                string QuerySQL = Request.QueryString["QuerySQL"];
                if (QuerySQL != null)
                {
                    //根据传递的 HTTP 请求中的查询SQL获取数据
                    DataText = DataTextProvider.Build(QuerySQL);
                }
                else if (Request.TotalBytes > 0)
                {
                    //从客户端发送的数据包中获取报表查询参数，URL有长度限制，当要传递的参数数据量比较大时，应该采用这样的方式
                    //这里演示了用这样的方式传递一个超长查询SQL语句。
                    byte[] FormData = Request.BinaryRead(Request.TotalBytes);
                    UTF8Encoding Unicode = new UTF8Encoding();
                    int charCount = Unicode.GetCharCount(FormData, 0, Request.TotalBytes);
                    char[] chars = new Char[charCount];
                    int charsDecodedCount = Unicode.GetChars(FormData, 0, Request.TotalBytes, chars, 0);

                    QuerySQL = new String(chars);

                    DataText = DataTextProvider.Build(QuerySQL);
                }
                else
                {
                    DataText = "";
                }
            }

            return DataText;
        }



        //初始化映射表(map)，在 Global.asax 中被调用
        public static void InitDataFunMap()
        {
            //Trace.Assert(SimpleDataFunMap.Count <= 0, "DataFunMap already initialized!");

            #region 业务
           
            //SpecialDataFunMap.Add("Test", Test);
            SpecialDataFunMap.Add("Picking", Picking);
            SpecialDataFunMap.Add("Scrap", Scrap);
            SpecialDataFunMap.Add("BackStock",BackStock);
            SpecialDataFunMap.Add("OrgRes", OrgRes);
            SpecialDataFunMap.Add("SaleManager", SaleManager);
            SpecialDataFunMap.Add("OutWorkShop", OutWorkShop);
            SpecialDataFunMap.Add("InWorkShop", InWorkShop);
            SpecialDataFunMap.Add("ProOutMake", ProOutMake);
            SpecialDataFunMap.Add("BackSupply", BackSupply);
            SpecialDataFunMap.Add("MaterIn", MaterIn);
            SpecialDataFunMap.Add("Other", Other);
            SpecialDataFunMap.Add("ExpendManager", ExpendManager);
            SpecialDataFunMap.Add("OtherOut", OtherOut);
            SpecialDataFunMap.Add("MaterInProject", MaterInProject);
            SpecialDataFunMap.Add("Requist", Requist);
            SpecialDataFunMap.Add("ProductOrder", ProductOrder);
            SpecialDataFunMap.Add("YieldRate", YieldRate);
            SpecialDataFunMap.Add("LivingToCook", LivingToCook);
            SpecialDataFunMap.Add("PackingRate", PackingRate);
            SpecialDataFunMap.Add("LivingToCookDetail", LivingToCookDetail);
           
            #endregion
        }

        private static string LivingToCookDetail(HttpRequest Request)
        {
            return LivingToCookDetail(Request.QueryString["name"]);
        }
        private static string PackingRate(HttpRequest Request)
        {
            return PackingRate(Request.QueryString["name"]);
        }  
        private static string LivingToCook(HttpRequest Request)
        {
            return LivingToCook(Request.QueryString["name"]);
        }  
        private static string YieldRate(HttpRequest Request)
        {
            return YieldRate(Request.QueryString["name"]);
        }  
        private static string OutWorkShop(HttpRequest Request)
        {
            return OutWorkShop(Request.QueryString["doucno"]);
        } 
        private static string InWorkShop(HttpRequest Request)
        {
            return InWorkShop(Request.QueryString["doucno"]);
        }
        private static string ProOutMake(HttpRequest Request)
        {
            return ProOutMake(Request.QueryString["doucno"]);
        }

        private static string BackSupply(HttpRequest Request)
        {
            return BackSupply(Request.QueryString["doucno"]);
        }

        private static string OrgRes(HttpRequest Request)
        {
            return OrgRes(Request.QueryString["doucno"]);
        }
        private static string SaleManager(HttpRequest Request)
        {
            return SaleManager(Request.QueryString["doucno"]);
        }
        private static string Scrap(HttpRequest Request)
        {
            return Scrap(Request.QueryString["doucno"]);
        }

        private static string Picking(HttpRequest Request)
        {
            return Picking(Request.QueryString["doucno"]);
        }
        private static string BackStock(HttpRequest Request)
        {
            return BackStock(Request.QueryString["doucno"]);
        }
        private static string MaterIn(HttpRequest Request)
        {
            return MaterIn(Request.QueryString["doucno"]);
        }
        private static string Other(HttpRequest Request)
        {
            return Other(Request.QueryString["doucno"]);
        }
        private static string ExpendManager(HttpRequest Request)
        {
            return ExpendManager(Request.QueryString["doucno"]);
        }
        private static string OtherOut(HttpRequest Request)
        {
            return OtherOut(Request.QueryString["doucno"]);
        }
        private static string MaterInProject(HttpRequest Request)
        {
            return MaterInProject(Request.QueryString["doucno"]);
        }
        private static string Requist(HttpRequest Request)
        {
            return Requist(Request.QueryString["doucno"]);
        }
        private static string ProductOrder(HttpRequest Request)
        {
            return ProductOrder(Request.QueryString["doucno"]);
        }

        #region 业务
        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        public static string Test(HttpRequest Request)
        {
            var json = JsonConvert.SerializeObject(new { });
            return json;
        }
        #endregion

        

        #endregion
    }
}