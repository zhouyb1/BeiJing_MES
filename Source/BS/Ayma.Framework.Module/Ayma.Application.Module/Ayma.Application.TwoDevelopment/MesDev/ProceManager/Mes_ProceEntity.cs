using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 14:55
    /// 描 述：工序管理
    /// </summary>
    public partial class Mes_ProceEntity 
    {
        #region 实体成员
        /// <summary>
        /// 工艺代码
        /// </summary>
        [Column("P_RECORD")]
        public string P_Record { get; set; }
        /// <summary>
        /// 工序号
        /// </summary>
        [Column("P_PROCODE")]
        public string P_ProCode { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        [Column("P_PRONAME")]
        public string P_ProName { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        [Column("P_WORKSHOP")]
        public string P_WorkShop { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("P_REMARK")]
        public string P_Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.P_Record = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.P_Record = keyValue;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

