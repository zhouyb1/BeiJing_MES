using Ayma.Util;
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
    public interface ToolsIBLL
    {
        #region 获取数据
        /// <summary>
        /// 根据仓库编码获取仓库实体信息
        /// <param name="code">仓库编码</param>
        /// </summary>
        /// <returns></returns>
        Mes_StockEntity ByCodeGetStockEntity(string code);
        /// <summary>
        /// 获取配方表树形结构列表
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetBomRecordTree();
        /// <summary>
        /// 获取仓库列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_StockEntity> GetStockList();
        /// <summary>
        /// 获取工序列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_ProceEntity> GetProceList(string parentId);
        /// <summary>
        /// 获取配方表树形结构列表
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetProceTreeList();
        /// <summary>
        /// 根据物料编码获取物料实体信息
        /// <param name="code">物料编码</param>
        /// </summary>
        /// <returns></returns>
        Mes_GoodsEntity ByCodeGetGoodsEntity(string code);
        /// <summary>
        /// 根据工艺代码获取工序表实体
        /// <param name="code">工艺代码</param>
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_ProceEntity> GetProceListBy(string code);
        /// <summary>
        /// 获取物料
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_GoodsEntity> GetGoodsList(); 
        /// <summary>
        /// 获取商品二级分类列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_GoodKindEntity> GetGoodsKind();
        /// <summary>
        /// 根据门编码获取门实体信息
        /// </summary>
        /// <param name="code">门编码</param>
        /// <returns></returns>
        Mes_DoorEntity ByCodeGetDoorEntity(string code);
        /// <summary>
        /// 获取门列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_DoorEntity> GetDoorList();
        /// <summary>
        /// 根据编码获取供应商实体信息
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        Mes_SupplyEntity ByCodeGetSupplyEntity(string code);
        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_SupplyEntity> GetSupplyList();
        /// <summary>
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="names">名称</param>
        /// <returns></returns>
        bool IsName(string tables,string field, string names);
        /// <summary>
        /// 单号重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="orderNo">单号</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        bool IsOrderNo(string tables, string field,string orderNo);
        /// <summary>
        /// 编码重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        bool IsCode(string tables,string field,string code);
        #endregion

        #region 提交数据

        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        void AuditingBill(string keyValue, string tables, string field);
        /// <summary>
        /// 提交单据,撤销单据
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <param name="proc">存储过程</param>
        /// <param name="errMsg">错误信息</param>
        int PostOrCancelOrDeleteBill(string orderNo, string proc, out string errMsg);
        /// <summary>
        /// 提交单据,撤销单据,删除单据(入库单)
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <param name="proc">存储过程</param>
        /// <param name="errMsg">错误信息</param>
        int PostOrCancelOrDeleteMaterInBill(string orderNo, string proc, out string errMsg);

        #endregion

    }
}
