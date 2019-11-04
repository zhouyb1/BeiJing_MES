using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Mes_ScrapHeadEntity
    {

            public string ID { set; get; }
            public string S_ScrapNo { set; get; }
            public string S_StockCode { set; get; }
            public string S_StockName { set; get; }
            public DateTime S_OrderDate { set; get; }
            public int S_Status { set; get; }
            public string S_CreateBy { set; get; }
            public DateTime S_CreateDate { set; get; }
            public string S_UpdateBy { set; get; }
            public DateTime S_UpdateDate { set; get; }
            public string S_DeleteBy { set; get; }
            public DateTime S_DeleteDate { set; get; }
            public string S_UploadBy { set; get; }
            public DateTime S_UploadDate { set; get; }
            public string S_Remark { set; get; }
    }
}
