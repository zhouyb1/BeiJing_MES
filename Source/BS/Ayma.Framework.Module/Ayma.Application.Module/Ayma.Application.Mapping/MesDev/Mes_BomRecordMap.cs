using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 17:41
    /// 描 述：配方表
    /// </summary>
    public partial class Mes_BomRecordMap : EntityTypeConfiguration<Mes_BomRecordEntity>
    {
        public Mes_BomRecordMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_BOMRECORD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

