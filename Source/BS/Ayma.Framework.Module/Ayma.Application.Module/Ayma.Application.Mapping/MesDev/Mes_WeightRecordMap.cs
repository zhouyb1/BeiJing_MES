using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 17:18
    /// 描 述：称重记录列表
    /// </summary>
    public partial class Mes_WeightRecordMap : EntityTypeConfiguration<Mes_WeightRecordEntity>
    {
        public Mes_WeightRecordMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_WEIGHTRECORD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

