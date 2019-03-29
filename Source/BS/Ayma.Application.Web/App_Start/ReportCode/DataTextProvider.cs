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
                    t.P_OrderNo ,
                    t.P_OrderDate ,
                    t.C_CreateBy,
                    d.C_Unit,
                    d.C_Qty,
                    d.C_GoodsCode,
                    d.C_GoodsName
            FROM    Mes_CollarHead t
                    LEFT JOIN dbo.Mes_CollarDetail d ON t.C_CollarNo = d.C_CollarNo
            WHERE   t.P_Status = 2 AND t.C_CollarNo ='{0}'
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
                            WHERE h.S_Status =2 AND h.S_ScrapNo ='{0}'"; 
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
                            WHERE   B_Status = 2 AND  h.B_BackStockNo ='{0}'";
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
                        WHERE   O_Status = 2 AND h.O_OutNo ='{0}'";
            ArrayList QueryList = new ArrayList();
            QueryList.Add(new ReportQueryItem(string.Format(sql, doucno), "OrgRes"));
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

        private static Dictionary<string, SimpleDataFun> SimpleDataFunMap = new Dictionary<string, SimpleDataFun>();

        //有参数报表数据的名称与函数映射表，参数来自 HttpRequest
        private delegate string SpecialDataFun(HttpRequest Request);

        private static Dictionary<string, SpecialDataFun> SpecialDataFunMap = new Dictionary<string, SpecialDataFun>();

        public static string BuildByHttpRequest(HttpRequest Request)
        {
            string DataText;
            string DataName = Request.QueryString["data"];

            Trace.Assert(SimpleDataFunMap.Count > 0, "DataFunMap isn't initialized!");

            if (DataName != null) //if (DataName != "")
            {
                //根据数据名称查找映射表，如果找到，执行对应的报表数据函数获取数据
                SimpleDataFun simpleFun;
                SpecialDataFun specialFun;
                if (SimpleDataFunMap.TryGetValue(DataName, out simpleFun))
                {
                    DataText = simpleFun();
                }
                else if (SpecialDataFunMap.TryGetValue(DataName, out specialFun))
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
            Trace.Assert(SimpleDataFunMap.Count <= 0, "DataFunMap already initialized!");

            #region 业务
           
            SpecialDataFunMap.Add("Test", Test);
            SpecialDataFunMap.Add("Picking", Picking);
            SpecialDataFunMap.Add("Scrap", Scrap);
            SpecialDataFunMap.Add("BackStock",BackStock);
            SpecialDataFunMap.Add("OrgRes", OrgRes);
            SpecialDataFunMap.Add("OutWorkShop", OutWorkShop);
            SpecialDataFunMap.Add("BackSupply", BackSupply);
            #endregion
        }

        private static string OutWorkShop(HttpRequest Request)
        {
            return OutWorkShop(Request.QueryString["doucno"]);
        }

        private static string BackSupply(HttpRequest Request)
        {
            return BackSupply(Request.QueryString["doucno"]);
        }

        private static string OrgRes(HttpRequest Request)
        {
            return OrgRes(Request.QueryString["doucno"]);
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