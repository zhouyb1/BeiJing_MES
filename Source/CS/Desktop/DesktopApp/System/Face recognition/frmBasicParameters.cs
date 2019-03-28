using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmBasicParameters : DockContent
    {
        public frmBasicParameters()
        {
            InitializeComponent();
        }

        private void frmBasicParameters_Load(object sender, EventArgs e)
        {

        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.1.128:8090/setPassWord";

            string postData = "oldPass=" + textBox1.Text.Trim() + "&newPass=" + textBox2.Text.Trim();

            string strtemp = FaceRecognition.Port(url, postData);
            JObject joModel = (JObject)JsonConvert.DeserializeObject(strtemp);
            string strData;
            if (bool.Parse(joModel["success"].ToString()))
            {
                strData = joModel["data"].ToString();
            }
            else
            {
                strData = joModel["msg"].ToString();
            }
            untCommon.InfoMsg(strData);
        }
    }
}
