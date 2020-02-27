using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayma.Application.TwoDevelopment.MesDev.MaterialsSum.ViewModel
{
   public class InventoryViewModel
    {
       
        public string F_OrderNo { get; set; }
        public string F_GoodsCode { get; set; }
        public string F_GoodsName { get; set; }
        public string F_Remark { get; set; }
        public string F_Unit { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public string F_Status { get; set; }
        public decimal? SRJE { get; set; }//库存
        public decimal? FCJE { get; set; }//库存
        public decimal? JCJE { get; set; }//库存
        public DateTime? F_CreateDate { get; set; }
        public decimal? IntervoryQty { get; set; }//库存
        public decimal? ThisIntervoryAumount { get; set; }//库存金额
        public decimal? Aoumount { get; set; }
        public decimal? M_Price { get; set; }
        public decimal? F_InQty { get; set; }
        public decimal? F_InPrice { get; set; }
        public decimal? F_OutQty { get; set; }
        public decimal? F_OutPrice { get; set; }
        public decimal? G_Price { get; set; }//结存的价格
    }
}
