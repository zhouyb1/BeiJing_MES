using Business.System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmInventory : DockContent
    {
        public frmInventory()
        {
            InitializeComponent();
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
            MesStockBLL StockBLL = new MesStockBLL();
            var Stock_rows = StockBLL.GetData(" where S_Kind = '4'");
            for (int i = 0; i < Stock_rows.Count; i++)
            {
                cmbStock.Items.Add(Stock_rows[i].S_Code);
                cmbStockName.Items.Add(Stock_rows[i].S_Name);
            }
            if (cmbStock.Items.Contains(Globels.strStockCode))
            {
                cmbStock.Text = Globels.strStockCode;
            }
            listView1.Items.Clear();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            MesInventoryBLL InventoryBLL = new MesInventoryBLL();
            var row2 = InventoryBLL.GetData("where I_StockCode = '" + cmbStock.Text + "' and I_Qty > 0 ");
            this.listView1.Items.Clear();
            Thread.Sleep(100);
            this.listView1.BeginUpdate();
            int nLen = row2.Count;
            for (int i = 0; i < nLen; i++)
            {
                ListViewItem lvi = new ListViewItem(cmbStock.Text);
                lvi.SubItems.Add(cmbStockName.Text);
                lvi.SubItems.Add(row2[i].I_GoodsCode);
                lvi.SubItems.Add(row2[i].I_GoodsName);
                lvi.SubItems.Add(row2[i].I_Batch);
                lvi.SubItems.Add(row2[i].I_Qty.ToString());
                lvi.SubItems.Add(row2[i].I_Unit);
                this.listView1.Items.Add(lvi);
            }

            this.listView1.EndUpdate();
        }

        private void cmbStockName_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesStockBLL StockBLL = new MesStockBLL();
            var row = StockBLL.GetData(" where S_Name = '" + cmbStockName.Text + "'");
            cmbStock.Text = row[0].S_Code;
        }

        private void cmbStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesStockBLL StockBLL = new MesStockBLL();
            var row = StockBLL.GetData(" where S_Code = '" + cmbStock.Text + "'");
            cmbStockName.Text = row[0].S_Name;
        }
    }
}
