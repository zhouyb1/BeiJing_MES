using System;


namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Sys_Button表映射类
    /// </summary>
    public class SysButton 
    {
        public SysButton()
        {

        }


        #region 属性
        /// <summary>
        /// 按钮编号
        /// </summary>
        public string B_Code { set; get; }
        /// <summary>
        /// 按钮名称
        /// </summary>
        public string B_Name { set; get; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string B_Remark { set; get; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool B_Active { set; get; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string B_CreateBy { set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime B_CreateDate { set; get; }
        /// <summary>
        /// 更新人
        /// </summary>
        public string B_UpdateBy { set; get; }
        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime B_UpdateDate { set; get; }
        #endregion
    }
}
