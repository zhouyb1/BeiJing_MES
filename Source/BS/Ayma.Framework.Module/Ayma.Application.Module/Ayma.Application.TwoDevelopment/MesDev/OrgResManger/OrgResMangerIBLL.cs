﻿using System.Data;
using Ayma.Application.TwoDevelopment.MesDev.ScrapManager;
using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-18 15:14
    /// 描 述：组装与拆分单据制作
    /// </summary>
    public interface OrgResMangerIBLL
    {
        #region 获取数据

        /// <summary>
        /// 出成率查询
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductRateView> GetProductRateList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_OrgResHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取组装与拆分单据查询页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_OrgResHeadEntity> OrgResManagerList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_OrgResHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_OrgResHeadEntity GetMes_OrgResHeadEntity(string keyValue);

        /// <summary>
        /// 获取仓库物料
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        DataTable GetGoodsList(string keyword, string queryJson,Pagination obj);
        /// <summary>
        /// 获取Mes_OrgResDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>

        /// <summary>
        /// 获取转换后的物料
        /// </summary>
        /// <returns></returns>
        DataTable GetSecGoodsList(string keyword, Pagination obj);
        IEnumerable<Mes_OrgResDetailEntity> GetMes_OrgResDetailList(string keyValue);
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
        void SaveEntity(string keyValue, Mes_OrgResHeadEntity entity,List<Mes_OrgResDetailEntity> mes_OrgResDetailList);
        #endregion

    }
}
