using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 13:55
    /// 描 述：物料列表
    /// </summary>
    public partial class Mes_GoodsEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        [Column("G_CODE")]
        public string G_Code { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("G_NAME")]
        public string G_Name { get; set; }
        /// <summary>
        /// 商品类型
        /// </summary>
        [Column("G_KIND")]
        public string G_Kind { get; set; }
        /// <summary>
        /// 保质时间
        /// </summary>
        [Column("G_PERIOD")]
        public string G_Period { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        [Column("G_PRICE")]
        public string G_Price { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("G_UNIT")]
        public string G_Unit { get; set; }
        /// <summary>
        /// 供应商编码
        /// </summary>
        [Column("G_SUPPLYCODE")]
        public string G_SupplyCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [Column("G_SUPPLY")]
        public string G_Supply { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("G_QTY")]
        public decimal? G_Qty { get; set; }
        /// <summary>
        /// 上限预警比例
        /// </summary>
        [Column("G_SUPER")]
        public decimal? G_Super { get; set; }
        /// <summary>
        /// 下限预警比例
        /// </summary>
        [Column("G_LOWER")]
        public decimal? G_Lower { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("G_CREATEBY")]
        public string G_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("G_CREATEDATE")]
        public DateTime? G_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("G_UPDATEBY")]
        public string G_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("G_UPDATEDATE")]
        public DateTime? G_UpdateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("G_REMARK")]
        public string G_Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = Guid.NewGuid().ToString();
            this.G_CreateDate = DateTime.Now;
            this.G_CreateBy = userInfo.realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = keyValue;
            this.G_UpdateDate = DateTime.Now;
            this.G_UpdateBy = userInfo.realName;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

