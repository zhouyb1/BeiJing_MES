﻿using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 11:20
    /// 描 述：报废单据管理
    /// </summary>
    public partial class ScrapManagerController : MvcControllerBase
    {
        private ScrapManagerIBLL scrapManagerIBLL = new ScrapManagerBLL();

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
            if (Request["keyValue"]==null)
            {
                ViewBag.OrderNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Scrap).ToString());//自动获取主编码
            }
             return View();
        }

        /// <summary>
        /// 需要报废的物料列表
        /// </summary>
        /// <returns></returns>
        public ActionResult MaterListIndex()
        {
            return View();
        }

        /// <summary>
        /// 报废单查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostIndex()
        {
            return View();
        }
        /// <summary>
        /// 报废单查询表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostPageForm()
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
        public ActionResult GetPageList(string pagination, string queryJson )
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = scrapManagerIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_ScrapHeadData = scrapManagerIBLL.GetMes_ScrapHeadEntity( keyValue );
            var detail = scrapManagerIBLL.GetMes_ScrapDeail(Mes_ScrapHeadData.S_ScrapNo);
            var jsonData = new {
                Mes_ScrapHeadData = Mes_ScrapHeadData,
                Mes_ScrapDetailData=detail
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 需报废物料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsList(string pagination, string stockCode, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var list = scrapManagerIBLL.GetGoodsList(paginationobj, stockCode, keyword);
            var jsonData = new
            {
                rows = list,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
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
            scrapManagerIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity,string detailList)
        {
            Mes_ScrapHeadEntity entity = strEntity.ToObject<Mes_ScrapHeadEntity>();
            var detail = detailList.ToObject<List<Mes_ScrapDetailEntity>>();
            scrapManagerIBLL.SaveEntity(keyValue,entity,detail);
            return Success("保存成功！");
        }
        #endregion

    }
}