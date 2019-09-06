using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-11 19:22
    /// 描 述：领料单
    /// </summary>
    public partial class Mes_CollarDetailEntity 
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
        /// 订单号
        /// </summary>
        [Column("C_ORDERNO")]
        public string C_OrderNo { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        [Column("C_SUPPLYCODE")]
        public string C_SupplyCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [Column("C_SUPPLYNAME")]
        public string C_SupplyName { get; set; }
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
        /// 价格
        /// </summary>
        [Column("C_PRICE")]
        public decimal? C_Price { get; set; }
        /// <summary>
        /// 班组
        /// </summary>
        [Column("C_TEAMCODE")]
        public string C_TeamCode { get; set; }

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

