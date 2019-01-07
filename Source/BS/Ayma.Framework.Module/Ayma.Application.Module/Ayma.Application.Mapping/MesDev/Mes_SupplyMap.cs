using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 09:31
    /// 描 述：供应商列表
    /// </summary>
    public partial class Mes_SupplyMap : EntityTypeConfiguration<Mes_SupplyEntity>
    {
        public Mes_SupplyMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_SUPPLY");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

