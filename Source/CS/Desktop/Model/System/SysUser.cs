using System;


namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Sys_User表映射类
    /// </summary>
    public  class SysUser 
    {
        public SysUser()
        {

        }


        #region 属性
        /// <summary>
        /// 用户账号
        /// </summary>
        public string U_Code { set; get; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string U_Name { set; get; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string U_Pwd { set; get; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public string U_Sex { set; get; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public string D_Code { set; get; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public string R_Code { set; get; }
        /// <summary>
        /// 用户手机
        /// </summary>
        public string U_Phone { set; get; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string U_Email { set; get; }
        /// <summary>
        /// 用户QQ
        /// </summary>
        public string U_QQ { set; get; }
        /// <summary>
        /// 用户微信
        /// </summary>
        public string U_WeChat { set; get; }
        /// <summary>
        /// 用户地址
        /// </summary>
        public string U_Address { set; get; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string U_Remark { set; get; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool U_Active { set; get; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string U_CreateBy { set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? U_CreateDate { set; get; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string U_UpdateBy { set; get; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? U_UpdateDate { set; get; }
        #endregion
    }
}
