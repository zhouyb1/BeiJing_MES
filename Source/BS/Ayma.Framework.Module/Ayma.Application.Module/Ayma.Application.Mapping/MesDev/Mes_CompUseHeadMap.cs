using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-07-04 16:14
    /// 描 述：强制使用记录单据
    /// </summary>
    public partial class Mes_CompUseHeadMap : EntityTypeConfiguration<Mes_CompUseHeadEntity>
    {
        public Mes_CompUseHeadMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_COMPUSEHEAD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

