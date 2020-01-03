using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Mes_InWorkShopHeadEntity
    {
        public string ID{ set; get; }
        public string I_InNo{ set; get; }
          public string I_StockCode{ set; get; }
          public string I_StockName{ set; get; }
          public string I_WorkShop{ set; get; }
          public string I_OrderNo{ set; get; }
          public string I_OrderDate{ set; get; }
          public Double I_Status { set; get; }
          public string I_CreateBy{ set; get; }
          public DateTime I_CreateDate { set; get; }
          public string I_UpdateBy{ set; get; }
          public DateTime I_UpdateDate{ set; get; }
          public string I_DeleteBy{ set; get; }
          public DateTime I_DeleteDate{ set; get; }
          public string I_UploadBy{ set; get; }
          public DateTime I_UploadDate{ set; get; }
          public string I_Remark { set; get; }
    }
}
