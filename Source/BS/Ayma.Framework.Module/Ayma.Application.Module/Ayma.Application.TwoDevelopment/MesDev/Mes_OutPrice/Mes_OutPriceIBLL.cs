using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-12-04 09:28
    /// 描 述：原物料售卖价格表
    /// </summary>
    public interface Mes_OutPriceIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_OutPriceEntity> GetList( string queryJson );
        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        IEnumerable<Mes_OutPriceEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_OutPriceEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        void DeleteEntity(List<Mes_OutPriceEntity> list);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        void SaveEntity(string keyValue,Mes_OutPriceEntity entity);
        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entityList"></param>
        /// <param name="entityList2"></param>
        void SaveEntity(List<Mes_OutPriceEntity> entityList);
        #endregion

    }
}
