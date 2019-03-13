using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 14:14
    /// 描 述：工艺代码表
    /// </summary>
    public partial class Mes_RecordMap : EntityTypeConfiguration<Mes_RecordEntity>
    {
        public Mes_RecordMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_RECORD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

