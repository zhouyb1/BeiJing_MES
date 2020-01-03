using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_InventoryLS表映射类
    /// </summary>
    public partial class MesInventoryLSEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// 库存时间
        /// </summary>
        public DateTime I_Date{ set; get; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string I_StockCode{ set; get; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string I_StockName{ set; get; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string I_GoodsCode{ set; get; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string I_GoodsName{ set; get; }
        /// <summary>
        /// 单位
        /// </summary>
        public string I_Unit{ set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public Double I_Qty{ set; get; }
        /// <summary>
        /// 批次
        /// </summary>
        public string I_Batch{ set; get; }
        /// <summary>
        /// 保质期
        /// </summary>
        public string I_Period{ set; get; }
        #endregion
    }
}
