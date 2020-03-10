using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ayma.Application.TwoDevelopment.MesDev
 
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-03-05 16:36
    /// 描 述：1
    /// </summary>
    public  class Mes_MonthBalancePrice
    {
        #region 实体成员
        /// <summary>
        /// M_GoodsCode
        /// </summary>
        /// <returns></returns>
        public string M_GoodsCode { get; set; }
        /// <summary>
        /// M_GoodsName
        /// </summary>
        /// <returns></returns>
        public string M_GoodsName { get; set; }




        /// <summary>
        /// 当月总金额
        /// </summary>
        /// <returns></returns>
        public decimal? M_TotalAmount { get; set; }

        /// <summary>
        /// 当月单价
        /// </summary>
        /// <returns></returns>
        public decimal? M_GoodsPrice { get; set; }

        /// <summary>
        /// 当月数量
        /// </summary>
        /// <returns></returns>
        public decimal? M_StockQty { get; set; }

        /// <summary>
        /// 上月单价
        /// </summary>
        /// <returns></returns>
        public decimal? M_LastPrice { get; set; }

        /// <summary>
        /// 上月总数量
        /// </summary>
        /// <returns></returns>
        public decimal? M_LastQty { get; set; }

        /// <summary>
        /// 商品类型 1=原材料 2=半成品 3=成品
        /// </summary>
        public int M_Kind { get; set; }

        #endregion
    }

}