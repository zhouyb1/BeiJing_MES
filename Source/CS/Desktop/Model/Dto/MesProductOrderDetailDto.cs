﻿using System;

namespace Model.Dto
{
    /// <summary>
    /// 描述: Dto层 -- Mes_ProductOrderDetail表映射类.
    /// </summary>
    public partial class MesProductOrderDetailDto
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
        /// 订单时间
        /// </summary>
        public DateTime P_OrderDate{ set; get; }

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

        #endregion
    }
}
