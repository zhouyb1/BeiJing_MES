using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Mes_InPrice
    {
        public string ID { set; get; }
      public string P_SupplyCode { set; get; }
      public string P_SupplyName { set; get; }
      public string P_GoodsCode { set; get; }
      public string P_GoodsName { set; get; }
      public Double P_InPrice { set; get; }
      public Double P_Itax { set; get; }
      
    }
}
