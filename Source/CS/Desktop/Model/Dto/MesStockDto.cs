using System;

namespace Model.Dto
{
    /// <summary>
    /// 描述: Dto层 -- Mes_Stock表映射类.
    /// </summary>
    public partial class MesStockDto
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        public string S_Code{ set; get; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string S_Name{ set; get; }

        /// <summary>
        /// 仓库类型
        /// </summary>
        public string S_Kind{ set; get; }

        /// <summary>
        /// 仓库保管人
        /// </summary>
        public string S_Peson{ set; get; }

        /// <summary>
        /// 添加人
        /// </summary>
        public string S_CreateBy{ set; get; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime S_CreateDate{ set; get; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string S_UpdateBy{ set; get; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime S_UpdateDate{ set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string S_Remark{ set; get; }

        #endregion
    }
}
