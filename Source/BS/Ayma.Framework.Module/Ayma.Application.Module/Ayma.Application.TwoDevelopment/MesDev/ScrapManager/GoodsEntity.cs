using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayma.Application.TwoDevelopment.MesDev.ScrapManager
{
    /// <summary>
    /// 报废物料列表
    /// </summary>
    public class GoodsEntity
    {
        public string G_ID { get; set; }
        public string G_StockCode { get; set; }
        public string G_StockName { get; set; }
        public string G_Batch { get; set; }
        public string G_GoodsCode { get; set; }
        public string G_GoodsName { get; set; }
        public string G_Unit { get; set; }
        public decimal? G_Price { get; set; }
        public int G_Qty { get; set; }
    }
}
