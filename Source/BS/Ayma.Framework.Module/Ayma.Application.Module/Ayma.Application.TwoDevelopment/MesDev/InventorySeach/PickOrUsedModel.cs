using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 17:17
    /// 描 述：物料领用或使用查询
    /// </summary>
    public partial class PickOrUsedModel 
    {
        #region 实体成员
        
        /// <summary>
        /// 商品编码
        /// </summary>
        [Column("GOODSCODE")]
        public string GoodsCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("GOODSNAME")]
        public string GoodsName { get; set; }
       
        /// <summary>
        /// 数量
        /// </summary>
        [Column("QTY")]
        public decimal? Qty { get; set; }
        /// <summary>
        /// 批次批次以入库时间（yyyymmdd)
        /// </summary>
        [Column("BATCH")]
        public string Batch { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        [Column("STOCKCODE")]
        public string StockCode { get; set; }
        #endregion

        
    }
}

