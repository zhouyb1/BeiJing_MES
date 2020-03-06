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
    /// 日 期：2019-11-07 13:51
    /// 描 述：其它入库单
    /// </summary>
    public partial class OtherWarehouseReceiptController : MvcControllerBase
    {
        private OtherWarehouseReceiptIBLL otherWarehouseReceiptIBLL = new OtherWarehouseReceiptBLL();

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
        /// 入库商品列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GoodsListIndex()
        {
            return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList( string queryJson )
        {
            var data = otherWarehouseReceiptIBLL.GetList(queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = otherWarehouseReceiptIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取列表分页数据（其它入库单查询）
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPostPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = otherWarehouseReceiptIBLL.GetPostPageList(paginationobj, queryJson);
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
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue,string state)
        {
            var Mes_OtherInHead = otherWarehouseReceiptIBLL.GetEntity(keyValue);
            var Mes_OtherInDetail = otherWarehouseReceiptIBLL.GetMes_OtherInDetaiEntity(Mes_OtherInHead.O_OtherInNo, state);
            var jsonData = new
            {
                Mes_OtherInHead = Mes_OtherInHead,
                Mes_OtherInDetail = Mes_OtherInDetail
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 单据查询界面：获取明细数据
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public ActionResult GetDetail(string orderNo,string state)
        {
            var data = otherWarehouseReceiptIBLL.GetMes_OtherInDetaiEntity(orderNo, state);
            return Success(data);
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
            otherWarehouseReceiptIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_MaterOtherInDetailList)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp = otherWarehouseReceiptIBLL.GetEntity(keyValue);
                if (entityTemp.O_Status == ErpEnums.OtherInStatusEnum.Audit)
                {
                    return Fail("该单据已审核,不能修改！");
                }
            }
                Mes_OtherInHeadEntity entity = strEntity.ToObject<Mes_OtherInHeadEntity>();
                List<Mes_OtherInDetailEntity> mes_OtherInDetailList =strmes_MaterOtherInDetailList.ToObject<List<Mes_OtherInDetailEntity>>();
                foreach (var item in mes_OtherInDetailList)
                {
                    if (string.IsNullOrEmpty(item.O_Qty.ToString()) || item.O_Qty == 0)
                    {
                        return Fail("物料【" + item.O_GoodsName + "】入库数量不能为空!");
                    }
                    if (string.IsNullOrEmpty(item.O_Price.ToString()) || item.O_Price == 0)
                    {
                        return Fail("物料【" + item.O_GoodsName + "】价格为空请及时维护价格!");
                    }
                }
                otherWarehouseReceiptIBLL.SaveEntity(keyValue, entity, mes_OtherInDetailList);
                return Success("保存成功！");
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
            var data = otherWarehouseReceiptIBLL.GetGoodsList(paginationobj, queryJson, keyword);
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
        /// 报表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintReport()
        {
            return View();
        }
        /// <summary>
        /// 单据查询页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostIndex()
        {
            return View();
        }
        #endregion

    }
}
