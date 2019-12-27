using Business.System;
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
    public partial class frmWorkShopInventory : DockContent
    {
        public frmWorkShopInventory()
        {
            InitializeComponent();
        }

        private void frmWorkShopInventory_Load(object sender, EventArgs e)
        {
            txtCode.Text = Globels.strWorkShop;
            txtName.Text = Globels.strWorkShopName;
            Search();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            string strSql = "select * from Mes_Barcode where B_WorkShopCode = '" + txtCode.Text + "' and B_Status = '1'";
            Mes_BarcodeBLL BarcodeBLL = new Mes_BarcodeBLL();
            var row = BarcodeBLL.GetList_Mes_Barcode(strSql);

            int nLen = row.Count;
            this.listView1.Items.Clear();
            this.listView1.BeginUpdate();
            for (int i = 0; i < nLen; i++)
            {
                ListViewItem lvi = new ListViewItem(row[i].B_Code);
                lvi.SubItems.Add(row[i].B_Name);
                lvi.SubItems.Add(row[i].B_Qty.ToString());
                
                this.listView1.Items.Add(lvi);
            }

            this.listView1.EndUpdate();
        }
    }
}
