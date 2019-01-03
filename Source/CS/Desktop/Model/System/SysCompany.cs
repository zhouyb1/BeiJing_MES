using System;


namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Sys_Company表映射类
    /// </summary>
    public  class SysCompany 
    {
        public SysCompany()
        {

        }



        #region 属性
        /// <summary>
        /// 公司编号
        /// </summary>
        public string C_Code { set; get; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string C_Name { set; get; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string C_Remark { set; get; }

        /// <summary>
        /// 备注说明
        /// </summary>
        public string C_Phone { set; get; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string C_Email { set; get; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string C_Fax { set; get; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string C_Address { set; get; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string C_CreateBy { set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? C_CreateDate { set; get; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string C_UpdateBy { set; get; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime? C_UpdateDate { set; get; }
        #endregion
    }
}
