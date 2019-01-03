using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;
using Business.System;
using Common;
using Model;
using Tools;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmInspectList : DockContent
    {
        private StringBuilder sqlwhere = new StringBuilder();

        public frmMain frmMain { get; set; }

        public frmInspectList(frmMain _frmMain)
        {
            InitializeComponent();

            frmMain = _frmMain;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            sqlwhere.Clear();

            if (!string.IsNullOrEmpty(txtKey.Text.Trim()))
            {
                sqlwhere.Append(string.Format(@" AND (Base_Equipment.E_Code='{0}' OR Base_Equipment.E_BoxCode='{0}')", txtKey.Text));
            }

            if (cbCity.SelectedIndex >= 0)
            {
                string city = cbCity.SelectedValue.ToString();

                sqlwhere.Append(string.Format(" AND (Base_Equipment.E_City = '{0}')", city));
            }


            string stardate = dateTimePicker1.Value.ToString("yyyy-MM-dd") + " 00:00:00.000";
            string enddate = dateTimePicker2.Value.ToString("yyyy-MM-dd") + " 23:59:59.999";

            sqlwhere.Append(string.Format(" AND (Base_Inspect.I_Date>='{0}' AND Base_Inspect.I_Date<='{1}')", stardate, enddate));

            Refresh();
        }


        private void loadDropdown()
        {
            AreaBLL areabll = new AreaBLL();
            var rows = areabll.loadData();

            //街道办
            var E_City_Datas = rows.Where(r => r.A_Parent == "440106").ToList();
            cbCity.DataSource = E_City_Datas;
            cbCity.ValueMember = "A_Code";
            cbCity.DisplayMember = "A_Name";
            cbCity.SelectedIndex = -1;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


        private void frmInspectList_Load(object sender, EventArgs e)
        {
            loadDropdown();

            this.dateTimePicker1.Value = DateTime.Now.AddDays(-7);

            //绑定分页控件事件
            pagerControl.OnPageChanged += pagerControl_OnPageChanged;
        }

        /// <summary>
        /// 页面改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pagerControl_OnPageChanged(object sender, EventArgs e)
        {
            Refresh();
        }


        /// <summary>
        /// 刷新数据
        /// </summary>
        private void Refresh()
        {
            try
            {
                InspectBLL inspectbll = new InspectBLL();

                int count = inspectbll.getRowCount(sqlwhere.ToString()); //获得总行数
                pagerControl.DrawControl(count);

                int star = (pagerControl.PageSize*pagerControl.PageIndex) - pagerControl.PageSize; //开始行
                int end = pagerControl.PageSize*pagerControl.PageIndex; //结束行

                var rows = inspectbll.getPagerData(star, end, sqlwhere.ToString()); //加载数据

                if (rows == null || rows.Count < 1)
                {
                    untCommon.InfoMsg("没有任何数据！");
                    return;
                }

                dataGridView.DataSource = rows;
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("巡检记录查询加载数据异常：" + ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.RowCount < 1)
                    return;

                //保存文件
                string saveFileName = "";
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xls";
                saveDialog.Filter = "Excel文件|*.xls";
                saveDialog.FileName = "巡检记录";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    saveFileName = saveDialog.FileName;
                    NPOIExcel excel = new NPOIExcel();

                    bool bl=excel.SaveToExcelNew(saveFileName, dataGridView);

                    if (bl)
                    {
                        untCommon.InfoMsg("导出成功！");
                    }
                    else
                    {
                        untCommon.InfoMsg("导出失败！");
                    }
                }


               
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("巡检记录导出异常：" + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.SelectedRows.Count < 1)
                    return;

                string E_ID = dataGridView.SelectedRows[0].Cells["E_ID"].Value.ToString();
                string E_Code = dataGridView.SelectedRows[0].Cells["E_Code"].Value.ToString();

                if (untCommon.QuestionMsg("您确定删除[" + E_Code + "]该项吗？"))
                {
                    InspectBLL inspectbll = new InspectBLL();
                    if (inspectbll.Delete(E_ID) > 0)
                    {
                        untCommon.InfoMsg("删除成功！");
                        Refresh();
                    }
                    else
                    {
                        untCommon.InfoMsg("删除失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("设备管理删除数据异常：" + ex.Message);
            }
        }
    }
}
