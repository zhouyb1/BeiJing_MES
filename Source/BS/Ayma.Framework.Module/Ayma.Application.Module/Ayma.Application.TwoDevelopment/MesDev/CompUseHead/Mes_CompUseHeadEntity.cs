using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-07-04 16:14
    /// 描 述：强制使用记录单据
    /// </summary>
    public partial class Mes_CompUseHeadEntity 
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
        [Column("C_NO")]
        public string C_No { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        [Column("C_WORKSHOP")]
        public string C_WorkShop { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Column("C_ORDERNO")]
        public string C_OrderNo { get; set; }    
        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("C_STOCKCODE")]
        public string C_StockCode { get; set; }  
        /// <summary>
        /// 仓库名称
        /// </summary>
        [Column("C_STOCKNAME")]
        public string C_StockName { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        [Column("C_ORDERDATE")]
        public DateTime? C_OrderDate { get; set; }
        /// <summary>
        /// 状态（1=单据生成，2=审核通过，3=单据完成，-1=单据删除）
        /// </summary>
        [Column("C_STATUS")]
        public ErpEnums.CompUserStatusEnum? C_Status { get; set; }
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
        /// 删除人
        /// </summary>
        [Column("C_DELETEBY")]
        public string C_DeleteBy { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [Column("C_DELETEDATE")]
        public DateTime? C_DeleteDate { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        [Column("C_UPLOADBY")]
        public string C_UploadBy { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        [Column("C_UPLOADDATE")]
        public DateTime? C_UploadDate { get; set; }
        /// <summary>
        /// 备注
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
            var userInfo = LoginUserInfo.Get();
            this.ID = Guid.NewGuid().ToString();
            this.C_Status = ErpEnums.CompUserStatusEnum.NoAudit;
            this.C_CreateDate = DateTime.Now;
            this.C_CreateBy = userInfo.userId;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = keyValue;
            this.C_UpdateDate = DateTime.Now;
            this.C_UpdateBy = userInfo.userId;
        }
        #endregion
        #region 扩展字段
        /// <summary>
        /// 车间名称
        /// </summary>
        [NotMapped]
        public string C_WorkShopName { get; set; }
        #endregion
    }
}

