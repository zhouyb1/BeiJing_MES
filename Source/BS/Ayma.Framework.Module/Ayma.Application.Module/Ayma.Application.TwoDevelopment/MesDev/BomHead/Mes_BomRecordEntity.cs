using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 17:41
    /// 描 述：配方表
    /// </summary>
    public partial class Mes_BomRecordEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 配方编码
        /// </summary>
        [Column("B_FORMULACODE")]
        public string B_FormulaCode { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("B_GOODSCODE")]
        public string B_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("B_GOODSNAME")]
        public string B_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("B_UNIT")]
        public string B_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("B_QTY")]
        public double? B_Qty { get; set; }
        /// <summary>
        /// 下级物料编码
        /// </summary>
        [Column("B_SECGOODSCODE")]
        public string B_SecGoodsCode { get; set; }
        /// <summary>
        /// 下级物料名称
        /// </summary>
        [Column("B_SECGOODSNAME")]
        public string B_SecGoodsName { get; set; }
        /// <summary>
        /// 下级物料数量
        /// </summary>
        [Column("B_SECQTY")]
        public double? B_SecQty { get; set; }
        /// <summary>
        /// 下级物料单位
        /// </summary>
        [Column("B_SECUNIT")]
        public string B_SecUnit { get; set; }
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

