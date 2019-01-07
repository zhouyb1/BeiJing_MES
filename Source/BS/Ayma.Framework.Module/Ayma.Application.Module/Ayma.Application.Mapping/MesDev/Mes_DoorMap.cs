using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 11:04
    /// 描 述：门列表
    /// </summary>
    public partial class Mes_DoorMap : EntityTypeConfiguration<Mes_DoorEntity>
    {
        public Mes_DoorMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_DOOR");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

