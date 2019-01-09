using Ayma.Application.TwoDevelopment.MesDev.Mes_ProductOrderHead;
using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 15:20
    /// 描 述：同步ERP成品商品资料
    /// </summary>
    public interface Mes_ProductGoodsIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_ProductGoodsEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_ProductGoods表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_ProductGoodsEntity GetMes_ProductGoodsEntity(string keyValue);
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
        void SaveEntity(string keyValue, Mes_ProductGoodsEntity entity);
        #endregion


         /// <summary>
        /// 获取ERP的商品资料
        /// </summary>
        List<ERPTgoodsListModel> GetErpTgoodsList();

    }
}
