using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_Inventory表映射类
    /// </summary>
    public partial class MesInventoryEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string I_StockCode{ set; get; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string I_StockName{ set; get; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string I_GoodsCode{ set; get; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string I_GoodsName{ set; get; }
        /// <summary>
        /// 单位
        /// </summary>
        public string I_Unit{ set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal I_Qty{ set; get; }
        /// <summary>
        /// 批次批次以入库时间（yyyymmdd)
        /// </summary>
        public string I_Batch{ set; get; }
        /// <summary>
        /// 保质期
        /// </summary>
        public string I_Period{ set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string I_Remark{ set; get; }
        #endregion
    }
}
