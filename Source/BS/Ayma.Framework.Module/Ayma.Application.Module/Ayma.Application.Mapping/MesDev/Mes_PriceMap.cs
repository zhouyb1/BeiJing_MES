﻿using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-09-12 14:05
    /// 描 述：价格表
    /// </summary>
    public partial class Mes_PriceMap : EntityTypeConfiguration<Mes_PriceEntity>
    {
        public Mes_PriceMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_PRICE");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
