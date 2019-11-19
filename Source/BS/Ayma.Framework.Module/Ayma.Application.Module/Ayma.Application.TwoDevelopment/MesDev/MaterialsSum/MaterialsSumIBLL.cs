using System.Data;
using Ayma.Util;
using System.Collections.Generic;
using System;
namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-09-16 10:59
    /// 描 述：原物料统计(入库、出库、次品)
    /// </summary>
    public interface MaterialsSumIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取选取的时间原物料入库详细
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        DataTable GetMaterialDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode);
        /// <summary>
        /// 获取选取的时间原物料出库详细
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        DataTable GetMaterialOutDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode);
        /// <summary>
        /// 获取选取的时间原物料退库详细
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        DataTable GetMaterialBackDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode);
        /// <summary>
        /// 获取选取的时间原物料销售详细
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        DataTable GetMaterialSaleDetailListByDate(Pagination pagination, string queryJson, string M_GoodsCode);
        /// <summary>
        /// <summary>
        /// 获取期初期末页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        DataTable GetMaterialSumListByDate(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        DataTable GetMaterialSumList(Pagination pagination, string queryJson);

        /// 获取Mes_MaterInDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        DataTable GetMes_MaterInDetailList(Pagination pagination, string queryJson);

        #endregion
    }
}
