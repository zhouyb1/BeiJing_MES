using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-18 10:37
    /// 描 述：退供应商单查询
    /// </summary>
    public interface Mes_BackSupplyQueryIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_BackSupplyHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_BackSupplyDetail表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<Mes_BackSupplyDetailEntity> GetMes_BackSupplyDetailList(string keyValue);
        /// <summary>
        /// 获取Mes_BackSupplyHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_BackSupplyHeadEntity GetMes_BackSupplyHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_BackSupplyDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_BackSupplyDetailEntity GetMes_BackSupplyDetailEntity(string keyValue);
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
        void SaveEntity(string keyValue, Mes_BackSupplyHeadEntity entity,List<Mes_BackSupplyDetailEntity> mes_BackSupplyDetailList);
        #endregion

    }
}
