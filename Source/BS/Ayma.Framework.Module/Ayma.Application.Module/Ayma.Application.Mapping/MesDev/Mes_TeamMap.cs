using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-06-27 15:26
    /// 描 述：班组表
    /// </summary>
    public partial class Mes_TeamMap : EntityTypeConfiguration<Mes_TeamEntity>
    {
        public Mes_TeamMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_TEAM");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

