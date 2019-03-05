using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-05 20:34
    /// 描 述：用户表
    /// </summary>
    public partial class Sys_UsersMap : EntityTypeConfiguration<Sys_UsersEntity>
    {
        public Sys_UsersMap()
        {
            #region 表、主键
            //表
            this.ToTable("SYS_USERS");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

