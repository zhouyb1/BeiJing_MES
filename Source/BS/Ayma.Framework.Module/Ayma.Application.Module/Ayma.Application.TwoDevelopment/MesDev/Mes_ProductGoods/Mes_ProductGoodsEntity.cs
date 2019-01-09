using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 15:20
    /// 描 述：同步ERP成品商品资料
    /// </summary>
    public partial class Mes_ProductGoodsEntity 
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
        #endregion
    }
}

