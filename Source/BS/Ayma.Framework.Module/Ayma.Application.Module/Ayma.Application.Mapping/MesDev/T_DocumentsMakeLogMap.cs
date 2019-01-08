using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 17:51
    /// 描 述：操作记录查询
    /// </summary>
    public partial class T_DocumentsMakeLogMap : EntityTypeConfiguration<T_DocumentsMakeLogEntity>
    {
        public T_DocumentsMakeLogMap()
        {
            #region 表、主键
            //表
            this.ToTable("T_DOCUMENTSMAKELOG");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

