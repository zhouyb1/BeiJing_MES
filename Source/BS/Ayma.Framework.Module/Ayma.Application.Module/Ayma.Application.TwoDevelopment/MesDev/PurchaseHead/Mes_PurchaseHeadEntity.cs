using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-05 11:20
    /// 描 述：采购单制作及查询
    /// </summary>
    public partial class Mes_PurchaseHeadEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 采购单号
        /// </summary>
        [Column("P_PURCHASENO")]
        public string P_PurchaseNo { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        [Column("P_STOCKCODE")]
        public string P_StockCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        [Column("P_STOCKNAME")]
        public string P_StockName { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        [Column("P_SUPPLYCODE")]
        public string P_SupplyCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [Column("P_SUPPLYNAME")]
        public string P_SupplyName { get; set; }
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
        public string P_Status { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("P_CREATEBY")]
        public string P_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("P_CREATEDATE")]
        public DateTime? P_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("P_UPDATEBY")]
        public string P_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("P_UPDATEDATE")]
        public string P_UpdateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("P_REMARK")]
        public DateTime? P_Remark { get; set; }
        /// <summary>
        /// 删除人
        /// </summary>
        [Column("P_DELETEBY")]
        public string P_DeleteBy { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [Column("P_DELETEDATE")]
        public DateTime? P_DeleteDate { get; set; }
        /// <summary>
        /// 提交人
        /// </summary>
        [Column("P_UPLOADBY")]
        public string P_UploadBy { get; set; }
        /// <summary>
        /// 提交时间
        /// </summary>
        [Column("P_UPLOADDATE")]
        public DateTime? P_UploadDate { get; set; }
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

