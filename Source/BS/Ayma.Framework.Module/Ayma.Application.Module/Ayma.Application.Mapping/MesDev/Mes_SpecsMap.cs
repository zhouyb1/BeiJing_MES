using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-12-28 19:24
    /// 描 述：物料包装数
    /// </summary>
    public partial class Mes_SpecsMap : EntityTypeConfiguration<Mes_SpecsEntity>
    {
        public Mes_SpecsMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_SPECS");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

