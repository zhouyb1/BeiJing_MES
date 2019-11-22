using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ayma.Application.TwoDevelopment.MesDev;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Web.Areas.MesDev.Controllers
{
    //公用控制器，执行重复调用的方法
    public class ToolsController : MvcControllerBase
    {
        private ToolsIBLL toosIBLL = new ToolsBLL();
        private Mes_ProductOrderHeadIBLL mesProductOrderHead=new Mes_ProductOrderHeadBLL();
        #region 获取数据
        /// <summary>
        /// 根据仓库编码或名称获取仓库实体信息
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
        /// 根据班组编码或名称获取班组实体信息
        /// </summary>
        /// <param name="code">班组编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ByCodeGetTeamEntity(string code)
        {
            var stockEntity = toosIBLL.ByCodeGetTeamEntity(code);
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
        /// 获取物料转换实体
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMesConverEntity(string goodsCode)
        {
            var entity = toosIBLL.GetMes_ConvertEntity(goodsCode);
            return Success(entity);
        }

      
        /// <summary>
        /// 根据参数获取仓库列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetStockListByParam(string strWhere)
        {
            var stockList = toosIBLL.GetStockListByParam(strWhere);
            return Success(stockList);
        }
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetCustomerList()
        {
            var Customer = toosIBLL.GetCustomerList();
            return Success(Customer);
        }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetDepartmentList()
        {
            var Department = toosIBLL.GetDepartmentList();
            return Success(Department);
        }
        /// <summary>
        /// 根据参数获取部门列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ByCodeGetDepartmentEntity(string code)
        {
            var Department = toosIBLL.ByCodeGetDepartmentEntity(code);
            return Success(Department);
        }
        /// <summary>
        /// 获取原物料仓库列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetOriginalStockList()
        {
            var stockList = toosIBLL.GetOriginalStockList();
            return Success(stockList);
        }
        /// <summary>
        /// 获取所有仓库列表
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
        /// 获取原料和半成品列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetOtherStockList()
        {
            var stockList = toosIBLL.GetOtherStockList();
            return Success(stockList);
        }

        /// <summary>
        /// 获取非成品仓库列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetNoProjStockList()
        {
            var stockList = toosIBLL.GetNoProjStockList();
            return Success(stockList);
        }  
        /// <summary>
        /// 获取线边仓仓库列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetLineStockList()
        {
            var stockList = toosIBLL.GetLineStockList();
            return Success(stockList);
        } 
        /// <summary>
        /// 获取成品仓库列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetProjStockList()
        {
            var stockList = toosIBLL.GetProjStockList();
            return Success(stockList);
        }
        /// <summary>
        /// 获取车间列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetWorkShopList()
        {
            var workshopList = toosIBLL.GetWorkShopList();
            return Success(workshopList);
        }
        /// <summary>
        /// 获取生产订单号列表
        /// </summary>
        /// <param name="orderNo">生产订单号</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetProductOrderList(string orderNo)
        {
            
            var productorderList = toosIBLL.GetProductOrderList(orderNo);
           
            return Success(productorderList);
        }
        /// <summary>
        /// 根据订单时间起止获取生产订单号列表
        /// </summary>
        /// <param name="orderStartDate">订单开始时间</param>
        /// <param name="orderEndDate">订单结束时间</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetProductOrderListBy(DateTime orderStartDate,DateTime orderEndDate)
        {

            var productorderList = toosIBLL.GetProductOrderListBy(orderStartDate, orderEndDate);
           
            return Success(productorderList);
        }
        /// <summary>
        /// 根据车间编码获取车间实体信息
        /// </summary>
        /// <param name="code">车间编码</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ByCodeGetWorkShopEntity(string code)
        {
            var workshopEntity = toosIBLL.ByCodeGetWorkShopEntity(code);
            return Success(workshopEntity);
        }
        /// <summary>
        /// 根据车间名称获取车间实体信息
        /// </summary>
        /// <param name="name">车间名称</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ByNameGetWorkShopEntity(string name)
        {
            var workshopEntity = toosIBLL.ByNameGetWorkShopEntity(name);
            return Success(workshopEntity);
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
        /// 根据物料名称获取物料实体信息
        /// </summary>
        /// <param name="name">物料名称</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ByNameGetGoodsEntity(string name)
        {
            var goodsEntity = toosIBLL.ByNameGetGoodsEntity(name);
            return Success(goodsEntity);
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
        /// 根据供应商获取物料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsListBySupplyName(string G_SupplyName)
        {
            var goodsList = toosIBLL.GetGoodsListBySupplyName(G_SupplyName);
            return Success(goodsList);
        } 
        /// <summary>
        /// 获取成品物料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetProjGoodsList()
        {
            var goodsList = toosIBLL.GetProjGoodsList();
            return Success(goodsList);
        }
        /// <summary>
        /// 获取原物料列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMaterialGoodsList()
        {
            var goodsList = toosIBLL.GetMaterialGoodsList();
            return Success(goodsList);
        }
        /// <summary>
        /// 获取不合格原因列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetReasonList()
        {
            var reasonList = toosIBLL.GetReasonList();
            return Success(reasonList);
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
        /// 获取班组列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTeamList()
        {
            var TName = toosIBLL.GetTeamList();
            return Success(TName);
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetRoleList()
        {
            var Role = toosIBLL.GetRoleList();
            return Success(Role);
        } 
        /// <summary>
        /// 根据编码获取商品二级分类实体
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetGoodsKindEntityBy(string code)
        {
            var goodsKind = toosIBLL.GetGoodsKindEntityBy(code);
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
        /// 获取工艺列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetRecordList()
        {
            var recordList = toosIBLL.GetRecordList();
            return Success(recordList);
        }
        /// <summary>
        /// 获取工序号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ByCodeGetProceEntity(String Code)
        {
            var recordList = toosIBLL.ByCodeGetProceEntity(Code);
            return Success(recordList);
        } 
        /// <summary>
        /// 获取工序列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetProceList()
        {
            var proceEntity = toosIBLL.GetProceList();
            return Success(proceEntity);
        } 
        /// <summary>
        /// 根据(工序号或工序名称)获取工序实体
        /// </summary>
        /// <param name="code">工序号或工序名称</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ByGetProceEntity(string code)
        {
            var proceEntity = toosIBLL.ByGetProceEntity(code);
            return Success(proceEntity);
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
        /// 根据名称获取供应商实体信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ByNameGetSupplyEntity(string name)
        {
            var supplyEntity = toosIBLL.ByNameGetSupplyEntity(name);
            return Success(supplyEntity);
        }
        /// <summary>
        /// 根据名字获取用户实体信息
        /// </summary>
        /// <param name="name">名字</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult ByNameGetUserEntity(string name)
        {
            var userEntity = toosIBLL.ByNameGetUserEntity(name);
            return Success(userEntity);
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
        /// 获取资质有效期的供应商列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetEffectSupplyList()
        {
            var supplyList = toosIBLL.GetEffectSupplyList();
            return Success(supplyList);
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetUserList()
        {
            var userList = toosIBLL.GetUserList();
            return Success(userList);
        } 
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetUserNoSysList()
        {
            var userList = toosIBLL.GetUserNoSysList();
            return Success(userList);
        }
        /// <summary>
        /// 获取班次列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetClassList()
        {
            var classList = toosIBLL.GetClassList();
            return Success(classList);
        }
        /// <summary>
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="names">名称</param>
        /// <param name="keyValue">主键Id</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult IsName(string tables,string field, string names,string keyValue)
        {
            var isName = toosIBLL.IsName(tables, field, names, keyValue);
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
        /// <param name="keyValue">主键Id</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult IsCode(string tables, string field, string code, string keyValue)
        {
            var isCode = toosIBLL.IsCode(tables, field, code, keyValue);
            return Success(isCode);
        }

        /// <summary>
        /// 获取配方列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetBomList(string goodsCode)
        {
            var data = toosIBLL.GetBomList(goodsCode);
            return Success(data);
        }

        /// <summary>
        /// 获取原物料code
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetCode(string goodsCode)
        {
            var data = toosIBLL.GetCode(goodsCode);
            return Success(data);
        }

        /// <summary>
        /// 获取成品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProductList(string stockCode)
        {
            var data = toosIBLL.GetProductList(stockCode);
            return Success(data);
        }

        /// <summary>
        /// 获取订单里的商品
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderGoodsList(string orderNo)
        {
            var data = toosIBLL.GetOrderGoodsList(orderNo);
            return Success(data);
        }

        /// <summary>
        /// 获取订单里的商品
        /// </summary>
        /// <returns></returns>
        public ActionResult GetOrderGoodEntity(string goodsCode)
        {
            var data = toosIBLL.GetOrderGoodsEntity(goodsCode);
            return Success(data);
        }

        /// <summary>
        /// 获取商品条码列表(成品)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetBarCodeList()
        {
            var list = toosIBLL.GetBarCodeList();
            return Success(list);
        }
        /// <summary>
        /// 编码重复验证和供应商编码重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="code">编码</param>
        /// <param name="keyValue">主键Id</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult IsCodeAndSupplyCode(string tables, string field, string code, string field2, string code2, string keyValue)
        {
            var isCode = toosIBLL.IsCodeAndSupplyCode(tables, field, code,field2,code2, keyValue);
            return Success(isCode);
        }

        /// <summary>
        /// 获取成品批次
        /// </summary>
        /// <param name="goodsCode"></param>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public ActionResult GetProductBatchList(string goodsCode,string stockCode)
        {
            var data = toosIBLL.GetProductBatchList(goodsCode, stockCode);
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