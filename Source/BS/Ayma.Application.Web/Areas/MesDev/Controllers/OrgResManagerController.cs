using System;
using System.Linq;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.Tools;
using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;

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
        public FileResult Export(Pagination pagination, string queryJson)
        {

            var datas = orgResMangerIBLL.GetProductRateList(pagination, queryJson);
            var dt = AsDataTable(datas);
            var queryParam = queryJson.ToJObject();
            DateTime StartTime = queryParam["StartTime"].ToDate();
            DateTime EndTime = queryParam["EndTime"].ToDate();
            string starttime = StartTime.ToString("yyyyMMdd");
            string endtime = EndTime.ToString("yyyyMMdd");
            var ms = NPOIExcel.ToExcelMoreheader(dt, "物料出成率列表", "物料出成率列表", starttime, endtime);
            if (starttime == endtime)
            {
                return File(ms.GetBuffer(), "application/vnd.ms-excel", starttime + "_物料出成率列表.xls");
            }
            else
            {
                return File(ms.GetBuffer(), "application/vnd.ms-excel", starttime +"-"+endtime+ "_物料出成率列表.xls");
            }
        }
        /// <summary>
        /// 获取导出Excel数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable AsDataTable(IEnumerable<ProductRateView> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ProductRateView));
            var table = new DataTable();
            //定义列名
            foreach (PropertyDescriptor prop in properties)
            {
                switch (prop.Name)
                {
                    case "rownum": table.Columns.Add("序号", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
                    case "O_GoodsName": table.Columns.Add("物料名称", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
                    case "O_GoodsCode": table.Columns.Add("物料编码", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
                    case "O_Unit": table.Columns.Add("单位", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
                    case "O_Qty": table.Columns.Add("使用数量", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
                    case "O_SecGoodsName": table.Columns.Add("物料名称.", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
                    case "O_SecGoodsCode": table.Columns.Add("物料编码.", Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType); break;
                    case "O_SecUnit": table.Columns.Add("单位.", typeof(string)); break;
                    case "O_SecQty": table.Columns.Add("产出数量", typeof(string)); break;
                    case "O_StockName": table.Columns.Add("作业日耗库", typeof(string)); break;
                    case "O_ProName": table.Columns.Add("作业工序", typeof(string)); break;
                    case "O_TeamName": table.Columns.Add("作业班组", typeof(string)); break;
                    case "ProductRate": table.Columns.Add("出成率(%)", typeof(string)); break;
                    case "targetRate": table.Columns.Add("考核区间(%)", typeof(string)); break;
                    case "DIFF": table.Columns.Add("偏差(%)", typeof(string)); break;
                    case "O_CreateBy": table.Columns.Add("制作人", typeof(string)); break;
                    default: break;
                }
            }

            table.Columns["序号"].SetOrdinal(0);
            table.Columns["物料名称"].SetOrdinal(1);
            table.Columns["物料编码"].SetOrdinal(2);
            table.Columns["单位"].SetOrdinal(3);
            table.Columns["使用数量"].SetOrdinal(4);
            table.Columns["物料名称."].SetOrdinal(5);
            table.Columns["物料编码."].SetOrdinal(6);
            table.Columns["单位."].SetOrdinal(7);
            table.Columns["产出数量"].SetOrdinal(8);
            table.Columns["作业日耗库"].SetOrdinal(9);
            table.Columns["作业工序"].SetOrdinal(10);
            table.Columns["作业班组"].SetOrdinal(11);
            table.Columns["出成率(%)"].SetOrdinal(12);
            table.Columns["考核区间(%)"].SetOrdinal(13);
            table.Columns["偏差(%)"].SetOrdinal(14);
            table.Columns["制作人"].SetOrdinal(15);
            //给数据
            foreach (var item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    switch (prop.Name)
                    {
                        case "O_GoodsName": row["物料名称"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "O_GoodsCode": row["物料编码"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "O_Unit": row["单位"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "O_Qty": row["使用数量"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "O_SecGoodsName": row["物料名称."] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "O_SecGoodsCode": row["物料编码."] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "O_SecUnit": row["单位."] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "O_SecQty": row["产出数量"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "O_StockName": row["作业日耗库"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "O_ProName": row["作业工序"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "O_TeamName": row["作业班组"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "ProductRate": row["出成率(%)"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "targetRate": row["考核区间(%)"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "DIFF": row["偏差(%)"] = prop.GetValue(item) ?? DBNull.Value; break;
                        case "O_CreateBy": row["制作人"] = prop.GetValue(item) ?? DBNull.Value; break;
                    }
                table.Rows.Add(row);
                for (int i = 0; i < table.Rows.Count; i++)
                {

                    table.Rows[i]["序号"] = i + 1;
                }
            }
            return table;
        }
    }
}
