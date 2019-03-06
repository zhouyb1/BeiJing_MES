﻿using Ayma.Application.TwoDevelopment.MesDev;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 17:27
    /// 描 述：历史库存查询
    /// </summary>
    public partial class Mes_InventoryLSMap : EntityTypeConfiguration<Mes_InventoryLSEntity>
    {
        public Mes_InventoryLSMap()
        {
            #region 表、主键
            //表
            this.ToTable("MES_INVENTORYLS");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
