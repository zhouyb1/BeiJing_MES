using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-07 10:38
    /// 描 述：原物料(半成品)销售
    /// </summary>
    public partial class Mes_SaleHeadEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        [Column("S_SALENO")]
        public string S_SaleNo { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("S_STOCKCODE")]
        public string S_StockCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        [Column("S_STOCKNAME")]
        public string S_StockName { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        [Column("S_COSTOMCODE")]
        public string S_CostomCode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [Column("S_COSTOMNAME")]
        public string S_CostomName { get; set; }
        /// <summary>
        /// 状态（1=单据生成，2=审核通过，3=单据完成，-1=单据删除）
        /// </summary>
        [Column("S_STATUS")]
        public ErpEnums.SaleOutStatusEnum? S_Status { get; set; }
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
        /// <summary>
        /// 月结
        /// </summary>
        [Column("MONTHBALANCE")]
        public string MonthBalance { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.ID = Guid.NewGuid().ToString();
            this.S_CreateBy = LoginUserInfo.Get().userId;
            this.S_Status = ErpEnums.SaleOutStatusEnum.NoAudit;
            this.S_CreateDate=DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.ID = keyValue;
            this.S_UpdateBy=LoginUserInfo.Get().userId;
            this.S_UpdateDate=DateTime.Now;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

