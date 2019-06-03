using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Mes_OutWorkShopHeadEntity
    {
        public string ID { set; get; }
        public string O_OutNo { set; get; }
        public string O_StockCode { set; get; }
        public string O_StockName { set; get; }
        public string O_WorkShop { set; get; }
        public string O_OrderNo { set; get; }
        public string O_OrderDate { set; get; }
        public decimal O_Status { set; get; }
        public string O_CreateBy { set; get; }
        public DateTime O_CreateDate { set; get; }
        public string O_UpdateBy { set; get; }
        public DateTime O_UpdateDate { set; get; }
        public string O_DeleteBy { set; get; }
        public DateTime O_DeleteDate { set; get; }
        public string O_UploadBy { set; get; }
        public DateTime O_UploadDate { set; get; }
        public string O_Remark { set; get; }
    }
}
