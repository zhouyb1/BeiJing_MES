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
    /// 日 期：2019-03-11 19:22
    /// 描 述：领料单
    /// </summary>
    public partial class PickingMaterController : MvcControllerBase
    {
        private PickingMaterIBLL pickingMaterIBLL = new PickingMaterBLL();
        private ToolsIBLL toolsIBLL = new ToolsBLL();
        private InventorySeachIBLL invSeachIbll = new InventorySeachBLL();
        private Mes_ProductOrderHeadIBLL orderBll = new Mes_ProductOrderHeadBLL();
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
        /// 订单原物料需求列表
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderMaterList()
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
                ViewBag.RequireNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Requist).ToString());//自动获取主编码
            }
            return View();
        }

        /// <summary>
        /// 报表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintViewReport()
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
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = pickingMaterIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_CollarHeadData = pickingMaterIBLL.GetMes_CollarHeadEntity( keyValue );
            var Mes_CollarDetailData = pickingMaterIBLL.GetMes_CollarDetailEntityList( Mes_CollarHeadData.C_CollarNo );
            var jsonData = new {
                Mes_CollarHeadData = Mes_CollarHeadData,
                Mes_CollarDetailData = Mes_CollarDetailData,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取库存料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterList(string pagination, string queryJson, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = pickingMaterIBLL.GetMaterList(paginationobj, queryJson, keyword).ToList();
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
            pickingMaterIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_CollarDetailEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp = pickingMaterIBLL.GetMes_CollarHeadEntity(keyValue);
                if (entityTemp.P_Status == ErpEnums.RequistStatusEnum.Audit)
                {
                    return Fail("该单据已审核,不能修改！");
                }
            }
            Mes_CollarHeadEntity entity = strEntity.ToObject<Mes_CollarHeadEntity>();
            //获取订单时间
            //var order = orderBll.GetEntityByNo(entity.P_OrderNo);
            entity.P_OrderDate = null;
            var mes_CollarDetailEntityList = strmes_CollarDetailEntity.ToObject<List<Mes_CollarDetailEntity>>();
            if (mes_CollarDetailEntityList.Any(c=>c.C_Qty<=0))
            {
                return Fail("数量只能是大于0的实数");
            }
            //获取库存
            var list = from goods in mes_CollarDetailEntityList
                       let stock = invSeachIbll.GetEntityBy(goods.C_GoodsCode,entity.C_StockCode, goods.C_Batch)
                       let qty =stock==null?0:stock.I_Qty
                where goods.C_Qty > qty
                select goods;
            foreach (var s in list)
            {
                return Fail(s.C_GoodsName + "不存在或库存不足");
            }
            pickingMaterIBLL.SaveEntity(keyValue, entity, mes_CollarDetailEntityList);
            return Success("保存成功！");
        }
        #endregion

    }
}
