using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 17:17
    /// 描 述：库存查询
    /// </summary>
    public partial class Mes_InventoryEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        [Column("I_KIND")]
        public string I_Kind { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("I_STOCKCODE")]
        public string I_StockCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        [Column("I_STOCKNAME")]
        public string I_StockName { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        [Column("I_GOODSCODE")]
        public string I_GoodsCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("I_GOODSNAME")]
        public string I_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("I_UNIT")]
        public string I_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("I_QTY")]
        public decimal? I_Qty { get; set; }
        /// <summary>
        /// 批次批次以入库时间（yyyymmdd)
        /// </summary>
        [Column("I_BATCH")]
        public string I_Batch { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("I_REMARK")]
        public string I_Remark { get; set; }

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
        #region 扩展字段
        [NotMapped]
        public decimal? I_Price { get; set; }


        #endregion
    }
}

