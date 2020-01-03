using System;

namespace Model.Dto
{
    /// <summary>
    /// 描述: Dto层 -- Mes_Basket表映射类.
    /// </summary>
    public partial class MesBasketDto
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
        public Double M_Weight{ set; get; }

        #endregion
    }
}
