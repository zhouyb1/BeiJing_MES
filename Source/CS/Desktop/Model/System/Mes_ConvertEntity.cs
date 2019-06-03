using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Mes_ConvertEntity
    {
        public string ID { set; get; }
        public string C_Code { set; get; }
        public string C_Name { set; get; }
        public string C_SecCode { set; get; }
        public string C_SecName { set; get; }
        public string C_CreateBy { set; get; }
        public DateTime C_CreateDate { set; get; }
        public string C_UpdateBy { set; get; }
        public DateTime C_UpdateDate { set; get; }
        public string C_Remark { set; get; }

    }
}
