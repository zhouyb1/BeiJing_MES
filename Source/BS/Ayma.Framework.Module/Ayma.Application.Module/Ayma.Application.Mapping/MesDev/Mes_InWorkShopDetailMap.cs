using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 11:57
    /// 描 述：车间入库到线边仓 表体映射
    /// </summary>
    public partial class Mes_InWorkShopDetailMap : EntityTypeConfiguration<Mes_InWorkShopDetailEntity>
    {
        public Mes_InWorkShopDetailMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_INWORKSHOPDETAIL");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

