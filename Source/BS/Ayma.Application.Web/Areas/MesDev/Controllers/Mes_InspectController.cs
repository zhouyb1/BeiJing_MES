﻿using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 20:54
    /// 描 述：抽检记录
    /// </summary>
    public partial class Mes_InspectController : MvcControllerBase
    {
        private Mes_InspectIBLL mes_InspectIBLL = new Mes_InspectBLL();
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
            if (Request["keyValue"] == null)
            {
                ViewBag.InspectNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Inspect).ToString());//自动获取主编码
            }
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
            var data = mes_InspectIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_InspectData = mes_InspectIBLL.GetMes_InspectEntity( keyValue );
            var jsonData = new {
                Mes_InspectData = Mes_InspectData,
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
            mes_InspectIBLL.DeleteEntity(keyValue);
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
            Mes_InspectEntity entity = strEntity.ToObject<Mes_InspectEntity>();
            if (string.IsNullOrWhiteSpace(entity.I_GoodsQty + ""))
            {
                return Fail("请输入正确的抽检数量!");
            }
            if (string.IsNullOrWhiteSpace(entity.I_QualifiedQty+""))
            {
                return Fail("请输入正确的合格数量!");
            }
            if (string.IsNullOrEmpty(keyValue))
            {
                var codeRulebll = new CodeRuleBLL();
                if (toolsIBLL.IsOrderNo("Mes_Inspect", "I_InspectNo", codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Inspect).ToString())))
                {
                    //若重复 先占用再赋值
                    codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.Inspect).ToString()); //标志已使用
                    entity.I_InspectNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Inspect).ToString());
                }
                else
                {
                    entity.I_InspectNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Inspect).ToString());
                }
                codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.Inspect).ToString()); //标志已使用
            }
            mes_InspectIBLL.SaveEntity(keyValue,entity);
            return Success("保存成功！");
        }
        #endregion

    }
}
