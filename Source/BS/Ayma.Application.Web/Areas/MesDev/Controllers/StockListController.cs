﻿using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 12:47
    /// 描 述：仓库列表
    /// </summary>
    public partial class StockListController : MvcControllerBase
    {
        private StockListIBLL stockListIBLL = new StockListBLL();
        private ToolsIBLL toosIBLL = new ToolsBLL();

        #region 视图功能

        /// <summary>
        /// 仓库导入页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ImportForm()
        {
             return View();
        } 
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
            var data = stockListIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_StockData = stockListIBLL.GetMes_StockEntity( keyValue );
            var jsonData = new {
                Mes_StockData = Mes_StockData,
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
            stockListIBLL.DeleteEntity(keyValue);
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
            Mes_StockEntity entity = strEntity.ToObject<Mes_StockEntity>();
            var resCode = toosIBLL.IsCode("Mes_Stock", "S_Code", entity.S_Code, keyValue);
            if (resCode)
            {
                return Fail("该编码已存在！");
            }
            var resName = toosIBLL.IsName("Mes_Stock", "S_Name", entity.S_Name, keyValue);
            if (resName)
            {
                return Fail("该名称已存在！");
            }
            stockListIBLL.SaveEntity(keyValue,entity);
            return Success("保存成功！");
        }
        #endregion

    }
}
