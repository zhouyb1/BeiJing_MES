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
    public partial class frmCallbackParameter : DockContent
    {
        public frmCallbackParameter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("是否要保存","",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                string url = "http://192.168.1.128:8090/setIdentifyCallBack";

                string postData = "pass=" + 12345678 + "&" + "callbackUrl=http://192.168.1.140:8099/api/FaceRecording/GetUserInfo";
                string strtemp = FaceRecognition.RequestWithHttps(url, postData);
                if (!FaceRecognition.json(strtemp))
                {
                    untCommon.InfoMsg("该IP地址不可用！");
                    return;
                }
                JObject joModel = (JObject)JsonConvert.DeserializeObject(strtemp);
                if (!bool.Parse(joModel["success"].ToString()))
                {
                    untCommon.InfoMsg(joModel["msg"].ToString());
                    return;
                }
                else
                {
                    untCommon.InfoMsg("人脸识别用户注册成功！");
                }
            }
        }
    }
}
