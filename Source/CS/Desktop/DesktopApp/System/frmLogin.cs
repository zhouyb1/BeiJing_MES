using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using Business;
using Model;
using Tools;
using System.Diagnostics;
using System.ComponentModel;

namespace DesktopApp
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }




        private void frmLogin_Load(object sender, EventArgs e)
        {
            loadSkin();
            Init();
        }

        private void loadSkin()
        {


            string filePath = Application.StartupPath + @"\config.lsf";

            if (File.Exists(filePath))
            {
                KBFile kbFile = new KBFile();
                kbFile.path = filePath;
                SysConfig config = kbFile.readObjectFromFile() as SysConfig;

                if (!string.IsNullOrEmpty(config.SkinFile))
                {
                    this.skin.SkinFile = config.SkinFile; //使用第三方皮肤控件
                    this.skin.SkinAllForm = true;
                    this.skin.Active = true;
                }
                else
                {
                    this.skin.SkinFile = null;  //使用第三方皮肤控件
                    this.skin.SkinAllForm = true;
                    this.skin.Active = false;
                }

            }
            else
            {
                this.skin.SkinFile = null; ; //使用第三方皮肤控件
                this.skin.SkinAllForm = true;
                this.skin.Active = false;
            }

        }

        public void setSkin(string skinFile)
        {
            if (!string.IsNullOrEmpty(skinFile))
            {
                this.skin.SkinFile = skinFile; //使用第三方皮肤控件
                this.skin.SkinAllForm = true;
                this.skin.Active = true;
            }
            else
            {
                this.skin.SkinFile =  null;
                this.skin.SkinAllForm = true;
                this.skin.Active = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
           this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            userLogin();
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (char)13)
            {
                userLogin();
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        private void userLogin()
        {
            if (checkInput())
            {
                try
                {
                    SysUserBLL sysUserBll = new SysUserBLL();
                    SysUser userEntity = sysUserBll.Login(this.txtUser.Text, txtPass.Text);

                    if (!userEntity.LoginOk)//登录失败
                    {
                        untCommon.ErrorMsg("登录失败:" + userEntity.LoginMsg);
                    }
                    else
                    {
                        if (cbRemember.Checked)
                        {
                            LoginInfo loginInfo = new LoginInfo();
                            loginInfo.Number = this.txtUser.Text;
                            loginInfo.Pwd = txtPass.Text;
                            loginInfo.Remember = true;

                            /*删除旧的配置文件*/
                            if (File.Exists(Application.StartupPath + @"\\loginConfig.lsf"))
                                File.Delete(Application.StartupPath + @"\\loginConfig.lsf");

                            KBFile kbFile = new KBFile();
                            kbFile.path = Application.StartupPath + @"\\loginConfig.lsf";
                            if (kbFile.writeObjectToFile(loginInfo) < 1)
                            {
                                MessageBox.Show("保存登陆配置文件失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            /*删除旧的配置文件*/
                            if (File.Exists(Application.StartupPath + @"\\loginConfig.lsf"))
                                File.Delete(Application.StartupPath + @"\\loginConfig.lsf");
                        }

                        Globels.strUser = this.txtUser.Text;

                        frmMain main = new frmMain(this);
                        main.User = userEntity;

                        main.Show();
                        this.Hide();
                    }
                }
                catch (Exception ex)
                {
                    untCommon.ErrorMsg("用户登录异常："+ex.Message);
                }
            }
        }

        /// <summary>
        /// 检查输入是否合法
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            if (this.txtUser.Text.Trim() == "")
            {
                untCommon.InfoMsg("请输入用户名");
                return false;

            }

            if (this.txtPass.Text == "")
            {
                untCommon.InfoMsg("请输入用密码");
                return false;

            }
            return true;
        }


        private string Data(string strTemp)
        {
            try
            {
                string[] strCode = strTemp.Split(':');
                string strData = strCode[1].ToString();
                return strData;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void Init()
        {

            try
            {
                if (File.Exists(Application.StartupPath + @"\\WorkShop.txt"))
                {
                    using (StreamReader sr = File.OpenText(Application.StartupPath + @"\\WorkShop.txt"))
                    {
                        string strTemp = sr.ReadLine();
                        Globels.strWorkShop = Data(strTemp);
                        strTemp = sr.ReadLine();
                        Globels.strTeam = Data(strTemp);
                        strTemp = sr.ReadLine();
                        Globels.strStockCode = Data(strTemp);
                        strTemp = sr.ReadLine();
                        Globels.strProce = Data(strTemp);
                        strTemp = sr.ReadLine();
                        Globels.strCom = Data(strTemp);
                        sr.Close();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            try
            {
                if (File.Exists(Application.StartupPath + @"\\loginConfig.lsf"))
                {
                    KBFile kbFile = new KBFile();
                    kbFile.path = Application.StartupPath + @"\\loginConfig.lsf";
                    LoginInfo loginInfo = kbFile.readObjectFromFile() as LoginInfo;
                    txtUser.Text = loginInfo.Number;
                    txtPass.Text = loginInfo.Pwd;
                    cbRemember.Checked = loginInfo.Remember;

                    txtPass.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("用户登陆模块出现异常：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("更新大约需要一分钟时间,是否更新系统,退出系统?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string strPath = AppDomain.CurrentDomain.BaseDirectory;
                string yourfilepath = strPath + "\\AutoUpdate.exe";
                Process p = new Process();
                p.StartInfo.FileName = yourfilepath;
                try
                {
                    p.Start();
                    Process current = Process.GetCurrentProcess();
                    Process[] processes = Process.GetProcessesByName(current.ProcessName);
                    foreach (Process process in processes)
                    {
                        if (process.Id == current.Id)
                        {
                            process.Kill();
                        }
                    }
                    Application.Exit();
                }
                catch (Win32Exception err)
                {
                    MessageBox.Show(err.Message);
                }
            }
            else
            {

            }
        }

    }
}