using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Mes_Device表映射类
    /// </summary>
    public partial class MesDeviceEntity
    {
        #region 属性
        /// <summary>
        /// 设备ID
        /// </summary>
        public string ID{ set; get; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string D_Department{ set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string D_TeamCode { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string D_TeamName { set; get; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string D_Name{ set; get; }
        /// <summary>
        /// 设备ip
        /// </summary>
        public string D_IP{ set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string D_Remark { set; get; }
        #endregion
    }
}
