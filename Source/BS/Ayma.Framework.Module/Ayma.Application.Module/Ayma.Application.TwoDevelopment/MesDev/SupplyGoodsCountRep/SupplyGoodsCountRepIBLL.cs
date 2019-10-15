using System.Data;
using Ayma.Util;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-09-25 14:42
    /// 描 述：供应商进货数据汇总
    /// </summary>
    public interface SupplyGoodsCountRepIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        DataTable GetPageList(Pagination pagination, string queryJson);
        
        /// <summary>
        /// 供应商进货数据明细数据
        /// </summary>
        /// <param name="supplyCode">供应商编码</param>
        /// <returns></returns>
        DataTable GetSupplyGoodsDetail(string keyValue,string queryJson);
        #endregion

    }
}
