using Ayma.Application.TwoDevelopment.MesDev.ScrapManager;
using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-18 15:14
    /// 描 述：组装与拆分单据制作
    /// </summary>
    public interface OrgResMangerIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_OrgResHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_OrgResHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_OrgResHeadEntity GetMes_OrgResHeadEntity(string keyValue);

        /// <summary>
        /// 获取仓库物料
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        IEnumerable<GoodsEntity> GetGoodsList(Pagination obj, string keyword, string queryJson);
        /// <summary>
        /// 获取Mes_OrgResDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        IEnumerable<Mes_OrgResDetailEntity> GetMes_OrgResDetailList(string keyValue);
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
        void SaveEntity(string keyValue, Mes_OrgResHeadEntity entity,List<Mes_OrgResDetailEntity> mes_OrgResDetailList);
        #endregion

    }
}
