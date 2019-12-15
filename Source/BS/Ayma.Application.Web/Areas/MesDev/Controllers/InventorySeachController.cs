using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 17:17
    /// 描 述：库存查询
    /// </summary>
    public partial class InventorySeachController : MvcControllerBase
    {
        private InventorySeachIBLL inventorySeachIBLL = new InventorySeachBLL();

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
        /// 物料领用和使用情况页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PickOrUserIndex()
        {
             return View();
        }
        /// <summary>
        /// 物料价值查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PriceSearchIndex()
        {
             return View();
        }
        /// <summary>
        /// 表单页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
             return View();
        }
        /// <summary>
        /// 明细页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult InvertoryList()
        {
            return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson,string stock,string goodsCode)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = inventorySeachIBLL.GetPageList(paginationobj, queryJson, stock, goodsCode);
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
        /// 获取物料领用列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPickPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = inventorySeachIBLL.GetPickPageList(paginationobj, queryJson);
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
        /// 获取物料使用列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetUsedPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = inventorySeachIBLL.GetUsedPageList(paginationobj, queryJson);
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
        /// 物料价值查询
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPricePageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = inventorySeachIBLL.GetPricePageList(paginationobj, queryJson);
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
        /// 获取表单数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var Mes_InventoryData = inventorySeachIBLL.GetMes_InventoryEntity( keyValue );
            var jsonData = new {
                Mes_InventoryData = Mes_InventoryData,
            };
            return Success(jsonData);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            inventorySeachIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string strEntity)
        {
            Mes_InventoryEntity entity = strEntity.ToObject<Mes_InventoryEntity>();
            inventorySeachIBLL.SaveEntity(keyValue,entity);
            return Success("保存成功！");
        }
        #endregion
        /// <summary>
        /// 获取库存列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetInventoryList(string pagination, string queryJson, string I_GoodsName, string I_StockName, string I_Unit, string I_Batch)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = inventorySeachIBLL.GetInventoryList(paginationobj, queryJson, I_GoodsName, I_StockName, I_Unit, I_Batch);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
    }
}
