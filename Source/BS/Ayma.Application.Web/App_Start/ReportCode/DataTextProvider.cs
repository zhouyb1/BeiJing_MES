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
using Ayma.Application.TwoDevelopment.MesDev.MaterialsSum.ViewModel;
using Ayma.Application.TwoDevelopment;
using Ayma.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace Ayma.Application.Web
{

#if _JSON_REPORT_DATA
    using MyDbReportData = DatabaseJsonReportData;
    using Ayma.DataBase.Repository;
    using Dapper;
    using System.Data;
    using Ayma.Application.TwoDevelopment.MesDev;

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
        t.C_StockToName ,
        t.C_CreateDate ,
        dbo.GetUserNameById(t.C_CreateBy) as C_CreateBy ,
        d.C_Unit,
        d.C_Qty,
        d.C_GoodsCode,
        d.C_Unit2,
        d.C_UnitQty,
        d.C_Qty2,
        d.C_StockName,
        d.C_GoodsName,
        t.C_Remark,
		d.C_Remark as Remark,
		case when M_UploadDate is null then dbo.GetPrice(d.C_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.C_GoodsCode,CONVERT(VARCHAR(6),M_UploadDate,112)) end C_Price,
		M_UploadDate,
				CONVERT(decimal(18,6),(case when M_UploadDate is null then dbo.GetPrice(d.C_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.C_GoodsCode,CONVERT(VARCHAR(6),M_UploadDate,112)) end*d.C_Qty))as aoumnt,
		'1'+d.C_Unit2+'='+convert(varchar(50),convert(decimal(18,1),d.C_UnitQty))+d.C_Unit as Packingspecification,
	    convert(varchar(50),cast(d.C_Qty/d.C_UnitQty as int))+d.C_Unit2+convert(varchar(50),convert(decimal(18,1),d.C_Qty%d.C_UnitQty))+d.C_Unit as Auxiliarynumber
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
                    a.B_BackSupplyNo,
                    a.B_StockName,
                    a.B_CreateDate,
                    a.B_CreateBy,
                    a.B_Remark,
                    b.B_GoodsCode,
                    b.B_GoodsName,
                    b.B_Unit,
                    b.B_Qty,
                    b.B_Batch
                    ,(select P_InPrice from Mes_InPrice where P_GoodsCode=b.B_GoodsCode) as Price,
                    ((select P_InPrice from Mes_InPrice where P_GoodsCode=b.B_GoodsCode)*b.B_Qty) as aumount
            FROM    dbo.Mes_BackSupplyHead a
                    LEFT JOIN dbo.Mes_BackSupplyDetail b ON a.B_BackSupplyNo=b.B_BackSupplyNo
            WHERE   a.B_BackSupplyNo ='{0}' and a.B_Status=2";
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
                                    h.S_CreateDate,
                                    h.S_CreateBy,
                                    h.S_Remark,
                                    d.S_GoodsCode ,
                                    d.S_GoodsName ,
                                    d.S_Unit ,
                                    d.S_Qty ,
                                    d.S_Batch,
									case when S_UploadDate is null then dbo.GetPrice(d.S_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.S_GoodsCode,CONVERT(VARCHAR(6),S_UploadDate,112)) end S_Price,
								CONVERT(decimal(18,6),(case when S_UploadDate is null then dbo.GetPrice(d.S_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.S_GoodsCode,CONVERT(VARCHAR(6),S_UploadDate,112)) end *d.S_Qty)) as aoumnt
                            FROM    dbo.Mes_ScrapHead h
                                    LEFT JOIN dbo.Mes_ScrapDetail d ON h.S_ScrapNo = d.S_ScrapNo 
                            WHERE   h.S_ScrapNo ='{0}'and h.S_Status=!=1"; 
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
            var strsql = @"SELECT h.B_BackStockNo ,
                                    h.B_CreateDate,
                                    h.B_CreateBy,
                                    h.B_Remark,
                                    h.B_StockName ,
                                    h.B_StockToName ,
                                    d.B_GoodsCode ,
                                    d.B_GoodsName ,
                                    d.B_Qty ,
                                    d.B_Unit,
									case when B_UploadDate is null then dbo.GetPrice(d.B_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.B_GoodsCode,CONVERT(VARCHAR(6),B_UploadDate,112)) end B_Price,
								CONVERT(decimal(18,6),(case when B_UploadDate is null then dbo.GetPrice(d.B_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.B_GoodsCode,CONVERT(VARCHAR(6),B_UploadDate,112)) end *d.B_Qty)) as aoumnt
                            FROM    dbo.Mes_BackStockHead h
                                    LEFT JOIN dbo.Mes_BackStockDetail d ON h.B_BackStockNo = d.B_BackStockNo
                            WHERE   h.B_BackStockNo ='{0}' and h.B_Status!=1";
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
                                    h.O_CreateDate,
                                    h.O_CreateBy,
                                    h.O_Remark,
                                    h.O_WorkShopName,
                                    dbo.GetProNamekByCode(O_ProCode) O_ProCode,
                                    d.O_GoodsName ,
                                    d.O_Qty,
                                    d.O_Unit,
                                    d.O_Price,
                                    d.O_Price*d.O_Qty O_Amount,
                                    d.O_SecGoodsName ,
                                    d.O_SecQty ,
                                    d.O_SecUnit,
                                    d.O_SecPrice,
                                    d.O_SecQty*d.O_SecPrice O_SecAmount
                            FROM    dbo.Mes_OrgResHead h
                                    LEFT JOIN dbo.Mes_OrgResDetail D ON h.O_OrgResNo = d.O_OrgResNo
                            WHERE   1=1 AND h.O_OrgResNo ='{0}'and h.O_Status=2";
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
		                        h.S_Remark,
		                        d.S_Batch,
		                        d.S_Otax,
		                        d.S_Price,
		                        (d.S_Price*d.S_Qty) as Notaxamount,
		                        (d.S_Price*(1+(d.S_Otax/100))) as Taxprice,
		                        ((d.S_Price*(1+(d.S_Otax/100)))* d.S_Qty)  as Taxamount,
		                        ((d.S_Price*(1+(d.S_Otax/100)))* d.S_Qty)-(d.S_Price*d.S_Qty) as tax
                        FROM    dbo.Mes_SaleHead h
                                LEFT JOIN dbo.Mes_SaleDetail d ON h.S_SaleNo = d.S_SaleNo
                            WHERE   h.S_SaleNo='{0}' and h.S_Status!=1";
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
            var strSql = @" SELECT 
        h.M_MaterInNo ,
        h.M_StockName ,
		h.M_SupplyName,
        h.M_CreateDate ,
        dbo.GetUserNameById(h.M_CreateBy ) as M_CreateBy ,
        d.M_GoodsCode ,
        d.M_GoodsName ,
        d.M_Kind,
        d.M_Unit,
        d.M_Qty,
        d.M_Batch,
        (d.M_Qty*g.G_Price) Amount,
        d.M_Price,
		d.M_StockCode,
		d.M_StockName,
		d.M_Unit2,
		d.M_UnitQty,
		d.M_Qty,
		d.M_Tax,
		d.M_Qty2,
		d.M_TaxPrice,
		(d.M_TaxPrice*d.M_Qty) as Taxamount,
		(d.M_Price*d.M_Qty) as Notaxamount,
        h.M_Remark,
		'1'+d.M_Unit2+'='+convert(varchar(50),convert(decimal(18,1),d.M_UnitQty))+d.M_Unit as Packingspecification,
		convert(varchar(50),cast(d.M_Qty/d.M_UnitQty as int))+d.M_Unit2+convert(varchar(50),convert(decimal(18,1),d.M_Qty%d.M_UnitQty))+d.M_Unit as Auxiliarynumber
                            FROM    dbo.Mes_MaterInHead h
                                    LEFT JOIN dbo.Mes_MaterInDetail d ON ( h.M_MaterInNo = d.M_MaterInNo )
                                    LEFT JOIN dbo.Mes_Goods g ON g.G_Code =d.M_GoodsCode
                            WHERE   h.M_MaterInNo='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strSql, doucno), "MaterIn"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }
        /// <summary>
        /// 原物料出入库统计
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string YWLCRKTJ(string starttime, string endtime, string ToDate, string S_Name, string M_GoodsName)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"                                          select * from  ( select 
                                    (select S_Name from Mes_Stock where S_Code=s.G_StockCode)as S_Name,
                                     s.G_StockCode
								    ,s.G_Code 
									,s.G_Name
                                    ,s.G_Unit
                                    ,'{0}' as 'startTime'
                                    ,'{1}' as 'endTime'
                                    ,(select ISNULL(O_SalePrice,0) from Mes_OutPrice where O_GoodsCode=s.G_Code ) as outPrice
                                    ,(select ISNULL(O_SalePrice,0) from Mes_OutPrice where O_GoodsCode=s.G_Code )*(select  ISNULL(SUM(S_Qty),0) from Mes_SaleDetail where S_GoodsCode=s.G_Code  and S_SaleNo in(select S_SaleNo from Mes_SaleHead where (S_CreateDate >='{0}' and S_CreateDate <='{1}')and S_Status=3)) as outamount
			                	   	,(select  ISNULL(SUM(B_Qty),0) from Mes_BackStockDetail where B_GoodsCode=s.G_Code  and B_BackStockNo in(select B_BackStockNo from Mes_BackStockHead where (B_CreateDate >='{0}' and B_CreateDate <='{1}')and B_Status=3))as withdrawingnumber
									,(select  ISNULL(SUM(S_Qty),0) from Mes_SaleDetail where S_GoodsCode=s.G_Code  and S_SaleNo in(select S_SaleNo from Mes_SaleHead where (S_CreateDate >='{0}' and S_CreateDate <='{1}')and S_Status=3)) as materialssales 
									,(select ISNULL(SUM(S_Qty),0) from Mes_ScrapDetail where S_GoodsCode=s.G_Code  and S_ScrapNo in(select S_ScrapNo from Mes_ScrapHead where (S_CreateDate >='{0}' and S_CreateDate <='{1}')and S_Status=3)) as scrapist  
									,(select  ISNULL(SUM(O_Qty),0) from Mes_OtherInDetail where O_GoodsCode=s.G_Code  and O_OtherInNo in(select O_OtherInNo from Mes_OtherInHead where (O_CreateDate >='{0}' and O_CreateDate <='{1}')and O_Status=3))  as otherwarehouse  
									,(select  ISNULL(SUM(O_Qty),0) from Mes_OtherOutDetail where O_GoodsCode=s.G_Code  and O_OtherOutNo in(select O_OtherOutNo from Mes_OtherOutHead where (O_CreateDate >='{0}' and O_CreateDate <='{1}')and O_Status=3)) as otheroutbound  								
									,(select  ISNULL(SUM(B_Qty),0) from Mes_BackSupplyDetail where B_GoodsCode=s.G_Code  and B_BackSupplyNo in(select B_BackSupplyNo from Mes_BackSupplyHead where (B_CreateDate >='{0}' and B_CreateDate <='{1}')and B_Status=3)) as supplierback                                    
								    ,(select ISNULL(SUM(I_Qty),0) from Mes_InventoryLS where I_GoodsCode=s.G_Code   and I_Date='{2}') as Initialinventory													
									,(select ISNULL(SUM(M_Qty),0) from Mes_MaterInDetail where M_MaterInNo in (select M_MaterInNo from Mes_MaterInHead where M_CreateDate>='{0}' and M_CreateDate<='{1}' and M_Status=3) and M_GoodsCode=s.G_Code) as Inventoryquantity								
									,dbo.GetPrice(s.G_Code,MONTH(GetDate())) as Price								  
								    ,(select  ISNULL(SUM(B_Qty),0) from Mes_BackStockDetail b where  b.B_BackStockNo in(select B_BackStockNo from Mes_BackStockHead h 
									where (h.B_CreateDate>='{0}' and h.B_CreateDate<='{1}' and B_Kind=1 and B_Status=3 )) AND B_GoodsCode=s.G_Code ) as Back_Qty							
									,dbo.GetPrice(s.G_Code,MONTH(GetDate()))*(select ISNULL(SUM(I_Qty),0) from Mes_InventoryLS where I_GoodsCode=s.G_Code   and I_Date='{2}') as initialamount
						      	    ,(select ISNULL(SUM(C_Qty),0) from Mes_CollarDetail where C_CollarNo in(select C_CollarNo from Mes_CollarHead  where (C_CreateDate>='{0}' and C_CreateDate<='{1}') and C_GoodsCode=s.G_Code  and P_Status=3)) as delivery
									,((select ISNULL(SUM(I_Qty),0) from Mes_InventoryLS where I_GoodsCode=s.G_Code   and I_Date='{2}')+(select ISNULL(SUM(M_Qty),0) from Mes_MaterInDetail where M_MaterInNo in (select M_MaterInNo from Mes_MaterInHead where M_CreateDate>='{0}' and M_CreateDate<='{1}' and M_Status=3) and M_GoodsCode=s.G_Code)-
									(select ISNULL(SUM(C_Qty),0) from Mes_CollarDetail where C_CollarNo in(select C_CollarNo from Mes_CollarHead  where (C_CreateDate>='{0}' and C_CreateDate<='{1}') and C_GoodsCode=s.G_Code  and P_Status=3))
									+(select  ISNULL(SUM(B_Qty),0) from Mes_BackStockDetail where B_GoodsCode=s.G_Code  and B_BackStockNo in(select B_BackStockNo from Mes_BackStockHead where (B_CreateDate >='{0}' and B_CreateDate <='{1}')and B_Status=3))
									-(select  ISNULL(SUM(S_Qty),0) from Mes_SaleDetail where S_GoodsCode=s.G_Code  and S_SaleNo in(select S_SaleNo from Mes_SaleHead where (S_CreateDate >='{0}' and S_CreateDate <='{1}')and S_Status=3))
									-(select ISNULL(SUM(S_Qty),0) from Mes_ScrapDetail where S_GoodsCode=s.G_Code  and S_ScrapNo in(select S_ScrapNo from Mes_ScrapHead where (S_CreateDate >='{0}' and S_CreateDate <='{1}')and S_Status=3))
									+(select  ISNULL(SUM(O_Qty),0) from Mes_OtherInDetail where O_GoodsCode=s.G_Code  and O_OtherInNo in(select O_OtherInNo from Mes_OtherInHead where (O_CreateDate >='{0}' and O_CreateDate <='{1}')and O_Status=3))
									-(select  ISNULL(SUM(O_Qty),0) from Mes_OtherOutDetail where O_GoodsCode=s.G_Code  and O_OtherOutNo in(select O_OtherOutNo from Mes_OtherOutHead where (O_CreateDate >='{0}' and O_CreateDate <='{1}')and O_Status=3))
									-(select  ISNULL(SUM(B_Qty),0) from Mes_BackSupplyDetail where B_GoodsCode=s.G_Code  and B_BackSupplyNo in(select B_BackSupplyNo from Mes_BackSupplyHead where (B_CreateDate >='{0}' and B_CreateDate <='{1}')and B_Status=3)))	 as Endinginventory																	   								   
								    ,((select ISNULL(SUM(I_Qty),0) from Mes_InventoryLS where I_GoodsCode=s.G_Code   and I_Date='{2}')+(select ISNULL(SUM(M_Qty),0) from Mes_MaterInDetail where M_MaterInNo in (select M_MaterInNo from Mes_MaterInHead where M_CreateDate>='{0}' and M_CreateDate<='{1}' and M_Status=3) and M_GoodsCode=s.G_Code)-
									(select ISNULL(SUM(C_Qty),0) from Mes_CollarDetail where C_CollarNo in(select C_CollarNo from Mes_CollarHead  where (C_CreateDate>='{0}' and C_CreateDate<='{1}')and C_GoodsCode=s.G_Code  and P_Status=3))
									+(select  ISNULL(SUM(B_Qty),0) from Mes_BackStockDetail where B_GoodsCode=s.G_Code  and B_BackStockNo in(select B_BackStockNo from Mes_BackStockHead where (B_CreateDate >='{0}' and B_CreateDate <='{1}')and B_Status=3))
									-(select  ISNULL(SUM(S_Qty),0) from Mes_SaleDetail where S_GoodsCode=s.G_Code  and S_SaleNo in(select S_SaleNo from Mes_SaleHead where (S_CreateDate >='{0}' and S_CreateDate <='{1}')and S_Status=3))
									-(select ISNULL(SUM(S_Qty),0) from Mes_ScrapDetail where S_GoodsCode=s.G_Code  and S_ScrapNo in(select S_ScrapNo from Mes_ScrapHead where (S_CreateDate >='{0}' and S_CreateDate <='{1}')and S_Status=3))
									+(select  ISNULL(SUM(O_Qty),0) from Mes_OtherInDetail where O_GoodsCode=s.G_Code  and O_OtherInNo in(select O_OtherInNo from Mes_OtherInHead where (O_CreateDate >='{0}' and O_CreateDate <='{1}')and O_Status=3))
									-(select  ISNULL(SUM(O_Qty),0) from Mes_OtherOutDetail where O_GoodsCode=s.G_Code  and O_OtherOutNo in(select O_OtherOutNo from Mes_OtherOutHead where (O_CreateDate >='{0}' and O_CreateDate <='{1}')and O_Status=3))
									-(select  ISNULL(SUM(B_Qty),0) from Mes_BackSupplyDetail where B_GoodsCode=s.G_Code  and B_BackSupplyNo in(select B_BackSupplyNo from Mes_BackSupplyHead where (B_CreateDate >='{0}' and B_CreateDate <='{1}')and B_Status=3)))*dbo.GetPrice(s.G_Code,MONTH(GetDate())) as finalamount																										
									from Mes_Goods s where  s.G_Kind=1");
            if (!S_Name.IsEmpty())
            {
                strSql.Append(" AND s.G_StockCode=" + S_Name);
            }
            if (!M_GoodsName.IsEmpty())
            {
                strSql.Append("  AND s.G_Code=" + M_GoodsName);
            }
            strSql.Append(@" ) t where ltrim(rtrim(t.Inventoryquantity)) not in ('0.000000')  or ltrim(rtrim(t.delivery)) not in ('0.000000') or ltrim(rtrim(t.back_qty)) not in ('0.000000') 
									or ltrim(rtrim(t.withdrawingnumber)) not in ('0.000000') or ltrim(rtrim(t.materialssales)) not in ('0.000000') or ltrim(rtrim(t.scrapist)) not in ('0.000000') or ltrim(rtrim(t.otherwarehouse)) not in ('0.000000') 
									or ltrim(rtrim(t.otheroutbound)) not in ('0.000000') or ltrim(rtrim(t.supplierback)) not in ('0.000000')									
									 ");
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strSql.ToString(), starttime, endtime, ToDate), "YWLCRKTJ"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }
        /// <summary>
        /// 供应商存货明细
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string GYSCHMX(string starttime, string endtime)
        {

            var strSql = @" SELECT 
                                '{0}' as strattime,
                                '{1}' as endtime,
                                h.M_MaterInNo,
                                h.M_SupplyCode,
                                h.M_SupplyName ,
                                d.M_GoodsCode ,
                                d.M_GoodsName ,
                                d.M_Unit ,
                                d.M_Tax,
                                MAX(d.M_Price) M_Price,
                                SUM(d.M_Price * M_Qty) row_amount, 
                                SUM(M_Qty) row_qty,
                                d.M_Unit2,
                                d.M_UnitQty
                        FROM    dbo.Mes_MaterInHead h
                                LEFT JOIN dbo.Mes_MaterInDetail d ON d.M_MaterInNo = h.M_MaterInNo
                        WHERE   h.M_Status = 3
                                AND d.M_Kind = 1 AND ( h.M_CreateDate >= '{0}' AND h.M_CreateDate <= '{1}' ) 
                        GROUP BY
                                h.M_SupplyName,
                                h.M_SupplyCode,
                                h.M_MaterInNo, 
                                M_GoodsCode ,
                                M_GoodsName ,
                                M_Unit,
                                d.M_Tax,
                                d.M_Unit2,
                                d.M_UnitQty order by h.M_SupplyCode";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strSql, starttime, endtime), "GYSCHMX"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }
        /// <summary>
        /// 其他出库汇总报表
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string QTCKHZBB(string starttime, string endtime, string GoodsCode, string StockCode)
        {
            var strSql = @"SELECT 
	                        '{0}' as starttime,
		                        '{1}' as endtime,
	                        MyData.F_CreateDate,
                               MyData.F_GoodsCode,
                               G.G_Name F_GoodsName,
                               MyData.F_InQty,
                               MyData.F_OutQty,
                               (F_OutQty - F_InQty) F_DiffQty
                        FROM
                        (
                            SELECT (CASE
                                        WHEN OutData.F_CreateDate IS NOT NULL THEN
                                            OutData.F_CreateDate
                                        ELSE
                                            InData.F_CreateDate
                                    END
                                   ) F_CreateDate,
                                   (CASE
                                        WHEN OutData.F_GoodsCode IS NOT NULL THEN
                                            OutData.F_GoodsCode
                                        ELSE
                                            InData.F_GoodsCode
                                    END
                                   ) F_GoodsCode,
                                   ISNULL(OutData.F_OutQty, 0) F_OutQty,
                                   ISNULL(InData.F_InQty, 0) F_InQty
                            FROM
                            (
                                SELECT CONVERT(VARCHAR(10), H.O_CreateDate, 120) F_CreateDate,
                                       D.O_GoodsCode F_GoodsCode,
                                       SUM(D.O_Qty) F_OutQty
                                FROM Mes_OtherOutHead H
                                    LEFT JOIN Mes_OtherOutDetail D
                                        ON D.O_OtherOutNo = H.O_OtherOutNo
                                WHERE H.O_Status = 3
                            AND  (H.O_CreateDate>='{0}' and CONVERT(VARCHAR(10), H.O_CreateDate, 120)<='{1}' )
                             {2}
                                GROUP BY CONVERT(VARCHAR(10), H.O_CreateDate, 120),
                                         O_GoodsCode
                            ) OutData
                                FULL JOIN
                                (
                                    SELECT CONVERT(VARCHAR(10), H.O_CreateDate, 120) F_CreateDate,
                                           D.O_GoodsCode F_GoodsCode,
                                           SUM(D.O_Qty) F_InQty
                                    FROM Mes_OtherInHead H
                                        LEFT JOIN dbo.Mes_OtherInDetail D
                                            ON D.O_OtherInNo = H.O_OtherInNo
                                    WHERE H.O_Status = 3
                                      AND  (H.O_CreateDate>='{0}' and CONVERT(VARCHAR(10), H.O_CreateDate, 120)<='{1}' )
                                    {2}
                                    GROUP BY CONVERT(VARCHAR(10), H.O_CreateDate, 120),
                                             O_GoodsCode
                                ) InData
                                    ON InData.F_CreateDate = OutData.F_CreateDate
                                       AND InData.F_GoodsCode = OutData.F_GoodsCode
                        ) MyData
                            LEFT JOIN Mes_Goods G
                                ON G.G_Code = MyData.F_GoodsCode
                        ORDER BY MyData.F_GoodsCode,
                                 MyData.F_CreateDate;
                ";

            // 虚拟参数
            StringBuilder sbCmd = new StringBuilder();
            StringBuilder sbInCmd = new StringBuilder();
            if (!GoodsCode.IsEmpty())
            {
                sbCmd.Append(" AND D.O_GoodsCode=" + GoodsCode);
            }

            if (!StockCode.IsEmpty())
            {
                sbCmd.Append(" AND H.O_StockCode=" + StockCode);

            }

            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strSql, starttime, endtime, sbCmd.ToString()), "QTCKHZBB"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }
        /// <summary>
        /// 领料出库汇总报表
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string LLCKHZBB(string starttime, string endtime, string GoodsCode, string StockCode)
        {

            var strSql = @"SELECT 
			'{0}' as starttime,
			'{1}' as endtime,
            MyData.F_CreateDate,
            MyData.F_GoodsCode,
            G.G_Name F_GoodsName,
            MyData.F_InQty,
			MyData.F_OutQty,
            (F_OutQty-F_InQty) F_DiffQty
                FROM
            (
                SELECT 
                    (CASE WHEN OutData.F_CreateDate IS NOT NULL THEN OutData.F_CreateDate ELSE InData.F_CreateDate  END) F_CreateDate,
            (CASE WHEN OutData.F_GoodsCode IS NOT NULL THEN OutData.F_GoodsCode ELSE InData.F_GoodsCode  END) F_GoodsCode,
            ISNULL(OutData.F_OutQty,0) F_OutQty,
            ISNULL(InData.F_InQty,0) F_InQty
                FROM
                (
                    SELECT CONVERT(VARCHAR(10),H.C_CreateDate,120) F_CreateDate,D.C_GoodsCode F_GoodsCode,SUM(D.C_Qty) F_OutQty FROM Mes_CollarHead H
                LEFT JOIN Mes_CollarDetail D ON D.C_CollarNo = H.C_CollarNo
            WHERE H.P_Status=3 
			AND (H.C_CreateDate>='{0}' AND H.C_CreateDate<'{1}')
                    {2}
            GROUP BY CONVERT(VARCHAR(10),H.C_CreateDate,120),C_GoodsCode
                )OutData
                FULL JOIN
            (
                SELECT CONVERT(VARCHAR(10),H.B_CreateDate,120) F_CreateDate,D.B_GoodsCode F_GoodsCode,SUM(D.B_Qty) F_InQty FROM Mes_BackStockHead H
                LEFT JOIN dbo.Mes_BackStockDetail D ON  D.B_BackStockNo = H.B_BackStockNo
            WHERE H.B_Status=3 AND (H.B_CreateDate>='{0}' AND H.B_CreateDate<'{1}')
			{3}
            GROUP BY CONVERT(VARCHAR(10),H.B_CreateDate,120),B_GoodsCode
                )InData ON InData.F_CreateDate = OutData.F_CreateDate AND InData.F_GoodsCode = OutData.F_GoodsCode
                )MyData LEFT JOIN Mes_Goods G ON G.G_Code=MyData.F_GoodsCode
            ORDER BY MyData.F_GoodsCode,MyData.F_CreateDate;
                ";
            // 虚拟参数
            StringBuilder sbOutCmd = new StringBuilder();
            StringBuilder sbInCmd = new StringBuilder();

            if (!GoodsCode.IsEmpty())
            {
                sbOutCmd.Append(" AND D.C_GoodsCode=" + GoodsCode);
                sbInCmd.Append("  AND D.B_GoodsCode=" + GoodsCode);
            }

            if (!StockCode.IsEmpty())
            {
                sbOutCmd.Append(" AND H.C_StockCode=" + StockCode);
                sbInCmd.Append("  AND H.B_StockToCode=" + StockCode);

            }

            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strSql, starttime, endtime, sbOutCmd, sbInCmd), "LLCKHZBB"));
            return MyDbReportData.TextFromMultiSQL(QueryList);

        }
        /// <summary>
        /// 供应商进货数据汇总
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string GYSJHSJHZ(string starttime, string endtime, string M_SupplyName)
        {
            var strSql = @" 		SELECT                            
									'{0}'as statrtime,
									'{1}' as endtime,
									h.M_MaterInNo ,
                                    m.M_SupplyCode ,
                                    m.M_SupplyName ,
                                    m.M_GoodsName ,
                                    m.M_GoodsCode ,
                                    m.M_StockName ,
                                    m.M_StockCode ,
                                    m.M_Price ,
                                    m.M_Qty ,
                                    m.M_Unit ,
                                    m.M_Tax,
                                    m.M_Qty*m.M_Price Amount,
                                    h.M_CreateDate ,
                                    dbo.GetUserNameById(h.M_CreateBy) M_CreateBy
                            FROM    dbo.Mes_MaterInHead h
                                    LEFT JOIN dbo.Mes_MaterInDetail m ON m.M_MaterInNo = h.M_MaterInNo  WHERE h.M_Status =3
									 AND ( h.M_CreateDate >= '{0}' AND h.M_CreateDate <='{1}' ) ORDER BY m.M_SupplyCode  asc";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(strSql, starttime, endtime), "GYSJHSJHZ"));
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
		                            h.E_Remark,
									case when E_UploadDate is null then dbo.GetPrice(d.E_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.E_GoodsCode,CONVERT(VARCHAR(6),E_UploadDate,112)) end E_Price,
								CONVERT(decimal(18,6),(case when E_UploadDate is null then dbo.GetPrice(d.E_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.E_GoodsCode,CONVERT(VARCHAR(6),E_UploadDate,112)) end *d.E_Qty)) as aoumnt
                            FROM    dbo.Mes_ExpendHead h
                                    LEFT JOIN dbo.Mes_ExpendDetail d ON h.E_ExpendNo = d.E_ExpendNo
                            WHERE   h.E_ExpendNo='{0}' and h.E_Status!=1";
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
		                            h.O_Remark,
		                            d.O_Batch,
	                            case when O_UploadDate is null then dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),O_UploadDate,112)) end O_Price,
								CONVERT(decimal(18,6),(case when O_UploadDate is null then dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),O_UploadDate,112)) end *d.O_Qty)) as aoumnt
                            FROM    dbo.Mes_OtherInHead h
                                    LEFT JOIN dbo.Mes_OtherInDetail d ON (h.O_OtherInNo = d.O_OtherInNo)
                            WHERE   h.O_OtherInNo='{0}' and h.O_Status!=1";
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
                                    h.M_CreateDate,
                                    h.M_CreateBy,
                                    h.M_Remark,
                                    h.M_CreateBy ,
                                    d.M_GoodsCode ,
                                    d.M_GoodsName ,
                                    d.M_Kind,
                                    d.M_Unit,
                                    d.M_Qty,
                                    d.M_Batch
                            FROM    dbo.Mes_MaterInHead h
                                    LEFT JOIN dbo.Mes_MaterInDetail d ON ( h.M_MaterInNo = d.M_MaterInNo )
                            WHERE   h.M_MaterInNo='{0}' and h.M_Status=2";
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
                                    h.R_CreateDate,
                                    h.R_CreateBy,
                                    h.R_Remark,
                                    h.R_StockName,
                                    h.R_StockToName,
                                    d.R_GoodsCode ,
                                    d.R_GoodsName ,
                                    d.R_Unit ,
                                    d.R_Qty ,
                                    d.R_Batch 
                            FROM    dbo.Mes_RequistHead h
                                    LEFT JOIN dbo.Mes_RequistDetail d ON h.R_RequistNo = d.R_RequistNo
                            WHERE   h.R_RequistNo='{0}' and h.R_Status=2";
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
                                    h.P_CreateDate ,
                                    h.P_CreateBy,
                                    h.P_Remark,
                                    h.P_OrderStationName,
                                    d.P_GoodsCode ,
                                    d.P_GoodsName ,
                                    d.P_Qty ,
                                    d.P_Unit 
                            FROM    dbo.Mes_ProductOrderHead h
                                    LEFT JOIN dbo.Mes_ProductOrderDetail d ON h.P_OrderNo = d.P_OrderNo
                            WHERE   h.P_OrderNo='{0}' and h.P_Status=2";
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
                                h.O_CreateDate,
                                h.O_CreateBy,
                                h.O_Remark,
                                d.O_GoodsCode ,
                                d.O_GoodsName ,
                                d.O_Unit ,
                                d.O_Qty ,
                                d.O_Batch,
                                h.O_StockName,
	case when O_UploadDate is null then dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),O_UploadDate,112)) end O_Price,
								CONVERT(decimal(18,6),(case when O_UploadDate is null then dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),O_UploadDate,112)) end *d.O_Qty)) as aoumnt
                        FROM    dbo.Mes_OutWorkShopHead h
                                LEFT JOIN dbo.Mes_OutWorkShopDetail d ON h.O_OutNo = d.O_OutNo
                        WHERE   h.O_OutNo ='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(sql, doucno), "OrgRes"));
            return MyDbReportData.TextFromMultiSQL(QueryList);
        }
        /// <summary>
        /// 库存明细统计
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string KCMXTJ(string queryJson)
        {
                var strSql = new StringBuilder();
                var strSql2 = new StringBuilder();
                var strSql3 = new StringBuilder();
               
                var queryParam = queryJson.ToJObject();
                //虚拟参数
                var dp = new DynamicParameters(new { });
                DateTime StartTime = queryParam["StartTime"].ToDate();
                DateTime EndTime = queryParam["EndTime"].ToDate();
                string Time = StartTime.AddDays(-1).ToShortDateString();
                dp.Add("StartTime", StartTime, DbType.DateTime);
                dp.Add("EndTime", EndTime, DbType.DateTime);
                dp.Add("Time", Time, DbType.DateTime);
                if (!queryParam["S_Code"].IsEmpty())
                {
                    dp.Add("S_Code", queryParam["S_Code"].ToString(), DbType.String);
                }
                else
                {
                    dp.Add("S_Code", "", DbType.String);
                }
                if (!queryParam["G_Code"].IsEmpty())
                {
                    dp.Add("G_Code", queryParam["G_Code"].ToString(), DbType.String);
                }
                else
                {
                    dp.Add("G_Code", "", DbType.String);
                }
                //页面传的参数
                if (!queryParam["start"].IsEmpty())
                {
                    dp.Add("start", queryParam["start"].ToDate(), DbType.DateTime);
                    string time = queryParam["start"].ToDate().AddDays(-1).ToShortDateString();
                    dp.Add("time", time, DbType.DateTime);
                }
                if (!queryParam["end"].IsEmpty())
                {
                    dp.Add("end", queryParam["end"].ToDate(), DbType.DateTime);
                }
                if (!queryParam["g_codes"].IsEmpty())
                {
                    dp.Add("g_codes", queryParam["g_codes"].ToString(), DbType.String);
                }
                if (!queryParam["g_stockcode"].IsEmpty())
                {
                    dp.Add("g_stockcode", queryParam["g_stockcode"].ToString(), DbType.String);
                }
                strSql.Append(@"
                           SELECT RH.M_CreateDate F_CreateDate
");
                if (!queryParam["S_Code"].IsEmpty() && !queryParam["G_Code"].IsEmpty() && !string.IsNullOrWhiteSpace(Time))
                {
                    strSql.Append(@"
                      ,@StartTime as starttime,
                      @EndTime endtime
                     ");
                }
                if (queryParam["S_Code"].IsEmpty() && queryParam["G_Code"].IsEmpty() && !queryParam["g_stockcode"].IsEmpty() && !queryParam["g_codes"].IsEmpty())
                {
                    strSql.Append(@"
                      ,@start as starttime,
                       @end as endtime
                     ");
                }
                strSql.Append(@"
                           ,RH.M_MaterInNo F_OrderNo,
                           RD.M_GoodsCode F_GoodsCode,
                           RD.M_GoodsName F_GoodsName,
                           RD.M_Unit F_Unit,
                           RD.M_Price,
                          dbo.GetPrice(RD.M_GoodsCode,datepart(year,RH.M_UploadDate)*100+datepart(month,RH.M_UploadDate)) as G_Price,
                           RD.M_Price*SUM(RD.M_Qty) AS Aoumount,
                           RD.M_TaxPrice F_InPrice,
						   '从【'+ RD.M_SupplyName+'】购进【'+RD.M_GoodsName+'】制单:'+ dbo.GetUserNameById(RH.M_CreateBy) as F_Remark,
                           'R' F_Status,
                           SUM(RD.M_Qty) F_InQty,
                           SUM(RD.M_Qty)*dbo.GetPrice(RD.M_GoodsCode,RIGHT('00'+CAST(MONTH(RH.M_UploadDate) AS VARCHAR(2)),2)) as SRJE
                    FROM Mes_MaterInHead RH
                        LEFT JOIN Mes_MaterInDetail RD
                            ON RH.M_MaterInNo = RD.M_MaterInNo               
                        LEFT JOIN Mes_Goods G
                            ON G.G_Code = RD.M_GoodsCode  Where 1=1              
                             ");
                if (!queryParam["S_Code"].IsEmpty() && !queryParam["G_Code"].IsEmpty() && !string.IsNullOrWhiteSpace(Time))
                {
                    strSql.Append(@"
                              AND RD.M_GoodsCode =@G_Code
                              AND RH.M_Status = 3
                              AND RD.M_Kind = 1
                              AND RD.M_StockCode=@S_Code
		                      AND (RH.M_CreateDate>=@StartTime and RH.M_CreateDate<=@EndTime)
                     ");
                }
                if (queryParam["S_Code"].IsEmpty() && queryParam["G_Code"].IsEmpty() && !queryParam["g_stockcode"].IsEmpty() && !queryParam["g_codes"].IsEmpty())
                {
                    strSql.Append(@"
                              AND RD.M_GoodsCode =@g_codes
                              AND RH.M_Status = 3
                              AND RD.M_Kind = 1
                              AND RD.M_StockCode=@g_stockcode
		                      AND (RH.M_CreateDate>=@start and RH.M_CreateDate<=@end)
                     ");
                }
                strSql.Append(@"
                         GROUP BY RH.M_CreateDate,
                                RH.M_MaterInNo,
                                RD.M_GoodsCode,
                                RD.M_GoodsName,
                                M_TaxPrice,
                                RH.M_UploadDate,
                                RD.M_SupplyName,
						        RH.M_CreateBy,
						        RD.M_Unit,
                                RD.M_Price
              ");

                strSql2.Append(@"
                      SELECT CH.C_CreateDate F_CreateDate"
                    );
                if (!queryParam["S_Code"].IsEmpty() && !queryParam["G_Code"].IsEmpty() && !string.IsNullOrWhiteSpace(Time))
                {
                    strSql2.Append(@"
                      ,@StartTime as starttime,
                     @EndTime endtime
                     ");
                }
                if (queryParam["S_Code"].IsEmpty() && queryParam["G_Code"].IsEmpty() && !queryParam["g_stockcode"].IsEmpty() && !queryParam["g_codes"].IsEmpty())
                {
                    strSql2.Append(@"
                      ,@start as starttime,
                       @end as endtime
                     ");
                }
                strSql2.Append(@"
                           ,CH.C_CollarNo F_OrderNo,
                           CD.C_GoodsCode F_GoodsCode,
                           CD.C_GoodsName F_GoodsName,
                          dbo.GetPrice(CD.C_GoodsCode,datepart(year,CH.M_UploadDate)*100+datepart(month,CH.M_UploadDate)) as G_Price,
                           CD.C_Unit F_Unit,
                           CD.C_Price F_OutPrice,
                           'C' F_Status,
                         '从【'+  CD.C_StockName+'】调拨【'+ CH.C_StockToName+'】制单:'+ dbo.GetUserNameById(CH.C_CreateBy) as F_Remark,
                           SUM(CD.C_Qty) F_OutQty,
                            SUM(CD.C_Qty)*dbo.GetPrice(CD.C_GoodsCode,RIGHT('00'+CAST(MONTH(CH.M_UploadDate) AS VARCHAR(2)),2)) as FCJE
                    FROM Mes_CollarHead CH
                        LEFT JOIN Mes_CollarDetail CD
                            ON CD.C_CollarNo = CH.C_CollarNo
                        LEFT JOIN Mes_Goods G     
                        ON G.G_Code = CD.C_GoodsCode        
                             ");
                if (!queryParam["S_Code"].IsEmpty() && !queryParam["G_Code"].IsEmpty() && !string.IsNullOrWhiteSpace(Time))
                {

                    strSql2.Append(@"             
                    WHERE CD.C_GoodsCode =@G_Code
                            AND CH.P_Status = 3
			                AND CD.C_StockCode=@S_Code
		                    AND (CH.C_CreateDate>=@StartTime and CH.C_CreateDate<=@EndTime)
                 
                            ");
                }
                if (queryParam["S_Code"].IsEmpty() && queryParam["G_Code"].IsEmpty() && !queryParam["g_stockcode"].IsEmpty() && !queryParam["g_codes"].IsEmpty())
                {

                    strSql2.Append(@"
                            WHERE CD.C_GoodsCode =@g_codes
                            AND CH.P_Status = 3
			                AND CD.C_StockCode=@g_stockcode
		                    AND (CH.C_CreateDate>=@start and CH.C_CreateDate<=@end)
                     ");
                }
                strSql2.Append(@"
                             GROUP BY CH.C_CollarNo,
                             CH.C_CreateDate,
                             CD.C_GoodsCode,
                             CD.C_GoodsName,
                             CD.C_Price,
                            CH.M_UploadDate,
                             CD.C_StockName,
							 CH.C_StockToName,
							 CH.C_CreateBy,
					 	     CD.C_Unit
            ");
                strSql3.Append(@"
                select isnull(sum(I_Qty),0) as IntervoryQty
                         ");
                if (!queryParam["S_Code"].IsEmpty() && !queryParam["G_Code"].IsEmpty() && !string.IsNullOrWhiteSpace(Time))
                {
                    strSql3.Append(" ,@StartTime as starttime, @EndTime endtime,((select g.G_Price from Mes_Goods g where g.G_Code=@G_Code)*isnull(sum(I_Qty),0)) as JCJE  ,@Time as F_CreateDate");
                }
                if (queryParam["S_Code"].IsEmpty() && queryParam["G_Code"].IsEmpty() && !queryParam["g_stockcode"].IsEmpty() && !queryParam["g_codes"].IsEmpty())
                {

                    strSql3.Append(" ,@start as starttime,@end as endtime,((select g.G_Price from Mes_Goods g where g.G_Code=@g_codes)*isnull(sum(I_Qty),0)) as JCJE,@time as F_CreateDate");
                }
                strSql3.Append(@",'上期结存' as F_Remark 
                             from  Mes_InventoryLS s 
         	");
                if (!queryParam["S_Code"].IsEmpty() && !queryParam["G_Code"].IsEmpty() && !string.IsNullOrWhiteSpace(Time))
                {
                    strSql3.Append(@" where s.I_GoodsCode=@G_Code and s.I_Date=@Time and s.I_StockCode=@S_Code");
                }
                if (queryParam["S_Code"].IsEmpty() && queryParam["G_Code"].IsEmpty() && !queryParam["g_stockcode"].IsEmpty() && !queryParam["g_codes"].IsEmpty())
                {

                    strSql3.Append(@" where s.i_goodscode=@g_codes and s.i_date=@time and s.i_stockcode=@g_stockcode");
                }
                string asss = strSql.ToString();
                string assss = strSql2.ToString();
                string asssss = strSql3.ToString();
                RepositoryFactory a = new RepositoryFactory();             
                List<InventoryViewModel> datas = new List<InventoryViewModel>();  
                 var a1 = new RepositoryFactory().BaseRepository().FindList<InventoryViewModel>(strSql.ToString(), dp);
                 var a2 = new RepositoryFactory().BaseRepository().FindList<InventoryViewModel>(strSql2.ToString(), dp);
                 var a3 = new RepositoryFactory().BaseRepository().FindList<InventoryViewModel>(strSql3.ToString(), dp);

                if(!a1.IsEmpty())
                {
                    datas.AddRange(a1);
                }
                if (!a2.IsEmpty())
                {
                    datas.AddRange(a2);
                }
                if (!a3.IsEmpty())
                {
                    datas.AddRange(a3);
                }
                else
                {
                    datas.Add(new InventoryViewModel { IntervoryQty = 0, F_GoodsCode = "上期结存", F_CreateDate = DateTime.Parse(Time) });
                }

                var rows= datas.OrderBy(r=>r.F_CreateDate).ToList();

                decimal? qty = rows[0].IntervoryQty;

                for (int i = 1; i < rows.Count; i++)
                {
                    if (rows[i].F_Status == "R")
                    {
                        qty = rows[i].IntervoryQty = qty + rows[i].F_InQty;
                    }
                    else
                    {
                        qty = rows[i].IntervoryQty = qty - rows[i].F_OutQty;
                    }
                    rows[i].JCJE = Math.Round((rows[i].IntervoryQty * rows[i].G_Price).ToDecimal(), 6);
                    Convert.ToDateTime(rows[i].F_CreateDate);
                }
               //自定义日期格式
                IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd HH':'mm':'ss" };
                var json = JsonConvert.SerializeObject(new { PurchaseSummary = rows },timeConverter);
                return json;

        
        }
        /// <summary>
        /// 出成率查询
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string CCLCX(string queryJson)
        {
            var strSql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            //虚拟参数
            var dp = new DynamicParameters(new { });

            DateTime StartTime = queryParam["StartTime"].ToDate();
            DateTime EndTime = queryParam["EndTime"].ToDate();
            dp.Add("StartTime", StartTime, DbType.String);
            dp.Add("EndTime", StartTime, DbType.String);
            strSql.Append(@"WITH CTE AS 
                                        (SELECT @StartTime as starttime,
                                                @EndTime as endtime,
                                                O_GoodsCode ,
                                                O_GoodsName ,
                                                O_Unit,
                                                O_SecGoodsCode,
                                                O_SecGoodsName,
                                                O_SecUnit,
                                                dbo.GetUserNameById(O_CreateBy)O_CreateBy,
                                                SUM(O_Qty) O_Qty ,
                                                SUM(O_SecQty) O_SecQty ,
                                               (SUM(O_SecQty) / SUM(O_Qty)) * 100 ProductRate,
                                               (CAST(CONVERT(DECIMAL(3),C_Min) AS VARCHAR ) + '-'+CAST(CONVERT(DECIMAL(3),C_Max) AS VARCHAR)) targetRate,
                                                CONVERT(DECIMAL(3),C_Min) O_Min,
                                                CONVERT(DECIMAL(3),C_Max) O_Max,
                                                O_StockName,
                                                O_TeamName,
                                                P_ProName O_ProName
                                        FROM    Mes_OrgResHead h
                                                LEFT JOIN Mes_OrgResDetail d ON d.O_OrgResNo = h.O_OrgResNo
                                                LEFT JOIN dbo.Mes_Convert c ON c.C_Code =d.O_GoodsCode AND c.C_SecCode =d.O_SecGoodsCode
                                                LEFT JOIN dbo.Mes_Proce ON P_ProNo =O_ProCode
                                                WHERE O_Status = 3 ");

            if (!queryParam["StockCode"].IsEmpty())
            {
                dp.Add("StockCode", queryParam["StockCode"].ToString(), DbType.String);
                strSql.Append(" AND O_StockCode =@StockCode ");
            }
            if (!queryParam["ProCode"].IsEmpty())
            {
                dp.Add("ProCode", queryParam["ProCode"].ToString(), DbType.String);
                strSql.Append(" AND O_ProCode =@ProCode ");
            }

            if (!queryParam["O_SecGoodsName"].IsEmpty())
            {
                dp.Add("O_SecGoodsName", "%" + queryParam["O_SecGoodsName"].ToString() + "%", DbType.String);
                strSql.Append(" AND O_SecGoodsName like @O_SecGoodsName ");
            }
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                dp.Add("startTime", queryParam["StartTime"].ToString(), DbType.String);
                dp.Add("endTime", queryParam["EndTime"].ToString(), DbType.String);
                strSql.Append(" AND (O_UploadDate > @startTime AND O_UploadDate <@endTime ) ");
            }
            strSql.Append(@" GROUP BY O_GoodsName ,
                                      O_GoodsCode ,
                                      O_Unit,
                                      P_ProName,
                                      O_SecGoodsCode,
                                      O_SecGoodsName,
                                      O_SecUnit,
                                      O_StockName,
                                      O_TeamName,
                                      C_Max,
                                      C_Min,
                                      O_CreateBy) 
                               SELECT *,(CASE WHEN CTE.ProductRate >CTE.O_Max THEN CTE.ProductRate-CTE.O_MAX
                                                      WHEN CTE.ProductRate <CTE.O_MIN THEN CTE.ProductRate-CTE.O_Min 
                                                      WHEN CTE.ProductRate>=CTE.O_MIN AND CTE.ProductRate<=CTE.O_Max THEN 0
                                                      ELSE 0 END )DIFF FROM CTE");
            var dt =  new RepositoryFactory().BaseRepository().FindList<ProductRateView>(strSql.ToString(), dp);
            var json = JsonConvert.SerializeObject(new { PurchaseSummary = dt });
            return json;
        }
        /// <summary>
        /// 出成率报表-按原物料
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string CCLBB(string queryJson)
        {
            string messsage = "";
            Ayma.Application.TwoDevelopment.MesDev.PickingMaterService picking = new Ayma.Application.TwoDevelopment.MesDev.PickingMaterService();
            DataTable dt = picking.GetProductReportData(queryJson, out messsage);
            var queryParam = queryJson.ToJObject();
            dt.Columns.Add("starttime", typeof(string));
            dt.Columns.Add("endtime", typeof(string));
            foreach (DataRow dr in dt.Rows)
            {
                //为新添加的列进行赋值
                dr["starttime"] = queryParam["StartTime"].ToString(); ;
                dr["endtime"] = queryParam["EndTime"].ToString();
            }
            if (string.IsNullOrEmpty(messsage))
            {
                #region 添加合计、统计行
                if (dt != null && dt.Rows.Count > 0)
                {
                    //插入统计行
                    if (true)
                    {
                        string current = dt.Rows[0]["F_CreateDate"].ToString();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string last = dt.Rows[i]["F_CreateDate"].ToString();
                            if (current != last)
                            {
                                DataRow dr = dt.NewRow();
                                dr["F_CreateDate"] = "[" + current + "]合计";
                                dt.Rows.InsertAt(dr, i);

                                current = last;
                                i++;
                            }
                        }
                        DataRow drEnd = dt.NewRow();
                        drEnd["F_CreateDate"] = "[" + current + "]合计";
                        dt.Rows.InsertAt(drEnd, dt.Rows.Count);

                        DataRow drSum = dt.NewRow();
                        drSum["F_CreateDate"] = "总计";
                        dt.Rows.InsertAt(drSum, dt.Rows.Count);
                    }

                    //计算统计行
                    if (true)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            //统计数量
                            if (dt.Columns[j].ColumnName.Contains("F_GoodsQty"))
                            {

                                decimal everysum_qty = 0;
                                decimal totalsum_qty = 0;
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    string current = dt.Rows[i]["F_CreateDate"].ToString();
                                    if (current.Contains("合计"))
                                    {
                                        dt.Rows[i][j] = Math.Round(everysum_qty, 2);
                                        everysum_qty = 0;
                                    }
                                    else
                                    {
                                        if (current == "总计")
                                        {
                                            dt.Rows[i][j] = Math.Round(totalsum_qty, 2);
                                            everysum_qty = 0;
                                        }
                                        else
                                        {
                                            if (dt.Rows[i][j] == DBNull.Value)
                                            {
                                                everysum_qty += 0;
                                                totalsum_qty += 0;
                                            }
                                            else
                                            {
                                                everysum_qty += decimal.Parse(dt.Rows[i][j].ToString());
                                                totalsum_qty += decimal.Parse(dt.Rows[i][j].ToString());
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                #endregion
            }

            var json = JsonConvert.SerializeObject(new { PurchaseSummary = dt });
            return json;
        }
        /// <summary>
        /// 供应商存货分类汇总
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string GYSCHFLHZ(string queryJson)
        {
            Ayma.Application.TwoDevelopment.MesDev.MaterInBillService mater = new Ayma.Application.TwoDevelopment.MesDev.MaterInBillService();
            var dt = mater.GetMaterInSum(queryJson);
            var queryParam = queryJson.ToJObject();
            dt.Columns.Add("starttime", typeof(string));
            dt.Columns.Add("endtime", typeof(string));
            foreach (DataRow dr in dt.Rows)
            {
                //为新添加的列进行赋值
                dr["starttime"] = queryParam["StartTime"].ToString(); ;
                dr["endtime"] = queryParam["EndTime"].ToString();
            }
            var json = JsonConvert.SerializeObject(new { PurchaseSummary = dt });
            return json;
        }
        /// <summary>
        /// 车间入库到线边仓
        /// </summary>
        /// <param name="doucno"></param>
        /// <returns></returns>
        public static string InWorkShop(string doucno)
        {
            var sql = @"SELECT  h.I_InNo ,
                               h.I_CreateDate,
                               h.I_CreateBy,
                               h.I_Remark,
                                d.I_GoodsCode ,
                                d.I_GoodsName ,
                                d.I_Unit ,
                                d.I_Qty ,
                                d.I_Batch,
                                h.I_StockName,
	                            case when I_UploadDate is null then dbo.GetPrice(d.I_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.I_GoodsCode,CONVERT(VARCHAR(6),I_UploadDate,112)) end I_Price,
								CONVERT(decimal(18,6),(case when I_UploadDate is null then dbo.GetPrice(d.I_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.I_GoodsCode,CONVERT(VARCHAR(6),I_UploadDate,112)) end *d.I_Qty)) as aoumnt
                        FROM    dbo.Mes_InWorkShopHead h
                                LEFT JOIN dbo.Mes_InWorkShopDetail d ON h.I_InNo = d.I_InNo
                        WHERE   h.I_InNo ='{0}' and h.I_Status!=1";
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
                                h.P_CreateDate,
                                h.P_CreateBy,
                                h.P_Remark,
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
                                d.O_Qty,
                                d.O_Unit,
		                        h.O_Remark,
		                        d.O_Batch,
		                       case when O_UploadDate is null then dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),O_UploadDate,112)) end O_Price,
								CONVERT(decimal(18,6),(case when O_UploadDate is null then dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),GETDATE(),112)) else dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),O_UploadDate,112)) end *d.O_Qty)) as aoumnt
                        FROM    dbo.Mes_OtherOutHead h
                                LEFT JOIN dbo.Mes_OtherOutDetail d ON h.O_OtherOutNo = d.O_OtherOutNo
                        WHERE    h.O_OtherOutNo ='{0}' and h.O_Status!=1";
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
            SpecialDataFunMap.Add("KCMXTJ", KCMXTJ);
            SpecialDataFunMap.Add("CCLCX", CCLCX);
            SpecialDataFunMap.Add("CCLBB", CCLBB);
            SpecialDataFunMap.Add("GYSCHFLHZ", GYSCHFLHZ);
            SpecialDataFunMap.Add("OutWorkShop", OutWorkShop);
            SpecialDataFunMap.Add("InWorkShop", InWorkShop);
            SpecialDataFunMap.Add("ProOutMake", ProOutMake);
            SpecialDataFunMap.Add("BackSupply", BackSupply);
            SpecialDataFunMap.Add("MaterIn", MaterIn);
            SpecialDataFunMap.Add("GYSCHMX", GYSCHMX);
            SpecialDataFunMap.Add("GYSJHSJHZ", GYSJHSJHZ);
            SpecialDataFunMap.Add("QTCKHZBB", QTCKHZBB);
            SpecialDataFunMap.Add("LLCKHZBB", LLCKHZBB);
            SpecialDataFunMap.Add("YWLCRKTJ", YWLCRKTJ);
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
        private static string YWLCRKTJ(HttpRequest Request)
        {
            return YWLCRKTJ(Request.QueryString["starttime"], Request.QueryString["endtime"], Request.QueryString["ToDate"], Request.QueryString["S_Name"], Request.QueryString["M_GoodsName"]);
        }
        private static string KCMXTJ(HttpRequest Request)
        {
            return KCMXTJ(Request.QueryString["queryJson"]);
        }
        private static string CCLCX(HttpRequest Request)
        {
            return CCLCX(Request.QueryString["queryJson"]);
        }
        private static string CCLBB(HttpRequest Request)
        {
            return CCLBB(Request.QueryString["queryJson"]);
        }
        private static string GYSCHFLHZ(HttpRequest Request)
        {
            return GYSCHFLHZ(Request.QueryString["queryJson"]);
        }
        private static string GYSCHMX(HttpRequest Request)
        {
            return GYSCHMX(Request.QueryString["starttime"], Request.QueryString["endtime"]);
        }
        private static string QTCKHZBB(HttpRequest Request)
        {
            return QTCKHZBB(Request.QueryString["starttime"], Request.QueryString["endtime"], Request.QueryString["GoodsCode"], Request.QueryString["StockCode"]);
        }
        private static string LLCKHZBB(HttpRequest Request)
        {
            return LLCKHZBB(Request.QueryString["starttime"], Request.QueryString["endtime"], Request.QueryString["GoodsCode"], Request.QueryString["StockCode"]);
        }
        private static string GYSJHSJHZ(HttpRequest Request)
        {
            return GYSJHSJHZ(Request.QueryString["starttime"], Request.QueryString["endtime"], Request.QueryString["M_SupplyName"]);
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