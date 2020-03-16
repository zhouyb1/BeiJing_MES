using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using System;
using Ayma.Application.TwoDevelopment.MesDev.MaterialsSum.ViewModel;
using System.Data;
using System.ComponentModel;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-09-16 10:59
    /// 描 述：原物料统计(入库、出库、次品)
    /// </summary>
    public partial class MaterialsSumController : MvcControllerBase
    {
        private MaterialsSumIBLL materialsSumIBLL = new MaterialsSumBLL();

        #region 视图功能

        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
             return View();
        }
        /// <summary>
        /// 库存明细页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult  InventoryDetail()
        {
            return View();
        }
        /// <summary>
        /// 打印页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintReport()
        {
            return View();
        }
        /// <summary>
        /// 打印页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintReport2()
        {
            return View();
        }
        /// <summary>
        /// 报表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MoneyIndex()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取库存明细表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetInventoryDetail(string pagination, string queryJson)
        {
            var queryParam = queryJson.ToJObject();
             List<InventoryViewModel> data ;
            Pagination paginationobj = pagination.ToObject<Pagination>();
            if ((!queryParam["S_Code"].IsEmpty() && !queryParam["G_Code"].IsEmpty()) || !queryParam["g_stockcode"].IsEmpty())
            {   
                data = materialsSumIBLL.GetInventoryDetail(paginationobj, queryJson); 
            }
            else
            {
                return Fail("请选择库存商品!");
            }
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };  
            return Success(jsonData);
        }
        /// <summary>
        /// 获取选取的时间原物料入库详细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterialDetailListByDate(string pagination, string queryJson, string M_GoodsCode)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materialsSumIBLL.GetMaterialDetailListByDate(paginationobj, queryJson, M_GoodsCode);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取选取的时间原物料出库详细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterialOutDetailListByDate(string pagination, string queryJson, string M_GoodsCode)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materialsSumIBLL.GetMaterialOutDetailListByDate(paginationobj, queryJson, M_GoodsCode);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取选取的时间原物料退库详细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterialBackDetailListByDate(string pagination, string queryJson, string M_GoodsCode)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materialsSumIBLL.GetMaterialBackDetailListByDate(paginationobj, queryJson, M_GoodsCode);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取选取的时间原物料销售详细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterialSaleDetailListByDate(string pagination, string queryJson, string M_GoodsCode)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materialsSumIBLL.GetMaterialSaleDetailListByDate(paginationobj, queryJson, M_GoodsCode);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取选取的时间原物料报废详细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterialScrapDetailListByDate(string pagination, string queryJson, string M_GoodsCode)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materialsSumIBLL.GetMaterialScrapDetailListByDate(paginationobj, queryJson, M_GoodsCode);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取选取的时间原物料其他入库详细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterialOtherDetailListByDate(string pagination, string queryJson, string M_GoodsCode)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materialsSumIBLL.GetMaterialOtherDetailListByDate(paginationobj, queryJson, M_GoodsCode);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取选取的时间原物料其他出库详细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterialOtherOutDetailListByDate(string pagination, string queryJson, string M_GoodsCode)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materialsSumIBLL.GetMaterialOtherOutDetailListByDate(paginationobj, queryJson, M_GoodsCode);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取选取的时间原物料退供应商详细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterialBackSupplyDetailListByDate(string pagination, string queryJson, string M_GoodsCode)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materialsSumIBLL.GetMaterialBackSupplyDetailListByDate(paginationobj, queryJson, M_GoodsCode);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取期初期末页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterialSumListByDate(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materialsSumIBLL.GetMaterialSumListByDate(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterialSumList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materialsSumIBLL.GetMaterialSumList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取明细tab页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterialDetailList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materialsSumIBLL.GetMes_MaterInDetailList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取Export表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public FileResult Export2(Pagination pagination,string queryJson)
        {

            pagination.page = 1;
            pagination.records = 0;
            pagination.rows = 999999;   
            pagination.sidx = "";
            pagination.sord = "ASC";
              var datas = materialsSumIBLL.GetInventoryDetail(pagination,queryJson);
            var dt = AsDataTable(datas);
            var queryParam = queryJson.ToJObject();
            var StartTime = "";
            var EndTime = "";
            if (!queryParam["start"].ToString().IsEmpty() && !queryParam["end"].ToString().IsEmpty())
            {
                StartTime = queryParam["start"].ToString();
                EndTime = queryParam["end"].ToString();
            }
            else
            {
                StartTime = queryParam["StartTime"].ToString();
                EndTime = queryParam["EndTime"].ToString();
            
            }
            DateTime StartTimes = queryParam["StartTime"].ToDate();
            DateTime EndTimes = queryParam["EndTime"].ToDate();
            string starttime = StartTimes.ToString("yyyyMMdd");
            string endtime = EndTimes.ToString("yyyyMMdd");
            DateTime Start = queryParam["start"].ToDate();
            DateTime End = queryParam["end"].ToDate();
            string start = Start.ToString("yyyyMMdd");
            string end = End.ToString("yyyyMMdd");
            var ms = NPOIExcel.ToExcelMoreheader(dt, "库存明细统计", "库存明细统计", StartTime, EndTime);

            if (!queryParam["start"].ToString().IsEmpty() && !queryParam["end"].ToString().IsEmpty())
            {
                if (start == end)
                {
                    return File(ms.GetBuffer(), "application/vnd.ms-excel", start + "_库存明细统计.xls");
                }
                else
                {
                    return File(ms.GetBuffer(), "application/vnd.ms-excel", start + "-" + end + "_库存明细统计.xls");
                }
            }
            else
            {
                if (starttime == endtime)
                {
                    return File(ms.GetBuffer(), "application/vnd.ms-excel", starttime + "_库存明细统计.xls");
                }
                else
                {
                    return File(ms.GetBuffer(), "application/vnd.ms-excel", starttime + "-" + endtime + "_库存明细统计.xls");
                }

            }

           
        }
        /// <summary>
        /// 获取导出Excel数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable AsDataTable(IEnumerable<InventoryViewModel> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(InventoryViewModel));
            var table = new DataTable();
            //定义列名
            foreach (PropertyDescriptor prop in properties)
            {
                switch (prop.Name)
                {
                    case "rownum": table.Columns.Add("序号", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
                    case "F_CreateDate": table.Columns.Add("日期", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
                    case "F_Remark": table.Columns.Add("摘要", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
                    case "F_GoodsCode": table.Columns.Add("商品编码", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
                    case "F_GoodsName": table.Columns.Add("商品名称", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
                    case "F_Unit": table.Columns.Add("单位", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
                    case "F_OrderNo": table.Columns.Add("单据编号", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
                    case "F_InQty": table.Columns.Add("收入_数量", typeof(string)); break;
                    case "G_Price": table.Columns.Add("收入_单位成本(元)", typeof(string)); break;
                    case "SRJE": table.Columns.Add("收入_金额", typeof(string)); break;
                    case "Aoumount": table.Columns.Add("收入_无税金额", typeof(string)); break;
                    case "F_OutQty": table.Columns.Add("发出_数量", typeof(string)); break;
                    case "G_Price1": table.Columns.Add("发出_单位成本(元)", typeof(string)); break;
                    case "FCJE": table.Columns.Add("发出_金额", typeof(string)); break;
                    case "IntervoryQty": table.Columns.Add("结存_数量", typeof(string)); break;
                    case "G_Price2": table.Columns.Add("结存_单位成本(元)", typeof(string)); break;
                    case "JCJE": table.Columns.Add("结存_金额", typeof(string)); break;
                    default: break;
                }
            }
            //表格列名排序
            table.Columns["序号"].SetOrdinal(0);
            table.Columns["日期"].SetOrdinal(1 );
            table.Columns["摘要"].SetOrdinal(2);
            table.Columns["商品编码"].SetOrdinal(3);
            table.Columns["商品名称"].SetOrdinal(4);
            table.Columns["单位"].SetOrdinal(5);
            table.Columns["单据编号"].SetOrdinal(6);
            table.Columns["收入_数量"].SetOrdinal(7);
            table.Columns["收入_单位成本(元)"].SetOrdinal(8);
            table.Columns["收入_金额"].SetOrdinal(9);
            table.Columns["收入_无税金额"].SetOrdinal(10);
            table.Columns["发出_数量"].SetOrdinal(11);
            table.Columns["发出_单位成本(元)"].SetOrdinal(12);
            table.Columns["发出_金额"].SetOrdinal(13);
            table.Columns["结存_数量"].SetOrdinal(14);
            table.Columns["结存_单位成本(元)"].SetOrdinal(15);
            table.Columns["结存_金额"].SetOrdinal(16);
            //给数据
            foreach (var item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    switch (prop.Name)
                    {
                        case "F_CreateDate": row["日期"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "F_Remark": row["摘要"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "F_GoodsCode": row["商品编码"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "F_GoodsName": row["商品名称"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "F_Unit": row["单位"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "F_OrderNo": row["单据编号"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "F_InQty": row["收入_数量"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "G_Price": row["收入_单位成本(元)"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "SRJE": row["收入_金额"] = Math.Round((item.F_InQty * item.G_Price).ToDecimal(), 6); break;
                        case "Aoumount": row["收入_无税金额"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "F_OutQty": row["发出_数量"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "G_Price1": row["发出_单位成本(元)"] = item.G_Price; break;
                        case "FCJE": row["发出_金额"] = Math.Round((item.F_OutQty * item.G_Price).ToDecimal(), 6); break;
                        case "IntervoryQty": row["结存_数量"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "G_Price2": row["结存_单位成本(元)"] = item.G_Price; break;
                        case "JCJE": row["结存_金额"] = Math.Round((item.IntervoryQty * item.G_Price).ToDecimal(), 6); break;
                    }
                table.Rows.Add(row);
                for (int i = 0; i < table.Rows.Count; i++)
                {

                    table.Rows[i]["序号"] = i + 1;
                }
            }
            return table;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 获取Export表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public FileResult Export(Pagination pagination,string queryJson)
        {

            pagination.page = 1;
            pagination.records = 0;
            pagination.rows = 99999999;
            pagination.sidx = "g_code";
            pagination.sord = "desc";
            DataTable dt =materialsSumIBLL.GetMaterialSumListByDate(pagination,queryJson);
            dt.Columns["rownum"].ColumnName = "序号";
            dt.Columns["g_code"].ColumnName = "商品编码";
            dt.Columns["g_name"].ColumnName = "商品名称";
            dt.Columns["s_name"].ColumnName = "仓库名称";
            dt.Columns["g_stockcode"].ColumnName = "仓库编码";
            dt.Columns["g_unit"].ColumnName = "单位";
            dt.Columns["price"].ColumnName = "加权平均价(元)";
            dt.Columns["inventoryquantity"].ColumnName = "入库数量";
            dt.Columns["delivery"].ColumnName = "出库数量";
            dt.Columns["initialinventory"].ColumnName = "期初库存_数量";
            dt.Columns["initialamount"].ColumnName = "期初库存_金额";
            dt.Columns["endinginventory"].ColumnName = "期末库存_数量";
            dt.Columns["finalamount"].ColumnName = "期末库存_金额";
            dt.Columns["back_qty"].ColumnName = "次品退库_数量";
            dt.Columns["withdrawingnumber"].ColumnName = "退回仓库_数量";
            dt.Columns["materialssales"].ColumnName = "原物料销售_数量";
            dt.Columns["outprice"].ColumnName = "原物料销售_单价";
            dt.Columns["outamount"].ColumnName = "原物料销售_金额";
            dt.Columns["scrapist"].ColumnName = "报废物料_数量";
            dt.Columns["otherwarehouse"].ColumnName = "其它入库_数量";
            dt.Columns["otheroutbound"].ColumnName = "其它出库_数量";
            dt.Columns["supplierback"].ColumnName = "退供应商_数量";
            dt.Columns.Remove("startTime");
            dt.Columns.Remove("endTime");
            //表格列名排序
            dt.Columns["序号"].SetOrdinal(0);
            dt.Columns["商品编码"].SetOrdinal(1);
            dt.Columns["商品名称"].SetOrdinal(2);
            dt.Columns["仓库名称"].SetOrdinal(3);
            dt.Columns["仓库编码"].SetOrdinal(4);
            dt.Columns["单位"].SetOrdinal(5);
            dt.Columns["加权平均价(元)"].SetOrdinal(6);
            dt.Columns["入库数量"].SetOrdinal(7);
            dt.Columns["出库数量"].SetOrdinal(8);
            dt.Columns["期初库存_数量"].SetOrdinal(9);
            dt.Columns["期初库存_金额"].SetOrdinal(10);
            dt.Columns["期末库存_数量"].SetOrdinal(11);
            dt.Columns["期末库存_金额"].SetOrdinal(12);
            dt.Columns["次品退库_数量"].SetOrdinal(13);
            dt.Columns["退回仓库_数量"].SetOrdinal(14);
            dt.Columns["原物料销售_数量"].SetOrdinal(15);
            dt.Columns["原物料销售_单价"].SetOrdinal(16);
            dt.Columns["原物料销售_金额"].SetOrdinal(17);
            dt.Columns["报废物料_数量"].SetOrdinal(18);
            dt.Columns["其它入库_数量"].SetOrdinal(19);
            dt.Columns["其它出库_数量"].SetOrdinal(20);
            dt.Columns["退供应商_数量"].SetOrdinal(21);
            //给数据     
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["原物料销售_单价"].ToString() == null || dt.Rows[i]["原物料销售_单价"].ToString()=="")
                    {
                        dt.Rows[i]["原物料销售_单价"] = 0;
                    }
                    else
                    {
                        dt.Rows[i]["原物料销售_单价"] = dt.Rows[i]["原物料销售_单价"];
                    }
                    if (dt.Rows[i]["原物料销售_金额"].ToString() == null || dt.Rows[i]["原物料销售_金额"].ToString() == "")
                    {
                        dt.Rows[i]["原物料销售_金额"] = 0;
                    }
                    else
                    {
                        dt.Rows[i]["原物料销售_金额"] = dt.Rows[i]["原物料销售_金额"];
                    }
                }
              var queryParam = queryJson.ToJObject();
              DateTime StartTime = queryParam["StartTime"].ToDate();
              DateTime EndTime = queryParam["EndTime"].ToDate();
              string starttime = StartTime.ToString("yyyyMMdd");
              string endtime = EndTime.ToString("yyyyMMdd");
              var ms = NPOIExcel.ToExcelMoreheader(dt, "原物料出入库统计", "原物料出入库统计", queryParam["StartTime"].ToString(), queryParam["EndTime"].ToString());
              if (starttime == endtime)
              {
                  return File(ms.GetBuffer(), "application/vnd.ms-excel", starttime + "_原物料出入库统计.xls");
              }
              else
              {
                  return File(ms.GetBuffer(), "application/vnd.ms-excel", starttime + "-" + endtime + "_原物料出入库统计.xls");
              }
        }
        
        #endregion

    }
}
