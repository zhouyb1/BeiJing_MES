using Ayma.DataBase.SqlServer;
using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 10:20
    /// 描 述：调拨单制作
    /// </summary>
    public partial class Mes_RequistDetailEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 调拨单号
        /// </summary>
        [Column("R_REQUISTNO")]
        public string R_RequistNo { get; set; }
       
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("R_GOODSCODE")]
        public string R_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("R_GOODSNAME")]
        public string R_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("R_UNIT")]
        public string R_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("R_QTY")]
        [DecimalPrecision(18, 6)]
        public decimal? R_Qty { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Column("R_BATCH")]
        public string R_Batch { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("R_REMARK")]
        public string R_Remark { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [Column("R_PRICE")]
        [DecimalPrecision(18, 6)]
        public decimal? R_Price { get; set; }
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

