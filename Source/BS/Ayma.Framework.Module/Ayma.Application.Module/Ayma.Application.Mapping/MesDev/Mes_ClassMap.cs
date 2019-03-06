using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 13:52
    /// 描 述：排班管理
    /// </summary>
    public partial class Mes_ClassMap : EntityTypeConfiguration<Mes_ClassEntity>
    {
        public Mes_ClassMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_CLASS");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

