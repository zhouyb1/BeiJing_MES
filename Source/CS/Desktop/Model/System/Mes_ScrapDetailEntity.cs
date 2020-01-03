using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Mes_ScrapDetailEntity
    {
        public string ID { set; get; }
        public string S_ScrapNo { set; get; }
        public string S_GoodsCode { set; get; }
        public string S_GoodsName { set; get; }
        public string S_Unit { set; get; }
        public Double S_Qty { set; get; }
        public string S_Batch { set; get; }
        public string S_Remark { set; get; }
        public Double S_Price { set; get; }
    }
}
