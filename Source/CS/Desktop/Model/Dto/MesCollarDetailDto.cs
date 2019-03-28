using System;

namespace Model.Dto
{
    /// <summary>
    /// 描述: Dto层 -- Mes_CollarDetail表映射类.
    /// </summary>
    public partial class MesCollarDetailDto
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }

        /// <summary>
        /// 领料单号
        /// </summary>
        public string C_CollarNo{ set; get; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string C_OrderNo{ set; get; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string C_GoodsCode{ set; get; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string C_GoodsName{ set; get; }

        /// <summary>
        /// 单位
        /// </summary>
        public string C_Unit{ set; get; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal C_Qty{ set; get; }

        /// <summary>
        /// 批次
        /// </summary>
        public string C_Batch{ set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string C_Remark{ set; get; }

        #endregion
    }
}
