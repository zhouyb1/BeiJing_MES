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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmImageQuery : DockContent
    {
        private frmMain frmMain;
        private string fileInfoLength = "";//文件大小
        private string image = "";//照片名
        private string imagefile = "";//照片路径
        private SysUser SysUser;
        Image imagename;
        public frmImageQuery(frmMain _frmMain, SysUser _User)
        {
            InitializeComponent();
            frmMain = _frmMain;
            SysUser = _User;
            dataGridView.AutoGenerateColumns = false;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                SysUserBLL SysUserBLL = new SysUserBLL();
                var rows = SysUserBLL.load_RealName(txt_RealName.Text);

                if (rows == null || rows.Rows.Count < 1)
                {
                    untCommon.InfoMsg("没有任何数据！");
                    return;
                }

                dataGridView.DataSource = rows;
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("用户管理加载数据异常：" + ex.Message);
            }
        }

        private void dataGridView_CelldecimalClick(object sender, DataGridViewCellEventArgs e)
        {
            photo();
        }

        private void photo()
        {
            try
            {
                string D_Code = dataGridView.SelectedRows[0].Cells["部门"].Value.ToString();
                MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
                var MesDevice = MesDeviceBLL.GetList_Deparemaent(D_Code,"");
                cmb_Device.DataSource = MesDevice;
                cmb_Device.DisplayMember = "D_Name";
                cmb_Image.Text = "照片1";
            }
            catch (Exception)
            {
                untCommon.InfoMsg("该员工照片显示错误！");
            }
        }

        private void cmb_Device_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                if (dataGridView.SelectedRows.Count < 1)
                    return;
                string EnCode = dataGridView.SelectedRows[0].Cells["工号"].Value.ToString();

                MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
                var MesDevice = MesDeviceBLL.GetList_Name(cmb_Device.Text);
                if (MesDevice.Count < 1)
                {
                    return;
                }
                string url = "http://" + MesDevice[0].D_IP + ":8090/face/find";

                string postData = "pass=" + 12345678 + "&personId=" + EnCode + "";

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
                    //untCommon.InfoMsg("人脸识别注册成功！");
                    var jData = (JArray)joModel["data"];
                    string strUrl = "";
                    List<string> list = new List<string>();
                    for (var i = 0; i < jData.Count; i++)
                    {
                        var jObj = (JObject)jData[i];
                        strUrl = (string)jObj["path"].ToString();
                        list.Add(strUrl);
                    }
                    Stream stream = FaceRecognition.Info(list[0].ToString());
                    pictureBox1.Image = Image.FromStream(stream);
                }
            }
            catch (Exception)
            {
                untCommon.InfoMsg("照片显示错误！");
            }
        }

        private void cmb_Image_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                if (dataGridView.SelectedRows.Count < 1)
                    return;
                string EnCode = dataGridView.SelectedRows[0].Cells["工号"].Value.ToString();

                MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
                var MesDevice = MesDeviceBLL.GetList_Name(cmb_Device.Text);

                string url = "http://" + MesDevice[0].D_IP + ":8090/face/find";

                string postData = "pass=" + 12345678 + "&personId=" + EnCode + "";

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
                    //untCommon.InfoMsg("人脸识别注册成功！");
                    var jData = (JArray)joModel["data"];
                    string strUrl = "";
                    int number = 0;
                    List<string> list = new List<string>();
                    for (var i = 0; i < jData.Count; i++)
                    {
                        var jObj = (JObject)jData[i];
                        strUrl = (string)jObj["path"].ToString();
                        list.Add(strUrl);
                    }
                    switch (cmb_Image.Text)
                    {
                        case "照片1":
                            number = 0;
                            break;
                        case "照片2":
                            number = 1;
                            break;
                        case "照片3":
                            number = 2;
                            break;
                    }
                    Stream stream = FaceRecognition.Info(list[number]);
                    pictureBox1.Image = Image.FromStream(stream);
                }
            }
            catch (Exception)
            {
                untCommon.InfoMsg("照片显示错误！");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                if (dataGridView.SelectedRows.Count < 1)
                    return;
                photo();
                if (pictureBox1.Image == null)
                {
                    untCommon.InfoMsg("请选择正确的照片！");
                    return;
                }
                if (file())
                {
                    string ID = Guid.NewGuid().ToString("N");//照片名称
                    SysUser user = new SysUser();
                    SysUserBLL SysUserBLL = new SysUserBLL();
                    string F_Account = dataGridView.SelectedRows[0].Cells["账号"].Value.ToString();
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
                    string strBase64 = FaceRecognition.ImageToBase64(imagefile);//照片转base64

                    MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
                    string D_Code = dataGridView.SelectedRows[0].Cells["部门"].Value.ToString();
                    var MesDevice = MesDeviceBLL.GetList_Deparemaent(D_Code,"");
                    if (MesDevice.Count < 1 || MesDevice == null)
                    {
                        untCommon.InfoMsg("该部门暂无人脸识别设备！");
                        return;
                    }
                    string url = "http://" + MesDevice[0].D_IP + ":8090/face/update";

                    //string postData = "pass=" + 12345678 + "\n" + "&personId=" + user.F_EnCode + "\n" + "&faceId=" + ID + "\n" + "&imgBase64:" + strBase64 + "";

                    string postData = "pass=" + 12345678 + "&personId=" + user.F_EnCode + "&faceId=" + ID + "&imgBase64=" + strBase64 + "";
                    string strtemp = FaceRecognition.HttpPost(url, postData);
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
                        untCommon.InfoMsg("人脸识别更新成功！");
                        string F_Picture = "";
                        switch (cmb_Image.Text)
                        {
                            case "照片1":
                                user.F_Picture1 = F_Picture;
                                break;
                            case "照片2":
                                user.F_Picture2 = F_Picture;
                                break;
                            case "照片3":
                                user.F_Picture3 = F_Picture;
                                break;
                        }
                        if (AMBaseAnnexesFileBLL.SaveEntityTrans("", user.F_UserId, F_Picture, AMBaseAnnexesFileEntity, user) > 0)
                        {
                            untCommon.InfoMsg("照片上传成功！");
                        }
                        else
                        {
                            untCommon.InfoMsg("照片上传失败！");
                        }
                    }
                    //if (AMBaseAnnexesFileBLL.SaveEntity("", AMBaseAnnexesFileEntity) > 0)
                    //{
                    //    if (SysUserBLL.Edit(user) > 0)
                    //    {

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
                    //string url = "183.236.45.60";
                    //if (FaceRecognition.uploadFileByHttp(url, imagefile))
                    //{
                    //}

                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("照片上传失败!");
            }
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image == null)
                {
                    untCommon.InfoMsg("请选择正确的照片！");
                    return;
                }
                photo();
                MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
                AMBaseAnnexesFileBLL AMBaseAnnexesFileBLL = new AMBaseAnnexesFileBLL();
                string D_Code = dataGridView.SelectedRows[0].Cells["部门"].Value.ToString();
                var MesDevice = MesDeviceBLL.GetList_Deparemaent(D_Code,"");
                string F_Account = dataGridView.SelectedRows[0].Cells["账号"].Value.ToString();
                SysUserBLL SysUserBLL = new SysUserBLL();
                var rows = SysUserBLL.getDetail(F_Account);
                string faceId = "";
                switch (cmb_Image.Text)
                {
                    case "照片1":
                        faceId = rows.F_Picture1;
                        rows.F_Picture1 = null;
                        break;
                    case "照片2":
                        faceId = rows.F_Picture2;
                        rows.F_Picture2 = null;
                        break;
                    case "照片3":
                        faceId = rows.F_Picture3;
                        rows.F_Picture3 = null;
                        break;
                }

                string url = "http://" + MesDevice[0].D_IP + ":8090/face/delete";

                //string postData = "pass=" + 12345678 + "\n" + "&personId=" + user.F_EnCode + "\n" + "&faceId=" + ID + "\n" + "&imgBase64:" + strBase64 + "";

                string postData = "pass=" + 12345678 + "&faceId=" + faceId + "";
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
                    if (SysUserBLL.Edit(rows) > 0)
                    {
                        AMBaseAnnexesFileBLL.DeleteEntity(faceId);
                        untCommon.InfoMsg("照片删除成功！");
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
               untCommon.ErrorMsg("照片删除失败!");
            }
        }

        private void frmImageQuery_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
