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
      public decimal P_InPrice { set; get; }
      public decimal P_Itax { set; get; }
      public string P_CreateBy { set; get; }
      public DateTime P_CreateDate { set; get; }
      public string P_StartBatch { set; get; }
      public string P_EndBatch { set; get; }
    }
}
