using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 10:20
    /// 描 述：调拨单制作
    /// </summary>
    public partial class Mes_RequistHeadEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 调拨单号
        /// </summary>
        [Column("R_REQUISTNO")]
        public string R_RequistNo { get; set; }
        /// <summary>
        /// 原仓库编码
        /// </summary>
        [Column("R_STOCKCODE")]
        public string R_StockCode { get; set; }
        /// <summary>
        /// 原仓库名称
        /// </summary>
        [Column("R_STOCKNAME")]
        public string R_StockName { get; set; }
        /// <summary>
        /// 调拨仓库编码
        /// </summary>
        [Column("R_STOCKTOCODE")]
        public string R_StockToCode { get; set; }
        /// <summary>
        /// 调拨仓库名称
        /// </summary>
        [Column("R_STOCKTONAME")]
        public string R_StockToName { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        [Column("P_ORDERNO")]
        public string P_OrderNo { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        [Column("P_ORDERDATE")]
        public DateTime? P_OrderDate { get; set; }
        /// <summary>
        /// 状态（1=单据生成，2=审核通过，3=单据完成，-1=单据删除）
   
        /// </summary>
        [Column("R_STATUS")]
        public ErpEnums.RequistStatusEnum? R_Status { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("R_CREATEBY")]
        public string R_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("R_CREATEDATE")]
        public DateTime? R_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("R_UPDATEBY")]
        public string R_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("R_UPDATEDATE")]
        public DateTime? R_UpdateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("R_REMARK")]
        public string R_Remark { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        [Column("R_DELETEBY")]
        public string R_DeleteBy { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [Column("R_DELETEDATE")]
        public DateTime? R_DeleteDate { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        [Column("R_UPLOADBY")]
        public string R_UploadBy { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        [Column("R_UPLOADDATE")]
        public DateTime? R_UploadDate { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = Guid.NewGuid().ToString();
            this.R_Status = ErpEnums.RequistStatusEnum.NoAudit;
            this.R_CreateDate = DateTime.Now;
            this.R_CreateBy = userInfo.userId;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = keyValue;
            this.R_UpdateDate = DateTime.Now;
            this.R_UpdateBy = userInfo.userId;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

