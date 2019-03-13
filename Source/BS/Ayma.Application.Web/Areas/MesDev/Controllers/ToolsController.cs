using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    //公用控制器，执行重复调用的方法
    public class ToolsController : MvcControllerBase
    {
        private ToolsIBLL toosIBLL = new ToolsBLL();
        
        #region 获取数据
        /// <summary>
        /// 根据仓库编码获取仓库实体信息
        /// </summary>
        /// <param name="code">仓库编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ByCodeGetStockEntity(string code)
        {
            var stockEntity = toosIBLL.ByCodeGetStockEntity(code);
            return Success(stockEntity);
        }
        /// <summary>
        /// 获取配方表树形结构列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetBomRecordTree()
        {
            var bomRecordTree = toosIBLL.GetBomRecordTree();
            return Success(bomRecordTree);
        }  
        /// <summary>
        /// 获取仓库列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetStockList()
        {
            var stockList = toosIBLL.GetStockList();
            return Success(stockList);
        } 
        
        
        /// <summary>
        /// 根据物料编码获取物料实体信息
        /// </summary>
        /// <param name="code">物料编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ByCodeGetGoodsEntity(string code)
        {
            var goodsEntity = toosIBLL.ByCodeGetGoodsEntity(code);
            return Success(goodsEntity);
        }
        
        /// <summary>
        /// 获取车间列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetWorkShopList()
        {
            var workShopList = toosIBLL.GetWorkShopList();
            return Success(workShopList);
        }
        /// <summary>
        /// 获取物料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsList()
        {
            var goodsList = toosIBLL.GetGoodsList();
            return Success(goodsList);
        }
        /// <summary>
        /// 获取商品二级分类列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsKind()
        {
            var goodsKind = toosIBLL.GetGoodsKind();
            return Success(goodsKind);
        }
        /// <summary>
        /// 根据门编码获取门实体信息
        /// </summary>
        /// <param name="code">门编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ByCodeGetDoorEntity(string code)
        {
            var doorEntity = toosIBLL.ByCodeGetDoorEntity(code);
            return Success(doorEntity);
        }
        /// <summary>
        /// 获取门列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetDoorList()
        {
            var doorList = toosIBLL.GetDoorList();
            return Success(doorList);
        }
        /// <summary>
        /// 根据编码获取供应商实体信息
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ByCodeGetSupplyEntity(string code)
        {
            var supplyEntity = toosIBLL.ByCodeGetSupplyEntity(code);
            return Success(supplyEntity);
        }
        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetSupplyList()
        {
            var supplyList = toosIBLL.GetSupplyList();
            return Success(supplyList);
        }
        /// <summary>
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="names">名称</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult IsName(string tables,string field, string names)
        {
            var isName = toosIBLL.IsName(tables,field,names);
            return Success(isName);
        }
        /// <summary>
        /// 单号重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="orderNo">单号</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult IsOrderNo(string tables,string field, string orderNo)
        {
            var isOrderNo = toosIBLL.IsOrderNo(tables,field, orderNo);
            return Success(isOrderNo);
        }
        /// <summary>
        /// 编码重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult IsCode(string tables,string field, string code)
        {
            var isCode = toosIBLL.IsCode(tables,field, code);
            return Success(isCode);
        }

        /// <summary>
        /// 获取配方列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetBomList(string goodsCode)
        {
            var data = toosIBLL.GetBomList(goodsCode);
            return Success(data);
        }

        /// <summary>
        /// 获取原物料code
        /// </summary>
        /// <returns></returns>
        public ActionResult GetCode(string goodsCode)
        {
            var data = toosIBLL.GetCode(goodsCode);
            return Success(data);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        [HttpPost]
        [AjaxOnly]
        public ActionResult AuditingBill(string keyValue, string tables, string field)
        {
            toosIBLL.AuditingBill(keyValue,tables,field);
            return Success("审核完成");
        }
        /// <summary>
        /// 提交单据(入库单)
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <param name="proc">存储过程</param>
        /// <param name="type">操作类型：1，提交单据，2撤销单据,3删除单据</param>
        [HttpPost]
        [AjaxOnly]
        public ActionResult PostOrCancelOrDeleteMaterInBill(string orderNo, string proc, int type)
        {
            string errMsg = "";
            int status = toosIBLL.PostOrCancelOrDeleteMaterInBill(orderNo, proc, out errMsg);
            if (status == 0)
            {
                switch (type)
                {
                    case 1:
                        return Success("提交成功");
                        break;
                    case 2:
                        return Success("撤销成功");
                        break;
                    case 3:
                        return Success("删除成功");
                        break;                       
                }               
            }
            return Fail(errMsg);
        }
        /// <summary>
        /// 提交单据
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <param name="proc">存储过程</param>
        /// <param name="type">操作类型：1，提交单据，2撤销单据,3删除单据</param>
        [HttpPost]
        [AjaxOnly]
        public ActionResult PostOrCancelOrDeleteBill(string orderNo, string proc,int type)
        {
            string errMsg = "";
            int status = toosIBLL.PostOrCancelOrDeleteBill(orderNo, proc, out errMsg);
            if (status == 0)
            {
                switch (type)
                {
                    case 1:
                        return Success("提交成功");
                        break;
                    case 2:
                        return Success("撤销成功");
                        break;
                    case 3:
                        return Success("删除成功");
                        break;                       
                }               
            }
            return Fail(errMsg);
        }
        #endregion
	}
}