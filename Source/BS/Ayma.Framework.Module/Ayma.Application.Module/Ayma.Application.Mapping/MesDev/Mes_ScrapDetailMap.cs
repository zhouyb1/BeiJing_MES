using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 11:23
    /// 描 述：报废单据管理
    /// </summary>
    public partial class Mes_ScrapDetailMap : EntityTypeConfiguration<Mes_ScrapDetailEntity>
    {
        public Mes_ScrapDetailMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_SCRAPDETAIL");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}