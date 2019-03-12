using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-11 19:22
    /// 描 述：领料单
    /// </summary>
    public partial class Mes_CollarHeadEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 领料单号
        /// </summary>
        [Column("C_COLLARNO")]
        public string C_CollarNo { get; set; }
        /// <summary>
        /// 原仓库编码
        /// </summary>
        [Column("C_STOCKCODE")]
        public string C_StockCode { get; set; }
        /// <summary>
        /// 原仓库名称
        /// </summary>
        [Column("C_STOCKNAME")]
        public string C_StockName { get; set; }
        /// <summary>
        /// 调拨仓库编码
        /// </summary>
        [Column("C_STOCKTOCODE")]
        public string C_StockToCode { get; set; }
        /// <summary>
        /// 调拨仓库名称
        /// </summary>
        [Column("C_STOCKTONAME")]
        public string C_StockToName { get; set; }
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
        [Column("P_STATUS")]
        public ErpEnums.RequistStatusEnum P_Status { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("C_CREATEBY")]
        public string C_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("C_CREATEDATE")]
        public DateTime? C_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("C_UPDATEBY")]
        public string C_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("C_UPDATEDATE")]
        public DateTime? C_UpdateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("C_REMARK")]
        public string C_Remark { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        [Column("M_DELETEBY")]
        public string M_DeleteBy { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [Column("M_DELETEDATE")]
        public DateTime? M_DeleteDate { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        [Column("M_UPLOADBY")]
        public string M_UploadBy { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        [Column("M_UPLOADDATE")]
        public DateTime? M_UploadDate { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.P_Status = ErpEnums.RequistStatusEnum.NoAudit;
            this.C_CreateBy = LoginUserInfo.Get().userId;
            this.C_CreateDate = DateTime.Now;
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

