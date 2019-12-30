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
        ///// <summary>
        ///// 获取Export表数据
        ///// <summary>
        ///// <param name="queryJson">查询参数</param>
        ///// <returns></returns>
        //public FileResult Export(Pagination pagination,string queryJson)
        //{


        //    var datas = materialsSumIBLL.GetInventoryDetail(pagination,queryJson);
        //    var dt = AsDataTable(datas);
        //    var ms = NPOIExcel.ToExcel(dt, "销售分析表", "销售分析表");
        //    return File(ms.GetBuffer(), "application/vnd.ms-excel", "销售分析.xls");
        //}
        ///// <summary>
        ///// 获取导出Excel数据
        ///// <summary>
        ///// <param name="queryJson">查询参数</param>
        ///// <returns></returns>
        //public DataTable AsDataTable(IEnumerable<InventoryViewModel> data)
        //{
        //    PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(InventoryViewModel));
        //    var table = new DataTable();
        //    //定义列名
        //    foreach (PropertyDescriptor prop in properties)
        //    {
        //        switch (prop.Name)
        //        {
        //            case "F_CreateDate": table.Columns.Add("日期", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
        //            case "F_Remark": table.Columns.Add("摘要", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
        //            case "F_GoodsCode": table.Columns.Add("商品编码", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
        //            case "F_GoodsName": table.Columns.Add("商品名称", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
        //            case "F_Unit": table.Columns.Add("单位", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
        //            case "F_OrderNo": table.Columns.Add("单据编号", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
        //            case "F_InQty": table.Columns.Add("收入(数量，含税价格(元)，金额)", typeof(string)); break;
        //            case "F_OutQty": table.Columns.Add("发出(数量，加权平均价(元)，金额)", typeof(string)); break;
        //            case "IntervoryQty": table.Columns.Add("结存(数量，加权平均价(元)，金额)", typeof(string)); break;
        //            default: break;
        //        }
        //    }
        //    //表格列名排序
        //    table.Columns["日期"].SetOrdinal(0);
        //    table.Columns["摘要"].SetOrdinal(1);
        //    table.Columns["商品编码"].SetOrdinal(2);
        //    table.Columns["商品名称"].SetOrdinal(3);
        //    table.Columns["单位"].SetOrdinal(4);
        //    table.Columns["收入(数量，含税价格(元)，金额)"].SetOrdinal(5);
        //    table.Columns["发出(数量，加权平均价(元)，金额)"].SetOrdinal(6);
        //    table.Columns["结存(数量，加权平均价(元)，金额)"].SetOrdinal(7);
        //    //给数据
        //    foreach (var item in data)
        //    {
        //        DataRow row = table.NewRow();
        //        foreach (PropertyDescriptor prop in properties)
        //            switch (prop.Name)
        //            {
        //                case "F_CreateDate": row["日期"] = prop.GetValue(item) ?? DBNull.Value; break;
        //                case "F_Remark": row["摘要"] = prop.GetValue(item) ?? DBNull.Value; break;
        //                case "F_GoodsCode": row["商品编码"] = prop.GetValue(item) ?? DBNull.Value; break;
        //                case "F_GoodsName": row["商品名称"] = prop.GetValue(item) ?? DBNull.Value; break;
        //                case "F_Unit": row["单位"] = prop.GetValue(item) ?? DBNull.Value; break;
        //                case "F_OrderNo": row["单据编号"] = prop.GetValue(item) ?? DBNull.Value; break;
        //                case "F_InQty": row["收入(数量，含税价格(元)，金额)"] = item.F_InQty + "," + item.F_InPrice; break;
        //                case "F_OutQty": row["发出(数量，加权平均价(元)，金额)"] = item.F_OutQty + "," + item.F_OutPrice; break;
        //                case "IntervoryQty": row["结存(数量，加权平均价(元)，金额)"] = item.IntervoryQty + "," + item.G_Price; break;
        //            }
        //        table.Rows.Add(row);
        //    }
        //    return table;
        //}
        #endregion

        #region 提交数据

        
        #endregion

    }
}
