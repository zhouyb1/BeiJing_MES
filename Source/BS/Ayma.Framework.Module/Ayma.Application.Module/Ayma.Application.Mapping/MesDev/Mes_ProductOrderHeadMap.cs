﻿using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-07 17:54
    /// 描 述：查询生成清单
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
