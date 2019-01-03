using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Inspect
    {
        /// <summary>
        /// ID
        /// </summary>
        public string E_ID { get; set; }


        /// <summary>
        /// 巡检类型
        /// </summary>
        public string I_Type { get; set; }

        /// <summary>
        /// 巡检结果
        /// </summary>
        public string I_Result { get; set; }

        /// <summary>
        /// 现成照片
        /// </summary>
        public string I_Photo { get; set; }

        /// <summary>
        /// 巡检人
        /// </summary>
        public string I_User { get; set; }

        /// <summary>
        /// 巡检时间
        /// </summary>
        public DateTime I_Date { get; set; }


        #region 扩展属性
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
        #endregion
    }
}
