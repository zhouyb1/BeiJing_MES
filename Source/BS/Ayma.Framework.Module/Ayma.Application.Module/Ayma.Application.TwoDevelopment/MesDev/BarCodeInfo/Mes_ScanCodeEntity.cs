using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ayma.Application.TwoDevelopment.MesDev.BarCodeInfo

{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-01-14 10:46
    /// 描 述：套餐二维码信息
    /// </summary>
    public partial class Mes_ScanCodeEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// S_Code
        /// </summary>
        /// <returns></returns>
        [Column("S_CODE")]
        public string S_Code { get; set; }
        /// <summary>
        /// S_Name
        /// </summary>
        /// <returns></returns>
        [Column("S_NAME")]
        public string S_Name { get; set; }
        /// <summary>
        /// S_MaterName
        /// </summary>
        /// <returns></returns>
        [Column("S_MATERNAME")]
        public string S_MaterName { get; set; }
        /// <summary>
        /// S_Date
        /// </summary>
        /// <returns></returns>
        [Column("S_DATE")]
        public string S_Date { get; set; }
        /// <summary>
        /// S_Producer
        /// </summary>
        /// <returns></returns>
        [Column("S_PRODUCER")]
        public string S_Producer { get; set; }
        /// <summary>
        /// S_Quality
        /// </summary>
        /// <returns></returns>
        [Column("S_QUALITY")]
        public string S_Quality { get; set; }
        /// <summary>
        /// S_Team
        /// </summary>
        /// <returns></returns>
        [Column("S_TEAM")]
        public string S_Team { get; set; }
        /// <summary>
        /// S_Standard
        /// </summary>
        /// <returns></returns>
        [Column("S_STANDARD")]
        public string S_Standard { get; set; }
        /// <summary>
        /// S_Storage
        /// </summary>
        /// <returns></returns>
        [Column("S_STORAGE")]
        public string S_Storage { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify( string keyValue)
        {
            this. ID= keyValue;
        }
        #endregion
    }
}

