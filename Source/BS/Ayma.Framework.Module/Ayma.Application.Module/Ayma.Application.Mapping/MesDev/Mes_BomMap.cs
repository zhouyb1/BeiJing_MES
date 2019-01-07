using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 15:39
    /// 描 述：物料清单列表
    /// </summary>
    public partial class Mes_BomMap : EntityTypeConfiguration<Mes_BomEntity>
    {
        public Mes_BomMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_BOM");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

