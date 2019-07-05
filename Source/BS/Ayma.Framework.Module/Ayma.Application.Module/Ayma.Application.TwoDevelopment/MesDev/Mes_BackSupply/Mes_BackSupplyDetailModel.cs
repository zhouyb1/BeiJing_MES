using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 16:30
    /// 描 述：退供应商单模型
    /// </summary>
    public partial class Mes_BackSupplyDetailModel 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 退供应商单号
        /// </summary>
        [Column("B_BACKSUPPLYNO")]
        public string B_BackSupplyNo { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("B_GOODSCODE")]
        public string B_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("B_GOODSNAME")]
        public string B_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("B_UNIT")]
        public string B_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("B_QTY")]
        public decimal? B_Qty { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Column("B_BATCH")]
        public string B_Batch { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("B_REMARK")]
        public string B_Remark { get; set; }
        #endregion

    }
}

