using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-08-06 10:54
    /// 描 述：原物料入库价格表
    /// </summary>
    public interface InPriceIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_InPriceEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_InPrice表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_InPriceEntity GetMes_InPriceEntity(string keyValue);
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        void DeleteEntity(List<Mes_InPriceEntity> list);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        void SaveEntity(string keyValue, Mes_InPriceEntity entity, Mes_PriceEntity entity2);

        /// <summary>
        /// 批量保存
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entityList"></param>
        /// <param name="entityList2"></param>
        void SaveEntity(List<Mes_InPriceEntity> entityList, List<Mes_PriceEntity> entityList2);

        #endregion

    }
}
