using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-15 16:11
    /// 描 述：线边仓退料到仓库
    /// </summary>
    public partial class Mes_BackStockDetailMap : EntityTypeConfiguration<Mes_BackStockDetailEntity>
    {
        public Mes_BackStockDetailMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_BACKSTOCKDETAIL");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

