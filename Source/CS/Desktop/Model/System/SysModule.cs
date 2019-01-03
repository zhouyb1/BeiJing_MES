using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Sys_Module表映射类
    /// </summary>
    public  class SysModule
    {
        public SysModule()
        {

        }


        #region 属性
        /// <summary>
        /// 模块编号
        /// </summary>
        public string M_Code { set; get; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string M_Name { set; get; }
        /// <summary>
        /// 上级菜单编号
        /// </summary>
        public string M_ParentCode { set; get; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string M_Remark { set; get; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool M_Active { set; get; }



        /// <summary>
        /// 创建人
        /// </summary>
        public string M_CreateBy { set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime M_CreateDate { set; get; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string M_UpdateBy { set; get; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime M_UpdateDate { set; get; }
        #endregion


        #region 扩展属性
        /// <summary>
        /// 是否选择
        /// </summary>
        public bool M_Choice { get; set; } 
        #endregion
    }
}
