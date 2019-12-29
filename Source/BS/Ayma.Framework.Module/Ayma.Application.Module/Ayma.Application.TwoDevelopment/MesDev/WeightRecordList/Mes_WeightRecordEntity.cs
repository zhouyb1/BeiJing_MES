using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 17:18
    /// 描 述：称重记录列表
    /// </summary>
    public partial class Mes_WeightRecordEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        [Column("P_ORDERNO")]
        public string P_OrderNo { get; set; }
        /// <summary>
        /// 称重类型
        /// </summary>
        [Column("W_KIND")]
        public string W_Kind { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [Column("W_DATE")]
        public DateTime? W_Date { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("W_GOODSCODE")]
        public string W_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("W_GOODSNAME")]
        public string W_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("W_UNIT")]
        public string W_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("W_QTY")]
        public double? W_Qty { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Column("W_BATCH")]
        public string W_Batch { get; set; }
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
        #endregion
    }
}

