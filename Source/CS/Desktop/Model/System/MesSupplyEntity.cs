using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_Supply表映射类
    /// </summary>
    public partial class MesSupplyEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        public string S_Code{ set; get; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string S_Name{ set; get; }
        /// <summary>
        /// 资质期限
        /// </summary>
        public DateTime S_EffectTime{ set; get; }
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
