using Ayma.DataBase.SqlServer;
using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-18 15:14
    /// 描 述：组装与拆分单据制作
    /// </summary>
    public partial class Mes_OrgResDetailEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 组装与拆分单据
        /// </summary>
        [Column("O_ORGRESNO")]
        public string O_OrgResNo { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("O_GOODSCODE")]
        public string O_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("O_GOODSNAME")]
        public string O_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("O_UNIT")]
        public string O_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("O_QTY")]
        [DecimalPrecision(18, 6)]
        public decimal? O_Qty { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Column("O_BATCH")]
        public string O_Batch { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [Column("O_PRICE")]
        [DecimalPrecision(18, 6)]
        public decimal? O_Price { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("O_SECGOODSCODE")]
        public string O_SecGoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("O_SECGOODSNAME")]
        public string O_SecGoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("O_SECUNIT")]
        public string O_SecUnit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("O_SECQTY")]
        [DecimalPrecision(18, 6)]
        public decimal? O_SecQty { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Column("O_SECBATCH")]
        public string O_SecBatch { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [Column("O_SECPRICE")]
        [DecimalPrecision(18, 6)]
        public decimal? O_SecPrice { get; set; }

        public int? O_Index { get; set; }

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

