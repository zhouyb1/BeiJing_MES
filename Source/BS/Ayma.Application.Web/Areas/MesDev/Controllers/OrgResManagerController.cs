﻿using System;
using System.Linq;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.Tools;
using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data;

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
        /// 报表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintReport2()
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
            var dicGoods = new Dictionary<string, List<Mes_OrgResDetailEntity>>();//记录批次库存
            var goods_list = new List< Mes_OrgResDetailEntity>();//记录物料批次数据
            Mes_OrgResDetailEntity reqGoods = null;
            for (var i = 0; i < mes_OrgResDetailList.Count; i++)
            {
                if (dicGoods.ContainsKey(mes_OrgResDetailList[i].O_GoodsCode))
                {
                    //根据 物料编码和仓库获取所有库存
                    var tempStock = dicGoods[mes_OrgResDetailList[i].O_GoodsCode];
                    for (var j = 0; j < tempStock.Count; j++) //需求20个，两个批次分别为5个，7个
                    {
                        if (mes_OrgResDetailList[i].O_Qty < tempStock[j].O_Qty)
                        {
                            //品种组装前物料
                            reqGoods = new Mes_OrgResDetailEntity();
                            reqGoods.O_GoodsCode = tempStock[j].O_GoodsCode;
                            reqGoods.O_GoodsName = tempStock[j].O_GoodsName;
                            reqGoods.O_Batch = tempStock[j].O_Batch;
                            reqGoods.O_Qty = mes_OrgResDetailList[i].O_Qty;
                            reqGoods.O_Unit = mes_OrgResDetailList[i].O_Unit;
                            reqGoods.O_Price = mes_OrgResDetailList[i].O_Price;
                            //组装产出物
                            reqGoods.O_SecGoodsCode = mes_OrgResDetailList[i].O_SecGoodsCode;
                            reqGoods.O_SecGoodsName = mes_OrgResDetailList[i].O_SecGoodsName;
                            reqGoods.O_SecBatch = mes_OrgResDetailList[i].O_SecBatch;
                            reqGoods.O_SecPrice = mes_OrgResDetailList[i].O_SecPrice;
                            reqGoods.O_SecUnit = mes_OrgResDetailList[i].O_SecUnit;
                            reqGoods.O_SecQty = mes_OrgResDetailList[i].O_SecQty;

                            goods_list.Add(reqGoods);
                            tempStock[j].O_Qty = tempStock[j].O_Qty - mes_OrgResDetailList[i].O_Qty;
                            break; //数量足够 跳出循环
                        }
                        reqGoods = new Mes_OrgResDetailEntity();
                        var qty = tempStock[j].O_Qty; //取全部
                        //拼装组装前物料
                        reqGoods.O_Qty = qty;
                        reqGoods.O_Price = mes_OrgResDetailList[i].O_Price;
                        reqGoods.O_GoodsCode = tempStock[j].O_GoodsCode;
                        reqGoods.O_GoodsName = tempStock[j].O_GoodsName;
                        reqGoods.O_Batch = tempStock[j].O_Batch;
                        reqGoods.O_Unit = mes_OrgResDetailList[i].O_Unit;

                        //拼装组装后产物
                        reqGoods.O_SecGoodsCode = mes_OrgResDetailList[i].O_SecGoodsCode;
                        reqGoods.O_SecGoodsName = mes_OrgResDetailList[i].O_SecGoodsName;
                        reqGoods.O_SecBatch = mes_OrgResDetailList[i].O_SecBatch;
                        reqGoods.O_SecPrice = mes_OrgResDetailList[i].O_SecPrice;
                        reqGoods.O_SecUnit = mes_OrgResDetailList[i].O_SecUnit;
                        reqGoods.O_SecQty = mes_OrgResDetailList[i].O_SecQty;

                        goods_list.Add(reqGoods);
                        tempStock.RemoveAt(j);
                        j--;
                        mes_OrgResDetailList[i].O_Qty = mes_OrgResDetailList[i].O_Qty - qty;
                    }
                }
                else
                {
                    //根据 物料编码和仓库获取所有库存
                    var stockList =stock.GetOrgGoodsList(entity.O_StockCode, mes_OrgResDetailList[i].O_GoodsCode).ToList();
                    for (var j = 0; j < stockList.Count; j++) //需求20个，两个批次分别为5个，7个
                    {
                        if (mes_OrgResDetailList[i].O_Qty < stockList[j].O_Qty)
                        {
                            //品种组装前物料
                            reqGoods = new Mes_OrgResDetailEntity();
                            reqGoods.O_GoodsCode = stockList[j].O_GoodsCode;
                            reqGoods.O_GoodsName = stockList[j].O_GoodsName;
                            reqGoods.O_Batch = stockList[j].O_Batch;
                            reqGoods.O_Qty = mes_OrgResDetailList[i].O_Qty;
                            reqGoods.O_Unit = mes_OrgResDetailList[i].O_Unit;
                            reqGoods.O_Price = mes_OrgResDetailList[i].O_Price;
                            //组装产出物
                            reqGoods.O_SecGoodsCode = mes_OrgResDetailList[i].O_SecGoodsCode;
                            reqGoods.O_SecGoodsName = mes_OrgResDetailList[i].O_SecGoodsName;
                            reqGoods.O_SecBatch = mes_OrgResDetailList[i].O_SecBatch;
                            reqGoods.O_SecPrice = mes_OrgResDetailList[i].O_SecPrice;
                            reqGoods.O_SecUnit = mes_OrgResDetailList[i].O_SecUnit;
                            reqGoods.O_SecQty = mes_OrgResDetailList[i].O_SecQty;

                            goods_list.Add(reqGoods);
                            stockList[j].O_Qty = stockList[j].O_Qty - mes_OrgResDetailList[i].O_Qty;
                            break; //数量足够 跳出循环 
                        }
                        reqGoods = new Mes_OrgResDetailEntity();
                        var qty = stockList[j].O_Qty; //取全部
                        //拼装组装前物料
                        reqGoods.O_Qty = qty;
                        reqGoods.O_Price = mes_OrgResDetailList[i].O_Price;
                        reqGoods.O_GoodsCode = stockList[j].O_GoodsCode;
                        reqGoods.O_GoodsName = stockList[j].O_GoodsName;
                        reqGoods.O_Batch = stockList[j].O_Batch;
                        reqGoods.O_Unit = mes_OrgResDetailList[i].O_Unit;

                        //拼装组装后产物
                        reqGoods.O_SecGoodsCode = mes_OrgResDetailList[i].O_SecGoodsCode;
                        reqGoods.O_SecGoodsName = mes_OrgResDetailList[i].O_SecGoodsName;
                        reqGoods.O_SecBatch = mes_OrgResDetailList[i].O_SecBatch;
                        reqGoods.O_SecPrice = mes_OrgResDetailList[i].O_SecPrice;
                        reqGoods.O_SecUnit = mes_OrgResDetailList[i].O_SecUnit;
                        reqGoods.O_SecQty = mes_OrgResDetailList[i].O_SecQty;

                        goods_list.Add(reqGoods);
                        stockList.RemoveAt(j);
                        j--;
                        mes_OrgResDetailList[i].O_Qty = mes_OrgResDetailList[i].O_Qty - qty;
                    }
                    dicGoods[mes_OrgResDetailList[i].O_GoodsCode] = stockList; //记录剩余批次库存
                }
            }
            orgResMangerIBLL.SaveEntity(keyValue, entity, goods_list);
            return Success("保存成功！");
        }
        #endregion
        /// <summary>
        /// 获取Export表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public FileResult Export(Pagination pagination, string queryJson)
        {
            DataTable dt = orgResMangerIBLL.GetProductRateList(pagination, queryJson);

            //给列名
            dt.Columns["O_GoodsName"].ColumnName = "转换前_物料名称";
            dt.Columns["O_GoodsCode"].ColumnName = "转换前_物料编码";
            dt.Columns["O_Unit"].ColumnName = "转换前_单位";
            dt.Columns["O_Qty"].ColumnName = "转换前_使用数量";
            dt.Columns["O_SecGoodsName"].ColumnName = "转换后_物料编码";
            dt.Columns["O_SecGoodsCode"].ColumnName = "转换后_物料名称";
            dt.Columns["O_SecUnit"].ColumnName = "转换后_单位";
            dt.Columns["O_SecQty"].ColumnName = "转换后_使用数量";
            dt.Columns["O_StockName"].ColumnName = "作业日耗库";
            dt.Columns["O_ProName"].ColumnName = "作业工序";
            dt.Columns["O_TeamName"].ColumnName = "作业班组";
            dt.Columns["ProductRate"].ColumnName = "出成率(%)";
            dt.Columns["O_CreateBy"].ColumnName = "制作人";
            //表格列名排序
            dt.Columns["转换前_物料名称"].SetOrdinal(0);
            dt.Columns["转换前_物料编码"].SetOrdinal(1);
            dt.Columns["转换前_单位"].SetOrdinal(2);
            dt.Columns["转换前_使用数量"].SetOrdinal(3);
            dt.Columns["转换后_物料编码"].SetOrdinal(4);
            dt.Columns["转换后_物料名称"].SetOrdinal(5);
            dt.Columns["转换后_单位"].SetOrdinal(6);
            dt.Columns["转换后_使用数量"].SetOrdinal(7);
            dt.Columns["作业日耗库"].SetOrdinal(8);
            dt.Columns["作业工序"].SetOrdinal(9);
            dt.Columns["作业班组"].SetOrdinal(10);
            dt.Columns["出成率(%)"].SetOrdinal(11);
            dt.Columns["制作人"].SetOrdinal(12);

            var queryParam = queryJson.ToJObject();
            var ms = NPOIExcel.ToExcelMoreheader(dt, "出成率实时查询", "出成率实时查询", queryParam["StartTime"].ToString(), queryParam["EndTime"].ToString());
            return File(ms.GetBuffer(), "application/vnd.ms-excel", "出成率实时查询.xls");
        }
    }
}
