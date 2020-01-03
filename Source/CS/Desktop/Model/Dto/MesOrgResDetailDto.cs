using System;

namespace Model.Dto
{
    /// <summary>
    /// 描述: Dto层 -- Mes_OrgResDetail表映射类.
    /// </summary>
    public partial class MesOrgResDetailDto
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }

        /// <summary>
        /// 组装与拆分单据
        /// </summary>
        public string O_OrgResNo{ set; get; }

        /// <summary>
        /// 生产订单号
        /// </summary>
        public string O_OrderNo{ set; get; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string O_GoodsCode{ set; get; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string O_GoodsName{ set; get; }

        /// <summary>
        /// 单位
        /// </summary>
        public string O_Unit{ set; get; }

        /// <summary>
        /// 数量
        /// </summary>
        public Double O_Qty{ set; get; }

        /// <summary>
        /// 批次
        /// </summary>
        public string O_Batch{ set; get; }

        /// <summary>
        /// 价格
        /// </summary>
        public Double O_Price{ set; get; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string O_SecGoodsCode{ set; get; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string O_SecGoodsName{ set; get; }

        /// <summary>
        /// 单位
        /// </summary>
        public string O_SecUnit{ set; get; }

        /// <summary>
        /// 数量
        /// </summary>
        public Double O_SecQty{ set; get; }

        /// <summary>
        /// 批次
        /// </summary>
        public string O_SecBatch{ set; get; }

        /// <summary>
        /// 价格
        /// </summary>
        public Double O_SecPrice{ set; get; }

        #endregion
    }
}
