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
    /// 日 期：2019-11-08 13:59
    /// 描 述：消耗物料
    /// </summary>
    public partial class Mes_ExpendManagerController : MvcControllerBase
    {
        private Mes_ExpendManagerIBLL mes_ExpendManagerIBLL = new Mes_ExpendManagerBLL();
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
             return View();
        }
        #endregion

        /// <summary>
        /// 物料选择页面
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsListIndex()
        {
            return View();
        }

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
            var data = mes_ExpendManagerIBLL.GetPageList(paginationobj, queryJson);
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
            var data = mes_ExpendManagerIBLL.GetGoodsList(paginationobj, queryJson, keyword).ToList();
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
            var Mes_ExpendHeadData = mes_ExpendManagerIBLL.GetMes_ExpendHeadEntity( keyValue );
            var Mes_ExpendDetailData = mes_ExpendManagerIBLL.GetMes_ExpendDetailEntity( Mes_ExpendHeadData.E_ExpendNo );
            var jsonData = new {
                Mes_ExpendHeadData = Mes_ExpendHeadData,
                Mes_ExpendDetailData = Mes_ExpendDetailData,
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
            mes_ExpendManagerIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_ExpendDetailEntity)
        {
            var entity = strEntity.ToObject<Mes_ExpendHeadEntity>();
            var detail  = strmes_ExpendDetailEntity.ToObject<List<Mes_ExpendDetailEntity>>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp =mes_ExpendManagerIBLL.GetMes_ExpendHeadEntity(keyValue);
                if (entityTemp.E_Status == ErpEnums.ExpendStatus.Audit)
                {
                    return Fail("该单据已审核,不能修改！");
                }
            }
            if (detail.Any(item => item.E_Qty <= 0))
            {
                return Fail("数量只能是大于0的实数");
            }
            foreach (var goods in from goods in detail let stockQty = invSeachIbll.GetEntityBy(goods.E_GoodsCode, entity.E_StockCode, goods.E_Batch).I_Qty where goods.E_Qty > stockQty select goods)
            {
                return Fail("【" + goods.E_GoodsName + "】" + "库存不足");
            }
            mes_ExpendManagerIBLL.SaveEntity(keyValue, entity, detail);
            return Success("保存成功！");
        }
        #endregion

    }
}
