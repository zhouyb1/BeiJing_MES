using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-10 20:23
    /// 描 述：生产订单原物料需求表
    /// </summary>
    public partial class Mes_MaterMap : EntityTypeConfiguration<Mes_MaterEntity>
    {
        public Mes_MaterMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_MATER");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

