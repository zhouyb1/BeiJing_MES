using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-07 10:38
    /// 描 述：原物料(半成品)销售
    /// </summary>
    public partial class Mes_SaleHeadMap : EntityTypeConfiguration<Mes_SaleHeadEntity>
    {
        public Mes_SaleHeadMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_SALEHEAD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

