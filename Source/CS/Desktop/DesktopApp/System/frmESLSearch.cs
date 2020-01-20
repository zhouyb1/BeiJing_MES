﻿using Business.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmESLSearch : DockContent
    {
        public frmESLSearch()
        {
            InitializeComponent();
        }

        private void frmESLSearch_Load(object sender, EventArgs e)
        {
            txtWorkShop.Text = Globels.strWorkShop;
            txtWorkShopName.Text = Globels.strWorkShopName;

        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime dt = dt1.AddDays(1);
            string strSql = "SELECT * FROM Mes_Barcode WHERE B_WorkShopCode = '" + Globels.strWorkShop + "' AND B_Ptime > '" + dt1 + "' and B_Ptime < '" + dt + "'";
            DataSet ds = new DataSet();
            Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
            ds = ConvertBLL.GetList(strSql);
            //cmbGoodsName.Items.Clear();
            listView1.Items.Clear();
            int nLen = ds.Tables[0].Rows.Count;
            this.listView1.BeginUpdate();
            for (int i = 0; i < nLen; i++)
            {
                
                
                    

                    ListViewItem lvi = new ListViewItem(ds.Tables[0].Rows[i]["B_Code"].ToString());
                    lvi.SubItems.Add(ds.Tables[0].Rows[i]["B_Name"].ToString());
                    lvi.SubItems.Add(ds.Tables[0].Rows[i]["B_Qty"].ToString());
                    if (ds.Tables[0].Rows[i]["B_Status"].ToString() == "1")
                    {
                        lvi.SubItems.Add("已生成");
                    }
                    else
                    {
                        lvi.SubItems.Add("已入库");
                    }


                    lvi.SubItems.Add(ds.Tables[0].Rows[0]["B_Remark"].ToString());
                    this.listView1.Items.Add(lvi);
                
            }
            this.listView1.EndUpdate();
        }
    }
}