using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-12 17:32
    /// 描 述：排班记录
    /// </summary>
    public partial class Mes_ArrangeMap : EntityTypeConfiguration<Mes_ArrangeEntity>
    {
        public Mes_ArrangeMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_ARRANGE");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

