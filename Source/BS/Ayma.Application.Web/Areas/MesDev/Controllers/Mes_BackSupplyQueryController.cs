using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-18 10:37
    /// 描 述：退供应商单查询
    /// </summary>
    public partial class Mes_BackSupplyQueryController : MvcControllerBase
    {
        private Mes_BackSupplyQueryIBLL mes_BackSupplyQueryIBLL = new Mes_BackSupplyQueryBLL();

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
            var data = mes_BackSupplyQueryIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_BackSupplyHeadData = mes_BackSupplyQueryIBLL.GetMes_BackSupplyHeadEntity( keyValue );
            var Mes_BackSupplyDetailData = mes_BackSupplyQueryIBLL.GetMes_BackSupplyDetailList( Mes_BackSupplyHeadData.B_BackSupplyNo );
            var jsonData = new {
                Mes_BackSupplyHeadData = Mes_BackSupplyHeadData,
                Mes_BackSupplyDetailData = Mes_BackSupplyDetailData,
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
            mes_BackSupplyQueryIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_BackSupplyDetailList)
        {
            Mes_BackSupplyHeadEntity entity = strEntity.ToObject<Mes_BackSupplyHeadEntity>();
            List<Mes_BackSupplyDetailEntity> mes_BackSupplyDetailList = strmes_BackSupplyDetailList.ToObject<List<Mes_BackSupplyDetailEntity>>();
            mes_BackSupplyQueryIBLL.SaveEntity(keyValue,entity,mes_BackSupplyDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}
