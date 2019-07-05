using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 16:30
    /// 描 述：退供应商单模型
    /// </summary>
    public partial class Mes_BackSupplyHeadModel 
    {
        #region 实体成员
        /// <summary>
        ///   主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 退供应商单号
        /// </summary>
        [Column("B_BACKSUPPLYNO")]
        public string B_BackSupplyNo { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("B_STOCKCODE")]
        public string B_StockCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("B_STOCKNAME")]
        public string B_StockName { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [Column("B_ORDERDATE")]
        public DateTime? B_OrderDate { get; set; }
        /// <summary>
        /// 状态（1=单据生成，2=审核通过，3=单据完成，-1=单据删除）
        /// </summary>
        [Column("B_STATUS")]
        public ErpEnums.BackSupplyStatusEnum B_Status { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("B_CREATEBY")]
        public string B_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("B_CREATEDATE")]
        public DateTime? B_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("B_UPDATEBY")]
        public string B_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("B_UPDATEDATE")]
        public DateTime? B_UpdateDate { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        [Column("B_DELETEBY")]
        public string B_DeleteBy { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [Column("B_DELETEDATE")]
        public DateTime? B_DeleteDate { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        [Column("B_UPLOADBY")]
        public string B_UploadBy { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        [Column("B_UPLOADDATE")]
        public DateTime? B_UploadDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("B_REMARK")]
        public string B_Remark { get; set; }
        #endregion

    }
}

