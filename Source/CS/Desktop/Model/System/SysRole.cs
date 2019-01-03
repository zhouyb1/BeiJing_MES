using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Sys_Role表映射类
    /// </summary>

    public  class SysRole 
    {
        public SysRole()
        {
        }



        #region 属性
        /// <summary>
        /// 角色编号
        /// </summary>
        public string R_Code { set; get; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string R_Name { set; get; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string R_Remark { set; get; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string R_CreateBy { set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? R_CreateDate { set; get; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string R_UpdateBy { set; get; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? R_UpdateDate { set; get; }
        #endregion
    }
}
