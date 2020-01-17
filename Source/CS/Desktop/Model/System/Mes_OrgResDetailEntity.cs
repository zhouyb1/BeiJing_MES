using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Mes_OrgResDetailEntity
    {
        public string ID { set; get; }
      public string O_OrgResNo { set; get; }
      public string O_GoodsCode { set; get; }
      public string O_GoodsName { set; get; }
      public string O_Unit { set; get; }
      public decimal O_Qty { set; get; }
      public string O_Batch { set; get; }
      public decimal O_Price { set; get; }
      public string O_SecGoodsCode { set; get; }
      public string O_SecGoodsName { set; get; }
      public string O_SecUnit { set; get; }
      public decimal O_SecQty { set; get; }
      public string O_SecBatch { set; get; }
      public decimal O_SecPrice { set; get; }
    }
}
