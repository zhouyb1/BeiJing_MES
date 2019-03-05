using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-05 20:21
    /// 描 述：商品二级分类
    /// </summary>
    public partial class Mes_GoodKindEntity 
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
        [Column("G_CODE")]
        public string G_Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Column("G_NAME")]
        public string G_Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("G_REMARK")]
        public string G_Remark { get; set; }
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

