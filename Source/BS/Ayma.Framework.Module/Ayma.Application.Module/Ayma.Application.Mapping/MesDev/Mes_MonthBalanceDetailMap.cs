using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-05-18 11:13
    /// 描 述：月结数量
    /// </summary>
    public partial class Mes_MonthBalanceDetailMap : EntityTypeConfiguration<Mes_MonthBalanceDetailEntity>
    {
        public Mes_MonthBalanceDetailMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_MONTHBALANCEDETAIL");
            //主键
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

