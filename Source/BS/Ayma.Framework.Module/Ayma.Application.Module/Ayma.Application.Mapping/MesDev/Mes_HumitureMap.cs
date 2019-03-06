using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 14:49
    /// 描 述：温湿度采集参数设置
    /// </summary>
    public partial class Mes_HumitureMap : EntityTypeConfiguration<Mes_HumitureEntity>
    {
        public Mes_HumitureMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_HUMITURE");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

