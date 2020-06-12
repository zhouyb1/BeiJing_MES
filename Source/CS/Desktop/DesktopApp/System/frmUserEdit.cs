using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;
using Model;
using Business.System;
using System.IO;
using System.Collections;
using System.Reflection;
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web;
using System.Net;

namespace DesktopApp
{
    public partial class frmUserEdit : Form
    {
        private int OperationType = 2;//1新增、2修改、3明细
        private string PrimaryKey = "";//账户
        private SysUser SysUser;
        private frmUserList frmParent;
        private string department = "";
        private string role = "";
        private string image = "";//照片名
        private string imagefile = "";//照片路径
        private string fileInfoLength = "";//文件大小
        private string strTeamCode = "";
        private string strTeamName = "";


        public frmUserEdit(frmUserList _frmUserList, SysUser _SysUser, string _PrimaryKey, int _OperationType)
        {
            InitializeComponent();

            PrimaryKey = _PrimaryKey;
            OperationType = _OperationType;
            SysUser = _SysUser;
            frmParent = _frmUserList;

            if (OperationType == 3)
            {
                getDetail();

                this.btnSave.Visible = false;
            }

            if (OperationType == 2)
            {
                loadDroplistData();
                getDetail();

                this.F_ModifyUserName.Text = SysUser.F_Account;
                this.F_Account.ReadOnly = true;
            }

            if (OperationType == 1)
            {
                F_CreateUserName.Text = SysUser.F_Account;
            }

            //loadDroplistData();
        }

        private void loadDroplistData()
        {
            try
            {
                //SysDepartmentBLL departmentbll = new SysDepartmentBLL();
                //var rowss = departmentbll.loadData("");
                //D_Code.DataSource = rows;
                //D_Code.ValueMember = "D_Code";
                //D_Code.DisplayMember = "D_Name";

                AMBaseDepartmentBLL AMBaseDepartmentBLL = new AMBaseDepartmentBLL();
                List<AMBaseDepartmentEntity> rows = AMBaseDepartmentBLL.GetList_D_Name();
                D_Code.DataSource = rows;
                D_Code.DisplayMember = "F_FullName";
               // D_Code.ValueMember = "D_Code";

                //if (string.IsNullOrEmpty(department))
                //{
                //    D_Code.SelectedIndex = 0;
                //}
                //else
                //{
                //    D_Code.SelectedValue = department;
                //}


                SysRoleBLL rolebll = new SysRoleBLL();
                var datas = rolebll.loadData("");
                R_Code.DataSource = datas;
                R_Code.ValueMember = "R_Code";
                R_Code.DisplayMember = "R_Name";

                Mes_TeamBLL TeamBLL = new Mes_TeamBLL();
                var Teams = TeamBLL.GetList_Team("");
                cmbTeam.DataSource = Teams;
                cmbTeam.ValueMember = "T_Code";
                cmbTeam.DisplayMember = "T_Name";

                //if (string.IsNullOrEmpty(role))
                //{
                //    R_Code.SelectedIndex = 0;
                //}
                //else
                //{
                //    R_Code.SelectedValue = role;
                //}
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("用户管理加载数据异常：" + ex.Message);
            }
        }
        /// <summary>
        /// 初始化页面数据
        /// </summary>
        private void getDetail()
        {
            try
            {
                SysUserBLL userbll = new SysUserBLL();
                SysUser user = userbll.getDetail(PrimaryKey);
                AMBaseAnnexesFileBLL AMBaseAnnexesFileBLL = new AMBaseAnnexesFileBLL();
                AMBaseAnnexesFileEntity AMBaseAnnexesFileEntity = AMBaseAnnexesFileBLL.GetEntity(user.F_Picture1);


                department = user.D_Code;
                strTeamCode = user.F_TeamCode;
                role = user.R_CSCode;

                F_Account.Text = user.F_Account;
                F_RealName.Text = user.F_RealName;
                F_Password.Text = "******";//user.F_Password ;
                F_Gender.SelectedItem = user.F_Gender == 1 ? "男" : "女";
                F_Kind.Text = "正式工";
                cmbImage.Text = "照片1";

                D_Code.Text = department;
                if (role != null)
                {
                    R_Code.SelectedValue = role;
                }
                if (strTeamCode != null)
                {
                    cmbTeam.SelectedValue = strTeamCode;
                }

                F_Mobile.Text = user.F_Mobile;
                F_Email.Text = user.F_Email;
                F_OICQ.Text = user.F_OICQ;
                F_WeChat.Text = user.F_WeChat;
                F_Indate.Text = user.F_Indate.ToString();
                F_Outdate.Text = user.F_Outdate.ToString();

                U_Address.Text = user.U_Address;
                F_Description.Text = user.F_Description;
                F_EnabledMark.Checked = user.F_EnabledMark;
                
                F_CreateUserName.Text = user.F_CreateUserName;
                F_ModifyUserName.Text = user.F_ModifyUserName;

                switch (user.F_Kind)
                {
                    case 1:
                        F_Kind.Text = "正式工";
                        break;
                    case 2:
                        F_Kind.Text = "临时工";
                        break;
                    case 3:
                        F_Kind.Text = "劳务工";
                        break;
                }
                
                //switch (cmbImage.Text)
                //{
                //    case "照片1":
                //        txtImageFile.Text = AMBaseAnnexesFileEntity.F_FilePath;
                //        break;
                //    case "照片2":
                //        txtImageFile.Text = AMBaseAnnexesFileEntity.F_FilePath;
                //        break;
                //    case "照片3":
                //        txtImageFile.Text = AMBaseAnnexesFileEntity.F_FilePath;
                //        break;
                //    case "照片4":
                //        txtImageFile.Text = AMBaseAnnexesFileEntity.F_FilePath;
                //        break;
                //    case "照片5":
                //        txtImageFile.Text = AMBaseAnnexesFileEntity.F_FilePath;
                //        break;
                //}

                F_RFIDCode.Text = user.F_RFIDCode;
                //user.F_Indate = F_Indate.Text;
                //user.F_Outdate = F_Outdate.Text;
                F_Cert.Text = user.F_Cert;
                F_Nation.Text = user.F_Nation;
                F_Record.Text = user.F_Record;
                F_Origin.Text = user.F_Origin;

                //照片显示
                //AMBaseAnnexesFileEntity = AMBaseAnnexesFileBLL.GetEntity(user.F_Picture1);

                //string strPath = System.AppDomain.CurrentDomain.BaseDirectory + AMBaseAnnexesFileEntity.F_FilePath;
                //string strPath = "http://183.236.45.60:7001/picture/" + 10006.jpg";
                
                string strPath = Application.StartupPath + "\\img\\" + user.F_Account + ".jpg";
                string url = "http://183.236.45.60:7001/picture/" + user.F_Account + ".jpg";
                Down(strPath,url);
                FileStream fs = new FileStream(strPath, FileMode.Open, FileAccess.Read);
                Byte[] mybyte = new byte[fs.Length];
                fs.Read(mybyte, 0, mybyte.Length);
                fs.Close();

                MemoryStream ms = new MemoryStream(mybyte);
                Bitmap myimge = new Bitmap(ms);
                pictureBox1.Image = myimge;
                
                if (user.F_CreateDate.HasValue)
                    F_CreateDate.Value = user.F_CreateDate.Value;
                else
                {
                    F_CreateDate.Value = DateTime.Now;
                }

                if (user.F_ModifyDate.HasValue)
                    F_ModifyDate.Value = user.F_ModifyDate.Value;
                else
                {
                    F_ModifyDate.Value = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("用户管理加载数据异常：" + ex.Message);
            }
        }

        private void Down(string strPath,string url)
        {
            try
            {
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
                //return path;


            }
            catch(Exception ex)
            {
                ;
            }



        }

        /// <summary>
        /// 添加
        /// </summary>
        private void addUser()
        {
            try
            {
                if (checkInput())
                {
                    SysUser user = new SysUser();

                    user.F_Account = F_Account.Text;
                    user.F_RealName = F_RealName.Text;
                    user.F_Password = F_Password.Text;
                    user.F_Gender = F_Gender.Text == "男" ? 1 : 0;
                    user.D_Code = D_Code.SelectedValue.ToString();

                    user.R_CSCode = R_Code.SelectedValue.ToString();
                    user.F_Mobile = F_Mobile.Text;
                    user.F_Email = F_Email.Text;
                    user.F_OICQ = F_OICQ.Text;
                    user.F_WeChat = F_WeChat.Text;

                    user.U_Address = U_Address.Text;
                    user.F_Description = F_Description.Text;
                    user.F_EnabledMark = F_EnabledMark.Checked;

                    user.F_CreateUserName = F_CreateUserName.Text;
                    user.F_CreateDate = F_CreateDate.Value;
                    user.F_ModifyUserName = SysUser.F_Account.ToString();
                    user.F_ModifyDate = DateTime.Now;
                    switch (F_Kind.Text)
                    {
                        case "正式工":
                            user.F_Kind = 1;
                            break;
                        case "临时工":
                            user.F_Kind = 2;
                            break;
                        case "劳务工":
                            user.F_Kind = 3;
                            break;
                    }

                    user.F_RFIDCode = F_RFIDCode.Text;
                    //user.F_Indate = F_Indate.Text;
                    //user.F_Outdate = F_Outdate.Text;
                    user.F_Cert = F_Cert.Text;
                    user.F_Nation = F_Nation.Text;
                    user.F_Record = F_Record.Text;
                    user.F_Origin = label99.Text;
                    //user.F_Picture1 = F_RFIDCode.Text;

                    SysUserBLL userbll = new SysUserBLL();
                    if (userbll.Add(user) > 0)
                    {
                        untCommon.InfoMsg("添加成功！");
                        frmParent.loadData();
                    }
                    else
                    {
                        untCommon.InfoMsg("添加失败！");
                    }
                }
            }
            catch (Exception ex)
            {

                untCommon.ErrorMsg("角色管理添加数据异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void updateUser()
        {
            try
            {
                if (checkInput())
                {
                    SysUser user = new SysUser();
                    AMBaseDepartmentBLL AMBaseDepartmentBLL = new AMBaseDepartmentBLL();
                    var rows = AMBaseDepartmentBLL.GetList_F_ID(D_Code.Text);

                    user.F_Account = F_Account.Text;
                    user.F_RealName = F_RealName.Text;
                    //user.F_Password = U_Pwd.Text;
                    user.F_Gender = F_Gender.Text == "男" ? 1 : 0;
                    //user.D_Code = D_Code.SelectedValue.ToString();
                    user.D_Code = D_Code.Text;
                    user.F_DepartmentId = rows[0].F_DepartmentId;

                    user.R_CSCode = R_Code.SelectedValue.ToString();
                    user.F_TeamCode = cmbTeam.SelectedValue.ToString();
                    user.F_TeamName = cmbTeam.Text.ToString();
                    user.F_Mobile = F_Mobile.Text;
                    user.F_Email = F_Email.Text;
                    user.F_OICQ = F_OICQ.Text;
                    user.F_WeChat = F_WeChat.Text;

                    user.U_Address = U_Address.Text;
                    user.F_Description = F_Description.Text;
                    user.F_EnabledMark = F_EnabledMark.Checked;

                    user.F_CreateUserName = F_CreateUserName.Text;
                    user.F_CreateDate = F_CreateDate.Value;

                    user.F_ModifyUserName = SysUser.F_RealName.ToString();
                    user.F_ModifyDate = DateTime.Now;
                    switch (F_Kind.Text)
                    {
                        case "正式工":
                            user.F_Kind = 1;
                            break;
                        case "临时工":
                            user.F_Kind = 2;
                            break;
                        case "劳务工":
                            user.F_Kind = 3;
                            break;
                    }

                    user.F_RFIDCode = F_RFIDCode.Text;
                    //user.F_Indate = F_Indate.Text;
                    //user.F_Outdate = F_Outdate.Text;
                    user.F_Cert = F_Cert.Text;
                    user.F_Nation = F_Nation.Text;
                    user.F_Record = F_Record.Text;
                    user.F_Origin = F_Origin.Text;
                    //user.F_Picture1 = F_RFIDCode.Text;

                    SysUserBLL userbll = new SysUserBLL();
                    if (userbll.Edit(user) > 0)
                    {
                        untCommon.InfoMsg("修改成功！");
                        frmParent.loadData();
                        this.Close();
                    }
                    else
                    {
                        untCommon.InfoMsg("修改失败！");
                    }
                }
            }
            catch (Exception ex)
            {

                untCommon.ErrorMsg("角色管理更新数据异常：" + ex.Message);
            }
        }


        /// <summary>
        /// 校验输入
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            if (string.IsNullOrEmpty(F_Account.Text))
            {
                untCommon.InfoMsg("用户编码不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(F_RealName.Text))
            {
                untCommon.InfoMsg("用户名称不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(F_Password.Text))
            {
                untCommon.InfoMsg("用户密码不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(F_Gender.Text))
            {
                untCommon.InfoMsg("用户性别不能为空！");
                return false;
            }

            if (D_Code.SelectedIndex < 0)
            {
                untCommon.InfoMsg("所属部门不能为空！");
                return false;
            }

            if (R_Code.SelectedIndex < 0)
            {
                untCommon.InfoMsg("用户角色不能为空！");
                return false;
            }

            return true;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (OperationType == 1)
            {
                addUser();
            }
            else
            {
                updateUser();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserEdit_Load(object sender, EventArgs e)
        {

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

                    user.F_Account = PrimaryKey;
                    switch (cmbImage.Text)
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
                        case "照片4":
                            user.F_Picture4 = AMBaseAnnexesFileEntity.F_Id;
                            break;
                        case "照片5":
                            user.F_Picture5 = AMBaseAnnexesFileEntity.F_Id;
                            break;
                    }

                    //Computer MyComputer = new Computer();
                    //imagefile = System.AppDomain.CurrentDomain.BaseDirectory;
                    //MyComputer.FileSystem.RenameFile(imagefile, image);//imagefile是所要重命名的文件的全路径，image是目标文件名
                    //image = Path.GetFileNameWithoutExtension(imagefile);// 没有扩展名的文件名
                    //FaceRecognition.Get_zjdz(imagefile);
                    string str = FaceRecognition.ImageToBase64(imagefile);

                    if (AMBaseAnnexesFileBLL.SaveEntity("", AMBaseAnnexesFileEntity) > 0)
                    {
                        if (SysUserBLL.Edit(user) > 0)
                        {
                            MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
                            var MesDevice = MesDeviceBLL.GetList_Deparemaent(D_Code.Text,"");
                            user = SysUserBLL.getDetail(PrimaryKey);

                            string url = "http://" + MesDevice[0].D_IP + ":8090/person/create";

                            string postData = "pass=12345678&personId=" + user.F_EnCode + "&faceId=" + ID + "&imgBase64" + str + "";

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
                            switch (cmbImage.Text)
                            {
                                case "照片1":
                                    AMBaseAnnexesFileBLL.DeleteEntity(user.F_Picture1);
                                    break;
                                case "照片2":
                                    AMBaseAnnexesFileBLL.DeleteEntity(user.F_Picture2);
                                    break;
                                case "照片3":
                                    AMBaseAnnexesFileBLL.DeleteEntity(user.F_Picture3);
                                    break;
                                case "照片4":
                                    AMBaseAnnexesFileBLL.DeleteEntity(user.F_Picture4);
                                    break;
                                case "照片5":
                                    AMBaseAnnexesFileBLL.DeleteEntity(user.F_Picture5);
                                    break;
                            }
                        }
                        else
                        {
                            untCommon.InfoMsg("上传失败！");
                        }
                    }
                    else
                    {
                        untCommon.InfoMsg("上传失败！");
                    }
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
                       
                        //string strPath = System.AppDomain.CurrentDomain.BaseDirectory + fileDialog.FileName;
                        //FileStream fs = new FileStream(strPath, FileMode.Open, FileAccess.Read);
                        //Byte[] mybyte = new byte[fs.Length];
                        //fs.Read(mybyte, 0, mybyte.Length);
                        //fs.Close();

                        //MemoryStream ms = new MemoryStream(mybyte);
                        //Bitmap myimge = new Bitmap(ms);
                        //pictureBox1.Image = myimge;

                        System.Drawing.Image imagename;
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

        private void cmbImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            SysUserBLL userbll = new SysUserBLL();
            SysUser user = userbll.getDetail(PrimaryKey);
            AMBaseAnnexesFileEntity AMBaseAnnexesFileEntity = new AMBaseAnnexesFileEntity();
            AMBaseAnnexesFileBLL AMBaseAnnexesFileBLL = new AMBaseAnnexesFileBLL();
            switch (cmbImage.Text)
            {
                case "照片1":
                    AMBaseAnnexesFileEntity = AMBaseAnnexesFileBLL.GetEntity(user.F_Picture1);
                    break;
                case "照片2":
                    AMBaseAnnexesFileEntity = AMBaseAnnexesFileBLL.GetEntity(user.F_Picture2);
                    break;
                case "照片3":
                    AMBaseAnnexesFileEntity = AMBaseAnnexesFileBLL.GetEntity(user.F_Picture3);
                    break;
                case "照片4":
                    AMBaseAnnexesFileEntity = AMBaseAnnexesFileBLL.GetEntity(user.F_Picture4);
                    break;
                case "照片5":
                    AMBaseAnnexesFileEntity = AMBaseAnnexesFileBLL.GetEntity(user.F_Picture5);
                    break;
            }
            //照片显示

            //string strPath = System.AppDomain.CurrentDomain.BaseDirectory + AMBaseAnnexesFileEntity.F_FilePath;
            //FileStream fs = new FileStream(strPath, FileMode.Open, FileAccess.Read);
            //Byte[] mybyte = new byte[fs.Length];
            //fs.Read(mybyte, 0, mybyte.Length);
            //fs.Close();

            //MemoryStream ms = new MemoryStream(mybyte);
            //Bitmap myimge = new Bitmap(ms);
            //pictureBox1.Image = myimge;
        }

        private void btnRegisterImage_Click(object sender, EventArgs e)
        {
            SysUser user = new SysUser();
            MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
            SysUserBLL SysUserBLL = new SysUserBLL();
            var MesDevice = MesDeviceBLL.GetList_Deparemaent(D_Code.Text,"");
            user = SysUserBLL.getDetail(PrimaryKey);
            string url = "http://" + MesDevice[0].D_IP + ":8090/person/takeImg";

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
