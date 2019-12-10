using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-12-10 15:07
    /// 描 述：IP与RFID对应表
    /// </summary>
    public partial class Mes_IPToRFIDMap : EntityTypeConfiguration<Mes_IPToRFIDEntity>
    {
        public Mes_IPToRFIDMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_IPTORFID");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

