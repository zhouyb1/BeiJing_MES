﻿using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-08 13:40
    /// 描 述：其它出库单
    /// </summary>
    public partial class Mes_OtherOutHeadMap : EntityTypeConfiguration<Mes_OtherOutHeadEntity>
    {
        public Mes_OtherOutHeadMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_OTHEROUTHEAD");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

