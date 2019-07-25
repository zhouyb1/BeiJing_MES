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
    public partial class GoodsPriceModel 
    {
        #region 实体成员
        
        /// <summary>
        /// 商品编码
        /// </summary>
        [Column("O_SECGOODSCODE")]
        public string O_SecGoodsCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("O_SECGOODSNAME")]
        public string O_SecGoodsName { get; set; }
       
        /// <summary>
        /// 价格
        /// </summary>
        [Column("O_SECPRICE")]
        public decimal? O_SecPrice { get; set; }
        /// <summary>
        /// 批次批次以入库时间（yyyymmdd)
        /// </summary>
        [Column("O_BATCH")]
        public string O_Batch { get; set; }
      
        #endregion

        
    }
}

