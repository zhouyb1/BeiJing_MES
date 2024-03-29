﻿using System.Collections.Generic;

namespace Ayma.Util
{
    /// <summary>
    /// 创建人：Ayma
    /// 日 期：2017.03.07
    /// 描 述：树结构数据
    /// </summary>
    public class TreeModel
    {
        /// <summary>
        /// 节点id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 节点显示数据
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 节点提示
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 节点数值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 显示图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 是否显示勾选框
        /// </summary>
        public bool showcheck { get; set; }
        /// <summary>
        /// 是否被勾选0 for unchecked, 1 for partial checked, 2 for checked
        /// </summary>
        public int checkstate { get; set; }
        /// <summary>
        /// 是否有子节点
        /// </summary>
        public bool hasChildren { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool isexpand { get; set; }
        /// <summary>
        /// 子节点是否已经加载完成了
        /// </summary>
        public bool complete { get; set; }
        /// <summary>
        /// 子节点列表数据
        /// </summary>
        public List<TreeModel> ChildNodes { get; set; }
        /// <summary>
        /// 父级节点ID
        /// </summary>
        public string parentId { get; set; }
    }
    
}
