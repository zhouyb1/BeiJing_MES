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
    /// 日 期：2019-03-13 11:57
    /// 描 述：出库单制作
    /// </summary>
    public partial class OutWorkShopManagerController : MvcControllerBase
    {
        private OutWorkShopManagerIBLL outWorkShopManagerIBLL = new OutWorkShopManagerBLL();
        private ToolsIBLL toolsIBLL = new ToolsBLL();
        private InventorySeachIBLL invSeachIbll = new InventorySeachBLL();
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
                ViewBag.OutNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Out).ToString());//自动获取主编码
            }
            return View();
        }

        /// <summary>
        /// 已提交单据的表单详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostForm()
        {
            return View();
        }

        /// <summary>
        /// 仓库物料视图
        /// </summary>
        /// <returns></returns>
        public ActionResult MaterListIndex()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PostOrderIndex()
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
            ;
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
            var data = outWorkShopManagerIBLL.GetPageList(paginationobj, queryJson);
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
        /// 出库单查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult PostPageIndex(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var list = outWorkShopManagerIBLL.GetPostIndex(paginationobj, queryJson);
            var jsonData = new
            {
                rows = list,
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
            var Mes_OutWorkShopHeadData = outWorkShopManagerIBLL.GetMes_OutWorkShopHeadEntity( keyValue );
            var Mes_OutWorkShopDetailData = outWorkShopManagerIBLL.GetMes_OutWorkShopDetailList( Mes_OutWorkShopHeadData.O_OutNo );
            var jsonData = new {
                Mes_OutWorkShopHeadData = Mes_OutWorkShopHeadData,
                Mes_OutWorkShopDetailData = Mes_OutWorkShopDetailData,
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
            var Mes_OutWorkShopHeadData = outWorkShopManagerIBLL.GetMes_OutWorkShopHeadEntity(keyValue);

            return Success(Mes_OutWorkShopHeadData.O_OrderNo);
        }
        /// <summary>
        /// 获取仓库物料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterList(string pagination, string stockCode, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var list = outWorkShopManagerIBLL.GetInventoryMaterList(paginationobj,stockCode,keyword).ToList();
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
            outWorkShopManagerIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_OutWorkShopDetailList)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp = outWorkShopManagerIBLL.GetMes_OutWorkShopHeadEntity(keyValue);
                if (entityTemp.O_Status == ErpEnums.ProOutStatusEnum.Audit)
                {
                    return Fail("单据已审核，不能修改！");
                }
            }
            Mes_OutWorkShopHeadEntity entity = strEntity.ToObject<Mes_OutWorkShopHeadEntity>();
            var mes_OutWorkShopDetailList = strmes_OutWorkShopDetailList.ToObject<List<Mes_OutWorkShopDetailEntity>>();
            if (mes_OutWorkShopDetailList.Any(c=>c.O_Qty<=0))
            {
                return Fail("数量只能是大于0的实数");
            }
            foreach (var goods in mes_OutWorkShopDetailList)
            {
                var stock_qty = invSeachIbll.GetEntityBy(goods.O_GoodsCode,entity.O_StockCode, goods.O_Batch).I_Qty;
                if (goods.O_Qty > stock_qty)
                {
                    return Fail("【" + goods.O_GoodsName + "】" + "库存不足");
                }
            }
            outWorkShopManagerIBLL.SaveEntity(keyValue, entity, mes_OutWorkShopDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}
