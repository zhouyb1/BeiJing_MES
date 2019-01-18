using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-18 09:40
    /// 描 述：篮子重量列表
    /// </summary>
    public partial class Mes_BasketMap : EntityTypeConfiguration<Mes_BasketEntity>
    {
        public Mes_BasketMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_BASKET");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

