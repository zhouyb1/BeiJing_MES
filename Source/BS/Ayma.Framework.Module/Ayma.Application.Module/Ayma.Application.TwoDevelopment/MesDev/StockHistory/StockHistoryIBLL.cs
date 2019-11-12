using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-12 09:16
    /// 描 述：库存快照
    /// </summary>
    public interface StockHistoryIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_InventoryLSEntity> GetPageList(Pagination pagination, string queryJson);
      
        /// <summary>
        /// 获取页面显示明细列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<Mes_InventoryLSEntity> GetInventoryList(Pagination pagination, string queryJson,string I_Date,string I_GoodsName, string I_StockName, string I_Unit, string I_Batch);
        #endregion


    }
}
