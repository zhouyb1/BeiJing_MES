﻿using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 16:30
    /// 描 述：退供应商单制作
    /// </summary>
    public partial class Mes_BackSupplyController : MvcControllerBase
    {
        private Mes_BackSupplyIBLL mes_BackSupplyIBLL = new Mes_BackSupplyBLL();
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
                ViewBag.BackSupplyNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.BackSupply).ToString());//自动获取主编码
            }
             return View();
        }
        /// <summary>
        /// 退供应商物料列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BackGoodsList()
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
            var data = mes_BackSupplyIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_BackSupplyHeadData = mes_BackSupplyIBLL.GetMes_BackSupplyHeadEntity( keyValue );
            var Mes_BackSupplyDetailData = mes_BackSupplyIBLL.GetMes_BackSupplyDetailList( Mes_BackSupplyHeadData.B_BackSupplyNo );
            var jsonData = new {
                Mes_BackSupplyHeadData = Mes_BackSupplyHeadData,
                Mes_BackSupplyDetailData = Mes_BackSupplyDetailData,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取退供应商物料数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetBackGoodsList(string pagination, string queryJson, string keyword, string stockCode)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            DataTable data = mes_BackSupplyIBLL.GetBackGoodsList(paginationobj, queryJson, keyword, stockCode);
            var jsonData = new
            {
                rows = data,
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
            mes_BackSupplyIBLL.DeleteEntity(keyValue);
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
            if (string.IsNullOrEmpty(keyValue))
            {
                var codeRulebll = new CodeRuleBLL();
                if (toolsIBLL.IsOrderNo("Mes_BackSupplyHead", "B_BackSupplyNo", codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.BackSupply).ToString())))
                {
                    //若重复 先占用再赋值
                    codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.BackSupply).ToString()); //标志已使用
                    entity.B_BackSupplyNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.BackSupply).ToString());
                }
                else
                {
                    entity.B_BackSupplyNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.BackSupply).ToString());
                }
                codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.BackSupply).ToString()); //标志已使用
            }
            mes_BackSupplyIBLL.SaveEntity(keyValue,entity,mes_BackSupplyDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}