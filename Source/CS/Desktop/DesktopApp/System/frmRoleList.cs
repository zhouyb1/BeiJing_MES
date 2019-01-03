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

namespace DesktopApp
{
    public partial class frmRoleList : DockContent 
    {
        public frmMain frmMain { get; set; }

        public frmRoleList(frmMain _frmMain)
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
                SysRoleBLL companybll = new SysRoleBLL();
                var rows = companybll.loadData(key);

                if (rows == null || rows.Count < 1)
                {
                    untCommon.InfoMsg("没有任何数据！");
                    return;
                }

                dataGridView.DataSource = rows;
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("角色管理加载数据异常："+ex.Message);
            }
        }

        private void frmRoleList_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addRole();
        }

        private void cmsAdd_Click(object sender, EventArgs e)
        {
            addRole();
        }

        private void addRole()
        {
            frmRoleEdit frmRoleEdit=new frmRoleEdit(this,frmMain.User,"",1);
            frmRoleEdit.ShowDialog();
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            updateRole();
        }

        private void cmsEdit_Click(object sender, EventArgs e)
        {
            updateRole();
        }

        private void updateRole()
        {
            if (dataGridView.SelectedRows.Count < 1)
                return;

            string R_Code = dataGridView.SelectedRows[0].Cells["R_Code"].Value.ToString();
            frmRoleEdit frmRoleEdit = new frmRoleEdit(this, frmMain.User, R_Code, 2);
            frmRoleEdit.ShowDialog();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            displayRole();
        }

        private void cmsDetail_Click(object sender, EventArgs e)
        {
            displayRole();
        }
        private void displayRole()
        {
            if (dataGridView.SelectedRows.Count < 1)
                return;

            string R_Code = dataGridView.SelectedRows[0].Cells["R_Code"].Value.ToString();
            frmRoleEdit frmRoleEdit = new frmRoleEdit(this, frmMain.User, R_Code, 3);
            frmRoleEdit.ShowDialog();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteRole();
        }

        private void cmsDelete_Click(object sender, EventArgs e)
        {
            deleteRole();
        }

        private void deleteRole()
        {
            try
            {
                if (dataGridView.SelectedRows.Count < 1)
                    return;

                string R_Code = dataGridView.SelectedRows[0].Cells["R_Code"].Value.ToString();

                if (untCommon.QuestionMsg("您确定删除[" + R_Code + "]该项吗？"))
                {
                    SysRoleBLL companybll = new SysRoleBLL();
                    if (companybll.Delete(R_Code) > 0)
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
                untCommon.ErrorMsg("角色管理删除数据异常："+ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


    }
}
