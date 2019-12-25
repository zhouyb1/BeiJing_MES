using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ayma.Application.TwoDevelopment.MesDev

{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-08 13:40
    /// 描 述：其它出库单
    /// </summary>
    public partial class Mes_OtherOutHeadEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        /// <returns></returns>
        [Column("O_OTHEROUTNO")]
        public string O_OtherOutNo { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        /// <returns></returns>
        [Column("O_STOCKCODE")]
        public string O_StockCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        /// <returns></returns>
        [Column("O_STOCKNAME")]
        public string O_StockName { get; set; }
        /// <summary>
        /// 部门编码
        /// </summary>
        /// <returns></returns>
        [Column("O_DEPARTCODE")]
        public string O_DepartCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        /// <returns></returns>
        [Column("O_DEPARTNAME")]
        public string O_DepartName { get; set; }
        /// <summary>
        /// 状态（1=单据生成，2=审核通过，3=单据完成，-1=单据删除）
        /// </summary>
        /// <returns></returns>
        [Column("O_STATUS")]
        public ErpEnums.OtherInStatusEnum O_Status { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        /// <returns></returns>
        [Column("O_CREATEBY")]
        public string O_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        [Column("O_CREATEDATE")]
        public DateTime? O_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        /// <returns></returns>
        [Column("O_UPDATEBY")]
        public string O_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        [Column("O_UPDATEDATE")]
        public DateTime? O_UpdateDate { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        /// <returns></returns>
        [Column("O_DELETEBY")]
        public string O_DeleteBy { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        /// <returns></returns>
        [Column("O_DELETEDATE")]
        public DateTime? O_DeleteDate { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        /// <returns></returns>
        [Column("O_UPLOADBY")]
        public string O_UploadBy { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        /// <returns></returns>
        [Column("O_UPLOADDATE")]
        public DateTime? O_UploadDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("O_REMARK")]
        public string O_Remark { get; set; }
        /// <summary>
        /// 月结
        /// </summary>
        /// <returns></returns>
        [Column("MONTHBALANCE")]
        public string MonthBalance { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = Guid.NewGuid().ToString();
            this.O_Status = ErpEnums.OtherInStatusEnum.NoAudit;
            this.O_CreateDate = DateTime.Now;
            this.O_CreateBy = userInfo.userId;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = keyValue;
            this.O_UpdateDate = DateTime.Now;
            this.O_UpdateBy = userInfo.userId;
        }
        #endregion
    }
}

