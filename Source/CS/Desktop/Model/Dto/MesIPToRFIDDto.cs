using System;

namespace Model.Dto
{
    /// <summary>
    /// 描述: Dto层 -- Mes_IPToRFID表映射类.
    /// </summary>
    public partial class MesIPToRFIDDto
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string I_IP{ set; get; }

        /// <summary>
        /// RFID编码
        /// </summary>
        public string I_RFIDCode{ set; get; }

        /// <summary>
        /// 门编码
        /// </summary>
        public string I_DoorCode{ set; get; }

        /// <summary>
        /// 门名称
        /// </summary>
        public string I_DoorName{ set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        public string I_Remark{ set; get; }

        #endregion
    }
}
