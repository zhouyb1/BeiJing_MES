﻿using Ayma.DataBase.SqlServer;
using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ayma.Application.TwoDevelopment.MesDev

{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-09-12 14:05
    /// 描 述：价格表
    /// </summary>
    public partial class Mes_PriceEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        /// <returns></returns>
        [Column("P_SUPPLYCODE")]
        public string P_SupplyCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        /// <returns></returns>
        [Column("P_SUPPLYNAME")]
        public string P_SupplyName { get; set; }
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
        /// 价格(不含税)
        /// </summary>
        /// <returns></returns>
        [Column("P_INPRICE")]
        [DecimalPrecision(18, 6)]
        public decimal? P_InPrice { get; set; }
        /// <summary>
        /// 供应商价格(含税)
        /// </summary>
        [Column("P_TAXPRICE")]
        [DecimalPrecision(18, 6)]
        public decimal? P_TaxPrice { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        /// <returns></returns>
        [Column("P_CREATEBY")]
        public string P_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        /// <returns></returns>
        [Column("P_CREATEDATE")]
        public DateTime? P_CreateDate { get; set; }
        /// <summary>
        /// P_Itax
        /// </summary>
        /// <returns></returns>
        [Column("P_ITAX")]
        [DecimalPrecision(18, 6)]
        public decimal? P_Itax { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        /// <returns></returns>
        [Column("P_STARTDATE")]
        public DateTime? P_StartDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        /// <returns></returns>
        [Column("P_ENDDATE")]
        public DateTime? P_EndDate { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = Guid.NewGuid().ToString();
            this.P_InPrice = (this.P_TaxPrice / (1 + (this.P_Itax / 100)));//含税价格 /1+税率
            this.P_CreateDate = DateTime.Now;
            this.P_CreateBy = userInfo.userId;
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

