using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-05-18 11:12
    /// 描 述：月结价
    /// </summary>
    public partial class Mes_MonthBalancePriceMap : EntityTypeConfiguration<Mes_MonthBalancePriceEntity>
    {
        public Mes_MonthBalancePriceMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_MONTHBALANCEPRICE");
            //主键
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

