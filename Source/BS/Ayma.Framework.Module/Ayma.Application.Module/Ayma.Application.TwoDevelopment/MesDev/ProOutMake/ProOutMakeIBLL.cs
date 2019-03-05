using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-02 17:05
    /// 描 述：成品出库单制作
    /// </summary>
    public interface ProOutMakeIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_ProOutHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_ProOutDetail表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<Mes_ProOutDetailEntity> GetMes_ProOutDetailList(string keyValue);
        /// <summary>
        /// 获取Mes_ProOutHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_ProOutHeadEntity GetMes_ProOutHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_ProOutDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_ProOutDetailEntity GetMes_ProOutDetailEntity(string keyValue);
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
        void SaveEntity(string keyValue, Mes_ProOutHeadEntity entity,List<Mes_ProOutDetailEntity> mes_ProOutDetailList);
        #endregion

    }
}
