﻿using Ayma.DataBase.SqlServer;
using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 11:23
    /// 描 述：报废单据管理
    /// </summary>
    public partial class Mes_ScrapDetailEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 报废单号
        /// </summary>
        [Column("S_SCRAPNO")]
        public string S_ScrapNo { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("S_GOODSCODE")]
        public string S_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("S_GOODSNAME")]
        public string S_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("S_UNIT")]
        public string S_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("S_QTY")]
        [DecimalPrecision(18, 6)]
        public decimal? S_Qty { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        [Column("S_BATCH")]
        public string S_Batch { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("S_REMARK")]
        public string S_Remark { get; set; }
        /// <summary>
        /// 价格 edit 2019年4月15日15:05:02
        /// </summary>
        [Column("S_PRICE")]
        [DecimalPrecision(18, 6)]
        public decimal? S_Price { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        [DecimalPrecision(18, 6)]
        [NotMapped]
        public decimal? S_Amount { get; set; }

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