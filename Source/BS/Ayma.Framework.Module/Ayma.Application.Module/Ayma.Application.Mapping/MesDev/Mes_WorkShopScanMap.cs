using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-12-21 17:37
    /// 描 述：车间物料扫描表
    /// </summary>
    public partial class Mes_WorkShopScanMap : EntityTypeConfiguration<Mes_WorkShopScanEntity>
    {
        public Mes_WorkShopScanMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_WORKSHOPSCAN");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

