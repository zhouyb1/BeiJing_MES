﻿using System;

namespace Model.Dto
{
    /// <summary>
    /// 描述: Dto层 -- Mes_Goods表映射类.
    /// </summary>
    public partial class MesGoodsDto
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }

        /// <summary>
        /// 商品编码
        /// </summary>
        public string G_Code{ set; get; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string G_Name{ set; get; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public string G_Kind{ set; get; }

        /// <summary>
        /// 保质时间
        /// </summary>
        public string G_Period{ set; get; }

        /// <summary>
        /// 价格
        /// </summary>
        public string G_Price{ set; get; }

        /// <summary>
        /// 单位
        /// </summary>
        public string G_Unit{ set; get; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        public string G_SupplyCode{ set; get; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string G_Supply{ set; get; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal G_Qty{ set; get; }

        /// <summary>
        /// 上限预警比例
        /// </summary>
        public decimal G_Super{ set; get; }

        /// <summary>
        /// 下限预警比例
        /// </summary>
        public decimal G_Lower{ set; get; }

        /// <summary>
        /// 添加人
        /// </summary>
        public string G_CreateBy{ set; get; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime G_CreateDate{ set; get; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string G_UpdateBy{ set; get; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime G_UpdateDate{ set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string G_Remark{ set; get; }

        #endregion
    }
}
