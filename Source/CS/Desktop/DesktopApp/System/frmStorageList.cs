﻿using System;
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
using Business.System;

namespace DesktopApp
{
    public partial class frmStorageList : DockContent
    {
        public frmMain frmMain { get; set; }
        private int rowindex;//获得当前选中的行的索引
        MesMaterInHeadBLL MaterInHeadBLL = new MesMaterInHeadBLL();
        public frmStorageList(frmMain _frmMain)
        {
            InitializeComponent();

            frmMain = _frmMain;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = null;
            string MaterInNo = txtMaterInNo.Text.Trim();
            loadData(MaterInNo);
        }
        public void loadData(string keyValue = "")
        {
            try
            {
                var rows = MaterInHeadBLL.GetList(keyValue);

                if (rows == null || rows.Count < 1)
                {
                    untCommon.InfoMsg("该入库单没有任何数据！");
                    return;
                }
                dataGridView.DataSource = rows;
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("物料入库加载数据异常：" + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var rows = MaterInHeadBLL.GetListMax();
            frmStorageAdd frmStorageAdd = new frmStorageAdd(this, frmMain.User, GetSerialNumber(rows[0].M_CreateDate));
            frmStorageAdd.ShowDialog();
        }

        private void frmStorageMake_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            loadData();

        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (rowindex < 0)
            {
                return;
            }
            if (MaterInHeadBLL.GetList(dataGridView.Rows[rowindex].Cells["入库单号"].Value.ToString())[0].M_Status == 1)
            {
                frmStorageEdit frmStorageEdit = new frmStorageEdit(this, frmMain.User, dataGridView.Rows[rowindex].Cells["入库单号"].Value.ToString(), dataGridView.Rows[rowindex].Cells["生产订单号"].Value.ToString());
                frmStorageEdit.ShowDialog();
            }
            else
            {
                untCommon.InfoMsg("请选择正确的入库单！");
            }
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dataGridView.Rows[0].Cells[0].Value != null)
                {
                    if (e.Value != null)
                    {
                        //if (e.ColumnIndex == 3)
                        //{
                        //    string stringValue = e.Value.ToString();
                        //    switch (stringValue)
                        //    {
                        //        case "0": e.Value = "原材料";
                        //            break;
                        //        case "1": e.Value = "半成品";
                        //            break;
                        //        case "2": e.Value = "成品";
                        //            break;
                        //    }

                        //}
                        if (e.ColumnIndex == 6)
                        {
                            string stringValue = e.Value.ToString();
                            switch (stringValue)
                            {
                                case "-1": e.Value = "单据删除";
                                    break;
                                case "1": e.Value = "单据生成";
                                    break;
                                case "2": e.Value = "审核通过";
                                    break;
                                case "3": e.Value = "单据完成";
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("物料入库加载数据异常：" + ex.Message);
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //获得当前选中的行的索引 
            rowindex = e.RowIndex;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// 入库单据生成
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public string GetSerialNumber(DateTime serialNumber)
        {
            string str_serialNumber = serialNumber.ToString("yyyyMMdd");
            if (str_serialNumber != "0")
            {
                //如果数据库最大值流水号中日期和生成日期在同一天，则顺序号加1
                if (str_serialNumber == DateTime.Now.ToString("yyyyMMdd"))
                {
                    var rows = MaterInHeadBLL.GetListMax();
                    if (rows[0].M_MaterInNo != null)
                    {
                        string str = rows[0].M_MaterInNo;
                        str = str.Substring(str.Length - 4, 4);
                        //str = str.Remove(0, str.Length - 9);
                        int lastNumber = int.Parse(str);
                        lastNumber++;
                        return "I" + DateTime.Now.ToString("yyyyMMdd") + lastNumber.ToString("0000");
                    }
                }
            }
            return "I" + DateTime.Now.ToString("yyyyMMdd") + "0001";
        }

        private void txtMaterInNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}