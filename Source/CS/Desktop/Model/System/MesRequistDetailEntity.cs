using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_RequistDetail表映射类
    /// </summary>
    public partial class MesRequistDetailEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// 调拨单号
        /// </summary>
        public string R_RequistNo{ set; get; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string P_OrderNo{ set; get; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string R_GoodsCode{ set; get; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string R_GoodsName{ set; get; }
        /// <summary>
        /// 单位
        /// </summary>
        public string R_Unit{ set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public Double R_Qty{ set; get; }
        /// <summary>
        /// 批次
        /// </summary>
        public string R_Batch{ set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string R_Remark{ set; get; }
        #endregion
    }
}
