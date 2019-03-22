using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 15:20
    /// 描 述：同步ERP成品商品资料
    /// </summary>
    public partial class Mes_ProductGoodsController : MvcControllerBase
    {
        private Mes_ProductGoodsIBLL mes_ProductGoodsIBLL = new Mes_ProductGoodsBLL();

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

        [HttpGet]
        public ActionResult ERPTgoodsList()
        {
            return View();
        }

        #endregion

        #region 获取数据

      
        #endregion

        #region 提交数据

      
        ///// <summary>
        ///// 保存实体数据（新增、修改）
        ///// </summary>
        ///// <param name="keyValue">主键</param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AjaxOnly]
        //public ActionResult SaveForm(string keyValue, string strEntity)
        //{
        //    Mes_ProductGoodsEntity entity = strEntity.ToObject<Mes_ProductGoodsEntity>();
        //    mes_ProductGoodsIBLL.SaveEntity(keyValue,entity);
        //    return Success("保存成功！");
        //}
        #endregion


        ///// <summary>
        ///// 同步ERP商品资料
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //[AjaxOnly]
        //public ActionResult GetErpTgoodsList(string keyValue)
        //{
        //    var result = mes_ProductGoodsIBLL.GetErpTgoodsList();
        //    var jsonData = new
        //    {
        //        ERPTgoodsList = result
        //    };
        //    return Success(jsonData); 

        //}


        ///// <summary>
        ///// 保存ERP商品资料
        ///// </summary>
        ///// <param name="ERPFoodListEntity"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AjaxOnly]
        //public ActionResult SaveERPTgoodsList(string ERPTgoodsListEntity)
        //{
        //    List<ERPTgoodsListModel> tgoodsEntity = ERPTgoodsListEntity.ToObject<List<ERPTgoodsListModel>>();
        //    mes_ProductGoodsIBLL.SaveErpTgoods(tgoodsEntity);
        //    return Success("保存成功!");
        //}

    }
}
