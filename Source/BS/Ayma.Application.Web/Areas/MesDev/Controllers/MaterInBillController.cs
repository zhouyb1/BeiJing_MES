using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 14:58
    /// 描 述：入库单制作
    /// </summary>
    public partial class MaterInBillController : MvcControllerBase
    {
        private MaterInBillIBLL materInBillIBLL = new MaterInBillBLL();
        private ToolsIBLL toolsIBLL=new ToolsBLL();

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
                ViewBag.M_MaterInNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.MaterIn).ToString());//自动获取主编码
            }
             return View();
        }
        /// <summary>
        /// 提交查询页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostIndex()
        {
            return View();
        }
        /// <summary>
        /// 成品入库单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProductIndex()
        {
            return View();
        }
       
        /// <summary>
        /// 成品入库单表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProductForm()
        {
            if (Request["keyValue"] == null)
            {
                ViewBag.OrderNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.MaterIn).ToString());//自动获取主编码
            }
            return View();
        }
        /// <summary>
        /// 入库商品列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GoodsListIndex()
        {
            return View();
        }
        /// <summary>
        /// 成品入库商品列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GoodsProductListIndex()
        {
            return View();
        }
        /// <summary>
        /// 成品入库单查询表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostProductForm()
        {
            return View();
        }

        /// <summary>
        /// 成品入库查询页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostProductIndex()
        {
            return View();
        }
        /// <summary>
        /// 报表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintReport()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取成品入库商品列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="keyword">编码/名称搜索</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetProductList(string pagination, string queryJson, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materInBillIBLL.GetProductGoodsList(paginationobj, queryJson, keyword);
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
        /// 获取非成品入库商品列表(非成品:原材料/半成品)
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="keyword">编码/名称搜索</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsList(string pagination, string queryJson, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materInBillIBLL.GetGoodsList(paginationobj, queryJson, keyword);
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
        /// 获取已提交的成品入库
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPostProductPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materInBillIBLL.GetPostProductPageList(paginationobj, queryJson);
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
        /// 获取成品入库显示数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetProductPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materInBillIBLL.GetProductPageList(paginationobj, queryJson);
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
        /// 获取已提交单据列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPostPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materInBillIBLL.GetPostPageList(paginationobj, queryJson);
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
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materInBillIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_MaterInHeadData = materInBillIBLL.GetMes_MaterInHeadEntity( keyValue );
            var Mes_MaterInDetailData = materInBillIBLL.GetMes_MaterInDetailList( Mes_MaterInHeadData.M_MaterInNo );
            var jsonData = new {
                Mes_MaterInHeadData = Mes_MaterInHeadData,
                Mes_MaterInDetailData = Mes_MaterInDetailData,
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
            var Mes_MaterInHeadData = materInBillIBLL.GetMes_MaterInHeadEntity( keyValue );

            return Success(Mes_MaterInHeadData.M_OrderNo);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult AuditingBill(string keyValue)
        {
            materInBillIBLL.AuditingBill(keyValue);
            return Success("审核完成");
        }
        /// <summary>
        /// 撤销单据
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult CancelBill(string orderNo)
        {
            string errMsg = "";
            int status = materInBillIBLL.CancelBill(orderNo, out errMsg);
            if (status == 0)
            {
                return Success("撤销成功");
            }
            return Fail(errMsg);

        }
        /// <summary>
        /// 提交单据
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult PostBill(string orderNo)
        {
            string errMsg = "";
            int status=materInBillIBLL.PostBill(orderNo,out errMsg);
            if (status==0)
            {
                return Success("提交成功");
            }
            return Fail(errMsg);

        }

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            materInBillIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="orderKind">入库单据类型(1,非成品 2,成品)</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue,  ErpEnums.OrderKindEnum orderKind, string strEntity, string strmes_MaterInDetailList)
        {
            Mes_MaterInHeadEntity entity = strEntity.ToObject<Mes_MaterInHeadEntity>();
            List<Mes_MaterInDetailEntity> mes_MaterInDetailList = strmes_MaterInDetailList.ToObject<List<Mes_MaterInDetailEntity>>();
            foreach (var item in mes_MaterInDetailList)
            {
                if (string.IsNullOrEmpty(item.M_Qty.ToString()))
                {
                    return Fail("商品【" + item.M_GoodsName + "】入库数量不能为空!");
                }
            }
            if (string.IsNullOrEmpty(keyValue))
            {
                var codeRulebll = new CodeRuleBLL();
                if (toolsIBLL.IsOrderNo("Mes_MaterInHead", "M_MaterInNo", codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.MaterIn).ToString())))
                {
                    //若重复 先占用再赋值
                    codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.MaterIn).ToString()); //标志已使用
                    entity.M_MaterInNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.MaterIn).ToString());
                }
                else
                {
                    entity.M_MaterInNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.MaterIn).ToString());
                }
                codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.MaterIn).ToString()); //标志已使用
            }
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.M_OrderKind = orderKind;
            }
            materInBillIBLL.SaveEntity(keyValue,entity,mes_MaterInDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}
