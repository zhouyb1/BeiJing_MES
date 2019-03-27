using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_Door表映射类
    /// </summary>
    public partial class MesDoorEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// 门编码
        /// </summary>
        public string D_Code{ set; get; }
        /// <summary>
        /// 门名称
        /// </summary>
        public string D_Name{ set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string D_Remark{ set; get; }
        #endregion
    }
}
