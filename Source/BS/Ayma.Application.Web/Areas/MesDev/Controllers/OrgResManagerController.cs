using System;
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
    /// 日 期：2019-03-18 15:14
    /// 描 述：组装与拆分单据制作
    /// </summary>
    public partial class OrgResManagerController : MvcControllerBase
    {
        private OrgResMangerIBLL orgResMangerIBLL = new OrgResMangerBLL();
        private ToolsIBLL toolsIBLL = new ToolsBLL();
        private InventorySeachIBLL stock = new InventorySeachBLL();
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
        /// 查询页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SearchIndex()
        {
             return View();
        } 
        /// <summary>
        /// 查询详情表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SearchForm()
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
                ViewBag.orderNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.Org).ToString());//自动获取主编码
            }
             return View();
        }

        /// <summary>
        /// 物料页面
        /// </summary>
        /// <returns></returns>
        public ActionResult GoodsListIndex()
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

        /// <summary>
        /// 出成率查询
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductRateIndex()
        {
            return View();
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 出成率查询
        /// </summary>
        /// <returns></returns>
        [AjaxOnly]
        [HttpGet]
        public ActionResult GetProductRateList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = orgResMangerIBLL.GetProductRateList(paginationobj, queryJson);
            //var jsonData = new
            //{
            //    rows = data,
            //    total = paginationobj.total,
            //    page = paginationobj.page,
            //    records = paginationobj.records
            //};
            return Success(data);
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
            var data = orgResMangerIBLL.GetPageList(paginationobj, queryJson);
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
        /// 获取组装与拆分单据查询页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult OrgResManagerList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = orgResMangerIBLL.OrgResManagerList(paginationobj, queryJson);
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
        /// 获取前物料
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetGoodsList(string pagination, string keyword, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = orgResMangerIBLL.GetGoodsList( keyword, queryJson,paginationobj);
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
        /// 获取转换后的物料
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSecGoodsList(string keyword, string pagination)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = orgResMangerIBLL.GetSecGoodsList(keyword, paginationobj);

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
            var Mes_OrgResHeadData = orgResMangerIBLL.GetMes_OrgResHeadEntity( keyValue );
            var Mes_OrgResDetailData = orgResMangerIBLL.GetMes_OrgResDetailList( Mes_OrgResHeadData.O_OrgResNo );
            var jsonData = new {
                Mes_OrgResHeadData = Mes_OrgResHeadData,
                Mes_OrgResDetailData = Mes_OrgResDetailData,
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
            var Mes_OrgResHeadData = orgResMangerIBLL.GetMes_OrgResHeadEntity(keyValue);

            return Success(Mes_OrgResHeadData.O_OrderNo);
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
            orgResMangerIBLL.DeleteEntity(keyValue);
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
        public ActionResult SaveForm(string keyValue, string strEntity, string detailList)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp = orgResMangerIBLL.GetMes_OrgResHeadEntity(keyValue);
                if (entityTemp.O_Status == ErpEnums.MaterInStatusEnum.Audit)
                {
                    return Fail("该单据已审核,不能修改！");
                }
            }
            Mes_OrgResHeadEntity entity = strEntity.ToObject<Mes_OrgResHeadEntity>();
            var mes_OrgResDetailList = detailList.ToObject<List<Mes_OrgResDetailEntity>>();
            if (mes_OrgResDetailList.Any(c=>c.O_Qty<=0||c.O_SecQty<=0))
            {
                return Fail("数量只能是大于0的实数");
            }

            var groupList = mes_OrgResDetailList.GroupBy(c => new {c.O_GoodsCode,c.O_GoodsName}).ToList();

            foreach (var t in groupList)
            {
                var stockQty = stock.GetOrgGoodsList(entity.O_StockCode, t.Key.O_GoodsCode).ToList().Sum(c => c.O_Qty);
                var factQty = t.ToList().Sum(c => c.O_Qty);
                if (factQty>stockQty)
                {
                    return Fail("当前【" + t.Key.O_GoodsName + "】录入数量大于总库存" + stockQty);
                }
            }

            #region 舍去
            //var  list = mes_OrgResDetailList.GroupBy(c => c.O_GoodsCode).ToList();
            //foreach (var item in list)
            //{
            //    var useNum = mes_OrgResDetailList.Where(c => c.O_GoodsCode == item.Key).ToList().Sum(c => c.O_Qty);
            //    var stock = new Mes_WorkShopScanBLL().GetMes_WorkShopScanEntity(item.Key,item,);
            //    if (useNum>stock.W_Qty)
            //    {
            //        return Fail("【"+stock.W_GoodsName+"】当前使用数量大于库存");
            //    }
            //} 
            #endregion

            //按照先进先出原则生成单据
            
            var goods_list = new List< Mes_OrgResDetailEntity>();//记录物料批次数据
            for (var i = 0; i < mes_OrgResDetailList.Count; i++)
            {
                //根据 物料编码和仓库获取所有库存
               var stockList= stock.GetOrgGoodsList(entity.O_StockCode, mes_OrgResDetailList[i].O_GoodsCode).ToList();
                for (var j = 0; j < stockList.Count; j++)//需求20个，两个批次分别为5个，7个
                {
                    if (mes_OrgResDetailList[i].O_Qty <= stockList[j].O_Qty)
                    {
                        //品种组装前物料
                        stockList[j].O_Qty = mes_OrgResDetailList[i].O_Qty;
                        stockList[j].O_Price = mes_OrgResDetailList[i].O_Price;
                        //组装产出物
                        stockList[j].O_SecGoodsCode = mes_OrgResDetailList[i].O_SecGoodsCode;
                        stockList[j].O_SecGoodsName = mes_OrgResDetailList[i].O_SecGoodsName;
                        stockList[j].O_SecBatch = mes_OrgResDetailList[i].O_SecBatch;
                        stockList[j].O_SecPrice = mes_OrgResDetailList[i].O_SecPrice;
                        stockList[j].O_SecUnit = mes_OrgResDetailList[i].O_SecUnit;
                        stockList[j].O_SecQty = mes_OrgResDetailList[i].O_SecQty;
                        goods_list.Add(stockList[j]);
                        break;//数量足够 跳出循环
                    }
                    var qty = stockList[j].O_Qty; //取全部
                    //拼装组装前物料
                    stockList[j].O_Qty = qty;
                    stockList[j].O_Price = mes_OrgResDetailList[i].O_Price;
                    //拼装组装后产物
                    stockList[j].O_SecGoodsCode = mes_OrgResDetailList[i].O_SecGoodsCode;
                    stockList[j].O_SecGoodsName = mes_OrgResDetailList[i].O_SecGoodsName;
                    stockList[j].O_SecBatch = mes_OrgResDetailList[i].O_SecBatch;
                    stockList[j].O_SecPrice = mes_OrgResDetailList[i].O_SecPrice;
                    stockList[j].O_SecUnit = mes_OrgResDetailList[i].O_SecUnit;
                    stockList[j].O_SecQty = mes_OrgResDetailList[i].O_SecQty;
                    goods_list.Add(stockList[j]);
                    stockList.RemoveAt(j);
                    j--;
                    mes_OrgResDetailList[i].O_Qty = mes_OrgResDetailList[i].O_Qty - qty;
                }
            }
            orgResMangerIBLL.SaveEntity(keyValue, entity, goods_list);
            return Success("保存成功！");
        }
        #endregion

    }
}
