using Ayma.Util;
using System.Collections.Generic;
using System.Data;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 13:55
    /// 描 述：物料列表
    /// </summary>
    public interface GoodsListIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取毛到净出成率
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        DataTable GetYieldRatePageList(string queryJson);  
        /// <summary>
        /// 获取生到熟出成率
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        DataTable GetLivingToCookPageList(string queryJson); 
        /// <summary>
        /// 获取包装偏差率
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        DataTable GetPackingRatePageList(string queryJson); 
        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        DataTable GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_Goods表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_GoodsEntity GetMes_GoodsEntity(string keyValue);
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
        void SaveEntity(string keyValue, Mes_GoodsEntity entity);
        #endregion

    }
}
