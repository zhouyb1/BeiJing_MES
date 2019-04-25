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
        /// 用户编码
        /// </summary>
        [Column("C_USERCODE")]
        public string C_UserCode { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        [Column("C_USERNAME")]
        public string C_UserName { get; set; }
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

