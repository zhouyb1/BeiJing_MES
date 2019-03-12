﻿using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-11 19:22
    /// 描 述：领料单
    /// </summary>
    public partial class Mes_CollarHeadMap : EntityTypeConfiguration<Mes_CollarHeadEntity>
    {
        public Mes_CollarHeadMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_COLLARHEAD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
