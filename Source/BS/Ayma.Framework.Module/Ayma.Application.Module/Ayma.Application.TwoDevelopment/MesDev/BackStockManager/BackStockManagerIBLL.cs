﻿using Ayma.Application.TwoDevelopment.MesDev.ScrapManager;
using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-15 16:11
    /// 描 述：线边仓退料到仓库
    /// </summary>
    public interface BackStockManagerIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_BackStockHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_BackStockHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_BackStockHeadEntity GetMes_BackStockHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_BackStockDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        IEnumerable<Mes_BackStockDetailEntity> GetMes_BackStockDetailList(string keyValue);

        /// <summary>
        /// 获取仓库物料
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        IEnumerable<GoodsEntity> GetGoodsList(Pagination obj, string stockCode, string keyword);

        /// <summary>
        /// 返回单据查询页面数据列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<Mes_BackStockHeadEntity> GetBacStockList(Pagination pagination, string queryJson);

        /// <summary>
        /// 根据单号获取物料列表
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        IEnumerable<Mes_BackStockDetailEntity> GetBackStockDetailList(string orderNo);
       

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
        void SaveEntity(string keyValue, Mes_BackStockHeadEntity entity, List<Mes_BackStockDetailEntity> mes_BackStockDetailList);
        #endregion

    }
}
