using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 10:20
    /// 描 述：调拨单制作
    /// </summary>
    public partial class Mes_RequistHeadMap : EntityTypeConfiguration<Mes_RequistHeadEntity>
    {
        public Mes_RequistHeadMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_REQUISTHEAD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

