using System.Linq;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.Organization;
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
    /// 日 期：2019-03-15 16:11
    /// 描 述：线边仓退料到仓库
    /// </summary>
    public partial class BackStockManagerController : MvcControllerBase
    {
        private BackStockManagerIBLL backStockManagerIBLL = new BackStockManagerBLL();
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
            //获取登录用户的角色
            var user = LoginUserInfo.Get();
            var list = new RoleBLL().GetList(user.roleIds);
            if (list.Count > 0)
            {
                if (list[0].F_FullName != "系统管理员")
                {
                    ViewBag.disabled = "disabled";
                }
            }
            return View();
        }

        /// <summary>
        /// 物料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GoodsListIndex()
        {
            return View();
        }

        /// <summary>
        /// 获取已提交单据的页面
        /// </summary>
        /// <returns></returns>
        public ActionResult BackPostIndex()
        {
            return View();
        }

        /// <summary>
        /// 获取表单页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PostPageForm()
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
            var data = backStockManagerIBLL.GetPageList(paginationobj, queryJson);
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
        [AjaxOnly]
        public ActionResult GetBacStockList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = backStockManagerIBLL.GetBacStockList(paginationobj, queryJson);
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
        /// 根据单号获取物料明细列表
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public ActionResult GetBackStockDetailList(string orderNo)
        {
            var data = backStockManagerIBLL.GetBackStockDetailList(orderNo);
            return Success(data);
        }

        /// <summary>
        /// 获取物料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsList(string pagination, string stockCode, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var list = backStockManagerIBLL.GetGoodsList(paginationobj, stockCode, keyword);
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
            var Mes_BackStockHeadData = backStockManagerIBLL.GetMes_BackStockHeadEntity( keyValue );
            var Mes_BackStockDetailData = backStockManagerIBLL.GetMes_BackStockDetailList( Mes_BackStockHeadData.B_BackStockNo );
            var jsonData = new {
                Mes_BackStockHeadData = Mes_BackStockHeadData,
                Mes_BackStockDetailData = Mes_BackStockDetailData,
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
            backStockManagerIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_BackStockDetailList)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp = backStockManagerIBLL.GetMes_BackStockHeadEntity(keyValue);
                if (entityTemp.B_Status == ErpEnums.ProOutStatusEnum.Audit)
                {
                    return Fail("该单据已审核,不能编辑!");
                }
            }
            Mes_BackStockHeadEntity entity = strEntity.ToObject<Mes_BackStockHeadEntity>();
            var mes_BackStockDetailEntity = strmes_BackStockDetailList.ToObject<List<Mes_BackStockDetailEntity>>();
            if (mes_BackStockDetailEntity.Any(c=>c.B_Qty<=0))
            {
                return Fail("数量只能是大于0的实数");
            }

            foreach (var goods in mes_BackStockDetailEntity)
            {
                var stock_qty = invSeachIbll.GetEntityBy(goods.B_GoodsCode, entity.B_StockCode,goods.B_Batch).I_Qty;
                if (goods.B_Qty>stock_qty)
                {
                    return Fail("【" + goods.B_GoodsName + "】" + "库存不足");
                }
            }
            backStockManagerIBLL.SaveEntity(keyValue,entity,mes_BackStockDetailEntity);
            return Success("保存成功！");
        }
        #endregion

    }
}
