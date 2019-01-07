using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 11:04
    /// 描 述：门列表
    /// </summary>
    public partial class Mes_DoorEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 门编码
        /// </summary>
        [Column("D_CODE")]
        public string D_Code { get; set; }
        /// <summary>
        /// 门名称
        /// </summary>
        [Column("D_NAME")]
        public string D_Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("D_REMARK")]
        public string D_Remark { get; set; }
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

