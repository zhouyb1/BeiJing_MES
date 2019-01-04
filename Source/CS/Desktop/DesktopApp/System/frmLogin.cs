using System;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using Business;
using Model;
using Tools;

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
                    this.skin.SkinFile = config.SkinFile; //ʹ�õ�����Ƥ���ؼ�
                    this.skin.SkinAllForm = true;
                    this.skin.Active = true;
                }
                else
                {
                    this.skin.SkinFile = null;  //ʹ�õ�����Ƥ���ؼ�
                    this.skin.SkinAllForm = true;
                    this.skin.Active = false;
                }

            }
            else
            {
                this.skin.SkinFile = null; ; //ʹ�õ�����Ƥ���ؼ�
                this.skin.SkinAllForm = true;
                this.skin.Active = false;
            }

        }

        public void setSkin(string skinFile)
        {
            if (!string.IsNullOrEmpty(skinFile))
            {
                this.skin.SkinFile = skinFile; //ʹ�õ�����Ƥ���ؼ�
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
        /// �û���¼
        /// </summary>
        private void userLogin()
        {
            if (checkInput())
            {
                try
                {
                    SysUserBLL sysUserBll = new SysUserBLL();
                    SysUser userEntity = sysUserBll.Login(this.txtUser.Text, txtPass.Text);

                    if (!userEntity.LoginOk)//��¼ʧ��
                    {
                        untCommon.ErrorMsg("��¼ʧ��:" + userEntity.LoginMsg);
                    }
                    else
                    {
                        if (cbRemember.Checked)
                        {
                            LoginInfo loginInfo = new LoginInfo();
                            loginInfo.Number = this.txtUser.Text;
                            loginInfo.Pwd = txtPass.Text;
                            loginInfo.Remember = true;

                            /*ɾ���ɵ������ļ�*/
                            if (File.Exists(Application.StartupPath + @"\\loginConfig.lsf"))
                                File.Delete(Application.StartupPath + @"\\loginConfig.lsf");

                            KBFile kbFile = new KBFile();
                            kbFile.path = Application.StartupPath + @"\\loginConfig.lsf";
                            if (kbFile.writeObjectToFile(loginInfo) < 1)
                            {
                                MessageBox.Show("�����½�����ļ�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            /*ɾ���ɵ������ļ�*/
                            if (File.Exists(Application.StartupPath + @"\\loginConfig.lsf"))
                                File.Delete(Application.StartupPath + @"\\loginConfig.lsf");
                        }

                        frmMain main = new frmMain(this);
                        main.User = userEntity;

                        main.Show();
                        this.Hide();
                    }
                }
                catch (Exception ex)
                {
                    untCommon.ErrorMsg("�û���¼�쳣��"+ex.Message);
                }
            }
        }

        /// <summary>
        /// ��������Ƿ�Ϸ�
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            if (this.txtUser.Text.Trim() == "")
            {
                untCommon.InfoMsg("�������û���");
                return false;

            }

            if (this.txtPass.Text == "")
            {
                untCommon.InfoMsg("������������");
                return false;

            }
            return true;
        }

        private void Init()
        {
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
                MessageBox.Show("�û���½ģ������쳣��" + ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

    }
}