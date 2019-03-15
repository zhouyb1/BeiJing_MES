using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 10:06
    /// 描 述：领料单查询
    /// </summary>
    public partial class Mes_CollarViewModel
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string P_Status { get; set; }

        /// <summary>
        /// 领料单号
        /// </summary>
        [Column("C_COLLARNO")]
        public string C_CollarNo { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        [Column("C_ORDERNO")]
        public string P_OrderNo { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("C_GOODSCODE")]
        public string C_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("C_GOODSNAME")]
        public string C_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("C_UNIT")]
        public string C_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("C_QTY")]
        public decimal? C_Qty { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Column("C_BATCH")]
        public string C_Batch { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("C_REMARK")]
        public string C_Remark { get; set; }
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
    }
}

