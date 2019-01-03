using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public class LoginInfo
    {
        public string Number { get; set; }

        public string Pwd { get; set; }

        public bool Remember { get; set; }
    }
}
