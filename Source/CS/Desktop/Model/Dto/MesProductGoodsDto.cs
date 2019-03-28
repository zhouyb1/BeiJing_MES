using System;

namespace Model.Dto
{
    /// <summary>
    /// 描述: Dto层 -- Mes_ProductGoods表映射类.
    /// </summary>
    public partial class MesProductGoodsDto
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
