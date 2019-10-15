using System.Data;
using Ayma.Util;
using System;
using System.Collections.Generic;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-09-25 14:42
    /// 描 述：供应商进货数据汇总
    /// </summary>
    public partial class SupplyGoodsCountRepBLL : SupplyGoodsCountRepIBLL
    {
        private SupplyGoodsCountRepService supplyGoodsCountRepService = new SupplyGoodsCountRepService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return supplyGoodsCountRepService.GetPageList(pagination, queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 供应商进货数据明细数据
        /// </summary>
        /// <param name="supplyCode"></param>
        /// <returns></returns>
        public DataTable GetSupplyGoodsDetail(string supplyCode,string queryJson)
        {
            try
            {
                return supplyGoodsCountRepService.GetSupplyGoodsDetail(supplyCode,queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        #endregion
    }
}
