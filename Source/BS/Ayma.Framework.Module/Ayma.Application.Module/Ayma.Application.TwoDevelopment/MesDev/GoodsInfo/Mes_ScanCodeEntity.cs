using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayma.Application.TwoDevelopment.MesDev.GoodsInfo
{
    /// <summary>
    /// 商品条码
    /// </summary>
    public partial class Mes_ScanCodeEntity
    {
        /// <summary>
        /// 条码
        /// </summary>
        public string S_Code { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string S_Name { get; set; }
        /// <summary>
        /// 原料
        /// </summary>
        public string S_MaterName { get; set; }
        /// <summary>
        /// 生成商
        /// </summary>
        public string S_Producer { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public string S_Quality { get; set; }
        /// <summary>
        /// 班组
        /// </summary>
        public string S_Team { get; set; }
        /// <summary>
        /// 查询次数
        /// </summary>
        public int S_ScanRecord { get; set; }
        /// <summary>
        /// 查询时间
        /// </summary>
        public DateTime? S_ScanTime { get; set; }

        /// <summary>
        /// 执行标准
        /// </summary>
        public string S_Standard { get; set; }

        /// <summary>
        /// 贮存条件
        /// </summary>
        public string S_Storage { get; set; }
    }
}
