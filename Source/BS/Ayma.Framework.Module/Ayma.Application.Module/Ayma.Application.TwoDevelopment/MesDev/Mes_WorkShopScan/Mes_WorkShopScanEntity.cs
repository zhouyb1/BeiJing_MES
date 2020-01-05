using Ayma.DataBase.SqlServer;
using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ayma.Application.TwoDevelopment.MesDev

{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-12-21 17:37
    /// 描 述：车间物料扫描表
    /// </summary>
    public partial class Mes_WorkShopScanEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 工艺代码
        /// </summary>
        /// <returns></returns>
        [Column("W_RECORDCODE")]
        public string W_RecordCode { get; set; }
        /// <summary>
        /// 工艺名称
        /// </summary>
        /// <returns></returns>
        [Column("W_RECORDNAME")]
        public string W_RecordName { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        /// <returns></returns>
        [Column("W_STOCKCODE")]
        public string W_StockCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        /// <returns></returns>
        [Column("W_STOCKNAME")]
        public string W_StockName { get; set; }
        /// <summary>
        /// 车间编码
        /// </summary>
        /// <returns></returns>
        [Column("W_WORKSHOP")]
        public string W_WorkShop { get; set; }
        /// <summary>
        /// 车间名称
        /// </summary>
        /// <returns></returns>
        [Column("W_WORKSHOPNAME")]
        public string W_WorkShopName { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        /// <returns></returns>
        [Column("W_ORDERNO")]
        public string W_OrderNo { get; set; }
        /// <summary>
        /// 状态（1扫描，2提交）
        /// </summary>
        /// <returns></returns>
        [Column("W_STATUS")]
        public int? W_Status { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        /// <returns></returns>
        [Column("W_CREATEBY")]
        public string W_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        [Column("W_CREATEDATE")]
        public DateTime? W_CreateDate { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        /// <returns></returns>
        [Column("W_GOODSCODE")]
        public string W_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        /// <returns></returns>
        [Column("W_GOODSNAME")]
        public string W_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        /// <returns></returns>
        [Column("W_UNIT")]
        public string W_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        [Column("W_QTY")]
        [DecimalPrecision(18, 6)]
        public decimal? W_Qty { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        /// <returns></returns>
        [Column("W_BATCH")]
        public string W_Batch { get; set; }
        /// <summary>
        /// W_Price
        /// </summary>
        /// <returns></returns>
        [Column("W_PRICE")]
        [DecimalPrecision(18, 6)]
        public decimal? W_Price { get; set; }
        /// <summary>
        /// W_Remark
        /// </summary>
        /// <returns></returns>
        [Column("W_REMARK")]
        public string W_Remark { get; set; }
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

