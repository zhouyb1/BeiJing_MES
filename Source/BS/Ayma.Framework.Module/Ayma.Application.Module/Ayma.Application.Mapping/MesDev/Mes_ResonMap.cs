using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 19:30
    /// 描 述：不合格原因表
    /// </summary>
    public partial class Mes_ResonMap : EntityTypeConfiguration<Mes_ResonEntity>
    {
        public Mes_ResonMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_RESON");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

