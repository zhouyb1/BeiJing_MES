using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Mes_InWorkShopDetailEntity
    {
        public string ID{ set; get; }
      public string I_InNo{ set; get; }
      public string I_GoodsCode{ set; get; }
      public string I_GoodsName{ set; get; }
      public string I_Unit{ set; get; }
      public decimal I_Qty { set; get; }
      public string I_Batch{ set; get; }
      public string I_Remark{ set; get; }
      public decimal I_Price { set; get; }
    }
}
