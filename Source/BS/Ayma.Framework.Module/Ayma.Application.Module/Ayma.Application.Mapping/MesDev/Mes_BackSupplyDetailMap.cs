using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 16:30
    /// 描 述：退供应商单制作
    /// </summary>
    public partial class Mes_BackSupplyDetailMap : EntityTypeConfiguration<Mes_BackSupplyDetailEntity>
    {
        public Mes_BackSupplyDetailMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_BACKSUPPLYDETAIL");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

