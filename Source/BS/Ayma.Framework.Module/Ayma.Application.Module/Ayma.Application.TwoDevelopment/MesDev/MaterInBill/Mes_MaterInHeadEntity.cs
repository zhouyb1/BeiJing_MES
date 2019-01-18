using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 14:58
    /// 描 述：入库单制作
    /// </summary>
    public partial class Mes_MaterInHeadEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 商品类型
        /// </summary>
        [Column("M_KIND")]
        public string M_Kind { get; set; }
        /// <summary>
        /// 入库单号
        /// </summary>
        [Column("M_MATERINNO")]
        public string M_MaterInNo { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("M_STOCKCODE")]
        public string M_StockCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("M_STOCKNAME")]
        public string M_StockName { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        [Column("M_ORDERNO")]
        public string M_OrderNo { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        [Column("M_ORDERDATE")]
        public DateTime? M_OrderDate { get; set; }
        /// <summary>
        /// 状态（1=单据生成，2=审核通过，3=单据完成，-1=单据删除）
   
        /// </summary>
        [Column("M_STATUS")]
        public string M_Status { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("M_CREATEBY")]
        public string M_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("M_CREATEDATE")]
        public DateTime? M_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("M_UPDATEBY")]
        public string M_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("M_UPDATEDATE")]
        public DateTime? M_UpdateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("M_REMARK")]
        public string M_Remark { get; set; }
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
        /// M_UploadDate
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

