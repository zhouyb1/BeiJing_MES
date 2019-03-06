using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 12:03
    /// 描 述：车间管理
    /// </summary>
    public partial class Mes_WorkShopMap : EntityTypeConfiguration<Mes_WorkShopEntity>
    {
        public Mes_WorkShopMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_WORKSHOP");
            //主键
            this.HasKey(t => t.Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

