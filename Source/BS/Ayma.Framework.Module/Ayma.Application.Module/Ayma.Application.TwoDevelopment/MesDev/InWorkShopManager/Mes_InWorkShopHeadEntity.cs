using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 11:57
    /// 描 述：车间入库到线边仓 表头
    /// </summary>
    public partial class Mes_InWorkShopHeadEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 入库单号
        /// </summary>
        [Column("I_INNO")]
        public string I_InNo { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("I_STOCKCODE")]
        public string I_StockCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        [Column("I_STOCKNAME")]
        public string I_StockName { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        [Column("I_WORKSHOP")]
        public string I_WorkShop { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        [Column("I_ORDERNO")]
        public string I_OrderNo { get; set; }
        /// <summary>
        /// 生产订单时间
        /// </summary>
        [Column("I_ORDERDATE")]
        public DateTime? I_OrderDate { get; set; }
        /// <summary>
        /// 状态状态（1=单据生成，2=审核通过，3=单据完成，-1=单据删除）
        /// </summary>
        [Column("I_STATUS")]
        public ErpEnums.InStatusEnum? I_Status { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("I_CREATEBY")]
        public string I_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("I_CREATEDATE")]
        public DateTime? I_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("I_UPDATEBY")]
        public string I_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("I_UPDATEDATE")]
        public DateTime? I_UpdateDate { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        [Column("I_DELETEBY")]
        public string I_DeleteBy { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [Column("I_DELETEDATE")]
        public DateTime? I_DeleteDate { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        [Column("I_UPLOADBY")]
        public string I_UploadBy { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        [Column("I_UPLOADDATE")]
        public DateTime? I_UploadDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("I_REMARK")]
        public string I_Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.ID = Guid.NewGuid().ToString();
            this.I_CreateBy = LoginUserInfo.Get().userId;
            this.I_CreateDate = DateTime.Now;
            this.I_Status = ErpEnums.InStatusEnum.NoAudit;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.I_UpdateBy = LoginUserInfo.Get().userId;
            this.I_UpdateDate = DateTime.Now;
            this.ID = keyValue;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

