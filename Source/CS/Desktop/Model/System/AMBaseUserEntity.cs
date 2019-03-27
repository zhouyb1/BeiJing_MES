using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- AM_Base_User表映射类
    /// </summary>
    public partial class AMBaseUserEntity
    {
        #region 属性
        /// <summary>
        /// 用户主键
        /// </summary>
        public string F_UserId{ set; get; }
        /// <summary>
        /// 工号
        /// </summary>
        public string F_EnCode{ set; get; }
        /// <summary>
        /// 登录账户
        /// </summary>
        public string F_Account{ set; get; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string F_Password{ set; get; }
        /// <summary>
        /// 密码秘钥
        /// </summary>
        public string F_Secretkey{ set; get; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string F_RealName{ set; get; }
        /// <summary>
        /// 呢称
        /// </summary>
        public string F_NickName{ set; get; }
        /// <summary>
        /// 头像
        /// </summary>
        public string F_HeadIcon{ set; get; }
        /// <summary>
        /// 快速查询
        /// </summary>
        public string F_QuickQuery{ set; get; }
        /// <summary>
        /// 简拼
        /// </summary>
        public string F_SimpleSpelling{ set; get; }
        /// <summary>
        /// 性别
        /// </summary>
        public int F_Gender{ set; get; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime F_Birthday{ set; get; }
        /// <summary>
        /// 手机
        /// </summary>
        public string F_Mobile{ set; get; }
        /// <summary>
        /// 电话
        /// </summary>
        public string F_Telephone{ set; get; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string F_Email{ set; get; }
        /// <summary>
        /// QQ号
        /// </summary>
        public string F_OICQ{ set; get; }
        /// <summary>
        /// 微信号
        /// </summary>
        public string F_WeChat{ set; get; }
        /// <summary>
        /// MSN
        /// </summary>
        public string F_MSN{ set; get; }
        /// <summary>
        /// 机构主键
        /// </summary>
        public string F_CompanyId{ set; get; }
        /// <summary>
        /// 部门主键
        /// </summary>
        public string F_DepartmentId{ set; get; }
        /// <summary>
        /// 安全级别
        /// </summary>
        public int F_SecurityLevel{ set; get; }
        /// <summary>
        /// 单点登录标识
        /// </summary>
        public int F_OpenId{ set; get; }
        /// <summary>
        /// 密码提示问题
        /// </summary>
        public string F_Question{ set; get; }
        /// <summary>
        /// 密码提示答案
        /// </summary>
        public string F_AnswerQuestion{ set; get; }
        /// <summary>
        /// 允许多用户同时登录
        /// </summary>
        public int F_CheckOnLine{ set; get; }
        /// <summary>
        /// 允许登录时间开始
        /// </summary>
        public DateTime F_AllowStartTime{ set; get; }
        /// <summary>
        /// 允许登录时间结束
        /// </summary>
        public DateTime F_AllowEndTime{ set; get; }
        /// <summary>
        /// 暂停用户开始日期
        /// </summary>
        public DateTime F_LockStartDate{ set; get; }
        /// <summary>
        /// 暂停用户结束日期
        /// </summary>
        public DateTime F_LockEndDate{ set; get; }
        /// <summary>
        /// 排序码
        /// </summary>
        public int F_SortCode{ set; get; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public int F_DeleteMark{ set; get; }
        /// <summary>
        /// 有效标志
        /// </summary>
        public int F_EnabledMark{ set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string F_Description{ set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime F_CreateDate{ set; get; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        public string F_CreateUserId{ set; get; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public string F_CreateUserName{ set; get; }
        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime F_ModifyDate{ set; get; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        public string F_ModifyUserId{ set; get; }
        /// <summary>
        /// 修改用户
        /// </summary>
        public string F_ModifyUserName{ set; get; }
        /// <summary>
        /// 地址
        /// </summary>
        public string U_Address{ set; get; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public string D_Code{ set; get; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public string R_Code{ set; get; }
        /// <summary>
        /// 员工类型，1，正式工，2，临时工，3，劳务工
        /// </summary>
        public int F_Kind{ set; get; }
        /// <summary>
        /// RFID编码
        /// </summary>
        public string F_RFIDCode{ set; get; }
        /// <summary>
        /// 组别
        /// </summary>
        public string F_Group{ set; get; }
        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime F_Indate{ set; get; }
        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime F_Outdate{ set; get; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string F_Cert{ set; get; }
        /// <summary>
        /// 民族
        /// </summary>
        public string F_Nation{ set; get; }
        /// <summary>
        /// 学历
        /// </summary>
        public string F_Record{ set; get; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string F_Origin{ set; get; }
        /// <summary>
        /// 照片1
        /// </summary>
        public string F_Picture1{ set; get; }
        /// <summary>
        /// 照片2
        /// </summary>
        public string F_Picture2{ set; get; }
        /// <summary>
        /// 照片3
        /// </summary>
        public string F_Picture3{ set; get; }
        /// <summary>
        /// 照片4
        /// </summary>
        public string F_Picture4{ set; get; }
        /// <summary>
        /// 照片5
        /// </summary>
        public string F_Picture5{ set; get; }
        #endregion
    }
}
