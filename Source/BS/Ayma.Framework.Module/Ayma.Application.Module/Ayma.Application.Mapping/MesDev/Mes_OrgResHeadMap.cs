using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-18 15:14
    /// 描 述：组装与拆分单据制作
    /// </summary>
    public partial class Mes_OrgResHeadMap : EntityTypeConfiguration<Mes_OrgResHeadEntity>
    {
        public Mes_OrgResHeadMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_ORGRESHEAD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

