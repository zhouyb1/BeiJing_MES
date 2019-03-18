using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-18 15:14
    /// 描 述：组装与拆分单据制作
    /// </summary>
    public partial class Mes_OrgResHeadEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 组织与拆分单据号
        /// </summary>
        [Column("O_ORGRESNO")]
        public string O_OrgResNo { get; set; }
        /// <summary>
        /// 工艺代码
        /// </summary>
        [Column("O_RECORD")]
        public string O_Record { get; set; }
        /// <summary>
        /// 工序号
        /// </summary>
        [Column("O_PROCODE")]
        public string O_ProCode { get; set; }
        /// <summary>
        /// 车间编码
        /// </summary>
        [Column("O_WORKSHOPCODE")]
        public string O_WorkShopCode { get; set; }
        /// <summary>
        /// 车间名称
        /// </summary>
        [Column("O_WORKSHOPNAME")]
        public string O_WorkShopName { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Column("O_ORDERNO")]
        public string O_OrderNo { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        [Column("O_ORDERDATE")]
        public DateTime? O_OrderDate { get; set; }
        /// <summary>
        /// 状态（1=单据生成，2=审核通过，3=单据完成，-1=单据删除）
   
        /// </summary>
        [Column("O_STATUS")]
        public string O_Status { get; set; }
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
        /// 备注
        /// </summary>
        [Column("O_REMARK")]
        public string O_Remark { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
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

