using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.Tools
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-19 15:30
    /// 描 述：物料转换实体
    /// </summary>
    public partial class Mes_ConvertEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// C_Code
        /// </summary>
        [Column("C_CODE")]
        public string C_Code { get; set; }
        /// <summary>
        /// C_Name
        /// </summary>
        [Column("C_NAME")]
        public string C_Name { get; set; }
        /// <summary>
        /// C_SecCode
        /// </summary>
        [Column("C_SECCODE")]
        public string C_SecCode { get; set; }
        /// <summary>
        /// C_SecName
        /// </summary>
        [Column("C_SECNAME")]
        public string C_SecName { get; set; }
        /// <summary>
        /// C_CreateBy
        /// </summary>
        [Column("C_CREATEBY")]
        public string C_CreateBy { get; set; }
        /// <summary>
        /// C_CreateDate
        /// </summary>
        [Column("C_CREATEDATE")]
        public DateTime? C_CreateDate { get; set; }
        /// <summary>
        /// C_UpdateBy
        /// </summary>
        [Column("C_UPDATEBY")]
        public string C_UpdateBy { get; set; }
        /// <summary>
        /// C_UpdateDate
        /// </summary>
        [Column("C_UPDATEDATE")]
        public DateTime? C_UpdateDate { get; set; }
        /// <summary>
        /// C_Remark
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