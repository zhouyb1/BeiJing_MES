using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Mes_BarcodeEntity
    {
        public string ID { set; get; }
        public string B_Barcode { set; get; }
        public string B_Code { set; get; }
        public string B_Name { set; get; }
        public Double B_Qty { set; get; }
        public string B_WorkShopCode { set; get; }
        public int B_Status { set; get; }
        public DateTime B_Ptime { set; get; }
        public DateTime B_Itime { set; get; }
        public DateTime B_Otime { set; get; }
        public DateTime B_Utime { set; get; }
        public string B_Remark { set; get; }
        

    }
}
