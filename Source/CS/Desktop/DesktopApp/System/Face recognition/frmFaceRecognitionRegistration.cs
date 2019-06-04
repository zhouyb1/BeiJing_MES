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
            MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
            var MesDevice = MesDeviceBLL.GetList_Deparemaent(cmb_Department.Text);
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

                string postData = "pass="+ 12345678 +"&person={\"id\":" + "\"" + user[i].F_EnCode + "\"," + "\"idcardNum\":" + "\"" + null + "\"," + "\"name\":" + "\"" + txtUserName.Text + "\"" + "}";
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

        private void frmFaceRecognitionRegistration_Load(object sender, EventArgs e)
        {
            SysUserBLL userbll = new SysUserBLL();
            SysUser user = userbll.getDetail(F_Account);

            AMBaseDepartmentBLL AMBaseDepartmentBLL = new AMBaseDepartmentBLL();
           // var rows = AMBaseDepartmentBLL.GetList_ID(user.F_DepartmentId);

            List<AMBaseDepartmentEntity> rows = AMBaseDepartmentBLL.GetList_D_Name();
            cmb_Department.DataSource = rows;
            cmb_Department.DisplayMember = "F_FullName";
            txtUserName.Text = user.F_RealName;
            cmb_Department.Text = user.D_Code;
            cmb_Image.Text = "照片1";

            MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
            var MesDevice = MesDeviceBLL.GetList_Deparemaent(cmb_Department.Text);
            if (MesDevice.Count < 1 || MesDevice == null)
            {
                untCommon.InfoMsg("该部门暂无人脸识别设备！");
                return;
            }
            string url = "http://" + MesDevice[0].D_IP + ":8090/person/findByPage";

            string postData = "pass=12345678&personId=" + user.F_EnCode;

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
            //List<string> list = new List<string>();
            //var rows = AMBaseDepartmentBLL.GetList_D_Name();
            //for (int i = 0; i < rows.Count; i++)
            //{
            //    list.Add(rows[i].F_FullName);
            //}
            //cmb_Department.DataSource = list;
            //txtUserName.Text = user.F_RealName;
            //cmb_Department.Text = user.D_Code;
            //AM_Base_Department.F_FullName
            //string U_Code = dataGridView.SelectedRows[0].Cells["F_Account"].Value.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool file()
        {
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
                    if (fileInfo.Length > 102400)
                    {
                        MessageBox.Show("上传的图片不能大于100K");
                        return false;
                    }
                    else
                    {
                        txtImageFile.Text = fileDialog.FileName;
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
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                if (file())
                {
                    string ID = Guid.NewGuid().ToString("N");//照片名称
                    SysUser user = new SysUser();
                    SysUserBLL SysUserBLL = new SysUserBLL();
                    user = SysUserBLL.getDetail(F_Account);
                    AMBaseAnnexesFileEntity AMBaseAnnexesFileEntity = new AMBaseAnnexesFileEntity();
                    AMBaseAnnexesFileBLL AMBaseAnnexesFileBLL = new AMBaseAnnexesFileBLL();
                    AMBaseAnnexesFileEntity.F_Id = ID;
                    AMBaseAnnexesFileEntity.F_FolderId = Guid.NewGuid().ToString();
                    AMBaseAnnexesFileEntity.F_FileName = Path.GetFileName(imagefile);//获取文件名和扩展名
                    AMBaseAnnexesFileEntity.F_FilePath = "D:/fileAnnexes/shop_erp/System/" + DateTime.Now.ToString("yyyyMMdd") + "/" + AMBaseAnnexesFileEntity.F_Id;
                    AMBaseAnnexesFileEntity.F_FileSize = fileInfoLength;
                    AMBaseAnnexesFileEntity.F_FileExtensions = Path.GetExtension(imagefile);//获取文件扩展名
                    AMBaseAnnexesFileEntity.F_FileType = imagefile.Substring(imagefile.LastIndexOf(".") + 1);// Path.GetExtension(imagefile).Substring(0, 1);
                    //AMBaseAnnexesFileEntity.F_DownloadCount = "";
                    AMBaseAnnexesFileEntity.F_CreateDate = DateTime.Now;   
                    AMBaseAnnexesFileEntity.F_CreateUserId = SysUser.F_Account.ToString();
                    AMBaseAnnexesFileEntity.F_CreateUserName = SysUser.F_RealName.ToString();

                    user.F_Account = F_Account;
                    switch (cmb_Image.Text)
                    {
                        case "照片1":
                            user.F_Picture1 = AMBaseAnnexesFileEntity.F_Id;
                            break;
                        case "照片2":
                            user.F_Picture2 = AMBaseAnnexesFileEntity.F_Id;
                            break;
                        case "照片3":
                            user.F_Picture3 = AMBaseAnnexesFileEntity.F_Id;
                            break;
                    }

                    //Computer MyComputer = new Computer();
                    //imagefile = System.AppDomain.CurrentDomain.BaseDirectory;
                    //MyComputer.FileSystem.RenameFile(imagefile, image);//imagefile是所要重命名的文件的全路径，image是目标文件名
                    //image = Path.GetFileNameWithoutExtension(imagefile);// 没有扩展名的文件名
                    //FaceRecognition.Get_zjdz(imagefile);
                    string urlUpdate = "http://192.168.1.140:7001/pp/"; //+ DateTime.Now.ToString("yyyyMMdd");//
                    FaceRecognition.HttpUploadFile(urlUpdate, imagefile, ID);

                    string strBase64 = FaceRecognition.ImageToBase64(imagefile);//照片转base64

                    MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
                    var MesDevice = MesDeviceBLL.GetList_Deparemaent(cmb_Department.Text);
                    if (MesDevice.Count<1||MesDevice == null)
                    {
                        untCommon.InfoMsg("该部门暂无人脸识别设备！");
                        return;
                    }
                    string url = "http://" + MesDevice[0].D_IP + ":8090/face/create";

                    //string postData = "pass=" + 12345678 + "\n" + "&personId=" + user.F_EnCode + "\n" + "&faceId=" + ID + "\n" + "&imgBase64:" + strBase64 + "";

                    string postData = "pass=" + 12345678 + "&personId=" + user.F_EnCode + "&faceId=" + ID + "&imgBase64=" + strBase64 + "";
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
                        //string urlUpdate = "http://192.168.1.140:7001/pp" + DateTime.Now.ToString("yyyyMMdd");//
                        //FaceRecognition.UpLoadFile(imagefile, urlUpdate, ID);
                        if (AMBaseAnnexesFileBLL.SaveEntityTrans(ID, user.F_Account,"", AMBaseAnnexesFileEntity, user) > 0)
                        {
                            untCommon.InfoMsg("照片上传成功！");
                            this.Close();
                        }
                        else
                        {
                            untCommon.InfoMsg("照片上传失败！");
                        }
                    }
                    #region 
                    //if (AMBaseAnnexesFileBLL.SaveEntity("", AMBaseAnnexesFileEntity) > 0)
                    //{
                    //    if (SysUserBLL.Edit(user) > 0)
                    //    {
                    //        switch (cmb_Image.Text)
                    //        {
                    //            case "照片1":
                    //                AMBaseAnnexesFileBLL.DeleteEntity(user.F_Picture1);
                    //                break;
                    //            case "照片2":
                    //                AMBaseAnnexesFileBLL.DeleteEntity(user.F_Picture2);
                    //                break;
                    //            case "照片3":
                    //                AMBaseAnnexesFileBLL.DeleteEntity(user.F_Picture3);
                    //                break;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        untCommon.InfoMsg("上传失败！");
                    //    }
                    //}
                    //else
                    //{
                    //    untCommon.InfoMsg("上传失败！");
                    //} 
                    #endregion
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("照片上传失败!");
            }
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
            SysUser user = new SysUser();
            MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
            SysUserBLL SysUserBLL = new SysUserBLL();
            var MesDevice = MesDeviceBLL.GetList_Deparemaent(cmb_Department.Text);
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
            }
        }
    }
}
