using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 14:14
    /// 描 述：工艺代码表 模型
    /// </summary>
    public partial class Mes_RecordModel 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [Column("R_RECORD")]
        public string R_Record { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Column("R_NAME")]
        public string R_Name { get; set; } 
        /// <summary>
        /// 成品物料编码
        /// </summary>
        [Column("R_GOODSCODE")]
        public string R_GoodsCode { get; set; }
        #endregion

        
        #region 扩展字段
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("GOODSNAME")]
        public string GoodsName { get; set; }
        #endregion
    }
}

