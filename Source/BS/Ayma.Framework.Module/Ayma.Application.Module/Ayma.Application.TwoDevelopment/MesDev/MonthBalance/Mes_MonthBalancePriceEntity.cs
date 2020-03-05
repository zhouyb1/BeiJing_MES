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
    public partial class Mes_MonthBalancePriceEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// M_Months
        /// </summary>
        /// <returns></returns>
        [Column("M_MONTHS")]
        public string M_Months { get; set; }
        /// <summary>
        /// M_GoodsCode
        /// </summary>
        /// <returns></returns>
        [Column("M_GOODSCODE")]
        public string M_GoodsCode { get; set; }
        /// <summary>
        /// M_GoodsName
        /// </summary>
        /// <returns></returns>
        [Column("M_GOODSNAME")]
        public string M_GoodsName { get; set; }
        /// <summary>
        /// M_GoodsPrice
        /// </summary>
        /// <returns></returns>
        [Column("M_GOODSPRICE")]
        public decimal? M_GoodsPrice { get; set; }
        /// <summary>
        /// M_StockQty
        /// </summary>
        /// <returns></returns>
        [Column("M_STOCKQTY")]
        public decimal? M_StockQty { get; set; }
        /// <summary>
        /// M_LastPrice
        /// </summary>
        /// <returns></returns>
        [Column("M_LASTPRICE")]
        public decimal? M_LastPrice { get; set; }
        /// <summary>
        /// M_LastQty
        /// </summary>
        /// <returns></returns>
        [Column("M_LASTQTY")]
        public decimal? M_LastQty { get; set; }
        #endregion
 
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.ID = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.ID = keyValue;
        }
        #endregion
    }
}