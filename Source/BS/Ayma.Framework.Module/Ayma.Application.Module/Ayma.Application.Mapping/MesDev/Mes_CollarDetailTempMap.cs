using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 10:06
    /// 描 述：领料单查询
    /// </summary>
    public partial class Mes_CollarDetailTempMap : EntityTypeConfiguration<Mes_CollarDetailTempEntity>
    {
        public Mes_CollarDetailTempMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_COLLARDETAILTEMP");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

