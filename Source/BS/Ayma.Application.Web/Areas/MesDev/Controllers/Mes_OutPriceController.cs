using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.JScript;
using Ayma.Application.TwoDevelopment.Tools;
namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-12-04 09:28
    /// 描 述：原物料售卖价格表
    /// </summary>
    public partial class Mes_OutPriceController : MvcControllerBase
    {
        private Mes_OutPriceIBLL mes_OutPriceIBLL = new Mes_OutPriceBLL();
        private ToolsIBLL Tools = new ToolsBLL();

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
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList( string queryJson )
        {
            var data = mes_OutPriceIBLL.GetList(queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = mes_OutPriceIBLL.GetPageList(paginationobj, queryJson);
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
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var data = mes_OutPriceIBLL.GetEntity(keyValue);
            return Success(data);
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
        public ActionResult DeleteForm(List<Mes_OutPriceEntity> strEntity)
        {
            mes_OutPriceIBLL.DeleteEntity(strEntity);
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
        public ActionResult SaveForm(string keyValue,Mes_OutPriceEntity entity)
        {
            var IsCode = Tools.IsCode("Mes_OutPrice", "O_GoodsCode", entity.O_GoodsCode, "");
            if (IsCode == true)
            {
                return Fail("物料【" + entity.O_GoodsName + "】的售卖价格已存在!");
            }
            mes_OutPriceIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="strEntity"></param>
        /// <param name="strEntity2"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult Save(string keyValue, string strEntity)
        {
            var entityList = strEntity.ToObject<List<Mes_OutPriceEntity>>();
            mes_OutPriceIBLL.SaveEntity(entityList);
            return Success("保存成功！");
        }
        #endregion

    }
}
