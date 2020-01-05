using Ayma.DataBase.SqlServer;
using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ayma.Application.TwoDevelopment.MesDev

{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-12-28 19:24
    /// 描 述：物料包装数
    /// </summary>
    public partial class Mes_SpecsEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 物料
        /// </summary>
        /// <returns></returns>
        [Column("S_GOODSCODE")]
        public string S_GoodsCode { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        [Column("S_GOODSNAME")]
        public string S_GoodsName { get; set; }
        /// <summary>
        /// 包装规格
        /// </summary>
        /// <returns></returns>
        [Column("S_UNITQTY")]
        [DecimalPrecision(18, 6)]
        public decimal? S_UnitQty { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("S_REMARK")]
        public string S_Remark { get; set; }
        #endregion
        [NotMapped]
        public string F_ItemName { get; set; }
        [NotMapped]
        public string F_ItemValue { get; set; }
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

