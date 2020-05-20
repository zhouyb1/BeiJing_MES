using Ayma.Application.Organization;
using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.Tools;
using Microsoft.JScript;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 10:20
    /// 描 述：调拨单制作
    /// </summary>
    public partial class RequistBillController : MvcControllerBase
    {
        private RequistBillIBLL requistBillIBLL = new RequistBillBLL();
        private ToolsIBLL toolsIBLL = new ToolsBLL();
        private UserIBLL useribll = new UserBLL();
        private InventorySeachIBLL inventorySearchIbll = new InventorySeachBLL();
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
        /// 调拨打印
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintReport()
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
            var rcode = useribll.GetEntityByUserId(user.userId);//通过用户id获取角色id
            var list = new RoleBLL().GetList(rcode.R_Code);
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
        /// 商品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GoodsListIndex()
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

        #region 获取数据

        /// <summary>
        /// 获取商品列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="stockCode">仓库编码</param>
        /// <param name="keyword">商品编码/商品名称搜索</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList(string pagination, string queryJson, string stockCode, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = requistBillIBLL.GetList(paginationobj, queryJson, stockCode, keyword);
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
        /// 获取已提交单据列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPostPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = requistBillIBLL.GetPostPageList(paginationobj, queryJson);
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
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = requistBillIBLL.GetPageList(paginationobj, queryJson);
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
        public ActionResult GetFormData(string keyValue,string state)
        {
            var Mes_RequistHeadData = requistBillIBLL.GetMes_RequistHeadEntity(keyValue);
            var Mes_RequistDetailData = requistBillIBLL.GetMes_RequistDetailList(Mes_RequistHeadData.R_RequistNo,state);
            var jsonData = new
            {
                Mes_RequistHeadData = Mes_RequistHeadData,
                Mes_RequistDetailData = Mes_RequistDetailData,
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
            var Mes_RequistHeadData = requistBillIBLL.GetMes_RequistHeadEntity(keyValue);

            return Success(Mes_RequistHeadData.P_OrderNo);
        }

        /// <summary>
        /// 获取调拨单查询页面明细书
        /// </summary>
        /// <returns></returns>
        public ActionResult GetDetail(string orderNo,string state)
        {
            var data = requistBillIBLL.GetMes_RequistDetailList(orderNo,state);
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
            requistBillIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string strmes_RequistDetailList)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp = requistBillIBLL.GetMes_RequistHeadEntity(keyValue);
                if (entityTemp.R_Status == ErpEnums.RequistStatusEnum.Audit)
                {
                    return Fail("该单据已审核,不能修改！");
                }
            }
            Mes_RequistHeadEntity entity = strEntity.ToObject<Mes_RequistHeadEntity>();
            List<Mes_RequistDetailEntity> mes_RequistDetailList = strmes_RequistDetailList.ToObject<List<Mes_RequistDetailEntity>>();
            #region 舍弃
            //foreach (Mes_RequistDetailEntity t in mes_RequistDetailList)
            //{
            //    //查询库存
            //    var listInv = inventorySearchIbll.GetListByStockAndCode(entity.R_StockCode, t.R_GoodsCode).ToList();
            //    if (listInv.Count > 1)
            //    {
            //        //获取库存里不同批次的商品中 最小的批次
            //        var batchMin = mes_RequistDetailList.Min(c => Convert.ToInt32(c.R_Batch));
            //        //库存实体
            //        var entityMinTemp = listInv.FirstOrDefault(c => c.I_Batch == batchMin.ToString());
            //        //明细中的数量
            //        var entityDetailMinTemp = mes_RequistDetailList.FirstOrDefault(c => c.R_Batch == batchMin.ToString());
            //        if (entityMinTemp != null && entityDetailMinTemp != null)
            //        {
            //            if (entityDetailMinTemp.R_Qty < entityMinTemp.I_Qty)//设置的数量小于库存数量时
            //            {
            //                return Fail("");
            //            }
            //        }


            //    }
            //} 
            #endregion
            requistBillIBLL.SaveEntity(keyValue, entity, mes_RequistDetailList);
            return Success("保存成功！");
        }
        #endregion

    }
}
