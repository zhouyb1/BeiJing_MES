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
using WeifenLuo.WinFormsUI.Docking;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Business.System;

namespace DesktopApp
{
    public partial class frmUserList : DockContent  
    {
        public frmMain frmMain { get; set; }

        public frmUserList(frmMain _frmMain)
        {
            InitializeComponent();

            frmMain = _frmMain;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string key = txtKey.Text.Trim();
            loadData(key);
        }

        public void loadData(string key="")
        {
            try
            {
                SysUserBLL companybll = new SysUserBLL();
                var rows = companybll.loadData(key);

                if (rows == null || rows.Rows.Count < 1)
                {
                    untCommon.InfoMsg("没有任何数据！");
                    return;
                }

                dataGridView.DataSource = rows;
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("用户管理加载数据异常："+ex.Message);
            }
        }

        private void frmUserList_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            loadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addUser();
        }

        private void cmsAdd_Click(object sender, EventArgs e)
        {
            addUser();
        }

        private void addUser()
        {
            frmUserEdit frmUserEdit=new frmUserEdit(this,frmMain.User,"",1);
            frmUserEdit.ShowDialog();
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            updateUser();
        }

        private void cmsEdit_Click(object sender, EventArgs e)
        {
            updateUser();
        }

        private void updateUser()
        {
            if (dataGridView.SelectedRows.Count < 1)
                return;
            string U_Code = dataGridView.SelectedRows[0].Cells["F_Account"].Value.ToString();
            frmUserEdit frmUserEdit = new frmUserEdit(this, frmMain.User, U_Code, 2);
            frmUserEdit.ShowDialog();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            displayUser();
        }

        private void cmsDetail_Click(object sender, EventArgs e)
        {
            displayUser();
        }

        private void displayUser()
        {
            if (dataGridView.SelectedRows.Count < 1)
                return;

            string U_Code = dataGridView.SelectedRows[0].Cells["F_Account"].Value.ToString();
            frmUserEdit frmUserEdit = new frmUserEdit(this, frmMain.User, U_Code, 3);
            frmUserEdit.ShowDialog();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteUser();
        }

        private void cmsDelete_Click(object sender, EventArgs e)
        {
            deleteUser();
        }

        private void deleteUser()
        {
            try
            {
                if (dataGridView.SelectedRows.Count < 1)
                    return;

                string U_Code = dataGridView.SelectedRows[0].Cells["F_Account"].Value.ToString();

                if (untCommon.QuestionMsg("您确定删除[" + U_Code + "]该项吗？"))
                {
                    SysUserBLL companybll = new SysUserBLL();
                    if (companybll.Delete(U_Code) > 0)
                    {
                        untCommon.InfoMsg("删除成功！");
                        loadData();
                    }
                    else
                    {
                        untCommon.InfoMsg("删除失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("用户管理删除数据异常："+ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnFaceRecognition_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count < 1)
                return;
            string F_Account = dataGridView.SelectedRows[0].Cells["F_Account"].Value.ToString();

            frmFaceRecognitionRegistration frmFaceRecognitionRegistration = new frmFaceRecognitionRegistration( F_Account,frmMain.User);
            frmFaceRecognitionRegistration.ShowDialog();
        }


    }
}
