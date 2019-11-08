using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-08 14:02
    /// 描 述：财务月结
    /// </summary>
    public partial class Mes_MonthBalanceMap : EntityTypeConfiguration<Mes_MonthBalanceEntity>
    {
        public Mes_MonthBalanceMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_MONTHBALANCE");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

