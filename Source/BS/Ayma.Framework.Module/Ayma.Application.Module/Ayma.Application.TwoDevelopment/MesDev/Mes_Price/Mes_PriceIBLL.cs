using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-12-17 12:37
    /// 描 述：变价记录表
    /// </summary>
    public interface Mes_PriceIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_PriceEntity> GetList( string queryJson );
        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        IEnumerable<Mes_PriceEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取供应商供应的物料变价数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        IEnumerable<Mes_PriceEntity> GetPriceBySupply(Pagination pagination, string P_SupplyCode, string P_GoodsCode);
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_PriceEntity GetEntity(string keyValue);
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
        void SaveEntity(string keyValue, Mes_PriceEntity entity);
        #endregion

    }
}
