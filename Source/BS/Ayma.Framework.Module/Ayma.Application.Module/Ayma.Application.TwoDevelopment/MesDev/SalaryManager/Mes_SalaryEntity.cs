using Ayma.DataBase.SqlServer;
using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 14:22
    /// 描 述：工资参数设定
    /// </summary>
    public partial class Mes_SalaryEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        [Column("S_USERKIND")]
        public string S_UserKind { get; set; }
        /// <summary>
        /// 工作类型
        /// </summary>
        [Column("S_WORKKIND")]
        public string S_WorkKind { get; set; }
        /// <summary>
        /// 单位时间
        /// </summary>
        [Column("S_TIMEUNIT")]
        public string S_TimeUnit { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [Column("S_PAY")]
        [DecimalPrecision(18, 6)]
        public decimal? S_Pay { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("S_REMARK")]
        public string S_Remark { get; set; }
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

