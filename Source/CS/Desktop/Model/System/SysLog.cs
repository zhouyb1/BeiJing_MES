using System;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- Sys_Log表映射类
    /// </summary>

    public partial class SysLog
    {
        public SysLog()
        {
        }


        #region 属性
        /// <summary>
        /// 日志编号(自动编号)
        /// </summary>
        public int L_Code { set; get; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime L_Date { set; get; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string L_User { set; get; }

        /// <summary>
        /// 操作模块
        /// </summary>
        public string L_Module { set; get; }

        /// <summary>
        /// 操作功能
        /// </summary>
        public string L_Button { set; get; }

        /// <summary>
        /// 关键字
        /// </summary>
        public string L_Key { set; get; }

        /// <summary>
        /// 操作结果
        /// </summary>
        public string L_Result { set; get; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string L_Describe { set; get; }
        #endregion
    }
}
