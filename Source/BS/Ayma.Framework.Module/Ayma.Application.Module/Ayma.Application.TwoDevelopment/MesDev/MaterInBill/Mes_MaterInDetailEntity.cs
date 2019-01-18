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
    public partial class Mes_MaterInDetailEntity 
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
        /// 生产订单号
        /// </summary>
        [Column("M_ORDERNO")]
        public string M_OrderNo { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("M_GOODSCODE")]
        public string M_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("M_GOODSNAME")]
        public string M_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("M_UNIT")]
        public string M_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("M_QTY")]
        public decimal? M_Qty { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Column("M_BATCH")]
        public string M_Batch { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("M_REMARK")]
        public string M_Remark { get; set; }
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

