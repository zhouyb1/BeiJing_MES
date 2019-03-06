using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 13:52
    /// 描 述：排班管理
    /// </summary>
    public partial class Mes_ClassEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [Column("C_CODE")]
        public string C_Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Column("C_NAME")]
        public string C_Name { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Column("C_STARTTIME")]
        public string C_StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Column("C_ENDTIME")]
        public string C_EndTime { get; set; }
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

