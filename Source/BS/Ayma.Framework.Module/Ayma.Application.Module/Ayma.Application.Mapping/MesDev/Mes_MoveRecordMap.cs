using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 15:11
    /// 描 述：人员走动记录列表
    /// </summary>
    public partial class Mes_MoveRecordMap : EntityTypeConfiguration<Mes_MoveRecordEntity>
    {
        public Mes_MoveRecordMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_MOVERECORD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

