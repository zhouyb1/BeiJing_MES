using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 11:57
    /// 描 述：出库单制作
    /// </summary>
    public partial class Mes_OutWorkShopHeadEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 出库单号
        /// </summary>
        [Column("O_OUTNO")]
        public string O_OutNo { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("O_STOCKCODE")]
        public string O_StockCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        [Column("O_STOCKNAME")]
        public string O_StockName { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        [Column("O_WORKSHOP")]
        public string O_WorkShop { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        [Column("O_ORDERNO")]
        public string O_OrderNo { get; set; }
        /// <summary>
        /// 生产订单时间
        /// </summary>
        [Column("O_ORDERDATE")]
        public DateTime? O_OrderDate { get; set; }
        /// <summary>
        /// 状态状态（1=单据生成，2=审核通过，3=单据完成，-1=单据删除）
        /// </summary>
        [Column("O_STATUS")]
        public ErpEnums.ProOutStatusEnum? O_Status { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("O_CREATEBY")]
        public string O_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("O_CREATEDATE")]
        public DateTime? O_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("O_UPDATEBY")]
        public string O_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("O_UPDATEDATE")]
        public DateTime? O_UpdateDate { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        [Column("O_DELETEBY")]
        public string O_DeleteBy { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [Column("O_DELETEDATE")]
        public DateTime? O_DeleteDate { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        [Column("O_UPLOADBY")]
        public string O_UploadBy { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        [Column("O_UPLOADDATE")]
        public DateTime? O_UploadDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("O_REMARK")]
        public string O_Remark { get; set; } 
        /// <summary>
        /// 出库类型 1-出库 2-退库
        /// </summary>
        [Column("O_KIND")]
        public int? O_Kind { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.ID = Guid.NewGuid().ToString();
            this.O_CreateBy = LoginUserInfo.Get().userId;
            this.O_CreateDate = DateTime.Now;
            this.O_Status = ErpEnums.ProOutStatusEnum.NoAudit;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.O_UpdateBy = LoginUserInfo.Get().userId;
            this.O_UpdateDate = DateTime.Now;
            this.ID = keyValue;
        }
        #endregion
        #region 扩展字段
        [NotMapped]
        public string O_WorkShopName { get; set; }
        #endregion
    }
}

