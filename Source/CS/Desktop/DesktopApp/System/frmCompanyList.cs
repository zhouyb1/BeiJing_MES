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
    public partial class frmCompanyList : DockContent 
    {
        public frmMain frmMain { get; set; }

        public frmCompanyList(frmMain _frmMain)
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
                SysCompanyBLL companybll = new SysCompanyBLL();
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
                untCommon.ErrorMsg("公司管理加载数据异常："+ex.Message);
            }
        }

        private void frmCompanyList_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addCompany();
        }

        private void cmsAdd_Click(object sender, EventArgs e)
        {
            addCompany();
        }

        private void addCompany()
        {
            frmCompanyEdit frmCompanyEdit=new frmCompanyEdit(this,frmMain.User,"",1);
            frmCompanyEdit.ShowDialog();
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            updateCompany();
        }

        private void cmsEdit_Click(object sender, EventArgs e)
        {
            updateCompany();
        }

        private void updateCompany()
        {
            if (dataGridView.SelectedRows.Count < 1)
                return;

            string C_Code = dataGridView.SelectedRows[0].Cells["C_Code"].Value.ToString();
            frmCompanyEdit frmCompanyEdit = new frmCompanyEdit(this, frmMain.User, C_Code, 2);
            frmCompanyEdit.ShowDialog();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            displayCompany();
        }

        private void cmsDetail_Click(object sender, EventArgs e)
        {
            displayCompany();
        }
        private void displayCompany()
        {
            if (dataGridView.SelectedRows.Count < 1)
                return;

            string C_Code = dataGridView.SelectedRows[0].Cells["C_Code"].Value.ToString();
            frmCompanyEdit frmCompanyEdit = new frmCompanyEdit(this, frmMain.User, C_Code, 3);
            frmCompanyEdit.ShowDialog();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteCompany();
        }

        private void cmsDelete_Click(object sender, EventArgs e)
        {
            deleteCompany();
        }

        private void deleteCompany()
        {
            try
            {
                if (dataGridView.SelectedRows.Count < 1)
                    return;

                string C_Code = dataGridView.SelectedRows[0].Cells["C_Code"].Value.ToString();

                if (untCommon.QuestionMsg("您确定删除[" + C_Code + "]该项吗？"))
                {
                    SysCompanyBLL companybll = new SysCompanyBLL();
                    if (companybll.Delete(C_Code) > 0)
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
               untCommon.ErrorMsg("公司管理删除数据异常："+ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void panTop_Paint(object sender, PaintEventArgs e)
        {

        }


    }
}
