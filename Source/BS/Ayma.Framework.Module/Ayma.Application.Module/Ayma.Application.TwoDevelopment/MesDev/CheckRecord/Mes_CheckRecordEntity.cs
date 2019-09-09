using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-04-25 15:41
    /// 描 述：考勤管理
    /// </summary>
    public partial class Mes_CheckRecordEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        
        /// <summary>
        /// 打卡日期
        /// </summary>
        [Column("C_SCANDATE")]
        public DateTime? C_ScanDate { get; set; }
        /// <summary>
        /// 打卡时间
        /// </summary>
        [Column("C_SCANTIME")]
        public DateTime? C_ScanTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("C_REMARK")]
        public string C_Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.ID = Guid.NewGuid().ToString();
            this.C_State = CheckState.成功;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.ID = keyValue;
            this.C_ScanDate = DateTime.Now;
            this.C_ScanTime = DateTime.Now;
        }
        #endregion
        #region 扩展字段
        /// <summary>
        /// 设备IP
        /// </summary>
        /// <returns></returns>
        [Column("C_IP")]
        public string C_Ip { get; set; }
        /// <summary>
        /// C_DeviceKey
        /// </summary>
        /// <returns></returns>
        [Column("C_DEVICEKEY")]
        public string C_DeviceKey { get; set; }
        /// <summary>
        /// 人员类型
        /// </summary>
        [Column("C_TYPE")]
        public string C_Type { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        /// <returns></returns>
        [Column("C_PERSONID")]
        public string C_PersonId { get; set; }
        /// <summary>
        /// 标识记录(0失败，1成功)
        /// </summary>
        /// <returns></returns>
        [Column("C_STATE")]
        public CheckState? C_State { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        /// <returns></returns>
        [Column("F_Account")]
        public string F_Account { get; set; }
        #endregion
    }

    public enum CheckState
    {
        成功=1,
        失败=0
    }
}

