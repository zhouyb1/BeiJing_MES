using System;

namespace Model.Dto
{
    /// <summary>
    /// 描述: Dto层 -- Mes_Device表映射类.
    /// </summary>
    public partial class MesDeviceDto
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string D_Department{ set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string D_Name{ set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string D_IP{ set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string D_Remark { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string status { set; get; }
        #endregion
    }
}
