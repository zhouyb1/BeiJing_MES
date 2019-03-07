using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using Ayma.Application.TwoDevelopment.MesDev;

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
        public List<TreeModel> GetProceTreeList()
        {
            try
            {
                List<Mes_ProceEntity> proceTreeList = toolsService.GetProceTreeList();
                List<TreeModel> treeList = new List<TreeModel>();
                foreach (var item in proceTreeList)
                {
                    TreeModel node = new TreeModel();
                    node.id = item.ID;
                    node.text = item.P_ProName;
                    node.value = item.P_RecordCode;
                    node.showcheck = false;
                    node.checkstate = 0;
                    node.isexpand = true;
                    node.parentId = item.P_ParentId;
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
        /// 获取工序列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_ProceEntity> GetProceList(string parentId)
        {
            try
            {
                return toolsService.GetProceList(parentId);
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
        /// 根据工艺代码获取工序表实体
        /// <param name="code">工艺代码</param>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_ProceEntity> GetProceListBy(string code)
        {
            try
            {
                return toolsService.GetProceListBy(code);
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
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="names">名称</param>
        /// <returns></returns>
        public bool IsName(string tables,string field, string names)
        {
            try
            {
                return toolsService.IsName(tables,field, names);
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
        /// 编码重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public bool IsCode(string tables,string field, string code)
        {
            try
            {
                return toolsService.IsCode(tables,field, code);
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
