﻿
namespace Ayma.Application.WorkFlow
{
    /// <summary>
    /// 创建人：Ayma
    /// 日 期：2017.04.17
    /// 描 述：工作流线段
    /// </summary>
    public class WfLineInfo
    {
        /// <summary>
        /// 线条Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 线条名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 开始端节点ID
        /// </summary>
        public string from { get; set; }
        /// <summary>
        /// 结束端节点ID
        /// </summary>
        public string to { get; set; }
        /// <summary>
        /// 线段类型 1.是 2.否 3.超时 4.超时或是 5.超时或否 6.是否
        /// </summary>
        public int wftype { get; set; }
    }
}
