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
    public partial class Mes_ScanRecordEntity 
    {
        #region 实体成员
        /// <summary>
        /// S_Barcode
        /// </summary>
        /// <returns></returns>
        [Column("S_BARCODE")]
        public string S_Barcode { get; set; }
        /// <summary>
        /// S_PrintfTime
        /// </summary>
        /// <returns></returns>
        [Column("S_PRINTFTIME")]
        public string S_PrintfTime { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// S_ScanCount
        /// </summary>
        /// <returns></returns>
        [Column("S_SCANCOUNT")]
        public int? S_ScanCount { get; set; }
        /// <summary>
        /// S_ScanTime
        /// </summary>
        /// <returns></returns>
        [Column("S_SCANTIME")]
        public DateTime? S_ScanTime { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.ID = Guid.NewGuid().ToString();
            this.S_ScanCount = 1;
            this.S_ScanTime = DateTime.Now;
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

