using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    public class ColumnModel
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 表头
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public int? width { get; set; }

        /// <summary>
        /// 对齐方式
        /// </summary>
        public string align { get; set; }

        /// <summary>
        /// 是否排序
        /// </summary>
        public bool sort { get; set; }

        /// <summary>
        /// 是否显示合计
        /// </summary>
        public bool statistics
        {
            get;
            set;
        }

        /// <summary>
        /// 子列
        /// </summary>
        public List<ColumnModel> children { get; set; }
    }

}
