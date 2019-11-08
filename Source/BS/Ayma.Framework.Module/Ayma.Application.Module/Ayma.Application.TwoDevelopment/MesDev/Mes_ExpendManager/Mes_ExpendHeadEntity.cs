using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-08 13:59
    /// 描 述：消耗物料
    /// </summary>
    public partial class Mes_ExpendHeadEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 消耗单号
        /// </summary>
        [Column("E_EXPENDNO")]
        public string E_ExpendNo { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("E_STOCKCODE")]
        public string E_StockCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        [Column("E_STOCKNAME")]
        public string E_StockName { get; set; }
        /// <summary>
        /// 单据时间
        /// </summary>
        [Column("E_ORDERDATE")]
        public DateTime? E_OrderDate { get; set; }
        /// <summary>
        /// 状态（1=单据生成，2=审核通过，3=单据完成，-1=单据删除）
        /// </summary>
        [Column("E_STATUS")]
        public ErpEnums.ExpendStatus? E_Status { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("E_CREATEBY")]
        public string E_CreateBy { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        [Column("E_CREATEDATE")]
        public DateTime? E_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("E_UPDATEBY")]
        public string E_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("E_UPDATEDATE")]
        public DateTime? E_UpdateDate { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        [Column("E_DELETEBY")]
        public string E_DeleteBy { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [Column("E_DELETEDATE")]
        public DateTime? E_DeleteDate { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        [Column("E_UPLOADBY")]
        public string E_UploadBy { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        [Column("E_UPLOADDATE")]
        public DateTime? E_UploadDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("E_REMARK")]
        public string E_Remark { get; set; }
        /// <summary>
        /// MonthBalance
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
            this.E_Status = ErpEnums.ExpendStatus.NoAudit;
            this.E_CreateBy = LoginUserInfo.Get().userId;
            this.E_CreateDate=DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.ID = keyValue;
            this.E_UpdateDate = DateTime.Now;
            this.E_UpdateBy = LoginUserInfo.Get().userId;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

