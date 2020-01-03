using System;
using System.Linq;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.Tools;
using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-18 15:14
    /// 描 述：组装与拆分单据制作
    /// </summary>
    public partial class OrgResManagerController : MvcControllerBase
    {
        private OrgResMangerIBLL orgResMangerIBLL = new OrgResMangerBLL();
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
        /// 查询页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SearchIndex()
        {
             return View();
        } 
        /// <summary>
        /// 查询详情表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SearchForm()
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
                ViewBag.orderNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Org).ToString());//自动获取主编码
            }
             return View();
        }

        /// <summary>
        /// 物料页面
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsListIndex()
        {
            return View();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintReport()
        {
            return View();
;        }

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
            var data = orgResMangerIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取组装与拆分单据查询页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult OrgResManagerList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = orgResMangerIBLL.OrgResManagerList(paginationobj, queryJson);
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
        /// 获取前物料
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGoodsList(string pagination, string keyword, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = orgResMangerIBLL.GetGoodsList( keyword, queryJson,paginationobj);
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
        /// 获取转换后的物料
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSecGoodsList(string keyword, string pagination)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = orgResMangerIBLL.GetSecGoodsList(keyword, paginationobj);

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
            var Mes_OrgResHeadData = orgResMangerIBLL.GetMes_OrgResHeadEntity( keyValue );
            var Mes_OrgResDetailData = orgResMangerIBLL.GetMes_OrgResDetailList( Mes_OrgResHeadData.O_OrgResNo );
            var jsonData = new {
                Mes_OrgResHeadData = Mes_OrgResHeadData,
                Mes_OrgResDetailData = Mes_OrgResDetailData,
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
            var Mes_OrgResHeadData = orgResMangerIBLL.GetMes_OrgResHeadEntity(keyValue);

            return Success(Mes_OrgResHeadData.O_OrderNo);
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
            orgResMangerIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string detailList)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp = orgResMangerIBLL.GetMes_OrgResHeadEntity(keyValue);
                if (entityTemp.O_Status == ErpEnums.MaterInStatusEnum.Audit)
                {
                    return Fail("该单据已审核,不能修改！");
                }
            }
            Mes_OrgResHeadEntity entity = strEntity.ToObject<Mes_OrgResHeadEntity>();
            var mes_OrgResDetailList = detailList.ToObject<List<Mes_OrgResDetailEntity>>();
            if (mes_OrgResDetailList.Any(c=>c.O_Qty<=0||c.O_SecQty<=0))
            {
                return Fail("数量只能是大于0的实数");
            }
            //var  list = mes_OrgResDetailList.GroupBy(c => c.O_GoodsCode).ToList();
            //foreach (var item in list)
            //{
            //    var useNum = mes_OrgResDetailList.Where(c => c.O_GoodsCode == item.Key).ToList().Sum(c => c.O_Qty);
            //    var stock = new Mes_WorkShopScanBLL().GetMes_WorkShopScanEntity(item.Key,item,);
            //    if (useNum>stock.W_Qty)
            //    {
            //        return Fail("【"+stock.W_GoodsName+"】当前使用数量大于库存");
            //    }
            //}
            orgResMangerIBLL.SaveEntity(keyValue, entity, mes_OrgResDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}
