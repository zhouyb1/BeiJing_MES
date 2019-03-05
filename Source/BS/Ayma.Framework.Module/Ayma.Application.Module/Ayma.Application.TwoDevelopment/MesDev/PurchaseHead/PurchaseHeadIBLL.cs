using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-05 11:20
    /// 描 述：采购单制作及查询
    /// </summary>
    public interface PurchaseHeadIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_PurchaseHeadEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取Mes_PurchaseDetail表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<Mes_PurchaseDetailEntity> GetMes_PurchaseDetailList(string keyValue);
        /// <summary>
        /// 获取Mes_PurchaseHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_PurchaseHeadEntity GetMes_PurchaseHeadEntity(string keyValue);
        /// <summary>
        /// 获取Mes_PurchaseDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        Mes_PurchaseDetailEntity GetMes_PurchaseDetailEntity(string keyValue);
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
        void SaveEntity(string keyValue, Mes_PurchaseHeadEntity entity,List<Mes_PurchaseDetailEntity> mes_PurchaseDetailList);
        #endregion

    }
}
