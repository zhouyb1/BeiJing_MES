using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-07 17:56
    /// 描 述：其它入库单据从表
    /// </summary>
    public partial class Mes_OtherInDetailMap : EntityTypeConfiguration<Mes_OtherInDetailEntity>
    {
        public Mes_OtherInDetailMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_OTHERINDETAIL");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

