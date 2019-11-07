using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-06 16:30
    /// 描 述：客户表
    /// </summary>
    public partial class Mes_CustomerMap : EntityTypeConfiguration<Mes_CustomerEntity>
    {
        public Mes_CustomerMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_CUSTOMER");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

