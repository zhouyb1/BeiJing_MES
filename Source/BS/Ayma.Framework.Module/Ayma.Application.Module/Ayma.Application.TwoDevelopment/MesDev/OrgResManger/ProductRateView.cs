using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    public class ProductRateView
    {
        public string O_GoodsCode { get; set; }
        public string O_GoodsName { get; set; }
        public string O_Unit { get; set; }
        public string O_SecGoodsCode { get; set; }
        public string O_SecGoodsName { get; set; }
        public string O_SecUnit { get; set; }
        public decimal O_Qty { get; set; }
        public decimal O_SecQty { get; set; }
        public decimal ProductRate { get; set; }
        public string targetRate { get; set; }
        public decimal? O_Min { get; set; }
        public decimal? O_Max { get; set; }
        public string O_StockName { get; set; }
        public string O_TeamName { get; set; }
        public string O_ProName { get; set; }
        public decimal? DIFF { get; set; }
    }
}
