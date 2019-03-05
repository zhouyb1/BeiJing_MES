using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-05 20:21
    /// 描 述：商品二级分类
    /// </summary>
    public partial class Mes_GoodKindMap : EntityTypeConfiguration<Mes_GoodKindEntity>
    {
        public Mes_GoodKindMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_GOODKIND");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

