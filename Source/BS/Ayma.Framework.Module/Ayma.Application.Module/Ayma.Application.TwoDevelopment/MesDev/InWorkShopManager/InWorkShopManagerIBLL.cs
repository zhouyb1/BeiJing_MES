using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 11:57
    /// 描 述：车间入库到线边仓
    /// </summary>
    public interface InWorkShopManagerIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_InWorkShopHeadEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 车间入库到线边仓查询
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_InWorkShopHeadEntity> GetSearchIndex(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_InWorkShopHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_InWorkShopHeadEntity GetMes_InWorkShopHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_InWorkShopDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        IEnumerable<Mes_InWorkShopDetailEntity> GetMes_InWorkShopDetailList(string keyValue);

        /// <summary>
        /// 获取仓库物料
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        IEnumerable<Mes_InventoryEntity> GetInventoryMaterList(Pagination paginationobj ,string stockCode); 
        /// <summary>
        /// 获取物料列表(半成品和成品)
        /// </summary>
        /// <param name="paginationobj">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_GoodsEntity> GetGoodsList(Pagination paginationobj, string queryJson);
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
        void SaveEntity(string keyValue, Mes_InWorkShopHeadEntity entity, List<Mes_InWorkShopDetailEntity> mes_OutWorkShopDetailList);
        #endregion

    }
}
