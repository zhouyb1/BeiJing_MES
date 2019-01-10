using Ayma.Util;
using System.Collections.Generic;
using System.Data;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 10:20
    /// 描 述：调拨单制作
    /// </summary>
    public interface RequistBillIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取商品列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="stockCode">仓库编码</param>
        /// <param name="keyword">商品编码/商品名称搜索</param>
        /// <returns></returns>
        DataTable GetList(Pagination pagination, string queryJson, string stockCode, string keyword);
        /// <summary>
        /// 获取已提交单据列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_RequistHeadEntity> GetPostPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_RequistHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_RequistDetail表数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<Mes_RequistDetailEntity> GetMes_RequistDetailList(string keyValue);
        /// <summary>
        /// 获取Mes_RequistHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_RequistHeadEntity GetMes_RequistHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_RequistDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_RequistDetailEntity GetMes_RequistDetailEntity(string keyValue);
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
        void SaveEntity(string keyValue, Mes_RequistHeadEntity entity,List<Mes_RequistDetailEntity> mes_RequistDetailList);
        #endregion

    }
}
