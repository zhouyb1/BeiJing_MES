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
    public partial class Mes_PurchaseDetailEntity 
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
        /// 生产订单号
        /// </summary>
        [Column("P_ORDERNO")]
        public string P_OrderNo { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("P_GOODSCODE")]
        public string P_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("P_GOODSNAME")]
        public string P_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("P_UNIT")]
        public string P_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("P_QTY")]
        public double? P_Qty { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [Column("P_PRICE")]
        public double? P_Price { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Column("P_BATCH")]
        public string P_Batch { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("P_REMARK")]
        public string P_Remark { get; set; }
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

