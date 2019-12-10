using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ayma.Application.TwoDevelopment.MesDev

{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-12-10 15:07
    /// 描 述：IP与RFID对应表
    /// </summary>
    public partial class Mes_IPToRFIDEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        /// <returns></returns>
        [Column("I_IP")]
        public string I_IP { get; set; }
        /// <summary>
        /// RFID编码
        /// </summary>
        /// <returns></returns>
        [Column("I_RFIDCODE")]
        public string I_RFIDCode { get; set; }
        /// <summary>
        /// 门编码
        /// </summary>
        /// <returns></returns>
        [Column("I_DOORCODE")]
        public string I_DoorCode { get; set; }
        /// <summary>
        /// 门名称
        /// </summary>
        /// <returns></returns>
        [Column("I_DOORNAME")]
        public string I_DoorName { get; set; }
        /// <summary>
        /// 状态，0 禁用，1启用
        /// </summary>
        /// <returns></returns>
        [Column("I_STATUS")]
        public int? I_Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("I_REMARK")]
        public string I_Remark { get; set; }
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
    }
}

