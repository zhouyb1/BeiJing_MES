using System;

namespace Model.Dto
{
    /// <summary>
    /// 描述: Dto层 -- Mes_ProOutDetail表映射类.
    /// </summary>
    public partial class MesProOutDetailDto
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }

        /// <summary>
        /// 出库单号
        /// </summary>
        public string P_ProOutNo{ set; get; }

        /// <summary>
        /// 生产订单号
        /// </summary>
        public string P_OrderNo{ set; get; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string P_GoodsCode{ set; get; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string P_GoodsName{ set; get; }

        /// <summary>
        /// 单位
        /// </summary>
        public string P_Unit{ set; get; }

        /// <summary>
        /// 数量
        /// </summary>
        public Double P_Qty{ set; get; }

        /// <summary>
        /// 批次
        /// </summary>
        public string P_Batch{ set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string P_Remark{ set; get; }

        #endregion
    }
}
