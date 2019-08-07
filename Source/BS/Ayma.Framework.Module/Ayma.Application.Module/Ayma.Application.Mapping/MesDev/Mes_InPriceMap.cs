using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-08-06 10:54
    /// 描 述：原物料入库价格表
    /// </summary>
    public partial class Mes_InPriceMap : EntityTypeConfiguration<Mes_InPriceEntity>
    {
        public Mes_InPriceMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_INPRICE");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

