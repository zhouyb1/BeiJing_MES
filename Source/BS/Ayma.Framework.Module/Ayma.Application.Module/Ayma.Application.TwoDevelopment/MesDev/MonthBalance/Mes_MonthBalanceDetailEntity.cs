using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ayma.Application.TwoDevelopment.MesDev

{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-03-09 17:13
    /// 描 述：1
    /// </summary>
    public partial class Mes_MonthBalanceDetailEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 月结月份
        /// </summary>
        /// <returns></returns>
        [Column("M_MONTHS")]
        public string M_Months { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        /// <returns></returns>
        [Column("M_GOODSCODE")]
        public string M_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        /// <returns></returns>
        [Column("M_GOODSNAME")]
        public string M_GoodsName { get; set; }
        /// <summary>
        /// 仓库编码
        /// </summary>
        /// <returns></returns>
        [Column("M_STOCKCODE")]
        public string M_StockCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        /// <returns></returns>
        [Column("M_STOCKNAME")]
        public string M_StockName { get; set; }
        /// <summary>
        /// 本月库存数量
        /// </summary>
        /// <returns></returns>
        [Column("M_STOCKQTY")]
        public double? M_StockQty { get; set; }
        /// <summary>
        /// 上月库存
        /// </summary>
        /// <returns></returns>
        [Column("M_LASTQTY")]
        public double? M_LastQty { get; set; }
        /// <summary>
        /// 原料入库数量
        /// </summary>
        /// <returns></returns>
        [Column("M_INQTY")]
        public double? M_InQty { get; set; }
        /// <summary>
        /// 原料退供应商数量
        /// </summary>
        /// <returns></returns>
        [Column("M_BACKSUPPLYQTY")]
        public double? M_BackSupplyQty { get; set; }
        /// <summary>
        /// 领料数量
        /// </summary>
        /// <returns></returns>
        [Column("M_OUTQTY")]
        public double? M_OutQty { get; set; }
        /// <summary>
        /// 退库数量
        /// </summary>
        /// <returns></returns>
        [Column("M_BACKSTOCKQTY")]
        public double? M_BackStockQty { get; set; }
        /// <summary>
        /// 报废数量
        /// </summary>
        /// <returns></returns>
        [Column("M_SCRAPQTY")]
        public double? M_ScrapQty { get; set; }
        /// <summary>
        /// 其他入库数量
        /// </summary>
        /// <returns></returns>
        [Column("M_OTHERINQTY")]
        public double? M_OtherInQty { get; set; }
        /// <summary>
        /// 其他出库数量
        /// </summary>
        /// <returns></returns>
        [Column("M_OTHEROUTQTY")]
        public double? M_OtherOutQty { get; set; }
        /// <summary>
        /// 售卖数量
        /// </summary>
        /// <returns></returns>
        [Column("M_SALEQTY")]
        public double? M_SaleQty { get; set; }
        /// <summary>
        /// 消耗数量
        /// </summary>
        /// <returns></returns>
        [Column("M_EXPENDQTY")]
        public double? M_ExpendQty { get; set; }
        /// <summary>
        /// 调拨数量
        /// </summary>
        /// <returns></returns>
        [Column("M_REQUISTQTY")]
        public double? M_RequistQty { get; set; }
        /// <summary>
        /// 物料转换数量
        /// </summary>
        /// <returns></returns>
        [Column("M_ORGRESOUTQTY")]
        public double? M_OrgresOutQty { get; set; }
        /// <summary>
        /// 抽检数量
        /// </summary>
        /// <returns></returns>
        [Column("M_INSPECTQTY")]
        public double? M_InspectQty { get; set; }
        /// <summary>
        /// 车间到线边仓的入库数量
        /// </summary>
        /// <returns></returns>
        [Column("M_INWORKSHOPQTY")]
        public double? M_InWorkShopQty { get; set; }
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