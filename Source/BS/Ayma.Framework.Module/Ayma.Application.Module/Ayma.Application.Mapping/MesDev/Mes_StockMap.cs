using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 12:47
    /// 描 述：仓库列表
    /// </summary>
    public partial class Mes_StockMap : EntityTypeConfiguration<Mes_StockEntity>
    {
        public Mes_StockMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_STOCK");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

