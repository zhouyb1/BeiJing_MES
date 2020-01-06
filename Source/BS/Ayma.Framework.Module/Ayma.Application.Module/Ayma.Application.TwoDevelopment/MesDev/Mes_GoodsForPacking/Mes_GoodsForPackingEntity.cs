using Ayma.DataBase.SqlServer;
using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ayma.Application.TwoDevelopment.MesDev

{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-01-05 11:30
    /// 描 述：半成品待移交表
    /// </summary>
    public partial class Mes_GoodsForPackingEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 组装单号
        /// </summary>
        /// <returns></returns>
        [Column("P_RESNO")]
        public string P_ResNo { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        /// <returns></returns>
        [Column("P_GOODSCODE")]
        public string P_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        /// <returns></returns>
        [Column("P_GOODSNAME")]
        public string P_GoodsName { get; set; }
        /// <summary>
        /// P_Oty
        /// </summary>
        /// <returns></returns>
        [Column("P_OTY")]
        [DecimalPrecision]
        public decimal? P_Oty { get; set; }
        /// <summary>
        /// 剩余数量
        /// </summary>
        /// <returns></returns>
        [Column("P_RESTQTY")]
        [DecimalPrecision]
        public decimal? P_RestQty { get; set; }
        /// <summary>
        /// 日耗仓编码
        /// </summary>
        /// <returns></returns>
        [Column("P_STOCKCODE")]
        public string P_StockCode { get; set; }
        /// <summary>
        /// 日耗仓名称
        /// </summary>
        /// <returns></returns>
        [Column("P_STOCKNAME")]
        public string P_StockName { get; set; }
        /// <summary>
        /// 生产时间
        /// </summary>
        /// <returns></returns>
        [Column("P_PROCUTIONDATE")]
        public DateTime? P_ProcutionDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("P_REMARK")]
        public string P_Remark { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Column("P_BATCH")]
        public string P_Batch { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Column("P_UNIT")]
        public string P_Unit { get; set; }
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

