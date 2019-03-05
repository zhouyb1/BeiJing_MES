using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-05 11:20
    /// 描 述：采购单制作及查询
    /// </summary>
    public partial class Mes_PurchaseHeadMap : EntityTypeConfiguration<Mes_PurchaseHeadEntity>
    {
        public Mes_PurchaseHeadMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_PURCHASEHEAD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

