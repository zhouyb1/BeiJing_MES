using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-08-06 10:54
    /// 描 述：原物料入库价格表
    /// </summary>
    public partial class Mes_InPriceEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        [Column("P_SUPPLYCODE")]
        public string P_SupplyCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [Column("P_SUPPLYNAME")]
        public string P_SupplyName { get; set; }
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
        /// 供应商价格(含税)
        /// </summary>
        [Column("P_TAXPRICE")]
        public decimal? P_TaxPrice { get; set; }
        /// <summary>
        /// 供应商价格(不含税)
        /// </summary>
        [Column("P_INPRICE")]
        public decimal? P_InPrice { get; set; }
        /// <summary>
        /// 购进税率
        /// </summary>
        /// <returns></returns>
        [Column("P_ITAX")]
        public decimal? P_Itax { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Column("P_STARTDATE")]
        public DateTime? P_StartDate { get; set; }
        /// <summary>
        /// 到期时间
        /// </summary>
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
            this.P_TaxPrice = this.P_InPrice * (1 + (this.P_Itax / 100));
            //this.P_CreateDate = DateTime.Now;
            //this.P_CreateBy = userInfo.realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.ID = keyValue;
            this.P_TaxPrice = this.P_InPrice * (1 + (this.P_Itax / 100));
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

