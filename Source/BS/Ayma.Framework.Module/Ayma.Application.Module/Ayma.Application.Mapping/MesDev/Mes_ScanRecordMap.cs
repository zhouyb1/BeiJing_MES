using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-01-14 10:46
    /// 描 述：套餐二维码信息
    /// </summary>
    public partial class Mes_ScanRecordMap : EntityTypeConfiguration<Mes_ScanRecordEntity>
    {
        public Mes_ScanRecordMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_SCANRECORD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

