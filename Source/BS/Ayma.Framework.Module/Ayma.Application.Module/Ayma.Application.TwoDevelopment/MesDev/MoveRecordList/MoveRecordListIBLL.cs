using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 15:11
    /// 描 述：人员走动记录列表
    /// </summary>
    public interface MoveRecordListIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_MoveRecordEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_MoveRecord表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_MoveRecordEntity GetMes_MoveRecordEntity(string keyValue);
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
        void SaveEntity(string keyValue, Mes_MoveRecordEntity entity);
        #endregion

    }
}
