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
    /// 描 述：车间入库到线边仓
    /// </summary>
    public partial class InWorkShopManagerController : MvcControllerBase
    {
        private InWorkShopManagerIBLL inWorkShopManagerIBLL = new InWorkShopManagerBLL();
        private ToolsIBLL toolsIBLL = new ToolsBLL();
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
                ViewBag.InNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.InX).ToString());//自动获取主编码
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
            var data = inWorkShopManagerIBLL.GetPageList(paginationobj, queryJson);
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
        /// 车间入库到线边仓查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult PostPageIndex(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var list = inWorkShopManagerIBLL.GetSearchIndex(paginationobj, queryJson);
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
            var Mes_InWorkShopHeadData = inWorkShopManagerIBLL.GetMes_InWorkShopHeadEntity( keyValue );
            var Mes_InWorkShopDetailData = inWorkShopManagerIBLL.GetMes_InWorkShopDetailList(Mes_InWorkShopHeadData.I_InNo);
            var jsonData = new {
                Mes_InWorkShopHeadData = Mes_InWorkShopHeadData,
                Mes_InWorkShopDetailData = Mes_InWorkShopDetailData,
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
            var Mes_InWorkShopHeadData = inWorkShopManagerIBLL.GetMes_InWorkShopHeadEntity(keyValue);

            return Success(Mes_InWorkShopHeadData.I_OrderNo);
        }
        /// <summary>
        /// 获取仓库物料
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterList(string pagination, string stockCode)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var list = inWorkShopManagerIBLL.GetInventoryMaterList(paginationobj,stockCode).ToList();
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
        /// 获取物料列表(半成品和成品)
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var dt = inWorkShopManagerIBLL.GetGoodsList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = dt,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 单据查询界面获取明细
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public ActionResult GetDetail(string orderNo)
        {
            var data = inWorkShopManagerIBLL.GetMes_InWorkShopDetailList(orderNo);
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
            inWorkShopManagerIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_InWorkShopDetailList)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp = inWorkShopManagerIBLL.GetMes_InWorkShopHeadEntity(keyValue);
                if (entityTemp.I_Status == ErpEnums.InStatusEnum.Audit)
                {
                    return Fail("该单据已审核,不能编辑！");
                }
            }
            Mes_InWorkShopHeadEntity entity = strEntity.ToObject<Mes_InWorkShopHeadEntity>();
            var mes_InWorkShopDetailList = strmes_InWorkShopDetailList.ToObject<List<Mes_InWorkShopDetailEntity>>();
            if (string.IsNullOrEmpty(strmes_InWorkShopDetailList))
            {
                return Fail("明细列表不能为空");
            }
            if (mes_InWorkShopDetailList.Any(c=>c.I_Qty<=0))
            {
                return Fail("数量只能是大于0的实数");
            }
            inWorkShopManagerIBLL.SaveEntity(keyValue, entity, mes_InWorkShopDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}
