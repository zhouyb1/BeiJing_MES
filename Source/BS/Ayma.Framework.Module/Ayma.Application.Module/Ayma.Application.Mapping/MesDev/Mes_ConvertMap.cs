using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;
using Ayma.Application.TwoDevelopment.Tools;

namespace Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-19 15:30
    /// 描 述：ds
    /// </summary>
    public partial class Mes_ConvertMap : EntityTypeConfiguration<Mes_ConvertEntity>
    {
        public Mes_ConvertMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_CONVERT");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}