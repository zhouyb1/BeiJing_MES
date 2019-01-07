using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 13:55
    /// 描 述：物料列表
    /// </summary>
    public partial class Mes_GoodsMap : EntityTypeConfiguration<Mes_GoodsEntity>
    {
        public Mes_GoodsMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_GOODS");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

