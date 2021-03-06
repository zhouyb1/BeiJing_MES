﻿using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 17:17
    /// 描 述：库存查询
    /// </summary>
    public interface InventorySeachIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_InventoryEntity> GetPageList(Pagination pagination, string queryJson,string stock,string goodsCode); 
        /// <summary>
        /// 获取物料领用列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<PickOrUsedModel> GetPickPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取物料使用列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<PickOrUsedModel> GetUsedPageList(Pagination pagination, string queryJson); 
        /// <summary>
        /// 获取物料价值查询列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<GoodsPriceModel> GetPricePageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_Inventory表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_InventoryEntity GetMes_InventoryEntity(string keyValue); 
        /// <summary>
        /// 根据仓库编码和商品编码获取列表
        /// </summary>
        /// <param name="stockCode">仓库编码</param>
        /// <param name="goodsCode">物料编码</param>
        /// <returns></returns>
        IEnumerable<Mes_InventoryEntity> GetListByStockAndCode(string stockCode,string goodsCode);

        /// <summary>
        /// 获取Mes_Inventory表实体数据 根据商品编码和仓库编码以及批次
        /// </summary>
        /// <param name="goodsCode">商品编码</param>
        /// <param name="stockCode">仓库编码</param>
        /// <param name="batch">批次</param>
        /// <returns></returns>
        Mes_InventoryEntity GetEntityBy(string goodsCode, string stockCode, string batch);

        /// <summary>
        ///根据goodsCode、批次获取Entity
        /// </summary>
        /// <param name="goodsCode"></param>
        /// <returns></returns>
        Mes_InventoryEntity GetListByParams(string goodsCode, string batch);
           /// <summary>
        /// 获取页面显示明细列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_InventoryEntity> GetInventoryList(Pagination pagination, string queryJson, string I_GoodsName, string I_StockName, string I_Unit, string I_Batch);

        /// <summary>
        /// 获取当前物料所有库存数据(有批次)作为组装物料
        /// </summary>
        /// <param name="stock"></param>
        /// <param name="goodsCode"></param>
        /// <returns></returns>
        IEnumerable<Mes_OrgResDetailEntity> GetOrgGoodsList(string stock, string goodsCode);
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        void SaveEntity(string keyValue, Mes_InventoryEntity entity);
        #endregion

    }
}
