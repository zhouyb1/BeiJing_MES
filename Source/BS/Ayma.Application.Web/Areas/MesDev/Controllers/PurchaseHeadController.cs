using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-05 11:20
    /// 描 述：采购单制作及查询
    /// </summary>
    public partial class PurchaseHeadController : MvcControllerBase
    {
        private PurchaseHeadIBLL purchaseHeadIBLL = new PurchaseHeadBLL();

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
        /// 表单页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
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
            var data = purchaseHeadIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_PurchaseHeadData = purchaseHeadIBLL.GetMes_PurchaseHeadEntity( keyValue );
            var Mes_PurchaseDetailData = purchaseHeadIBLL.GetMes_PurchaseDetailList( Mes_PurchaseHeadData.P_PurchaseNo );
            var jsonData = new {
                Mes_PurchaseHeadData = Mes_PurchaseHeadData,
                Mes_PurchaseDetailData = Mes_PurchaseDetailData,
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
            purchaseHeadIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_PurchaseDetailList)
        {
            Mes_PurchaseHeadEntity entity = strEntity.ToObject<Mes_PurchaseHeadEntity>();
            List<Mes_PurchaseDetailEntity> mes_PurchaseDetailList = strmes_PurchaseDetailList.ToObject<List<Mes_PurchaseDetailEntity>>();
            purchaseHeadIBLL.SaveEntity(keyValue,entity,mes_PurchaseDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}
