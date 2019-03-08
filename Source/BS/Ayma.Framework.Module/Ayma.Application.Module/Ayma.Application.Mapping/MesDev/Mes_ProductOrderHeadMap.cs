using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-07 11:05
    /// 描 述：生产订单管理
    /// </summary>
    public partial class Mes_ProductOrderHeadMap : EntityTypeConfiguration<Mes_ProductOrderHeadEntity>
    {
        public Mes_ProductOrderHeadMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_PRODUCTORDERHEAD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

