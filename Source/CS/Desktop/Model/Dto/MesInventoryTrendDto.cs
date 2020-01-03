using System;

namespace Model.Dto
{
    /// <summary>
    /// 描述: Dto层 -- Mes_InventoryTrend表映射类.
    /// </summary>
    public partial class MesInventoryTrendDto
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public string I_OrderKind{ set; get; }

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
        /// 批次
        /// </summary>
        public string I_Batch{ set; get; }

        /// <summary>
        /// 保质期
        /// </summary>
        public string I_Period{ set; get; }

        /// <summary>
        /// 单据号
        /// </summary>
        public string I_OrderNo{ set; get; }

        /// <summary>
        /// 初始数量
        /// </summary>
        public Double I_QtyOld{ set; get; }

        /// <summary>
        /// 新数量
        /// </summary>
        public Double I_QtyNew{ set; get; }

        /// <summary>
        /// 移动数量
        /// </summary>
        public Double I_QtyTrend{ set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string I_Remark{ set; get; }

        #endregion
    }
}
