using System.Linq;
using Ayma.Application.TwoDevelopment;
using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-07 10:38
    /// 描 述：原物料(半成品)销售
    /// </summary>
    public partial class Mes_SaleManagerController : MvcControllerBase
    {
        private Mes_SaleManagerIBLL mes_SaleManagerIBLL = new Mes_SaleManagerBLL();
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
        /// 打印页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PrintReport()
        {
            return View();
        }
        /// <summary>
        /// 物料列表页面
        /// </summary>
        /// <returns></returns>
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
            return View();
        }
        /// <summary>
        /// 获取已提交的原物料单据列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostGoodsListIndex()
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
            var data = mes_SaleManagerIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取物料列表数据
        /// </summary>
        /// <returns></returns>
       [HttpGet]
       [AjaxOnly]
       public ActionResult GetGoodsList(string pagination, string queryJson, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = mes_SaleManagerIBLL.GetGoodsList(paginationobj, queryJson, keyword).ToList();
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
            var Mes_SaleHeadData = mes_SaleManagerIBLL.GetMes_SaleHeadEntity(keyValue);
            var Mes_SaleDetailData = mes_SaleManagerIBLL.GetMes_SaleDetail(Mes_SaleHeadData.S_SaleNo);
            var jsonData = new
            {
                Mes_SaleHeadData = Mes_SaleHeadData,
                Mes_SaleDetailData = Mes_SaleDetailData,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 报表：获取已提交的原物料销售单据
        /// </summary>
        /// <returns></returns>
        [AjaxOnly]
        public ActionResult GetPostList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = mes_SaleManagerIBLL.GetPostList(paginationobj, queryJson);
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
        /// 报表：获取已提交的原物料销售单据详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetDetailList(string SaleNo)
        {
            var list = mes_SaleManagerIBLL.GetDetailList(SaleNo);
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
            mes_SaleManagerIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_SaleDetailEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp = mes_SaleManagerIBLL.GetMes_SaleHeadEntity(keyValue);
                if (entityTemp.S_Status == ErpEnums.SaleOutStatusEnum.Audit)
                {
                    return Fail("该单据已审核,不能修改！");

                }
            }
            Mes_SaleHeadEntity entity = strEntity.ToObject<Mes_SaleHeadEntity>();
            var detail = strmes_SaleDetailEntity.ToObject<List<Mes_SaleDetailEntity>>();
            if (detail.Any(item => item.S_Qty <= 0))
            {
                return Fail("数量只能是大于0的实数");
            }
            //List<Mes_SaleDetailEntity> mes_SaleDetailList =
            //  strmes_SaleDetailEntity.ToObject<List<Mes_SaleDetailEntity>>();
            //foreach (var item in mes_SaleDetailList)
            //{
            //    if (string.IsNullOrEmpty(item.S_Qty.ToString()) || item.S_Qty <= 0)
            //    {
            //        return Fail("数量只能是大于0的实数!");
            //    }
            //    if (string.IsNullOrEmpty(item.S_Price.ToString()) || item.S_Price == 0)
            //    {
            //        return Fail("物料【" + item.S_GoodsName + "】价格为空请及时维护价格!");
            //    }
            //}
            foreach (var goods in detail)
            {
                var stock_qty = invSeachIbll.GetEntityBy(goods.S_GoodsCode, entity.S_StockCode, goods.S_Batch).I_Qty;
                if (goods.S_Qty > stock_qty)
                {
                    return Fail("【" + goods.S_GoodsName + "】" + "库存不足");
                }
            }
            mes_SaleManagerIBLL.SaveEntity(keyValue, entity, detail);
            return Success("保存成功！");
            #endregion

        }
    }
}
