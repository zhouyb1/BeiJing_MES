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
    public partial class frmDepartmentList : DockContent 
    {
        public frmMain frmMain { get; set; }

        public frmDepartmentList(frmMain _frmMain)
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
                SysDepartmentBLL companybll = new SysDepartmentBLL();
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
                untCommon.ErrorMsg("部门管理加载数据异常："+ex.Message);
            }
        }

        private void frmDepartmentList_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addDepartment();
        }

        private void cmsAdd_Click(object sender, EventArgs e)
        {
            addDepartment();
        }

        private void addDepartment()
        {
            frmDepartmentEdit frmDepartmentEdit=new frmDepartmentEdit(this,frmMain.User,"",1);
            frmDepartmentEdit.ShowDialog();
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            updateDepartment();
        }

        private void cmsEdit_Click(object sender, EventArgs e)
        {
            updateDepartment();
        }

        private void updateDepartment()
        {
            if (dataGridView.SelectedRows.Count < 1)
                return;

            string D_Code = dataGridView.SelectedRows[0].Cells["D_Code"].Value.ToString();
            frmDepartmentEdit frmDepartmentEdit = new frmDepartmentEdit(this, frmMain.User, D_Code, 2);
            frmDepartmentEdit.ShowDialog();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            displayDepartment();
        }

        private void cmsDetail_Click(object sender, EventArgs e)
        {
            displayDepartment();
        }
        private void displayDepartment()
        {
            if (dataGridView.SelectedRows.Count < 1)
                return;

            string D_Code = dataGridView.SelectedRows[0].Cells["D_Code"].Value.ToString();
            frmDepartmentEdit frmDepartmentEdit = new frmDepartmentEdit(this, frmMain.User, D_Code, 3);
            frmDepartmentEdit.ShowDialog();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteDepartment();
        }

        private void cmsDelete_Click(object sender, EventArgs e)
        {
            deleteDepartment();
        }

        private void deleteDepartment()
        {
            try
            {
                if (dataGridView.SelectedRows.Count < 1)
                    return;

                string D_Code = dataGridView.SelectedRows[0].Cells["D_Code"].Value.ToString();

                if (untCommon.QuestionMsg("您确定删除[" + D_Code + "]该项吗？"))
                {
                    SysDepartmentBLL companybll = new SysDepartmentBLL();
                    if (companybll.Delete(D_Code) > 0)
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
                untCommon.ErrorMsg("部门管理删除数据异常："+ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


    }
}
