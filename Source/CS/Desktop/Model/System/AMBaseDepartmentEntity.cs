using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- AM_Base_Department表映射类
    /// </summary>
    public partial class AMBaseDepartmentEntity
    {
        #region 属性
        /// <summary>
        /// 部门主键
        /// </summary>
        public string F_DepartmentId{ set; get; }
        /// <summary>
        /// 公司主键
        /// </summary>
        public string F_CompanyId{ set; get; }
        /// <summary>
        /// 父级主键
        /// </summary>
        public string F_ParentId{ set; get; }
        /// <summary>
        /// 部门代码
        /// </summary>
        public string F_EnCode{ set; get; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string F_FullName{ set; get; }
        /// <summary>
        /// 部门简称
        /// </summary>
        public string F_ShortName{ set; get; }
        /// <summary>
        /// 部门类型
        /// </summary>
        public string F_Nature{ set; get; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string F_Manager{ set; get; }
        /// <summary>
        /// 外线电话
        /// </summary>
        public string F_OuterPhone{ set; get; }
        /// <summary>
        /// 内线电话
        /// </summary>
        public string F_InnerPhone{ set; get; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string F_Email{ set; get; }
        /// <summary>
        /// 部门传真
        /// </summary>
        public string F_Fax{ set; get; }
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
        #endregion
    }
}
