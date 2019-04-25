using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-04-25 15:20
    /// 描 述：社保设置
    /// </summary>
    public partial class Mes_SocialSetMap : EntityTypeConfiguration<Mes_SocialSetEntity>
    {
        public Mes_SocialSetMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_SOCIALSET");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

