﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Ayma.Application.Organization;
using Ayma.Util;
using Ayma.Application.TwoDevelopment.MesDev;
using System.Web.Mvc;
using System.Collections.Generic;
using Ayma.Application.Base.SystemModule;
using Ayma.Application.TwoDevelopment;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 14:58
    /// 描 述：入库单制作
    /// </summary>
    public partial class MaterInBillController : MvcControllerBase
    {
        private MaterInBillIBLL materInBillIBLL = new MaterInBillBLL();
        private UserIBLL useribll = new UserBLL();
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

        public ActionResult MaterRateIndex()
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
        /// 提交查询页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostIndex()
        {
            return View();
        }
        /// <summary>
        /// 成品入库单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProductIndex()
        {
            return View();
        }

        /// <summary>
        /// 成品入库单表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ProductForm()
        {
            if (Request["keyValue"] == null)
            {
                ViewBag.OrderNo = new CodeRuleBLL().GetBillCode(((int)ErpEnums.OrderNoRuleEnum.ProjectMaterIn).ToString());//自动获取主编码
            }
            return View();
        }
        /// <summary>
        /// 入库商品列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GoodsListIndex()
        {
            return View();
        }
        /// <summary>
        /// 成品入库商品列表页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GoodsProductListIndex()
        {
            return View();
        }
        /// <summary>
        /// 成品入库单查询表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostProductForm()
        {
            return View();
        }

        /// <summary>
        /// 成品入库查询页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PostProductIndex()
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
        /// 报表页2
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintReport2()
        {
            return View();
        }
        /// <summary>
        /// 报表页3
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PrintReport3()
        {
            return View();
        }
        /// <summary>
        /// 供应商存货明细
        /// </summary>
        /// <returns></returns>
        public ActionResult SupplyGoodsListIndex()
        {
            return View();
        }

        #endregion

        /// <summary>
        /// 原物料入库列表详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MaterInSumIndex()
        {
            return View();
        }

        #region 获取数据
        /// <summary>
        /// 获取成品入库商品列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="keyword">编码/名称搜索</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetProductList(string pagination, string queryJson, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materInBillIBLL.GetProductGoodsList(paginationobj, queryJson, keyword);
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
        /// 获取非成品入库商品列表(非成品:原材料/半成品)
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="keyword">编码/名称搜索</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsList(string pagination, string queryJson, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materInBillIBLL.GetGoodsList(paginationobj, queryJson, keyword);
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
        /// 获取原物料入库列表详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterInDetail(string queryJson)
        {
            var dt = materInBillIBLL.GetMaterInDetailSum(queryJson);
            #region 舍去
            //if (dt.Rows.Count > 0)
            //{
            //    //计算合计列
            //    if (true)
            //    {
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            decimal everysum_qty = 0;
            //            decimal everysum_amount = 0;



            //            for (int j = 0; j < dt.Columns.Count; j++)
            //            {

            //                if (dt.Columns[j].ColumnName.IndexOf("_qty") > 0)
            //                {
            //                    if (dt.Rows[i][j] == DBNull.Value)
            //                    {
            //                        everysum_qty += 0;
            //                    }
            //                    else
            //                    {
            //                        everysum_qty += decimal.Parse(dt.Rows[i][j].ToString());
            //                    }
            //                }
            //                if (dt.Columns[j].ColumnName.IndexOf("_") > 0)
            //                {
            //                    if (dt.Rows[i][j] == DBNull.Value)
            //                    {
            //                        everysum_qty += 0;
            //                    }
            //                    else
            //                    {
            //                        everysum_qty += decimal.Parse(dt.Rows[i][j].ToString());
            //                    }
            //                }
            //                if (dt.Columns[j].ColumnName.IndexOf("_amount") > 0)
            //                {
            //                    if (dt.Rows[i][j] == DBNull.Value)
            //                    {
            //                        everysum_amount += 0;
            //                    }
            //                    else
            //                    {
            //                        everysum_amount += decimal.Parse(dt.Rows[i][j].ToString());
            //                    }
            //                }

            //                dt.Rows[i]["allqty"] = Math.Round(everysum_qty, 2);
            //                dt.Rows[i]["allamount"] = Math.Round(everysum_amount, 2);
            //            }
            //        }
            //    }
            //} 
            #endregion
            return Success(dt);
        }

        /// <summary>
        /// 渲染表头
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult GetPageTitle(string queryJson)
        {
            var headList = materInBillIBLL.GetPageTitle(queryJson);
            return Success(headList);
        }

        /// <summary>
        /// 获取供应商存货明细
        /// </summary>
        /// <returns></returns>
        public ActionResult GetSupplyGoodsList(string pagination,string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var dt = materInBillIBLL.GetSupplyGoodsList(queryJson);

            if (dt.Rows.Count > 0)
            {
                //insert统计行
                var supplyName = dt.Rows[0]["m_supplyname"].ToString();
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var newSupplyName = dt.Rows[i]["m_supplyname"].ToString();
                    if (supplyName != newSupplyName)
                    {
                        var dr = dt.NewRow();
                        dr["m_supplyname"] = "【" + supplyName + "】小计";
                        dt.Rows.InsertAt(dr, i);

                        supplyName = newSupplyName;
                        i++;
                    }
                }
                var endRow = dt.NewRow();
                endRow["m_supplyname"] = "【" + supplyName + "】小计";
                dt.Rows.InsertAt(endRow, dt.Rows.Count);
                //计算小计行
                for (var j = 0; j < dt.Columns.Count; j++)
                {
                    if (dt.Columns[j].ColumnName.LastIndexOf("_qty") > 0 ||
                        dt.Columns[j].ColumnName.LastIndexOf("_amount") > 0)
                    {
                        decimal total_qty = 0;
                        decimal total_amount = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string classname = dt.Rows[i]["m_supplyname"].ToString();
                            if (classname.LastIndexOf("小计") > 0)
                            {
                                dt.Rows[i][j] = Math.Round(total_qty, 2);
                                total_qty = 0;
                            }
                            else
                            {
                                if (dt.Rows[i][j] == DBNull.Value)
                                {
                                    total_qty += 0;
                                    total_amount += 0;
                                }
                                else
                                {
                                    total_qty += decimal.Parse(dt.Rows[i][j].ToString());
                                    total_amount += decimal.Parse(dt.Rows[i][j].ToString());
                                }

                            }

                        }
                    }
                }
            }
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
        /// 获取已提交的成品入库
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPostProductPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materInBillIBLL.GetPostProductPageList(paginationobj, queryJson);
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
        /// 获取成品入库显示数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetProductPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materInBillIBLL.GetProductPageList(paginationobj, queryJson);
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
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPostPageList(string pagination, string queryJson, string M_MaterInNo)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = materInBillIBLL.GetPostPageList(paginationobj, queryJson,M_MaterInNo);
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
            var data = materInBillIBLL.GetPageList(paginationobj, queryJson);
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
            var Mes_MaterInHeadData = materInBillIBLL.GetMes_MaterInHeadEntity(keyValue);
            var Mes_MaterInDetailData = materInBillIBLL.GetMes_MaterInDetailList(Mes_MaterInHeadData.M_MaterInNo);
            var jsonData = new
            {
                Mes_MaterInHeadData = Mes_MaterInHeadData,
                Mes_MaterInDetailData = Mes_MaterInDetailData,
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
            var Mes_MaterInHeadData = materInBillIBLL.GetMes_MaterInHeadEntity(keyValue);

            return Success(Mes_MaterInHeadData.M_OrderNo);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult AuditingBill(string keyValue)
        {
            materInBillIBLL.AuditingBill(keyValue);
            return Success("审核完成");
        }
        /// <summary>
        /// 撤销单据
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult CancelBill(string orderNo)
        {
            string errMsg = "";
            int status = materInBillIBLL.CancelBill(orderNo, out errMsg);
            if (status == 0)
            {
                return Success("撤销成功");
            }
            return Fail(errMsg);

        }
        /// <summary>
        /// 提交单据
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult PostBill(string orderNo)
        {
            string errMsg = "";
            int status = materInBillIBLL.PostBill(orderNo, out errMsg);
            if (status == 0)
            {
                return Success("提交成功");
            }
            return Fail(errMsg);

        }

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            materInBillIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="orderKind">入库单据类型(1,非成品 2,成品)</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, ErpEnums.OrderKindEnum orderKind, string strEntity,string strmes_MaterInDetailList)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var entityTemp = materInBillIBLL.GetMes_MaterInHeadEntity(keyValue);
                if (entityTemp.M_Status == ErpEnums.MaterInStatusEnum.Audit)
                {
                    return Fail("该单据已审核,不能修改！");
                }
            }
            Mes_MaterInHeadEntity entity = strEntity.ToObject<Mes_MaterInHeadEntity>();
            List<Mes_MaterInDetailEntity> mes_MaterInDetailList =
                strmes_MaterInDetailList.ToObject<List<Mes_MaterInDetailEntity>>();
            foreach (var item in mes_MaterInDetailList)
            {
                if (string.IsNullOrEmpty(item.M_Price.ToString()) || item.M_Price == 0)
                {
                    return Fail("物料【" + item.M_GoodsName + "】价格为空请及时维护价格!");
                }
                if (string.IsNullOrEmpty(item.M_Qty.ToString()) || item.M_Qty == 0)
                {
                    return Fail("物料【" + item.M_GoodsName + "】入库数量不能为空!");
                }
                if (string.IsNullOrEmpty(item.M_Qty2.ToString()) || item.M_Qty2 == 0)
                {
                    return Fail("物料【" + item.M_GoodsName + "】包装数量不能为空!");
                }  
            }
            entity.M_Status = ErpEnums.MaterInStatusEnum.NoAudit;
            entity.M_OrderKind = orderKind; //单据类型 成品与非成品
            materInBillIBLL.SaveEntity(keyValue, entity, mes_MaterInDetailList);
            return Success("保存成功！");
        }

        #endregion

    }
}
