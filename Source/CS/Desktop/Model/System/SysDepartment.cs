using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Sys_Department表映射类
    /// </summary>
    public  class SysDepartment
    {
        public SysDepartment()
        {
        }



        #region 属性
        /// <summary>
        /// 部门编号
        /// </summary>
        public string D_Code { set; get; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string D_Name { set; get; }
        /// <summary>
        /// 所属公司
        /// </summary>
        public string C_Code { set; get; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string D_Remark { set; get; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string D_CreateBy { set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? D_CreateDate { set; get; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string D_UpdateBy { set; get; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? D_UpdateDate { set; get; }
        #endregion
    }
}
