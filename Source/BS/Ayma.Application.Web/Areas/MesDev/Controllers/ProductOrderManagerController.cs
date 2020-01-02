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
    /// 日 期：2019-03-07 11:05
    /// 描 述：生产订单管理
    /// </summary>
    public partial class ProductOrderManagerController : MvcControllerBase
    {
        private ProductOrderManagerIBLL productOrderManagerIBLL = new ProductOrderManagerBLL();
        private ToolsIBLL toolsIBLL = new ToolsBLL();
        private PickingMaterIBLL pickingMaterIbll = new PickingMaterBLL();
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
        /// <summary>
        /// 表单页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PlanForm()
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
            var data = productOrderManagerIBLL.GetPageList(paginationobj, queryJson);
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
            //var Mes_ProductOrderHeadData = productOrderManagerIBLL.GetMes_ProductOrderHeadEntity( keyValue );
            //var Mes_ProductOrderDetailData = productOrderManagerIBLL.GetMes_ProductOrderDetaillist(Mes_ProductOrderHeadData.P_OrderNo);
            //var jsonData = new {
            //    Mes_ProductOrderHeadData = Mes_ProductOrderHeadData,
            //    Mes_ProductOrderDetailData = Mes_ProductOrderDetailData,
            //};

            var data = productOrderManagerIBLL.GetMes_ProductOrderDetailEntity(keyValue);
            return Success(data);
        }

        /// <summary>
        /// 获取order明细
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetOrderDetail(string orderNo)
        {
            var list = productOrderManagerIBLL.GetMes_ProductOrderDetaillist(orderNo);
            return Success(list);
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
            productOrderManagerIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_ProductOrderDetaillist)
        {
            Mes_ProductOrderHeadEntity entity = null;
            List<Mes_ProductOrderDetailEntity >mes_ProductOrderDetaillist = strmes_ProductOrderDetaillist.ToObject<List<Mes_ProductOrderDetailEntity>>();
            productOrderManagerIBLL.SaveEntity(keyValue, entity, mes_ProductOrderDetaillist);
            return Success("保存成功！");
        }
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult AuditingBill(string keyValue)
        {
            productOrderManagerIBLL.AuditingBill(keyValue);
            return Success("审核成功！");
        }

        /// <summary>
        /// 原辅料统计页面
        /// </summary>
        /// <returns></returns>
        public ActionResult BomPartSum()
        {
            return View();
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetBomTreeList(string parentId,int qty)
        {
            var list = productOrderManagerIBLL.GetBomList(parentId,qty);
            return Success(list);
        }

        [HttpPost]
        [AjaxOnly]
        public ActionResult SaveBomData(string strJsonBomList, string orderNo, DateTime orderDate, string strCollarHead)
        {
            var materList = strJsonBomList.ToObject<List<Mes_BomRecordEntity>>();
            var bomList = materList.Select(c => new Mes_MaterEntity
            {
                P_GoodsCode = c.B_GoodsCode,
                P_GoodsName = c.B_GoodsName,
                P_Unit = c.B_Unit,
                P_Qty = c.B_Total,
                P_OrderNo = orderNo,
                P_OrderDate = orderDate,
                P_ErpCode = c.B_ErpCode
            }).ToList();
            productOrderManagerIBLL.SaveBomList(bomList);

            var collarHead = strCollarHead.ToObject<Mes_CollarHeadEntity>();//领料单头
            var codeRulebll = new CodeRuleBLL();
            if (toolsIBLL.IsOrderNo("Mes_CollarHead", "C_CollarNo", codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.PickMater).ToString())))
            {
                //若重复 先占用再赋值
                codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.PickMater).ToString()); //标志已使用
                collarHead.C_CollarNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.PickMater).ToString());
            }
            else
            {
                collarHead.C_CollarNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.PickMater).ToString());
            }
            var detailList = new List<Mes_CollarDetailEntity>();
            materList.ForEach(d => detailList.Add(new Mes_CollarDetailEntity()
            {
                C_Qty = d.B_Qty,
                C_Remark = d.B_Remark,
                C_GoodsCode = d.B_GoodsCode,
                C_GoodsName = d.B_GoodsName,
                C_SupplyCode = d.G_SupplyCode,
                C_SupplyName = d.G_SupplyName,
                C_TeamCode = collarHead.C_TeamCode,
                C_Unit = d.B_Unit,
                C_OrderNo =collarHead.P_OrderNo,
                C_CollarNo = collarHead.C_CollarNo,
                C_Price = d.G_Price,
                C_Batch = DateTime.Now.ToString("yyyyMMdd")
            }));
            pickingMaterIbll.SaveEntity("",collarHead,detailList);
            return Success("处理成功！");
        }

        #endregion

    }
}
