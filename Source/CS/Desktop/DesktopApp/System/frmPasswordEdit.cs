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

namespace DesktopApp
{
    public partial class frmPasswordEdit : Form
    {
        private frmMain frmMain;
        public frmPasswordEdit(frmMain _frmMain)
        {
            InitializeComponent();
            frmMain = _frmMain;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOldPwd.Text))
            {
                untCommon.InfoMsg("旧密码不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(txtNewPwd.Text))
            {
                untCommon.InfoMsg("新密码不能为空！");
                return;
            }
            if (string.IsNullOrEmpty(txtAgainPwd.Text))
            {
                untCommon.InfoMsg("确认密码不能为空！");
                return;
            }

            if (frmMain.User.U_Pwd != txtOldPwd.Text)
            {
                untCommon.InfoMsg("旧密码不匹配！");
                return;
            }

            if (txtAgainPwd.Text != txtNewPwd.Text)
            {
                untCommon.InfoMsg("两次输入密码不一致！");
                return;
            }

            frmMain.User.U_Pwd = txtNewPwd.Text;
            try
            {

                SysUserBLL userbll = new SysUserBLL();
                if (userbll.EditPassword(frmMain.User) > 0)
                {
                    untCommon.InfoMsg("密码修改成功！");
                }
                else
                {
                    untCommon.InfoMsg("密码修改失败！");
                }
            }
            catch (Exception ex)
            {
                 untCommon.InfoMsg("密码修改异常："+ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
