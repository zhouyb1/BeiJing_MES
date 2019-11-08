using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-08 13:59
    /// 描 述：消耗物料
    /// </summary>
    public interface Mes_ExpendManagerIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_ExpendHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_ExpendHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_ExpendHeadEntity GetMes_ExpendHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_ExpendDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        IEnumerable<Mes_ExpendDetailEntity> GetMes_ExpendDetailEntity(string keyValue);

        /// <summary>
        /// 获取原物料
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        IEnumerable<Mes_InventoryEntity> GetGoodsList(Pagination pagination, string queryJson, string keyword);

        /// <summary>
        /// 报表：获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_ExpendHeadEntity> GetPostGoodsList(Pagination pagination, string queryJson);

        /// <summary>
        /// 报表：单据详情
        /// </summary>
        /// <param name="expendNo"></param>
        /// <returns></returns>
        IEnumerable<Mes_ExpendDetailEntity> GetDetail(string expendNo);
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
        void SaveEntity(string keyValue, Mes_ExpendHeadEntity entity,List<Mes_ExpendDetailEntity> detail);
        #endregion

    }
}
