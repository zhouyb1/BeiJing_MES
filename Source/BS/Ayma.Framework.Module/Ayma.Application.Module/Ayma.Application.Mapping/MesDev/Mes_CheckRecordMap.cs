using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-04-25 15:41
    /// 描 述：考勤管理
    /// </summary>
    public partial class Mes_CheckRecordMap : EntityTypeConfiguration<Mes_CheckRecordEntity>
    {
        public Mes_CheckRecordMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_CHECKRECORD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

