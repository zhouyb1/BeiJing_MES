﻿using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-15 16:11
    /// 描 述：线边仓退料到仓库
    /// </summary>
    public partial class Mes_BackStockDetailEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 退仓库单号
        /// </summary>
        [Column("B_BACKSTOCKNO")]
        public string B_BackStockNo { get; set; }
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
        /// 批次
        /// </summary>
        [Column("B_BATCH")]
        public string B_Batch { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("B_REMARK")]
        public string B_Remark { get; set; }
        /// <summary>
        /// 单价 edit 2019年4月15日16:34:30
        /// </summary>
        [Column("B_PRICE")]
        public double? B_Price { get; set; }
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

