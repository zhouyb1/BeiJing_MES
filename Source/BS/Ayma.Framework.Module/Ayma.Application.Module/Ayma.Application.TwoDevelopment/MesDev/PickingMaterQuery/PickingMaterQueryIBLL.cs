using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 10:06
    /// 描 述：领料单查询
    /// </summary>
    public interface PickingMaterQueryIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_CollarHeadEntity> GetPageList(Pagination pagination, string queryJson,string C_CollarNo);
        /// <summary>
        /// 获取Mes_CollarHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_CollarHeadEntity GetMes_CollarHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_CollarDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        IEnumerable<Mes_CollarDetailEntity> GetMes_CollarDetailEntityList(string keyValue);
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
        void SaveEntity(string keyValue, Mes_CollarHeadEntity entity,Mes_CollarDetailEntity mes_CollarDetailEntity);
        #endregion

    }
}
