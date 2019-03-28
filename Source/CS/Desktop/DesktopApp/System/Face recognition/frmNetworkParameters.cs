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
    public partial class frmNetworkParameters : DockContent
    {
        public class ResultInfo<T>
        {
            private int result;//表示接口是否调通，1 成功，0 失败，通常只要设备服务器能响应，该值均为 1
            private Boolean success;//此次操作是否成功，成功为 true，失败为false
            private T data;//接口返回的业务数据，类型可为数值、字符串或集合等
            private String msg;//接口返回的信息，通常是错误类型码的原因信息
        }

        public frmNetworkParameters()
        {
            InitializeComponent();
        }

        private void frmNetworkParameters_Load(object sender, EventArgs e)
        {

        }

        private void btn_SettingPassword_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.1.128:8090/setPassWord";

            string postData = "oldPass=" + txt_OldPassword.Text.Trim() + "&newPass=" + txt_Password.Text.Trim();

            string strtemp = FaceRecognition.Port(url, postData);
            if (!FaceRecognition.json(strtemp))
            {
                untCommon.InfoMsg("该IP地址不可用！");
                return;
            }
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

        private void btn_SettingInternet_Click(object sender, EventArgs e)
        {

        }

        private void btn_SettingWifi_Click(object sender, EventArgs e)
        {
            string url = "http://192.168.1.128:8090/setWifi";

            string postData = "oldPass=" + txt_OldPassword.Text.Trim() + "&newPass=" + txt_Password.Text.Trim();

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
