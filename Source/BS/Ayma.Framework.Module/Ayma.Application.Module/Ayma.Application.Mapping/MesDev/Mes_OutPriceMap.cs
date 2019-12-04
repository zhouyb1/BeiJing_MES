using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-12-04 09:28
    /// 描 述：原物料售卖价格表
    /// </summary>
    public partial class Mes_OutPriceMap : EntityTypeConfiguration<Mes_OutPriceEntity>
    {
        public Mes_OutPriceMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_OUTPRICE");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

