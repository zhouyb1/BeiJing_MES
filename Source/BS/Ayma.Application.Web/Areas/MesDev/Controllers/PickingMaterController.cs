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
        /// 领料计划页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TempIndex()
        {
            return View();
        }   /// <summary>
        /// 领料计划Form页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TempForm()
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

        /// <summary>
        /// 报表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CollarRport()
        {
            return View();
        }


        /// <summary>
        /// 报表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult OtherRport()
        {
            return View();
        }

        #endregion

        #region 获取数据

        /// <summary>
        /// 获取领料计划页面
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTempPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = pickingMaterIBLL.GetTempPageList(paginationobj, queryJson);
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
            var Mes_CollarHeadData = pickingMaterIBLL.GetMes_CollarHeadEntity(keyValue);
            var Mes_CollarDetailData = pickingMaterIBLL.GetMes_CollarDetailEntityList(Mes_CollarHeadData.C_CollarNo);
            var jsonData = new
            {
                Mes_CollarHeadData = Mes_CollarHeadData,
                Mes_CollarDetailData = Mes_CollarDetailData,
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取领料计划
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTempFormData(string keyValue)
        {
            var Mes_CollarHeadTempData = pickingMaterIBLL.GetMes_CollarHeadTempEntity(keyValue);
            var Mes_CollarDetailTempData = pickingMaterIBLL.GetMes_CollarDetailTempEntity(Mes_CollarHeadTempData.C_CollarNo);
            var jsonData = new
            {
                Mes_CollarHeadTempData = Mes_CollarHeadTempData,
                Mes_CollarDetailTempData = Mes_CollarDetailTempData,
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
                return Fail("领料数量只能是大于0的实数");
            }
            if (mes_CollarDetailEntityList.Any(c => c.C_Qty2 <= 0))
            {
                return Fail("包装数量只能是大于0的实数");
            }
            foreach (var goods in mes_CollarDetailEntityList)
            {
               var stock= invSeachIbll.GetEntityBy(goods.C_GoodsCode, goods.C_StockCode, goods.C_Batch);
               if (stock==null)
               {
                   return Fail("[" + goods.C_GoodsName + "]" + "库存不存在！");
               }
               if (goods.C_Qty>stock.I_Qty)
               {
                   return Fail(goods.C_GoodsName + "不存在或库存不足");
               }
            }

            pickingMaterIBLL.SaveEntity(keyValue, entity, mes_CollarDetailEntityList);
            return Success("保存成功！");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult AutoCreateOrder(string date)
        {
            string message = "";
            bool datas = pickingMaterIBLL.AutoCreateOrder(date, out message);
            if (datas)
            {
                return Success("生成成功");
            }
            else
            {
                return Fail(message);
            }
        }
        #endregion

        #region 报表数据

        /// <summary>
        /// 转换率报表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GoodsConvertReport()
        {
            return View();
        }

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetReportData(string queryJson)
        {
            string messsage = "";
            DataTable dt = pickingMaterIBLL.GetProductReportData(queryJson, out messsage);
            if (string.IsNullOrEmpty(messsage))
            {
                #region 添加合计、统计行
                if (dt != null && dt.Rows.Count > 0)
                {
                    //插入统计行
                    if (true)
                    {
                        string current = dt.Rows[0]["F_CreateDate"].ToString();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string last = dt.Rows[i]["F_CreateDate"].ToString();
                            if (current != last)
                            {
                                DataRow dr = dt.NewRow();
                                dr["F_CreateDate"] = "[" + current + "]合计";
                                dt.Rows.InsertAt(dr, i);

                                current = last;
                                i++;
                            }
                        }
                        DataRow drEnd = dt.NewRow();
                        drEnd["F_CreateDate"] = "[" + current + "]合计";
                        dt.Rows.InsertAt(drEnd, dt.Rows.Count);

                        DataRow drSum = dt.NewRow();
                        drSum["F_CreateDate"] = "总计";
                        dt.Rows.InsertAt(drSum, dt.Rows.Count);
                    }

                    //计算统计行
                    if (true)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            //统计数量
                            if (dt.Columns[j].ColumnName.Contains("F_GoodsQty"))
                            {

                                decimal everysum_qty = 0;
                                decimal totalsum_qty = 0;
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    string current = dt.Rows[i]["F_CreateDate"].ToString();
                                    if (current.Contains("合计"))
                                    {
                                        dt.Rows[i][j] = Math.Round(everysum_qty, 2);
                                        everysum_qty = 0;
                                    }
                                    else
                                    {
                                        if (current == "总计")
                                        {
                                            dt.Rows[i][j] = Math.Round(totalsum_qty, 2);
                                            everysum_qty = 0;
                                        }
                                        else
                                        {
                                            if (dt.Rows[i][j] == DBNull.Value)
                                            {
                                                everysum_qty += 0;
                                                totalsum_qty += 0;
                                            }
                                            else
                                            {
                                                everysum_qty += decimal.Parse(dt.Rows[i][j].ToString());
                                                totalsum_qty += decimal.Parse(dt.Rows[i][j].ToString());
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                } 
                #endregion

                return Success(dt);
            }
            else
            {
                return Fail(messsage);
            }
        }



        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult GetReportTitle(string queryJson)
        {
            string messsage = "";
            var jsonData = pickingMaterIBLL.GetProductReportTitle(queryJson, out messsage);
            if (string.IsNullOrEmpty(messsage))
                return Success(jsonData);
            else
            {
                return Fail(messsage);
            }
        }


        /// <summary>
        /// 获取领料统计报表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetCollarRport(string queryJson)
        {
      
            DataTable dt = pickingMaterIBLL.GetCollarRport(queryJson);
          
            #region 添加合计、统计行
            if (dt != null && dt.Rows.Count > 0)
            {
                //插入统计行
                if (true)
                {
                    string current = dt.Rows[0]["F_GoodsName"].ToString();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string last = dt.Rows[i]["F_GoodsName"].ToString();
                        if (current != last)
                        {
                            DataRow dr = dt.NewRow();
                            dr["F_GoodsName"] = "[" + current + "]合计";
                            dt.Rows.InsertAt(dr, i);

                            current = last;
                            i++;
                        }
                    }
                    DataRow drEnd = dt.NewRow();
                    drEnd["F_GoodsName"] = "[" + current + "]合计";
                    dt.Rows.InsertAt(drEnd, dt.Rows.Count);

                    DataRow drSum = dt.NewRow();
                    drSum["F_GoodsName"] = "总计";
                    dt.Rows.InsertAt(drSum, dt.Rows.Count);
                }

                //计算统计行
                if (true)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        //统计数量
                        if (dt.Columns[j].ColumnName.Contains("qty"))
                        {

                            decimal everysum_qty = 0;
                            decimal totalsum_qty = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string current = dt.Rows[i]["F_GoodsName"].ToString();
                                if (current.Contains("合计"))
                                {
                                    dt.Rows[i][j] = Math.Round(everysum_qty, 2);
                                    everysum_qty = 0;
                                }
                                else
                                {
                                    if (current == "总计")
                                    {
                                        dt.Rows[i][j] = Math.Round(totalsum_qty, 2);
                                        everysum_qty = 0;
                                    }
                                    else
                                    {
                                        if (dt.Rows[i][j] == DBNull.Value)
                                        {
                                            everysum_qty += 0;
                                            totalsum_qty += 0;
                                        }
                                        else
                                        {
                                            everysum_qty += decimal.Parse(dt.Rows[i][j].ToString());
                                            totalsum_qty += decimal.Parse(dt.Rows[i][j].ToString());
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            #endregion

            return Success(dt);
        }


        /// <summary>
        /// 获取其他出库单报表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetOtherRport(string queryJson)
        {

            DataTable dt = pickingMaterIBLL.GetCollarRport(queryJson);

            #region 添加合计、统计行
            if (dt != null && dt.Rows.Count > 0)
            {
                //插入统计行
                if (true)
                {
                    string current = dt.Rows[0]["F_GoodsName"].ToString();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string last = dt.Rows[i]["F_GoodsName"].ToString();
                        if (current != last)
                        {
                            DataRow dr = dt.NewRow();
                            dr["F_GoodsName"] = "[" + current + "]合计";
                            dt.Rows.InsertAt(dr, i);

                            current = last;
                            i++;
                        }
                    }
                    DataRow drEnd = dt.NewRow();
                    drEnd["F_GoodsName"] = "[" + current + "]合计";
                    dt.Rows.InsertAt(drEnd, dt.Rows.Count);

                    DataRow drSum = dt.NewRow();
                    drSum["F_GoodsName"] = "总计";
                    dt.Rows.InsertAt(drSum, dt.Rows.Count);
                }

                //计算统计行
                if (true)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        //统计数量
                        if (dt.Columns[j].ColumnName.Contains("qty"))
                        {

                            decimal everysum_qty = 0;
                            decimal totalsum_qty = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                string current = dt.Rows[i]["F_GoodsName"].ToString();
                                if (current.Contains("合计"))
                                {
                                    dt.Rows[i][j] = Math.Round(everysum_qty, 2);
                                    everysum_qty = 0;
                                }
                                else
                                {
                                    if (current == "总计")
                                    {
                                        dt.Rows[i][j] = Math.Round(totalsum_qty, 2);
                                        everysum_qty = 0;
                                    }
                                    else
                                    {
                                        if (dt.Rows[i][j] == DBNull.Value)
                                        {
                                            everysum_qty += 0;
                                            totalsum_qty += 0;
                                        }
                                        else
                                        {
                                            everysum_qty += decimal.Parse(dt.Rows[i][j].ToString());
                                            totalsum_qty += decimal.Parse(dt.Rows[i][j].ToString());
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            #endregion

            return Success(dt);
        }
        #endregion
    }
}
