using System.Text;
using Ayma.Application.TwoDevelopment.MesDev.GoodsInfo;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using Ayma.Application.TwoDevelopment.MesDev;
using Ayma.Application.Organization;
using Microsoft.JScript;

namespace Ayma.Application.TwoDevelopment.Tools
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2018-10-09 10:32
    /// 描 述：商家
    /// </summary>
    public partial class ToolsBLL : ToolsIBLL
    {
        private  ToolsService toolsService=new ToolsService();

        #region 获取数据
        /// <summary>
        /// 根据仓库编码获取仓库实体信息
        /// <param name="code">仓库编码</param>
        /// </summary>
        /// <returns></returns>
        public Mes_StockEntity ByCodeGetStockEntity(string code)
        {
            try
            {
                return toolsService.ByCodeGetStockEntity(code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
      
        /// <summary>
        /// 获取配方表树形数据
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetBomRecordTree()
        {
            try
            {
                List<Mes_BomRecordEntity> bomRecordList = toolsService.GetBomRecordList();
                List<TreeModel> treeList = new List<TreeModel>();
                foreach (var item in bomRecordList)
                {
                    TreeModel node = new TreeModel();
                    node.id = item.ID;
                    node.text = item.B_GoodsName;
                    node.value = item.B_GoodsCode;
                    node.showcheck = false;
                    node.checkstate = 0;
                    node.isexpand = true;
                    node.parentId = item.B_ParentID;
                    treeList.Add(node);
                }
                return treeList.ToTree();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }  
        /// <summary>
        /// 获取工序表树形列表
        /// </summary>
        /// <returns></returns>
        //public List<TreeModel> GetProceTreeList()
        //{
        //    try
        //    {
        //        List<Mes_ProceEntity> proceTreeList = toolsService.GetProceTreeList();
        //        List<TreeModel> treeList = new List<TreeModel>();
        //        foreach (var item in proceTreeList)
        //        {
        //            TreeModel node = new TreeModel();
        //            node.id = item.ID;
        //            node.text = item.P_ProName;
        //            node.value = item.P_RecordCode;
        //            node.showcheck = false;
        //            node.checkstate = 0;
        //            node.isexpand = true;
        //            node.parentId = item.P_ParentId;
        //            treeList.Add(node);
        //        }
        //        return treeList.ToTree();
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is ExceptionEx)
        //        {
        //            throw;
        //        }
        //        else
        //        {
        //            throw ExceptionEx.ThrowBusinessException(ex);
        //        }
        //    }
        //}
        /// <summary>
        /// 根据参数获取仓库列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_StockEntity> GetStockListByParam(string strWhere)
        {
            try
            {
                return toolsService.GetStockListByParam(strWhere);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        } 
        /// <summary>
        /// 获取仓库列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_StockEntity> GetStockList()
        {
            try
            {
                return toolsService.GetStockList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        } 
        /// <summary>
        /// 获取非成品仓库列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_StockEntity> GetNoProjStockList()
        {
            try
            {
                return toolsService.GetNoProjStockList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        } 
        /// <summary>
        /// 获取线边仓仓库列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_StockEntity> GetLineStockList()
        {
            try
            {
                return toolsService.GetLineStockList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        } 
        /// <summary>
        /// 获取成品仓库列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_StockEntity> GetProjStockList()
        {
            try
            {
                return toolsService.GetProjStockList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
      
        /// <summary>
        /// 获取工艺列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_RecordEntity> GetRecordList()
        {
            try
            {
                return toolsService.GetRecordList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 根据车间编码获取车间实体信息
        /// </summary>
        /// <param name="code">车间编码</param>
        /// <returns></returns>
        public Mes_WorkShopEntity ByCodeGetWorkShopEntity(string code)
        {
            try
            {
                return toolsService.ByCodeGetWorkShopEntity(code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 根据车间名称获取车间实体信息
        /// </summary>
        /// <param name="name">车间名称</param>
        /// <returns></returns>
        public Mes_WorkShopEntity ByNameGetWorkShopEntity(string name)
        {
            try
            {
                return toolsService.ByNameGetWorkShopEntity(name);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取工序列表
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<Mes_ProceEntity> GetProceList(string parentId)
        //{
        //    try
        //    {
        //        return toolsService.GetProceList(parentId);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is ExceptionEx)
        //        {
        //            throw;
        //        }
        //        else
        //        {
        //            throw ExceptionEx.ThrowBusinessException(ex);
        //        }
        //    }
        //}
        /// <summary>
        /// 根据物料编码获取物料实体信息
        /// <param name="code">物料编码</param>
        /// </summary>
        /// <returns></returns>
        public Mes_GoodsEntity ByCodeGetGoodsEntity(string code)
        {
            try
            {
                return toolsService.ByCodeGetGoodsEntity(code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 根据物料名称获取物料实体信息
        /// <param name="name">物料名称</param>
        /// </summary>
        /// <returns></returns>
        public Mes_GoodsEntity ByNameGetGoodsEntity(string name)
        {
            try
            {
                return toolsService.ByNameGetGoodsEntity(name);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 根据工艺代码获取工序表实体
        /// <param name="code">工艺代码</param>
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<Mes_ProceEntity> GetProceListBy(string code)
        //{
        //    try
        //    {
        //        return toolsService.GetProceListBy(code);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is ExceptionEx)
        //        {
        //            throw;
        //        }
        //        else
        //        {
        //            throw ExceptionEx.ThrowBusinessException(ex);
        //        }
        //    }
        //}
        /// <summary>
        /// 工具工艺代码获取工序实体
        /// </summary>
        /// <param name="code">工艺代码</param>
        /// <returns></returns>
        public IEnumerable<Mes_ProceEntity> ByCodeGetProceEntity(string code)
        {
            try
            {
                return toolsService.ByCodeGetProceEntity(code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取物料列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_GoodsEntity> GetGoodsList()
        {
            try
            {
                return toolsService.GetGoodsList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        } 
        /// <summary>
        /// 获取成品物料列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_GoodsEntity> GetProjGoodsList()
        {
            try
            {
                return toolsService.GetProjGoodsList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取不合格原因列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_ResonEntity> GetReasonList()
        {
            try
            {
                return toolsService.GetReasonList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取商品二级分类列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_GoodKindEntity> GetGoodsKind()
        {
            try
            {
                return toolsService.GetGoodsKind();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 根据编码获取商品二级分类实体
        /// </summary>
        /// <returns></returns>
        public Mes_GoodKindEntity GetGoodsKindEntityBy(string code)
        {
            try
            {
                return toolsService.GetGoodsKindEntityBy(code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 根据门编码获取门实体信息
        /// </summary>
        /// <param name="code">门编码</param>
        /// <returns></returns>
        public Mes_DoorEntity ByCodeGetDoorEntity(string code)
        {
            try
            {
                return toolsService.ByCodeGetDoorEntity(code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_DoorEntity> GetDoorList()
        {
            try
            {
                return toolsService.GetDoorList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 根据主键获取供应商实体信息
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public Mes_SupplyEntity ByCodeGetSupplyEntity(string code)
        {
            try
            {
                return toolsService.ByCodeGetSupplyEntity(code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        } 
        /// <summary>
        /// 根据名字获取用户实体信息
        /// </summary>
        /// <param name="name">名字</param>
        /// <returns></returns>
        public UserEntity ByNameGetUserEntity(string name)
        {
            try
            {
                return toolsService.ByNameGetUserEntity(name);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_SupplyEntity> GetSupplyList()
        {
            try
            {
                return toolsService.GetSupplyList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetUserList()
        {
            try
            {
                return toolsService.GetUserList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取用户列表(超级管理员除外)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetUserNoSysList()
        {
            try
            {
                return toolsService.GetUserNoSysList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取班次列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_ClassEntity> GetClassList()
        {
            try
            {
                return toolsService.GetClassList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="names">名称</param>
        /// <param name="keyValue">主键Id</param>
        /// <returns></returns>
        public bool IsName(string tables, string field, string names, string keyValue)
        {
            try
            {
                return toolsService.IsName(tables,field, names,keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 单号重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="orderNo">单号</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public bool IsOrderNo(string tables,string field, string orderNo)
        {
            try
            {
                return toolsService.IsOrderNo(tables, field,orderNo);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 排班重复验证
        /// </summary>
        /// <param name="A_F_EnCode">用户编码</param>
        /// <param name="A_ClassCode">班次</param>
        /// <param name="A_Date">日期</param>
        /// <returns></returns>
        public bool IsExistRecord(string keyValue,string A_F_EnCode, string A_ClassCode, DateTime A_Date)
        {
            try
            {
                return toolsService.IsExistRecord(keyValue,A_F_EnCode, A_ClassCode, A_Date);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 编码重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="code">编码</param>
        /// <param name="keyValue">主键Id</param>
        /// <returns></returns>
        public bool IsCode(string tables, string field, string code, string keyValue)
        {
            try
            {
                return toolsService.IsCode(tables, field, code, keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取配方列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_BomRecordEntity> GetBomList(string goodsCode)
        {
            try
            {
                return toolsService.GetBomList(goodsCode);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取原物料code
        /// </summary>
        /// <returns></returns>
        public Mes_GoodsEntity GetCode(string goodsCode)
        {
            try
            {
                return toolsService.GetCode(goodsCode);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取车间
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_WorkShopEntity> GetWorkShopList()
        {
            try
            {
               return  toolsService.GetWorkShopList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取生产订单号列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_ProductOrderHeadEntity> GetProductOrderList(string orderNo)
        {
            try
            {
                return toolsService.GetProductOrderList(orderNo);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 获取Mes_Convert表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_GoodsEntity GetMes_ConvertEntity(string goodsCode)
        {
            try
            {
                return toolsService.GetMes_ConvertEntity(goodsCode);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取订单里的商品
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public IEnumerable<Mes_ProductOrderDetailEntity> GetOrderGoodsList(string orderNo)
        {
            try
            {
                return toolsService.GetOrderGoodsList(orderNo);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取订单商品实体
        /// </summary>
        /// <param name="goodsCode"></param>
        /// <returns></returns>
        public Mes_ProductOrderDetailEntity GetOrderGoodsEntity(string goodsCode)
        {
            try
            {
                return toolsService.GetOrderGoodsEntity(goodsCode);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取班组
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_TeamEntity> GetTeamList()
        {
            try
            {
                return toolsService.GetTeamList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取条码(成品)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_ScanCodeEntity> GetBarCodeList()
        {
            try
            {
                return toolsService.GetBarCodeList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        #endregion

        #region 提交数据

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        public void AuditingBill(string keyValue, string tables, string field)
        {
            try
            {
                toolsService.AuditingBill(keyValue,tables,field);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 提交单据,撤销单据
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <param name="proc">存储过程</param>
        /// <param name="errMsg">错误信息</param>
        public int PostOrCancelOrDeleteBill(string orderNo, string proc, out string errMsg)
        {
            try
            {
                return toolsService.PostOrCancelOrDeleteBill(orderNo, proc, out errMsg);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }  
        /// <summary>
        /// 提交单据,撤销单据,删除单据(入库单)
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <param name="proc">存储过程</param>
        /// <param name="errMsg">错误信息</param>
        public int PostOrCancelOrDeleteMaterInBill(string orderNo, string proc, out string errMsg)
        {
            try
            {
                return toolsService.PostOrCancelOrDeleteMaterInBill(orderNo, proc, out errMsg);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        #endregion
    }
}
