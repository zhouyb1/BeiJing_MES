using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 14:22
    /// 描 述：工资参数设定
    /// </summary>
    public partial class Mes_SalaryMap : EntityTypeConfiguration<Mes_SalaryEntity>
    {
        public Mes_SalaryMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_SALARY");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

