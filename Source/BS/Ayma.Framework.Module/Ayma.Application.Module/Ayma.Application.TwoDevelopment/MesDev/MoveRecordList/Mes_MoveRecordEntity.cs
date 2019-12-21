using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 15:11
    /// 描 述：人员走动记录列表
    /// </summary>
    public partial class Mes_MoveRecordEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        [Column("M_USERCODE")]
        public string M_UserCode { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        [Column("M_USERNAME")]
        public string M_UserName { get; set; }
        /// <summary>
        /// iP
        /// </summary>
        [Column("M_IP")]
        public string M_IP { get; set; }
        /// <summary>
        /// RFID编码
        /// </summary>
        [Column("M_RFIDCODE")]
        public string M_RFIDCode { get; set; }
        /// <summary>
        /// 门编码
        /// </summary>
        [Column("M_DOORCODE")]
        public string M_DoorCode { get; set; }
        /// <summary>
        /// 门名称
        /// </summary>
        [Column("M_DOORNAME")]
        public string M_DoorName { get; set; }
        /// <summary>
        /// 进出状态
        /// </summary>
        [Column("M_STATUS")]
        public string M_Status { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        [Column("M_DATE")]
        public DateTime? M_Date { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("M_REMARK")]
        public string M_Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.ID = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.ID = keyValue;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

