using Ayma.Application.TwoDevelopment.MesDev.ScrapManager;
using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 11:20
    /// 描 述：报废单据管理
    /// </summary>
    public interface ScrapManagerIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_ScrapHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取报废单查询页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_ScrapHeadEntity> ScrapManagerList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_ScrapHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_ScrapHeadEntity GetMes_ScrapHeadEntity(string keyValue);

        /// <summary>
        /// 获取明细list
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        IEnumerable<Mes_ScrapDetailEntity> GetMes_ScrapDeail(string orderNo);
        /// <summary>
        /// 获取仓库物料
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        IEnumerable<GoodsEntity> GetGoodsList(Pagination obj, string stockCode, string keyword);
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
        void SaveEntity(string keyValue, Mes_ScrapHeadEntity entity, List<Mes_ScrapDetailEntity> detailList);
        #endregion

    }
}
