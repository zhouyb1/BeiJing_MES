using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-12 09:16
    /// 描 述：库存快照
    /// </summary>
    public partial class StockHistoryController : MvcControllerBase
    {
        private StockHistoryIBLL stockHistoryIBLL = new StockHistoryBLL();

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

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = stockHistoryIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取库存列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetInventoryList(string pagination, string queryJson, string I_Date,string I_GoodsName, string I_StockName, string I_Unit, string I_Batch)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = stockHistoryIBLL.GetInventoryList(paginationobj, queryJson, I_Date, I_GoodsName, I_StockName, I_Unit, I_Batch);
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



    }
}
