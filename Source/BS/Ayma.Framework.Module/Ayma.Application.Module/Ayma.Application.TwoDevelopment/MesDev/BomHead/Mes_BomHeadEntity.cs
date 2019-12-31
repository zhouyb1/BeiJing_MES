using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 17:41
    /// 描 述：配方表
    /// </summary>
    public partial class Mes_BomHeadEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 工艺代码
        /// </summary>
        [Column("B_RECORDCODE")]
        public string B_RecordCode { get; set; }
        /// <summary>
        /// 工序号
        /// </summary>
        [Column("B_PRONO")]
        public string B_ProNo { get; set; }
        /// <summary>
        /// 配方编码
        /// </summary>
        [Column("B_FORMULACODE")]
        public string B_FormulaCode { get; set; }
        /// <summary>
        /// 配方名称
        /// </summary>
        [Column("B_FORMULANAME")]
        public string B_FormulaName { get; set; }
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
        /// 工序编码
        /// </summary>
        [Column("B_PROCECODE")]
        public string B_ProceCode { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        [Column("B_PROCENAME")]
        public string B_ProceName { get; set; }
        /// <summary>
        /// 是否生效
        /// </summary>
        [Column("B_AVAIL")]
        public int? B_Avail { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Column("B_STARTTIME")]
        public DateTime? B_StartTime { get; set; }
        /// <summary>
        /// 截止时间
        /// </summary>
        [Column("B_ENDTIME")]
        public DateTime? B_EndTime { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("B_UNIT")]
        public string B_Unit { get; set; }
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

