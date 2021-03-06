﻿using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-02 17:05
    /// 描 述：成品出库单制作
    /// </summary>
    public partial class ProOutMakeController : MvcControllerBase
    {
        private ProOutMakeIBLL proOutMakeIBLL = new ProOutMakeBLL();
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
        /// 打印页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintReport()
        {
             return View();
        } 
        /// <summary>
        /// 出库单查询页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SearchIndex()
        {
             return View();
        }  
        /// <summary>
        /// 出库单查询表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SearchForm()
        {
             return View();
        } 
        /// <summary>
        /// 添加商品界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GoodsListIndex()
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
                ViewBag.OutNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.ProOut).ToString());//自动获取主编码
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
            var data = proOutMakeIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取页面显示列表数据 单据完成状态
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSearchPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = proOutMakeIBLL.GetSearchPageList(paginationobj, queryJson);
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
            var Mes_ProOutHeadData = proOutMakeIBLL.GetMes_ProOutHeadEntity( keyValue );
            var Mes_ProOutDetailData = proOutMakeIBLL.GetMes_ProOutDetailList( Mes_ProOutHeadData.P_ProOutNo );
            var jsonData = new {
                Mes_ProOutHeadData = Mes_ProOutHeadData,
                Mes_ProOutDetailData = Mes_ProOutDetailData,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 根据主键获取订单号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetOrderNoBy(string keyValue)
        {
            var Mes_ProOutHeadData = proOutMakeIBLL.GetMes_ProOutHeadEntity(keyValue);

            return Success(Mes_ProOutHeadData.P_OrderNo);
        }
        /// <summary>
        /// 获取仓库成品物料列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="stockCode">仓库编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterList(string pagination, string stockCode)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var list = proOutMakeIBLL.GetInventoryProMaterList(paginationobj, stockCode).ToList();
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
            proOutMakeIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_ProOutDetailList)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp = proOutMakeIBLL.GetMes_ProOutHeadEntity(keyValue);
                if (entityTemp.P_Status == ErpEnums.ProOutStatusEnum.Audit)
                {
                    return Fail("该单据已审核,不能修改！");
                }
            }
            Mes_ProOutHeadEntity entity = strEntity.ToObject<Mes_ProOutHeadEntity>();
            List<Mes_ProOutDetailEntity> mes_ProOutDetailList = strmes_ProOutDetailList.ToObject<List<Mes_ProOutDetailEntity>>();
            proOutMakeIBLL.SaveEntity(keyValue,entity,mes_ProOutDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}
