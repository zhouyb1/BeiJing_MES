using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 14:55
    /// 描 述：工序管理
    /// </summary>
    public partial class Mes_ProceMap : EntityTypeConfiguration<Mes_ProceEntity>
    {
        public Mes_ProceMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_PROCE");
            //主键
            this.HasKey(t => t.P_Record);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

