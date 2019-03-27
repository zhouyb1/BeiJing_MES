using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_MoveRecord表映射类
    /// </summary>
    public partial class MesMoveRecordEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// 用户编码
        /// </summary>
        public string M_UserCode{ set; get; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string M_UserName{ set; get; }
        /// <summary>
        /// iP
        /// </summary>
        public string M_IP{ set; get; }
        /// <summary>
        /// RFID编码
        /// </summary>
        public string M_RFIDCode{ set; get; }
        /// <summary>
        /// 门编码
        /// </summary>
        public string M_DoorCode{ set; get; }
        /// <summary>
        /// 门名称
        /// </summary>
        public string M_DoorName{ set; get; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime M_Date{ set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string M_Remark{ set; get; }
        #endregion
    }
}
