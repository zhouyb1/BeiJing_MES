using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-07 10:38
    /// 描 述：原物料(半成品)销售
    /// </summary>
    public partial class Mes_SaleDetailEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        [Column("S_SALENO")]
        public string S_SaleNo { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("S_GOODSCODE")]
        public string S_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("S_GOODSNAME")]
        public string S_GoodsName { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        [Column("S_OTAX")]
        public double? S_Otax { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("S_UNIT")]
        public string S_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("S_QTY")]
        public double? S_Qty { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Column("S_BATCH")]
        public string S_Batch { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [Column("S_PRICE")]
        public double? S_Price { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("S_REMARK")]
        public string S_Remark { get; set; }
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

