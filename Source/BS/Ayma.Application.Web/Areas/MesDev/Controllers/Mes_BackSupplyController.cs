using Ayma.Application.Organization;
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
    /// 日 期：2019-03-14 16:30
    /// 描 述：退供应商单制作
    /// </summary>
    public partial class Mes_BackSupplyController : MvcControllerBase
    {
        private Mes_BackSupplyIBLL mes_BackSupplyIBLL = new Mes_BackSupplyBLL();
        private ToolsIBLL toolsIBLL = new ToolsBLL();
        private InventorySeachIBLL inventorySeachBll=new InventorySeachBLL();
        private MaterInBillIBLL materInBillIBLL = new MaterInBillBLL();
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
        /// 添加退供应商数据表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddForm()
        {
            if (Request["keyValue"] == null)
            {
                ViewBag.BackSupplyNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.BackSupply).ToString());//自动获取主编码
            }
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
        /// <summary>
        /// 退供应商物料列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BackGoodsList()
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
            var data = mes_BackSupplyIBLL.GetPageList(paginationobj, queryJson);
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
        /// 根据入库单Id获取表单详情
        /// </summary>
        /// <param name="materInKeyValue">入库单Id</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetAddFormData(string materInKeyValue)
        {
            var Mes_BackSupplyHeadModel = mes_BackSupplyIBLL.GetMes_BackSupplyHeadModel(materInKeyValue);
            var MaterInHeadData = materInBillIBLL.GetMes_MaterInHeadEntity(materInKeyValue);
            var Mes_MaterInDetailData = mes_BackSupplyIBLL.GetMes_BackSupplyList(MaterInHeadData.M_MaterInNo);
            var jsonData = new
            {
                Mes_BackSupplyHeadModel = Mes_BackSupplyHeadModel.Rows[0],
                Mes_MaterInDetailData = Mes_MaterInDetailData,
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
            var Mes_BackSupplyHeadData = mes_BackSupplyIBLL.GetMes_BackSupplyHeadEntity( keyValue );
            var Mes_BackSupplyDetailData = mes_BackSupplyIBLL.GetMes_BackSupplyDetailList( Mes_BackSupplyHeadData.B_BackSupplyNo );
            var jsonData = new {
                Mes_BackSupplyHeadData = Mes_BackSupplyHeadData,
                Mes_BackSupplyDetailData = Mes_BackSupplyDetailData,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取退供应商物料数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetBackGoodsList(string pagination, string queryJson, string keyword, string stockCode)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            DataTable data = mes_BackSupplyIBLL.GetBackGoodsList(paginationobj, queryJson, keyword, stockCode);
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
            mes_BackSupplyIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_BackSupplyDetailList)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp = mes_BackSupplyIBLL.GetMes_BackSupplyHeadEntity(keyValue);
                if (entityTemp.B_Status == ErpEnums.BackSupplyStatusEnum.Audit)
                {
                    return Fail("该单据已审核,不能修改！");
                }
            }
            Mes_BackSupplyHeadEntity entity = strEntity.ToObject<Mes_BackSupplyHeadEntity>();
            List<Mes_BackSupplyDetailEntity> mes_BackSupplyDetailList = strmes_BackSupplyDetailList.ToObject<List<Mes_BackSupplyDetailEntity>>();
            foreach (Mes_BackSupplyDetailEntity item in mes_BackSupplyDetailList)
            {
                var itemEntity=inventorySeachBll.GetEntityBy(item.B_GoodsCode, entity.B_StockCode,item.B_Batch);
                if (item.B_Qty>itemEntity.I_Qty)
                {
                    return Fail("商品【" + item.B_GoodsName + "】的库存数量不足!");
                }
	            if (string.IsNullOrEmpty(item.B_Batch))
	            {
                    return Fail("商品【" + item.B_GoodsName + "】的批次不能为空!");
	            }
	        }
            mes_BackSupplyIBLL.SaveEntity(keyValue,entity,mes_BackSupplyDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}
