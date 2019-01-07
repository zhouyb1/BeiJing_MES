using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 15:39
    /// 描 述：物料清单列表
    /// </summary>
    public partial class Mes_BomEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// BOM日期
        /// </summary>
        [Column("B_DATE")]
        public DateTime? B_Date { get; set; }
        /// <summary>
        /// BOM单号
        /// </summary>
        [Column("B_ORDERNO")]
        public string B_OrderNo { get; set; }
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
        /// 等级
        /// </summary>
        [Column("B_GRADE")]
        public string B_Grade { get; set; }
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
        /// 下级物料单位
        /// </summary>
        [Column("B_SECUNIT")]
        public string B_SecUnit { get; set; }
        /// <summary>
        /// 下级物料数量
        /// </summary>
        [Column("B_SECQTY")]
        public decimal? B_SecQty { get; set; }
        /// <summary>
        /// 转换率
        /// </summary>
        [Column("B_CONVERSION")]
        public decimal? B_Conversion { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("B_CREATEBY")]
        public string B_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("B_CREATEDATE")]
        public DateTime? B_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("B_UPDATEBY")]
        public string B_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("B_UPDATEDATE")]
        public DateTime? B_UpdateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("B_REMARK")]
        public string B_Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = Guid.NewGuid().ToString();
            this.B_CreateDate = DateTime.Now;
            this.B_CreateBy = userInfo.realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = keyValue;
            this.B_UpdateDate = DateTime.Now;
            this.B_UpdateBy = userInfo.realName;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

