using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 11:57
    /// 描 述：出库单制作
    /// </summary>
    public interface OutWorkShopManagerIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_OutWorkShopHeadEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 领料单查询
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_OutWorkShopHeadEntity> GetPostIndex(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_OutWorkShopHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_OutWorkShopHeadEntity GetMes_OutWorkShopHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_OutWorkShopDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        IEnumerable<Mes_OutWorkShopDetailEntity> GetMes_OutWorkShopDetailList(string keyValue);

        /// <summary>
        /// 获取仓库物料
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        IEnumerable<Mes_InventoryEntity> GetInventoryMaterList(Pagination paginationobj ,string stockCode,string keyword);
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
        void SaveEntity(string keyValue, Mes_OutWorkShopHeadEntity entity,List<Mes_OutWorkShopDetailEntity> mes_OutWorkShopDetailList);
        #endregion

    }
}
