using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-18 11:16
    /// 描 述：库存动态表查询
    /// </summary>
    public partial class Mes_InventoryTrendMap : EntityTypeConfiguration<Mes_InventoryTrendEntity>
    {
        public Mes_InventoryTrendMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_INVENTORYTREND");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

