﻿using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-03-05 16:34
    /// 描 述：1
    /// </summary>
    public partial class Mes_MonthBalanceEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("ID")]
        public string ID { get; set; }

        /// <summary>
        /// 月结月份 201911
        /// </summary>
        /// <returns></returns>
        [Column("M_MONTHS")]
        public string M_Months { get; set; }

        /// <summary>
        /// 月结时间
        /// </summary>
        /// <returns></returns>
        [Column("M_MONTHBALANCETIME")]
        public DateTime? M_MonthBalanceTime { get; set; }
        /// <summary>
        /// 月结人
        /// </summary>
        /// <returns></returns>
        [Column("M_MONTHBALANCEBY")]
        public string M_MonthBalanceBy { get; set; }
        /// <summary>
        /// 状态 1月结，2反月结
        /// </summary>
        /// <returns></returns>
        [Column("M_STATUS")]
        public int? M_Status { get; set; }
        /// <summary>
        /// M_Remark
        /// </summary>
        /// <returns></returns>
        [Column("M_REMARK")]
        public string M_Remark { get; set; }
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