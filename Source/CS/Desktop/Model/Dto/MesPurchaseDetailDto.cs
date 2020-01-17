using System;

namespace Model.Dto
{
    /// <summary>
    /// 描述: Dto层 -- Mes_PurchaseDetail表映射类.
    /// </summary>
    public partial class MesPurchaseDetailDto
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }

        /// <summary>
        /// 采购单号
        /// </summary>
        public string P_PurchaseNo{ set; get; }

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
        public decimal P_Qty{ set; get; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal P_Price{ set; get; }

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
