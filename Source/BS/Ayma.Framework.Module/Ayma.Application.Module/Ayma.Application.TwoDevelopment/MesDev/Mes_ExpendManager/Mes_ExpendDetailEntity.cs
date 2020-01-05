using Ayma.DataBase.SqlServer;
using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-08 13:59
    /// 描 述：消耗物料
    /// </summary>
    public partial class Mes_ExpendDetailEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        [Column("E_EXPENDNO")]
        public string E_ExpendNo { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("E_GOODSCODE")]
        public string E_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("E_GOODSNAME")]
        public string E_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("E_UNIT")]
        public string E_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("E_QTY")]
        [DecimalPrecision(18, 6)]
        public decimal? E_Qty { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Column("E_BATCH")]
        public string E_Batch { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [Column("E_PRICE")]
        [DecimalPrecision(18, 6)]
        public decimal? E_Price { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("E_REMARK")]
        public string E_Remark { get; set; }
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

