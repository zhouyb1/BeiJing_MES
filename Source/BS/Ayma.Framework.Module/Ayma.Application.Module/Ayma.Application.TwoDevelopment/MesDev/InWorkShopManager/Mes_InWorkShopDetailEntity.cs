using Ayma.DataBase.SqlServer;
using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 11:57
    /// 描 述：车间入库到线边仓 表体
    /// </summary>
    public partial class Mes_InWorkShopDetailEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 车间入库到线边仓单号
        /// </summary>
        [Column("I_INNO")]
        public string I_InNo { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("I_GOODSCODE")]
        public string I_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
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
        [DecimalPrecision(18, 6)]
        public decimal? I_Qty { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Column("I_BATCH")]
        public string I_Batch { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("I_REMARK")]
        public string I_Remark { get; set; } 
        /// <summary>
        /// 价格
        /// </summary>
        [Column("I_PRICE")]
        [DecimalPrecision(18, 6)]
        public decimal? I_Price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [DecimalPrecision(18, 6)]
        [NotMapped]
        public decimal? I_Amount { get; set; }

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

