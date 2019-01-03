/*******************************************************************************
 * Copyright © 2018 Sfliao.Framework 版权所有
 * Author: Sfliao
 * Description: 桌面应用快速开发
 
*********************************************************************************/

namespace Tools
{
    /// <summary>
    /// 定义AJAX返回类型
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 操作结果类型
        /// </summary>
        public object state { get; set; }
        /// <summary>
        /// 获取 消息内容
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 获取 返回数据
        /// </summary>
        public object data { get; set; }
    }
    /// <summary>
    /// 表示 ajax 操作结果类型的枚举
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// 消息结果类型
        /// </summary>
        info,
        /// <summary>
        /// 成功结果类型
        /// </summary>
        success,
        /// <summary>
        /// 警告结果类型
        /// </summary>
        warning,
        /// <summary>
        /// 异常结果类型
        /// </summary>
        error
    }
}
