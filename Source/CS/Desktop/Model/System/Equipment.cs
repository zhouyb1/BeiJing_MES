using System;

namespace Model
{
    public class Equipment
    {
        public Equipment()
        {

        }

    
        #region 属性
        

        /// <summary>
        /// 设备箱码
        /// </summary>
        public string E_BoxCode { set; get; }

        /// <summary>
        /// 设备编码
        /// </summary>
        public string E_Code{ set; get; }
        /// <summary>
        /// 街道办
        /// </summary>
        public string E_City{ set; get; }
        /// <summary>
        /// 改制村（社区）
        /// </summary>
        public string E_Village{ set; get; }
        /// <summary>
        /// 摄像点（机）详细安装地点
        /// </summary>
        public string E_Address{ set; get; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string E_IP{ set; get; }
        /// <summary>
        /// 监控点编号
        /// </summary>
        public string E_MonitorNumber{ set; get; }

        /// <summary>
        /// 摄像机类型
        /// </summary>
        public string E_CameraType{ set; get; }
        /// <summary>
        /// 摄像机数量
        /// </summary>
        public int E_CameraQty{ set; get; }

        /// <summary>
        /// 横杆朝向
        /// </summary>
        public string E_Direction { get; set; }

        /// <summary>
        /// 监控范围
        /// </summary>
        public string E_Range { get; set; }

        /// <summary>
        /// 安装方式
        /// </summary>
        public string E_InstallType { get; set; }

        /// <summary>
        /// 高度（米）
        /// </summary>
        public decimal? E_Height{ set; get; }
        /// <summary>
        /// 臂长（米）
        /// </summary>
        public decimal? E_Width{ set; get; }

        /// <summary>
        /// 经度
        /// </summary>
        public string E_Longitude{ set; get; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string E_Latitude{ set; get; }
        /// <summary>
        /// 取电方式
        /// </summary>
        public string E_ElectricityType { set; get; }
        /// <summary>
        /// 设备箱数量
        /// </summary>
        public int? E_EquipmentBoxQty{ set; get; }
        /// <summary>
        /// 光纤收发器（一光一电）
        /// </summary>
        public int? E_OpticalFiberQty1{ set; get; }
        /// <summary>
        /// 光纤收发器（一光二电）
        /// </summary>
        public int? E_OpticalFiberQty2{ set; get; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool E_Active{ set; get; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string E_CreateBy { set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? E_CreateDate { set; get; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string E_UpdateBy { set; get; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? E_UpdateDate { set; get; }
        #endregion
    } 
    
}