﻿using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 13:52
    /// 描 述：排班管理
    /// </summary>
    public partial class ClassManagerController : MvcControllerBase
    {
        private ClassManagerIBLL classManagerIBLL = new ClassManagerBLL();
        private ToolsIBLL toosIBLL = new ToolsBLL();

        #region 视图功能
        /// <summary>
        /// 导入班次表
        /// </summary>
        /// <returns></returns>
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
            var data = classManagerIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_ClassData = classManagerIBLL.GetMes_ClassEntity( keyValue );
            var jsonData = new {
                Mes_ClassData = Mes_ClassData,
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
            classManagerIBLL.DeleteEntity(keyValue);
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
            Mes_ClassEntity entity = strEntity.ToObject<Mes_ClassEntity>();
            var resCode = toosIBLL.IsCode("Mes_Class", "C_Code", entity.C_Code, keyValue);
            if (resCode)
            {
                return Fail("该编码已存在！");
            }
            var resName = toosIBLL.IsName("Mes_Class", "C_Name", entity.C_Name, keyValue);
            if (resName)
            {
                return Fail("该名称已存在！");
            }
            classManagerIBLL.SaveEntity(keyValue,entity);
            return Success("保存成功！");
        }
        #endregion

    }
}
