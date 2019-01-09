using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 15:20
    /// 描 述：同步ERP成品商品资料
    /// </summary>
    public partial class Mes_ProductGoodsMap : EntityTypeConfiguration<Mes_ProductGoodsEntity>
    {
        public Mes_ProductGoodsMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_PRODUCTGOODS");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

