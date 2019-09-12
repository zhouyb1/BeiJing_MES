using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.JScript;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-08-06 10:54
    /// 描 述：原物料入库价格表
    /// </summary>
    public partial class InPriceController : MvcControllerBase
    {
        private InPriceIBLL inPriceIBLL = new InPriceBLL();

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
            var data = inPriceIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_InPriceData = inPriceIBLL.GetMes_InPriceEntity(keyValue);
            var jsonData = new
            {
                Mes_InPriceData = Mes_InPriceData,
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
            inPriceIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strEntity2) 
        {
            Mes_InPriceEntity entity = strEntity.ToObject<Mes_InPriceEntity>();
            Mes_PriceEntity entity2 = strEntity2.ToObject<Mes_PriceEntity>();
            inPriceIBLL.SaveEntity(keyValue, entity, entity2);
            return Success("保存成功！");
            //if (!string.IsNullOrEmpty(entity.P_StartBatch) && !string.IsNullOrEmpty(entity.P_EndBatch))
            //{
            //    if (entity.P_StartBatch.Contains("."))
            //    {
            //        return Fail("起始批次不能有小数点.");
            //    }
            //    if (entity.P_EndBatch.Contains("."))
            //    {
            //        return Fail("终止批次不能有小数点.");
            //    }
            //    if (entity.P_StartBatch.Length != 8)
            //    {
            //        return Fail("起始批次必须为8为数字.");
            //    }
            //    if (entity.P_EndBatch.Length != 8)
            //    {
            //        return Fail("终止批次必须为8为数字.");
            //    }

            //    if (Convert.ToInt32(entity.P_StartBatch) > Convert.ToInt32(entity.P_EndBatch))
            //    {
            //        return Fail("起始批次不能大于终止批次.");
            //    }
            //}
        }
        #endregion

    }
}
