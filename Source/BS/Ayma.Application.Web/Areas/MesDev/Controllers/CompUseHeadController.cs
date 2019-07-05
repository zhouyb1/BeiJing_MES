using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-07-04 16:14
    /// 描 述：强制使用记录单据
    /// </summary>
    public partial class CompUseHeadController : MvcControllerBase
    {
        private CompUseHeadIBLL compUseHeadIBLL = new CompUseHeadBLL();
        private ToolsIBLL toolsIBLL = new ToolsBLL();
        private InventorySeachIBLL inventorySeachBll = new InventorySeachBLL();
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
        /// 添加物料列表
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
                ViewBag.C_No = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.CompUser).ToString());//自动获取主编码
            }
             return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取强制使用单据物料数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsList(string pagination, string queryJson, string keyword, string stockCode)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            DataTable data = compUseHeadIBLL.GetGoodsList(paginationobj, queryJson, keyword, stockCode);
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
            var data = compUseHeadIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_CompUseHeadData = compUseHeadIBLL.GetMes_CompUseHeadEntity( keyValue );
            var Mes_CompUseDetailData = compUseHeadIBLL.GetMes_CompUseDetailList( Mes_CompUseHeadData.C_No );
            var jsonData = new {
                Mes_CompUseHeadData = Mes_CompUseHeadData,
                Mes_CompUseDetailData = Mes_CompUseDetailData,
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
            compUseHeadIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_CompUseDetailList)
        {
           
            Mes_CompUseHeadEntity entity = strEntity.ToObject<Mes_CompUseHeadEntity>();
            if (string.IsNullOrEmpty(keyValue))
            {
                var codeRulebll = new CodeRuleBLL();
                if (toolsIBLL.IsOrderNo("Mes_CompUseHead", "C_No", codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.CompUser).ToString())))
                {
                    //若重复 先占用再赋值
                    codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.CompUser).ToString()); //标志已使用
                    entity.C_No = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.CompUser).ToString());
                }
                else
                {
                    entity.C_No = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.CompUser).ToString());
                }
                codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.CompUser).ToString()); //标志已使用
            }
            List<Mes_CompUseDetailEntity> mes_CompUseDetailList = strmes_CompUseDetailList.ToObject<List<Mes_CompUseDetailEntity>>();
            foreach (Mes_CompUseDetailEntity item in mes_CompUseDetailList)
            {
                var itemEntity = inventorySeachBll.GetEntityBy(item.C_GoodsCode, entity.C_StockCode, item.C_Batch);
                if (item.C_Qty > itemEntity.I_Qty)
                {
                    return Fail("商品【" + item.C_GoodsName + "】的库存数量不足!");
                }
                if (string.IsNullOrEmpty(item.C_Batch))
                {
                    return Fail("商品【" + item.C_GoodsName + "】的批次不能为空!");
                }
            }
            compUseHeadIBLL.SaveEntity(keyValue,entity,mes_CompUseDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}
