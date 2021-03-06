﻿using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.Tools;
using System.Data;
using System;
namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-12 17:32
    /// 描 述：排班记录
    /// </summary>
    public partial class Mes_ArrangeController : MvcControllerBase
    {
        private Mes_ArrangeIBLL mes_ArrangeIBLL = new Mes_ArrangeBLL();
        private ToolsIBLL toolsIBLL = new ToolsBLL();
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
            var data = mes_ArrangeIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public ActionResult GetDataList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            DataTable data = mes_ArrangeIBLL.GetDataList(paginationobj, queryJson);
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
        /// 获取页面子列表数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSubDataList(DateTime date, string time, string orderno, string workshopcode, string classcode)
        {
            string datetime = date.ToString("yyyy-MM-dd");
            DataTable data = mes_ArrangeIBLL.GetSubDataList(datetime, time, orderno, workshopcode, classcode);
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
            var Mes_ArrangeData = mes_ArrangeIBLL.GetMes_ArrangeEntity( keyValue );
            var time = Mes_ArrangeData.A_DateTime.ToTimeString();           
            var jsonData = new {
                Mes_ArrangeData = Mes_ArrangeData,
                time=time,
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
            mes_ArrangeIBLL.DeleteEntity(keyValue);
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
            if (!string.IsNullOrEmpty(keyValue))
            {
                Mes_ArrangeEntity entity = strEntity.ToObject<Mes_ArrangeEntity>();
                string A_F_EnCode = entity.A_F_EnCode;
                string A_ClassCode = entity.A_ClassCode;
                DateTime A_Date = Convert.ToDateTime(entity.A_Date);
                if (toolsIBLL.IsExistRecord(keyValue,A_F_EnCode, A_ClassCode, A_Date))
                {
                    return Fail("同一个用户编码不能在同一班次出现!");
                }
                mes_ArrangeIBLL.SaveEntity(keyValue, entity);
            }
            else
            {
                var data = strEntity.ToList<Mes_ArrangeEntity>();
                for (int i = 0; i < data.Count; i++)
                {
                    string A_F_EnCode = data[i].A_F_EnCode;
                    string A_ClassCode = data[i].A_ClassCode;
                    DateTime A_Date = Convert.ToDateTime(data[i].A_Date);
                    if (toolsIBLL.IsExistRecord(keyValue,A_F_EnCode, A_ClassCode, A_Date))
                    {
                        return Fail("同一个用户编码不能在同一班次出现!");
                    }
                    mes_ArrangeIBLL.SaveEntity(keyValue, data[i]);
                }
            }
            return Success("保存成功！");
        }
        #endregion

    }
}
