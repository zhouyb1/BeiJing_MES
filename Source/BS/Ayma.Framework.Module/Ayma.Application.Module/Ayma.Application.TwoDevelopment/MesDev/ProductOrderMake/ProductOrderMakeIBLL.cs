using Ayma.Util;
using System.Collections.Generic;
using System.Data;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-02 15:05
    /// 描 述：生成订单制作
    /// </summary>
    public interface ProductOrderMakeIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取商品列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="keyword">商品编码/商品名称搜索</param>
        /// <returns></returns>
        DataTable GetGoodsList(Pagination pagination, string queryJson, string keyword);
        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_ProductOrderHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_ProductOrderDetail表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<Mes_ProductOrderDetailEntity> GetMes_ProductOrderDetailList(string keyValue);
        /// <summary>
        /// 获取Mes_ProductOrderHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_ProductOrderHeadEntity GetMes_ProductOrderHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_ProductOrderDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_ProductOrderDetailEntity GetMes_ProductOrderDetailEntity(string keyValue);
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
        void SaveEntity(string keyValue, Mes_ProductOrderHeadEntity entity,List<Mes_ProductOrderDetailEntity> mes_ProductOrderDetailList);
        #endregion

    }
}
