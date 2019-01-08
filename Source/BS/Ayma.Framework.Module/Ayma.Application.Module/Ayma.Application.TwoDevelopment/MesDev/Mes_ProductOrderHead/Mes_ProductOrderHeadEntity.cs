﻿using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 17:54
    /// 描 述：查询生成清单
    /// </summary>
    public partial class Mes_ProductOrderHeadEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        [Column("P_ORDERNO")]
        public string P_OrderNo { get; set; }
        /// <summary>
        /// 订单时间
        /// </summary>
        [Column("P_ORDERDATE")]
        public DateTime? P_OrderDate { get; set; }
        /// <summary>
        /// 车站ID
        /// </summary>
        [Column("P_ORDERSTATIONID")]
        public string P_OrderStationID { get; set; }
        /// <summary>
        /// 车站名称
        /// </summary>
        [Column("P_ORDERSTATIONNAME")]
        public string P_OrderStationName { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("P_CREATEBY")]
        public string P_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("P_CREATEDATE")]
        public DateTime? P_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("P_UPDATEBY")]
        public string P_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("P_UPDATEDATE")]
        public DateTime? P_UpdateDate { get; set; }
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
        #region 扩展字段
        /// <summary>
        /// 物料编码
        /// </summary>
        [NotMapped]
        public string P_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [NotMapped]
        public string P_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [NotMapped]
        public string P_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [NotMapped]
        public string P_Qty { get; set; }
        #endregion
    }
}
