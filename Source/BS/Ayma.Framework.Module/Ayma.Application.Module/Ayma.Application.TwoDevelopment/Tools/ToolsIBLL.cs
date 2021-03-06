﻿using System.Security.Cryptography.X509Certificates;
using Ayma.Application.TwoDevelopment.MesDev.GoodsInfo;
using Ayma.Util;
using System.Collections.Generic;
using System.Data;
using Ayma.Application.TwoDevelopment.MesDev;
using Ayma.Application.Organization;
using System;

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
        /// 根据物料编码获取包装规格数
        /// <param name="code">物料编码</param>
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_SpecsEntity> ByGoodsCodeGetUnit(string keyword);
        /// <summary>
        /// 根据部门编码获取部门实体信息
        /// <param name="code">仓库编码</param>
        /// </summary>
        /// <returns></returns>
        DepartmentEntity ByCodeGetDepartmentEntity(string code);
        /// <summary>
        /// 根据班组编码获取班组实体信息
        /// <param name="code">班组编码</param>
        /// </summary>
        /// <returns></returns>
        Mes_TeamEntity ByCodeGetTeamEntity(string code);
        /// <summary>
        /// 根据仓库编码获取仓库实体信息
        /// <param name="code">仓库编码</param>
        /// </summary>
        /// <returns></returns>
        Mes_StockEntity ByCodeGetStockEntity(string code);
        /// <summary>
        /// 根据车间编码获取车间实体信息
        /// </summary>
        /// <param name="code">车间编码</param>
        /// <returns></returns>
        Mes_WorkShopEntity ByCodeGetWorkShopEntity(string code); 
        /// <summary>
        /// 根据车间名称获取车间实体信息
        /// </summary>
        /// <param name="name">车间名称</param>
        /// <returns></returns>
        Mes_WorkShopEntity ByNameGetWorkShopEntity(string name);
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_CustomerEntity> GetCustomerList();
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<DepartmentEntity> GetDepartmentList();
        /// <summary>
        /// 获取配方表树形结构列表
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetBomRecordTree();
        /// <summary>
        /// 根据参数获取仓库列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_StockEntity> GetStockListByParam(string strWhere);
        /// <summary>
        /// 根据仓库获取物料列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_GoodsEntity> GetGoodsListByStock(string S_Code);
        /// <summary>
        /// 获取原物料仓库列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_StockEntity> GetOriginalStockList();
        /// <summary>
        /// 获取原物料仓库和半成品列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_StockEntity> GetOtherStockList();
        /// <summary>
        /// 获取所有仓库列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_StockEntity> GetStockList();
        /// <summary>
        /// 获取非成品仓库列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_StockEntity> GetNoProjStockList();   
        /// <summary>
        /// 获取线边仓仓库列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_StockEntity> GetLineStockList();  
        /// <summary>
        /// 获取成品仓库列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_StockEntity> GetProjStockList();
        /// <summary>
        /// 获取车间列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_WorkShopEntity> GetWorkShopList();
        /// <summary>
        /// 获取生产订单号列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_ProductOrderHeadEntity> GetProductOrderList(string orderNo);   
        /// <summary>
        /// 根据订单时间起止获取生产订单号列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_ProductOrderHeadEntity> GetProductOrderListBy(DateTime orderStartDate, DateTime orderEndDate);
        /// <summary>
        /// 获取工艺列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_RecordEntity> GetRecordList();
        /// <summary>
        /// 获取工序号
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_OrgResHeadEntity> ByCodeGetProceEntity(string Code);
        /// <summary>
        /// 获取工序列表
        /// </summary>
        /// <returns></returns>
        //IEnumerable<Mes_ProceEntity> GetProceList(string parentId);
        /// <summary>
        /// 获取配方表树形结构列表
        /// </summary>
        /// <returns></returns>
        //List<TreeModel> GetProceTreeList();
        /// <summary>
        /// 根据物料编码获取物料实体信息
        /// <param name="code">物料编码</param>
        /// </summary>
        /// <returns></returns>
        Mes_GoodsEntity ByCodeGetGoodsEntity(string code);
        /// <summary>
        /// 根据仓库编码获取物料实体信息
        /// <param name="code">物料编码</param>
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_GoodsEntity> ByStokcGetGoodsEntity(string code); 
        /// <summary>
        /// 根据物料名称获取物料实体信息
        /// <param name="name">物料名称</param>
        /// </summary>
        /// <returns></returns>
        Mes_GoodsEntity ByNameGetGoodsEntity(string name);
        /// <summary>
        /// 根据工艺代码获取工序表实体
        /// <param name="code">工艺代码</param>
        /// </summary>
        /// <returns></returns>
        //IEnumerable<Mes_ProceEntity> GetProceListBy(string code);
        /// <summary>
        /// 获取物料
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_GoodsEntity> GetGoodsList();
        /// <summary>
        /// 根据供应商获取物料
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_GoodsEntity> GetGoodsListBySupplyName(string G_SupplyName); 
        /// <summary>
        /// 获取成品物料
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_GoodsEntity> GetProjGoodsList();
        /// <summary>
        /// 获取原物料物料
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_GoodsEntity> GetMaterialGoodsList();
        /// <summary>
        /// 获取不合格原因实体
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_ResonEntity> GetReasonList();
        /// <summary>
        /// 获取商品二级分类列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_GoodKindEntity> GetGoodsKind(); 
        /// <summary>
        /// 根据编码获取商品二级分类实体
        /// </summary>
        /// <returns></returns>
        Mes_GoodKindEntity GetGoodsKindEntityBy(string code);
        /// <summary>
        /// 根据门编码获取门实体信息
        /// </summary>
        /// <param name="code">门编码</param>
        /// <returns></returns>
        Mes_DoorEntity ByCodeGetDoorEntity(string code);
        /// <summary>
        /// 获取工序实体
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_ProceEntity> GetProceList(); 
        /// <summary>
        /// 根据(工序号或工序名称)获取工序实体
        /// </summary>
        /// <param name="code">工序号</param>
        /// <returns></returns>
        Mes_ProceEntity ByGetProceEntity(string code);
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
        /// 根据名称获取供应商实体信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        Mes_SupplyEntity ByNameGetSupplyEntity(string name); 
        /// <summary>
        /// 根据名字获取用户实体信息
        /// </summary>
        /// <param name="name">名字</param>
        /// <returns></returns>
        UserEntity ByNameGetUserEntity(string name);
        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_SupplyEntity> GetSupplyList();
        /// <summary>
        /// 获取资质有效期的供应商列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_SupplyEntity> GetEffectSupplyList();
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserEntity> GetUserList();   
        /// <summary>
        /// 获取用户列表(超级管理员除外)
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserEntity> GetUserNoSysList();
        /// <summary>
        /// 获取班次列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_ClassEntity> GetClassList();
        /// <summary>
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="names">名称</param>
        /// <param name="keyValue">主键Id</param>
        /// <returns></returns>
        bool IsName(string tables, string field, string names, string keyValue);
        /// <summary>
        /// 单号重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="orderNo">单号</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        bool IsOrderNo(string tables, string field,string orderNo);
        /// <summary>
        /// 排班重复验证
        /// </summary>
        /// <param name="A_F_EnCode">用户编码</param>
        /// <param name="A_ClassCode">班次</param>
        /// <param name="A_Date">日期</param>
        /// <returns></returns>
        bool IsExistRecord(string keyValue,string A_F_EnCode, string A_ClassCode, DateTime A_Date);
        /// <summary>
        /// 编码重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="code">编码</param>
        /// <param name="keyValue">主键Id</param>
        /// <returns></returns>
        bool IsCode(string tables,string field,string code,string keyValue);

        /// <summary>
        /// 获取配方列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_BomRecordEntity> GetBomList(string goodsCode);

        /// <summary>
        /// 获取原物料code
        /// </summary>
        /// <returns></returns>
        Mes_GoodsEntity GetCode(string goodsCode);

        /// <summary>
        /// 获取Mes_Convert表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_GoodsEntity GetMes_ConvertEntity(string goodsCode);
        /// <summary>
        /// 获取订单里的商品
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        IEnumerable<Mes_ProductOrderDetailEntity> GetOrderGoodsList(string orderNo);
        /// <summary>
        /// 获取订单商品实体
        /// </summary>
        /// <param name="goodsCode"></param>
        /// <returns></returns>
        Mes_ProductOrderDetailEntity GetOrderGoodsEntity(string goodsCode);

        /// <summary>
        /// 获取班组
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_TeamEntity> GetTeamList();
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<RoleEntity> GetRoleList();
        /// <summary>
        /// 获取条码(成品)
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_ScanCodeEntity> GetBarCodeList();
        /// <summary>
        /// 编码重复验证和供应商编码重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="code">编码</param>
        /// <param name="keyValue">主键Id</param>
        /// <returns></returns>
        bool IsCodeAndSupplyCode(string tables, string field, string code, string field2, string code2, string keyValue);

        /// <summary>
        /// 获取成品批次列表
        /// </summary>
        /// <param name="goodsCode"></param>
        /// <returns></returns>
        DataTable GetProductBatchList(string goodsCode, string stockCode);
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

        /// <summary>
        /// 获取成品列表
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        DataTable GetProductList(string stockCode);

        #endregion

    }
}
