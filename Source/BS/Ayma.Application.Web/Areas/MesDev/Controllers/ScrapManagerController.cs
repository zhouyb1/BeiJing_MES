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
    /// 日 期：2019-03-14 11:20
    /// 描 述：报废单据管理
    /// </summary>
    public partial class ScrapManagerController : MvcControllerBase
    {
        private ScrapManagerIBLL scrapManagerIBLL = new ScrapManagerBLL();
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
            if (Request["keyValue"]==null)
            {
                ViewBag.OrderNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Scrap).ToString());//自动获取主编码
            }
             return View();
        }

        /// <summary>
        /// 需要报废的物料列表
        /// </summary>
        /// <returns></returns>
        public ActionResult MaterListIndex()
        {
            return View();
        }

        /// <summary>
        /// 报废单查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostIndex()
        {
            return View();
        }
        /// <summary>
        /// 报废单查询表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
        public ActionResult GetPageList(string pagination, string queryJson )
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = scrapManagerIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取报废单查询页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ScrapManagerList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = scrapManagerIBLL.ScrapManagerList(paginationobj, queryJson);
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
            var Mes_ScrapHeadData = scrapManagerIBLL.GetMes_ScrapHeadEntity( keyValue );
            var detail = scrapManagerIBLL.GetMes_ScrapDeail(Mes_ScrapHeadData.S_ScrapNo);
            var jsonData = new {
                Mes_ScrapHeadData = Mes_ScrapHeadData,
                Mes_ScrapDetailData=detail
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 需报废物料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsList(string pagination, string stockCode, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var list = scrapManagerIBLL.GetGoodsList(paginationobj, stockCode, keyword);
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
            scrapManagerIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity,string detailList)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp = scrapManagerIBLL.GetMes_ScrapHeadEntity(keyValue);
                if (entityTemp.S_Status == ErpEnums.ScrapStatusEnum.Audit)
                {
                    return Fail("该单据已审核,不能编辑!");
                }
            }
            Mes_ScrapHeadEntity entity = strEntity.ToObject<Mes_ScrapHeadEntity>();
            var detail = detailList.ToObject<List<Mes_ScrapDetailEntity>>();
            if (detail.Any(item => item.S_Qty<=0))
            {
                return Fail("数量只能是大于0的实数");
            }
            foreach (var goods in detail)
            {
                var stock_qty = invSeachIbll.GetListByParams(goods.S_GoodsCode, goods.S_Batch).I_Qty;
                if (goods.S_Qty > stock_qty)
                {
                    return Fail("【" + goods.S_GoodsName + "】" + "库存不足");
                }
            }
            if (string.IsNullOrEmpty(keyValue))
            {
                var codeRulebll = new CodeRuleBLL();
                if (toolsIBLL.IsOrderNo("Mes_ScrapHead", "S_ScrapNo", codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Scrap).ToString())))
                {
                    //若重复 先占用再赋值
                    codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.Scrap).ToString()); //标志已使用
                    entity.S_ScrapNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Scrap).ToString());
                }
                else
                {
                    entity.S_ScrapNo = codeRulebll.GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Scrap).ToString());
                }
                codeRulebll.UseRuleSeed(((int)ErpEnums.OrderNoRuleEnum.Scrap).ToString()); //标志已使用
            }
            scrapManagerIBLL.SaveEntity(keyValue,entity,detail);
            return Success("保存成功！");
        }
        #endregion

    }
}
