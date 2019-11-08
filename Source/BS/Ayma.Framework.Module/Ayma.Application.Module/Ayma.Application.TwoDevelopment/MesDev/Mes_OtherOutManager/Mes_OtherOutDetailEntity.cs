using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-08 20:39
    /// 描 述：s
    /// </summary>
    public partial class Mes_OtherOutDetailEntity
    {
        #region 实体成员
        /// <summary>
        ///  主键
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 单号
        /// </summary>
        /// <returns></returns>
        [Column("O_OTHEROUTNO")]
        public string O_OtherOutNo { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        /// <returns></returns>
        [Column("O_GOODSCODE")]
        public string O_GoodsCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        /// <returns></returns>
        [Column("O_GOODSNAME")]
        public string O_GoodsName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        /// <returns></returns>
        [Column("O_UNIT")]
        public string O_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        [Column("O_QTY")]
        public double? O_Qty { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        /// <returns></returns>
        [Column("O_BATCH")]
        public string O_Batch { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        /// <returns></returns>
        [Column("O_PRICE")]
        public double? O_Price { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("O_REMARK")]
        public string O_Remark { get; set; }
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