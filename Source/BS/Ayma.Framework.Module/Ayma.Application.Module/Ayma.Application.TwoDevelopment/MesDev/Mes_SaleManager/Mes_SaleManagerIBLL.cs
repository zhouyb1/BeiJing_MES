using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-07 10:38
    /// 描 述：原物料(半成品)销售
    /// </summary>
    public interface Mes_SaleManagerIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_SaleHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_SaleHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_SaleHeadEntity GetMes_SaleHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_SaleDetail表实体数据
        /// </summary>
        /// <param name="saleNo">主键</param>
        /// <returns></returns>
        IEnumerable<Mes_SaleDetailEntity> GetMes_SaleDetail(string saleNo);

        /// <summary>
        /// 获取原物料
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        IEnumerable<Mes_InventoryEntity> GetGoodsList(Pagination pagination, string queryJson, string keyword);

        /// <summary>
        ///  报表：已提交的原物料销售单据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<Mes_SaleHeadEntity> GetPostList(Pagination pagination, string queryJson);
        /// <summary>
        /// 报表：原物料销售单据详情
        /// </summary>
        /// <param name="saleNo"></param>
        /// <returns></returns>
        IEnumerable<Mes_SaleDetailEntity> GetDetailList(string saleNo);

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
        void SaveEntity(string keyValue, Mes_SaleHeadEntity entity, List<Mes_SaleDetailEntity> detail);
        #endregion

    }
}
