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
    public partial class frmDictionaryList : DockContent 
    {
        public frmMain frmMain { get; set; }

        public frmDictionaryList(frmMain _frmMain)
        {
            InitializeComponent();

            frmMain = _frmMain;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string type =cbType.SelectedIndex<0?"": cbType.SelectedItem.ToString();
            string key = txtKey.Text.Trim();
            loadData(type,key);
        }

        public void loadData(string type="",string key="")
        {
            try
            {
                SysDictionaryBLL companybll = new SysDictionaryBLL();
                var rows = companybll.loadData(type,key);

                if (rows == null || rows.Count < 1)
                {
                    untCommon.InfoMsg("没有任何数据！");
                    return;
                }

                dataGridView.DataSource = rows;
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("常规信息管理加载数据异常：" + ex.Message);
            }
        }

        private void frmDictionaryList_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addDictionary();
        }

        private void cmsAdd_Click(object sender, EventArgs e)
        {
            addDictionary();
        }

        private void addDictionary()
        {
            frmDictionaryEdit frmDictionaryEdit=new frmDictionaryEdit(this,frmMain.User,"",1);
            frmDictionaryEdit.ShowDialog();
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            updateDictionary();
        }

        private void cmsEdit_Click(object sender, EventArgs e)
        {
            updateDictionary();
        }

        private void updateDictionary()
        {
            if (dataGridView.SelectedRows.Count < 1)
                return;

            string D_Code = dataGridView.SelectedRows[0].Cells["D_Code"].Value.ToString();
            frmDictionaryEdit frmDictionaryEdit = new frmDictionaryEdit(this, frmMain.User, D_Code, 2);
            frmDictionaryEdit.ShowDialog();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            displayDictionary();
        }

        private void cmsDetail_Click(object sender, EventArgs e)
        {
            displayDictionary();
        }
        private void displayDictionary()
        {
            if (dataGridView.SelectedRows.Count < 1)
                return;

            string D_Code = dataGridView.SelectedRows[0].Cells["D_Code"].Value.ToString();
            frmDictionaryEdit frmDictionaryEdit = new frmDictionaryEdit(this, frmMain.User, D_Code, 3);
            frmDictionaryEdit.ShowDialog();
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteDictionary();
        }

        private void cmsDelete_Click(object sender, EventArgs e)
        {
            deleteDictionary();
        }

        private void deleteDictionary()
        {
            try
            {
                if (dataGridView.SelectedRows.Count < 1)
                    return;

                string D_Code = dataGridView.SelectedRows[0].Cells["D_Code"].Value.ToString();

                if (untCommon.QuestionMsg("您确定删除[" + D_Code + "]该项吗？"))
                {
                    SysDictionaryBLL companybll = new SysDictionaryBLL();
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
                untCommon.ErrorMsg("常规信息管理删除数据异常：" + ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


    }
}
