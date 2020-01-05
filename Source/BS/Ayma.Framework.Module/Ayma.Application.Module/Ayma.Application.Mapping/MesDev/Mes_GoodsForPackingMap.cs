using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-01-05 11:30
    /// 描 述：半成品待移交表
    /// </summary>
    public partial class Mes_GoodsForPackingMap : EntityTypeConfiguration<Mes_GoodsForPackingEntity>
    {
        public Mes_GoodsForPackingMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_GOODSFORPACKING");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

