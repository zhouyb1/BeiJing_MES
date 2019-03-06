using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 17:41
    /// 描 述：配方表
    /// </summary>
    public interface BomHeadIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_BomHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_BomRecord表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<Mes_BomRecordEntity> GetMes_BomRecordList(string keyValue);
        /// <summary>
        /// 获取Mes_BomHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_BomHeadEntity GetMes_BomHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_BomRecord表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_BomRecordEntity GetMes_BomRecordEntity(string keyValue);
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
        void SaveEntity(string keyValue, Mes_BomHeadEntity entity,List<Mes_BomRecordEntity> mes_BomRecordList);
        #endregion

    }
}
