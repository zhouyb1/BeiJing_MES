using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-11 19:22
    /// 描 述：物料出成率
    /// </summary>
    public partial class YieldRateModel 
    {
        #region 实体成员
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("O_GOODSNAME")]
        public string O_GoodsName { get; set; }
        /// <summary>
        /// 配方名称
        /// </summary>
        [Column("FORMULANAME")]
        public string FormulaName { get; set; }
        /// <summary>
        /// 一月
        /// </summary>
        [Column("JANUARY")]
        public string January { get; set; }
        /// <summary>
        /// 二月
        /// </summary>
        [Column("FEBRUARY")]
        public string February { get; set; }
        /// <summary>
        /// 三月
        /// </summary>
        [Column("MARCH")]
        public string March { get; set; }
        /// <summary>
        /// 四月
        /// </summary>
        [Column("APRIL")]
        public string April { get; set; }
        /// <summary>
        /// 五月
        /// </summary>
        [Column("MAY")]
        public string May { get; set; }
        /// <summary>
        /// 六月
        /// </summary>
        [Column("JUNE")]
        public string June { get; set; }
        /// <summary>
        /// 七月
        /// </summary>
        [Column("JULY")]
        public string July { get; set; }
        /// <summary>
        /// 八月
        /// </summary>
        [Column("AUGUST")]
        public string August { get; set; }
        /// <summary>
        /// 九月
        /// </summary>
        [Column("SEPTEMBER")]
        public string September { get; set; }
        /// <summary>
        /// 十月
        /// </summary>
        [Column("OCTOBER")]
        public string October { get; set; } 
        /// <summary>
        /// 十一月
        /// </summary>
        [Column("NOVEMBER")]
        public string November { get; set; }
        /// <summary>
        /// 十二月
        /// </summary>
        [Column("DECEMBER")]
        public string December { get; set; }

        #endregion

        
    }
}

