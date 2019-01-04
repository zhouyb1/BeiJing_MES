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
        public string F_Account { set; get; }
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string F_RealName { set; get; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string F_Password { set; get; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public int? F_Gender { set; get; }
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
        public string F_Mobile { set; get; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string F_Email { set; get; }
        /// <summary>
        /// 用户QQ
        /// </summary>
        public string F_OICQ { set; get; }
        /// <summary>
        /// 用户微信
        /// </summary>
        public string F_WeChat { set; get; }
        /// <summary>
        /// 用户地址
        /// </summary>
        public string U_Address { set; get; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string F_Description { set; get; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool F_EnabledMark { set; get; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string F_CreateUserName { set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? F_CreateDate { set; get; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string F_ModifyUserName { set; get; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? F_ModifyDate { set; get; }
        /// <summary>
        /// 用户主键
        /// </summary>		
        public string F_UserId { get; set; }
        /// <summary>
        /// 工号
        /// </summary>	
        public string F_EnCode { get; set; }
        /// <summary>
        /// 密码秘钥
        /// </summary>	
        public string F_Secretkey { get; set; }
        /// <summary>
        /// 呢称
        /// </summary>
        public string F_NickName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>	
        public string F_HeadIcon { get; set; }
        /// <summary>
        /// 快速查询
        /// </summary>	
        public string F_QuickQuery { get; set; }
        /// <summary>
        /// 简拼
        /// </summary>	
        public string F_SimpleSpelling { get; set; }

        /// <summary>
        /// 生日
        /// </summary>	
        public DateTime? F_Birthday { get; set; }

        /// <summary>
        /// 电话
        /// </summary>		
        public string F_Telephone { get; set; }

        /// <summary>
        /// MSN
        /// </summary>		
        public string F_MSN { get; set; }
        /// <summary>
        /// 公司主键
        /// </summary>		
        public string F_CompanyId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>		
        public string F_DepartmentId { get; set; }
        /// <summary>
        /// 安全级别
        /// </summary>	
        public int? F_SecurityLevel { get; set; }
        /// <summary>
        /// 单点登录标识
        /// </summary>		
        public int? F_OpenId { get; set; }
        /// <summary>
        /// 密码提示问题
        /// </summary>		
        public string F_Question { get; set; }
        /// <summary>
        /// 密码提示答案
        /// </summary>	
        public string F_AnswerQuestion { get; set; }
        /// <summary>
        /// 允许多用户同时登录
        /// </summary>		
        public int? F_CheckOnLine { get; set; }
        /// <summary>
        /// 允许登录时间开始
        /// </summary>		
        public DateTime? F_AllowStartTime { get; set; }
        /// <summary>
        /// 允许登录时间结束
        /// </summary>		
        public DateTime? F_AllowEndTime { get; set; }
        /// <summary>
        /// 暂停用户开始日期
        /// </summary>		
        public DateTime? F_LockStartDate { get; set; }
        /// <summary>
        /// 暂停用户结束日期
        /// </summary>		
        public DateTime? F_LockEndDate { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>	
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>	
        public int? F_DeleteMark { get; set; }


        /// <summary>
        /// 创建用户主键
        /// </summary>		
        public string F_CreateUserId { get; set; }

        /// <summary>
        /// 修改用户主键
        /// </summary>		
        public string F_ModifyUserId { get; set; }

        #endregion

        #region 扩展属性
        /// <summary>
        /// 登录信息
        /// </summary>
        public string LoginMsg { get; set; }
        /// <summary>
        /// 登录状态
        /// </summary>
        public bool LoginOk { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string StrGender { get; set; }
        #endregion
    }
}
