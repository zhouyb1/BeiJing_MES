using Ayma.Application.CRM;
using System.Data.Entity.ModelConfiguration;

namespace  Ayma.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 14:28
    /// 描 述：费用支出
    /// </summary>
    public class CrmExpensesMap : EntityTypeConfiguration<CrmExpensesEntity>
    {
        public CrmExpensesMap()
        {
            #region 表、主键
            //表
            this.ToTable("AM_CRM_EXPENSES");
            //主键
            this.HasKey(t => t.F_ExpensesId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

