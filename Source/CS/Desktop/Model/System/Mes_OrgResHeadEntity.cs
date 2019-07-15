using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Mes_OrgResHeadEntity
    {
        public string ID { set; get; }
        public string O_OrgResNo { set; get; }
        public string O_Record { set; get; }
        public string O_ProCode { set; get; }
        public string O_WorkShopCode { set; get; }
        public string O_WorkShopName { set; get; }
        public string O_OrderNo { set; get; }
        public string O_OrderDate { set; get; }
        public int O_Status { set; get; }
        public string O_CreateBy { set; get; }
        public DateTime O_CreateDate { set; get; }
        public string O_UpdateBy { set; get; }
        public DateTime O_UpdateDate { set; get; }
        public string O_Remark { set; get; }
        public string O_DeleteBy { set; get; }
        public DateTime O_DeleteDate { set; get; }
        public string O_UploadBy { set; get; }
        public DateTime O_UploadDate { set; get; }
        public string O_TeamCode { set; get; }
        public string O_TeamName { set; get; }
    }
}
