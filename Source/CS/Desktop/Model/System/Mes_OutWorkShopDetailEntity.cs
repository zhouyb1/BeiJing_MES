using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Mes_OutWorkShopDetailEntity
    {

        public string ID { set; get; }
        public string O_OutNo { set; get; }
        public string O_GoodsCode { set; get; }
        public string O_GoodsName { set; get; }
        public string O_Unit { set; get; }
        public Double O_Qty { set; get; }
        public string O_Batch { set; get; }
        public string O_Remark { set; get; }
        public Double O_Price { set; get; }
    }
}
