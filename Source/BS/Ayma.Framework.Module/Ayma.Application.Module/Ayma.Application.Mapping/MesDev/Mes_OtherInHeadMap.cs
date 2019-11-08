using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-07 13:51
    /// 描 述：其它入库单
    /// </summary>
    public partial class Mes_OtherInHeadMap : EntityTypeConfiguration<Mes_OtherInHeadEntity>
    {
        public Mes_OtherInHeadMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_OTHERINHEAD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

