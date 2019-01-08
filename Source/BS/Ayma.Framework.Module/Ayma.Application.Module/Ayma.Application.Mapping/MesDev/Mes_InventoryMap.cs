using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 17:17
    /// 描 述：库存查询
    /// </summary>
    public partial class Mes_InventoryMap : EntityTypeConfiguration<Mes_InventoryEntity>
    {
        public Mes_InventoryMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_INVENTORY");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

