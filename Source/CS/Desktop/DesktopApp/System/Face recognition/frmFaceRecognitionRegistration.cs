using Business;
using Business.System;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmFaceRecognitionRegistration : DockContent
    {
        private string F_Account;
        private string image = "";//照片名
        private string imagefile = "";//照片路径
        private string fileInfoLength = "";//文件大小
        private SysUser SysUser;
        Image imagename;

        public frmFaceRecognitionRegistration(string _F_Account, SysUser _SysUser)
        {
            InitializeComponent();
            F_Account = _F_Account;
            SysUser = _SysUser;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            RegFace();
            RegPicture();
        }
        /// <summary>
        /// 注册人员到设备
        /// </summary>
        private void RegFace()
        {
            #region
            MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
            var MesDevice = MesDeviceBLL.GetList_Deparemaent(txtDepartment.Text, txtTeam.Text);
            SysUserBLL userbll = new SysUserBLL();
            var user = userbll.getDetail_F_EnCode(F_Account);
            if (MesDevice.Count < 1 || MesDevice == null)
            {
                untCommon.InfoMsg("该部门暂无人脸识别设备！");
                return;
            }
            for (int i = 0; i < MesDevice.Count; i++)
            {
                string url = "http://" + MesDevice[i].D_IP + ":8090/person/create";

                string postData = "pass=" + 12345678 + "&person={\"id\":" + "\"" + user[i].F_Account + "\"," + "\"idcardNum\":" + "\"" + null + "\"," + "\"name\":" + "\"" + txtUserName.Text + "\"" + "}";
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
            #endregion
        }
        /// <summary>
        /// 注册照片
        /// </summary>
        private void RegPicture()
        {
            #region
            SysUserBLL userbll = new SysUserBLL();
            SysUser user = userbll.getDetail(F_Account);

            string strBase64 = FaceRecognition.ImageToBase64(imagefile);//照片转base64

            MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
            var MesDevice = MesDeviceBLL.GetList_Deparemaent(txtDepartment.Text, txtTeam.Text);
            if (MesDevice.Count < 1 || MesDevice == null)
            {
                untCommon.InfoMsg("该部门暂无人脸识别设备！");
                return;
            }
            for (int i = 0; i < MesDevice.Count; i++)
            {
                string url = "http://" + MesDevice[i].D_IP + ":8090/face/create";

                //string postData = "pass=" + 12345678 + "\n" + "&personId=" + user.F_EnCode + "\n" + "&faceId=" + ID + "\n" + "&imgBase64:" + strBase64 + "";

                string postData = "pass=" + 12345678 + "&personId=" + user.F_Account + "&faceId=" + user.F_Account + "&imgBase64=" + strBase64 + "";
                string strtemp = FaceRecognition.HttpPost(url, postData);
                if (!FaceRecognition.json(strtemp))
                {
                    untCommon.InfoMsg("该IP地址不可用！");
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
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
                    label3.Text = "已注册";
                    untCommon.InfoMsg("人脸识别注册成功！");
                }
            }
            #endregion
        }
        /// <summary>
        /// 更新照片
        /// </summary>
        private void UpdatePicture()
        {
            #region
            try
            {
                SysUserBLL userbll = new SysUserBLL();
                SysUser user = userbll.getDetail(F_Account);

                string strBase64 = FaceRecognition.ImageToBase64(imagefile);//照片转base64

                MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
                var MesDevice = MesDeviceBLL.GetList_Deparemaent(txtDepartment.Text, txtTeam.Text);
                if (MesDevice.Count < 1 || MesDevice == null)
                {
                    untCommon.InfoMsg("该部门暂无人脸识别设备！");
                    return;
                }

                for (int i = 0; i < MesDevice.Count; i++)
                {
                    string url = "http://" + MesDevice[i].D_IP + ":8090/face/update";
                    string postData = "pass=" + 12345678 + "&personId=" + user.F_Account + "&faceId=" + user.F_Account + "&imgBase64=" + strBase64 + "";
                    string strtemp = FaceRecognition.HttpPost(url, postData);
                    if (!FaceRecognition.json(strtemp))
                    {
                        untCommon.InfoMsg("该IP地址不可用！");
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
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
                        untCommon.InfoMsg("人脸识别更新成功！");
                    }
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("照片上传失败!");
            }
            #endregion
        }
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="strPath"></param>
        private void Down(string strPath,string url)
        {
            #region
            try
            {
                //string url = "http://183.236.45.60:7001/picture/1006.jpg";
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();


                //创建本地文件写入流
                Stream stream = new FileStream(strPath, FileMode.Create);

                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();

            }
            catch(Exception ex)
            {
                untCommon.ErrorMsg("此用户没有照片，请先上传照片！");
                this.Close();
                return;
            }
            #endregion
            //return path;
        }

        private void frmFaceRecognitionRegistration_Load(object sender, EventArgs e)
        {
            #region
            SysUserBLL userbll = new SysUserBLL();
            SysUser user = userbll.getDetail(F_Account);

            imagefile = Application.StartupPath + "\\img\\" + user.F_Account + ".jpg";
            string urlpicture = "http://183.236.45.60:7001/picture/" + user.F_Account + ".jpg";
            Down(imagefile, urlpicture);

            //if(!File.Exists(imagefile))
            //{
            //    untCommon.InfoMsg("该用户还没有图片，请先上传图片！");
            //    this.Close();
            //    return;
            //}
            if (File.Exists(imagefile))
            {
                FileStream fs = new FileStream(imagefile, FileMode.Open, FileAccess.Read);
                Byte[] mybyte = new byte[fs.Length];
                fs.Read(mybyte, 0, mybyte.Length);
                fs.Close();


                MemoryStream ms = new MemoryStream(mybyte);
                Bitmap myimge = new Bitmap(ms);
                pictureBox1.Image = myimge;
            }

            AMBaseDepartmentBLL AMBaseDepartmentBLL = new AMBaseDepartmentBLL();
           // var rows = AMBaseDepartmentBLL.GetList_ID(user.F_DepartmentId);

            List<AMBaseDepartmentEntity> rows = AMBaseDepartmentBLL.GetList_D_Name();
            txtDepartment.Text = user.D_Code;
            txtUserName.Text = user.F_RealName;
            txtTeam.Text = user.F_TeamName;

            MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
            var MesDevice = MesDeviceBLL.GetList_Deparemaent(txtDepartment.Text,txtTeam.Text);
            if (MesDevice.Count < 1 || MesDevice == null)
            {
                untCommon.InfoMsg("该部门暂无人脸识别设备！");
                this.Close();
                return;
            }
            string url = "http://" + MesDevice[0].D_IP + ":8090/person/findByPage";

            string postData = "pass=12345678&personId=" + user.F_Account;

            string strtemp = FaceRecognition.Port(url, postData);
            if (!FaceRecognition.json(strtemp))
            {
                label3.Text = "该部门暂无可用设备";
                return;
            }
            JObject joModel = (JObject)JsonConvert.DeserializeObject(strtemp);
            if (!bool.Parse(joModel["success"].ToString()))
            {
                label3.Text = "未注册";
                return;
            }
            else
            {
                label3.Text = "已注册";
            }
            #endregion
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool file()
        {
            #region
            //初始化一个OpenFileDialog类
            OpenFileDialog fileDialog = new OpenFileDialog();
            //fileDialog.Filter = "(*.jpg)|*.jpg";
            //判断用户是否正确的选择了文件
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //获取用户选择文件的后缀名
                string extension = Path.GetExtension(fileDialog.FileName);
                //声明允许的后缀名
                string[] str = new string[] { ".gif", ".jpge", ".jpg" };
                if (!((IList)str).Contains(extension))
                {
                    MessageBox.Show("仅能上传gif,jpge,jpg格式的图片！");
                    return false;
                }
                else
                {
                    //获取用户选择的文件，并判断文件大小不能超过100K，fileInfo.Length是以字节为单位的
                    FileInfo fileInfo = new FileInfo(fileDialog.FileName);
                    fileInfoLength = fileInfo.Length.ToString();
                    if (fileInfo.Length > 204800)
                    {
                        MessageBox.Show("上传的图片不能大于200K");
                        return false;
                    }
                    else
                    {
                        //string strPath = System.AppDomain.CurrentDomain.BaseDirectory + fileDialog.FileName;
                        //FileStream fs = new FileStream(strPath, FileMode.Open, FileAccess.Read);
                        //Byte[] mybyte = new byte[fs.Length];
                        //fs.Read(mybyte, 0, mybyte.Length);
                        //fs.Close();

                        //MemoryStream ms = new MemoryStream(mybyte);
                        //Bitmap myimge = new Bitmap(ms);
                        //pictureBox1.Image = myimge;


                        imagename = System.Drawing.Image.FromFile(fileDialog.FileName);
                        //image = Path.GetExtension(fileDialog.FileName);//获取扩展名
                        image = Path.GetFileName(fileDialog.FileName);//获取文件名和扩展名
                        imagefile = fileDialog.FileName;//获取文件路径

                        Bitmap myBmp = new Bitmap(imagename);

                        pictureBox1.Image = myBmp;
                        //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; //设置picturebox为缩放模式
                        //pictureBox1.Width = myBmp.Width;
                        //pictureBox1.Height = myBmp.Height;
                        return true;
                    }
                }
            }
            return false;
            #endregion
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            
        }

        public static bool connectState(string path)
        {
            return connectState(path, "", "");
        }

        /// <summary>
        /// 连接远程共享文件夹
        /// </summary>
        /// <param name="path">远程共享文件夹的路径</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public static bool connectState(string path, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = "net use " + path + " " + passWord + " /user:" + userName;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
                else
                {
                    throw new Exception(errormsg);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                MessageBox.Show(ex.Message);
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }
        private void btnRegisterImage_Click(object sender, EventArgs e)
        {
            SysUserBLL userbll = new SysUserBLL();
            SysUser user = userbll.getDetail(F_Account);

            string strBase64 = FaceRecognition.ImageToBase64(imagefile);//照片转base64

            MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
            var MesDevice = MesDeviceBLL.GetList_Deparemaent(txtDepartment.Text,txtTeam.Text);
            if (MesDevice.Count < 1 || MesDevice == null)
            {
                untCommon.InfoMsg("该部门暂无人脸识别设备！");
                return;
            }
            for (int i = 0; i < MesDevice.Count; i++)
            {
                string url = "http://" + MesDevice[i].D_IP + ":8090/face/create";

                //string postData = "pass=" + 12345678 + "\n" + "&personId=" + user.F_EnCode + "\n" + "&faceId=" + ID + "\n" + "&imgBase64:" + strBase64 + "";

                string postData = "pass=" + 12345678 + "&personId=" + user.F_EnCode + "&faceId=" + user.F_EnCode + "&imgBase64=" + strBase64 + "";
                string strtemp = FaceRecognition.HttpPost(url, postData);
                if (!FaceRecognition.json(strtemp))
                {
                    untCommon.InfoMsg("该IP地址不可用！");
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
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
                    untCommon.InfoMsg("人脸识别注册成功！");
                }
            }
            /*SysUser user = new SysUser();
            MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
            SysUserBLL SysUserBLL = new SysUserBLL();
            var MesDevice = MesDeviceBLL.GetList_Deparemaent(txtDepartment.Text,txtTeam.Text);
            user = SysUserBLL.getDetail(F_Account);
            if (MesDevice.Count <1)
            {
                untCommon.InfoMsg("该部门没有可用设备！");
                return;
            }
            string url = "http://" + MesDevice[0].D_IP + ":8090/face/takeImg";

            string postData = "pass=12345678&personId=" + user.F_EnCode + "";

            string strtemp = FaceRecognition.Port(url, postData);
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
                untCommon.InfoMsg("人脸识别注册成功！");
            }*/
        }

        private void cmb_Image_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strFilepath = "http://192.168.1.140:7001/pp/微信图片_20190610160357.jpg";
            string strSavepath = System.Windows.Forms.Application.StartupPath;

            FaceRecognition.Download(strFilepath, strSavepath);

        }

        private void btnUploadServer(object sender, EventArgs e)
        {
            SysUserBLL userbll = new SysUserBLL();
            SysUser user = userbll.getDetail(F_Account);

            

            MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
            var MesDevice = MesDeviceBLL.GetList_Deparemaent(txtDepartment.Text, txtTeam.Text);
            if (MesDevice.Count < 1 || MesDevice == null)
            {
                untCommon.InfoMsg("该部门暂无人脸识别设备！");
                return;
            }
            for (int i = 0; i < MesDevice.Count; i++)
            {
                string url = "http://" + MesDevice[i].D_IP + ":8090/face/delete";

                //string postData = "pass=" + 12345678 + "\n" + "&personId=" + user.F_EnCode + "\n" + "&faceId=" + ID + "\n" + "&imgBase64:" + strBase64 + "";

                string postData = "pass=" + 12345678 + "&faceId=" + user.F_EnCode + "";
                string strtemp = FaceRecognition.HttpPost(url, postData);
                if (!FaceRecognition.json(strtemp))
                {
                    untCommon.InfoMsg("该IP地址不可用！");
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
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
                    untCommon.InfoMsg("人脸图片删除成功！");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "(*.Jpg)|*.Jpg|所有文件(*.*)|*.*";

            if (openFileDialog1.ShowDialog(this) == DialogResult.Cancel)
            {
                openFileDialog1.Dispose();
                return;
            }

            string strPathFile = openFileDialog1.FileName;
            openFileDialog1.Dispose();

            FileInfo fileInfo = new FileInfo(strPathFile);
            fileInfoLength = fileInfo.Length.ToString();
            if (fileInfo.Length > 1024000)
            {
                MessageBox.Show("上传的图片不能大于1M");
                return;
            }
            else
            {

                imagename = System.Drawing.Image.FromFile(strPathFile);

                image = Path.GetFileName(strPathFile);//获取文件名和扩展名
                imagefile = strPathFile;//获取文件路径

                Bitmap myBmp = new Bitmap(imagename);

                pictureBox1.Image = myBmp;
                //pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; //设置picturebox为缩放模式
                //pictureBox1.Width = myBmp.Width;
                //pictureBox1.Height = myBmp.Height;
                return;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
            var MesDevice = MesDeviceBLL.GetList_Deparemaent(txtDepartment.Text, txtTeam.Text);
            SysUserBLL userbll = new SysUserBLL();
            var user = userbll.getDetail_F_EnCode(F_Account);
            if (MesDevice.Count < 1 || MesDevice == null)
            {
                untCommon.InfoMsg("该部门暂无人脸识别设备！");
                return;
            }
            for (int i = 0; i < MesDevice.Count; i++)
            {
                string url = "http://" + MesDevice[i].D_IP + ":8090/person/delete";

                string postData = "pass=" + 12345678 + "&id=" + user[i].F_Account + "";
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
                    label3.Text = "未注册";
                    pictureBox1.Image = null;
                    untCommon.InfoMsg("人脸识别用户删除成功！");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdatePicture();
        }

    }
}
