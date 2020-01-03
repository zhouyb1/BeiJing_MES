using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_MaterInDetail表映射类
    /// </summary>
    public partial class MesMaterInDetailEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// 入库单号
        /// </summary>
        public string M_MaterInNo{ set; get; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string M_OrderNo{ set; get; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string M_GoodsCode{ set; get; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string M_GoodsName{ set; get; }
        /// <summary>
        /// 税率
        /// </summary>
        public Double M_GoodsItax { set; get; }
        /// <summary>
        /// 单位
        /// </summary>
        public string M_Unit{ set; get; }
        /// <summary>
        /// 数量
        /// </summary>
        public Double M_Qty{ set; get; }
        /// <summary>
        /// 批次
        /// </summary>
        public string M_Batch{ set; get; }

        /// <summary>
        /// 价格
        /// </summary>
        public Double M_Price { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string M_Remark{ set; get; }
        /// <summary>
        /// 物料类型
        /// </summary>
        public string M_Kind { set; get; }
        #endregion
    }
}
