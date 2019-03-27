using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_WeightRecord表映射类
    /// </summary>
    public partial class MesWeightRecordEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string P_OrderNo{ set; get; }
        /// <summary>
        /// 称重类型
        /// </summary>
        public string W_Kind{ set; get; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime W_Date{ set; get; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string W_GoodsCode{ set; get; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string W_GoodsName{ set; get; }
        /// <summary>
        /// 单位
        /// </summary>
        public string W_Unit{ set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal W_Qty{ set; get; }
        /// <summary>
        /// 批次
        /// </summary>
        public string W_Batch{ set; get; }
        #endregion
    }
}
