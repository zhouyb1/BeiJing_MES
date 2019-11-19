using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using System;


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
       
        #endregion

        #region 提交数据

        
        #endregion

    }
}
