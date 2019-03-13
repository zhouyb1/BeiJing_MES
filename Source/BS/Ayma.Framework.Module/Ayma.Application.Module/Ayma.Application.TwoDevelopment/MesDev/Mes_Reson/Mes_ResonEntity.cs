using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 19:30
    /// 描 述：不合格原因表
    /// </summary>
    public partial class Mes_ResonEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 原因编码
        /// </summary>
        [Column("R_CODE")]
        public string R_Code { get; set; }
        /// <summary>
        /// 原因名称
        /// </summary>
        [Column("R_NAME")]
        public string R_Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("R_REMARK")]
        public string R_Remark { get; set; }
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

