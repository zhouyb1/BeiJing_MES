using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-11 19:22
    /// 描 述：领料单
    /// </summary>
    public interface PickingMaterIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_CollarHeadEntity> GetPageList(Pagination pagination, string queryJson);
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
        IEnumerable<Mes_CollarDetailEntity >GetMes_CollarDetailEntityList(string keyValue);

        /// <summary>
        /// 获取库存物料表
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_InventoryEntity> GetMaterList(Pagination pagination, string queryJson, string keyword, string C_Teamcode);
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
        void SaveEntity(string keyValue, Mes_CollarHeadEntity entity,List<Mes_CollarDetailEntity> mes_CollarDetailEntityList);
        #endregion

    }
}
