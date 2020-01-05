using Ayma.DataBase.SqlServer;
using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-04-25 15:20
    /// 描 述：社保设置
    /// </summary>
    public partial class Mes_SocialSetEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        [Column("S_USERCODE")]
        public string S_UserCode { get; set; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        [Column("S_USERNAME")]
        public string S_UserName { get; set; }
        /// <summary>
        /// 工资基数
        /// </summary>
        [Column("S_WAGEBASE")]
        [DecimalPrecision(18, 6)]
        public decimal? S_Wagebase { get; set; }
        /// <summary>
        /// 养老单位比例
        /// </summary>
        [Column("S_PENSIONUNITRATIO")]
        [DecimalPrecision(18, 6)]
        public decimal? S_PensionUnitRatio { get; set; }
        /// <summary>
        /// 养老个人比例
        /// </summary>
        [Column("S_PENSIONPERSONRATIO")]
        [DecimalPrecision(18, 6)]
        public decimal? S_PensionPersonRatio { get; set; }
        /// <summary>
        /// 失业单位比例
        /// </summary>
        [Column("S_OUTWORKUNITRATIO")]
        [DecimalPrecision(18, 6)]
        public decimal? S_OutWorkUnitRatio { get; set; }
        /// <summary>
        /// 失业个人比例
        /// </summary>
        [Column("S_OUTWORKPERSONRATIO")]
        [DecimalPrecision(18, 6)]
        public decimal? S_OutWorkPersonRatio { get; set; }
        /// <summary>
        /// 医疗单位比例
        /// </summary>
        [Column("S_MEDICALUNITRATIO")]
        [DecimalPrecision(18, 6)]
        public decimal? S_MedicalUnitRatio { get; set; }
        /// <summary>
        /// 医疗个人比例
        /// </summary>
        [Column("S_MEDICALPRESONRATIO")]
        [DecimalPrecision(18, 6)]
        public decimal? S_MedicalPresonRatio { get; set; }
        /// <summary>
        /// 工伤单位比例
        /// </summary>
        [Column("S_INJURYUNITRATIO")]
        [DecimalPrecision(18, 6)]
        public decimal? S_InJuryUnitRatio { get; set; }
        /// <summary>
        /// 生育单位比例
        /// </summary>
        [Column("S_BEARUNITRATIO")]
        [DecimalPrecision(18, 6)]
        public decimal? S_BearUnitRatio { get; set; }
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

