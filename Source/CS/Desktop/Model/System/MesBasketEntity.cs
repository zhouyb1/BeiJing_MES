using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_Basket表映射类
    /// </summary>
    public partial class MesBasketEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string B_BasketCode{ set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string B_BasketName{ set; get; }
        /// <summary>
        /// 
        /// </summary>
        public decimal M_Weight{ set; get; }
        #endregion
    }
}
