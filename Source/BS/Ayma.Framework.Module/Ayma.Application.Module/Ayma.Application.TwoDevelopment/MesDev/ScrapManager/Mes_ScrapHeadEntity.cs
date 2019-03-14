using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 11:20
    /// 描 述：报废单据管理
    /// </summary>
    public partial class Mes_ScrapHeadEntity 
    {
        #region 实体成员
        /// <summary>
        ///   主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 报废单号
        /// </summary>
        [Column("S_SCRAPNO")]
        public string S_ScrapNo { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("S_STOCKCODE")]
        public string S_StockCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("S_STOCKNAME")]
        public string S_StockName { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [Column("S_ORDERDATE")]
        public DateTime? S_OrderDate { get; set; }
        /// <summary>
        /// 状态（1=单据生成，2=审核通过，3=单据完成，-1=单据删除）
        /// </summary>
        [Column("S_STATUS")]
        public ErpEnums.ScrapStatusEnum? S_Status { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("S_CREATEBY")]
        public string S_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("S_CREATEDATE")]
        public DateTime? S_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("S_UPDATEBY")]
        public string S_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("S_UPDATEDATE")]
        public DateTime? S_UpdateDate { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        [Column("S_DELETEBY")]
        public string S_DeleteBy { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [Column("S_DELETEDATE")]
        public DateTime? S_DeleteDate { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        [Column("S_UPLOADBY")]
        public string S_UploadBy { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        [Column("S_UPLOADDATE")]
        public DateTime? S_UploadDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("S_REMARK")]
        public string S_Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.S_Status = ErpEnums.ScrapStatusEnum.NoAudit;
            this.ID = Guid.NewGuid().ToString();
            this.S_CreateBy = LoginUserInfo.Get().userId;
            this.S_CreateDate = DateTime.Now;
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

