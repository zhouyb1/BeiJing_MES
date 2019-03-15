using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-15 16:11
    /// 描 述：线边仓退料到仓库
    /// </summary>
    public partial class BackStockManagerController : MvcControllerBase
    {
        private BackStockManagerIBLL backStockManagerIBLL = new BackStockManagerBLL();

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
            if (Request["keyValue"] == null)
            {
                ViewBag.OrderNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.BackToStock).ToString());//自动获取主编码
            }
            return View();
        }
        /// <summary>
        /// 物料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GoodsListIndex()
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
            var data = backStockManagerIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取物料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsList(string pagination, string stockCode, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var list = backStockManagerIBLL.GetGoodsList(paginationobj, stockCode, keyword);
            var jsonData = new
            {
                rows = list,
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
            var Mes_BackStockHeadData = backStockManagerIBLL.GetMes_BackStockHeadEntity( keyValue );
            var Mes_BackStockDetailData = backStockManagerIBLL.GetMes_BackStockDetailList( Mes_BackStockHeadData.B_BackStockNo );
            var jsonData = new {
                Mes_BackStockHeadData = Mes_BackStockHeadData,
                Mes_BackStockDetailData = Mes_BackStockDetailData,
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
            backStockManagerIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_BackStockDetailList)
        {
            Mes_BackStockHeadEntity entity = strEntity.ToObject<Mes_BackStockHeadEntity>();
            var mes_BackStockDetailEntity = strmes_BackStockDetailList.ToObject<List<Mes_BackStockDetailEntity>>();
            backStockManagerIBLL.SaveEntity(keyValue,entity,mes_BackStockDetailEntity);
            return Success("保存成功！");
        }
        #endregion

    }
}
