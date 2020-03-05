using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 10:06
    /// 描 述：领料单查询
    /// </summary>
    public partial class PickingMaterQueryController : MvcControllerBase
    {
        private PickingMaterQueryIBLL pickingMaterQueryIBLL = new PickingMaterQueryBLL();

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
        public ActionResult GetPageList(string pagination, string queryJson, string C_CollarNo)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = pickingMaterQueryIBLL.GetPageList(paginationobj, queryJson,C_CollarNo);
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
        /// 获取领料单明细数据
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public ActionResult GetPikingMaterList(string orderNo)
        {
            var data = pickingMaterQueryIBLL.GetMes_CollarDetailEntityList(orderNo);
            return Success(data);
        }

        /// <summary>
        /// 获取表单数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var Mes_CollarHeadData = pickingMaterQueryIBLL.GetMes_CollarHeadEntity(keyValue);
            var Mes_CollarDetailData = pickingMaterQueryIBLL.GetMes_CollarDetailEntityList(Mes_CollarHeadData.C_CollarNo);
            var jsonData = new
            {
                Mes_CollarHeadData = Mes_CollarHeadData,
                Mes_CollarDetailData = Mes_CollarDetailData,
            };
            return Success(jsonData);
        }
        #endregion

      
    }
}
