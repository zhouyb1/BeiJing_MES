using Business.System;
using Model;
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
    public partial class frmInWorkShopList : DockContent
    {
        public frmMain frmMain { get; set; }
        //decimal Period; //保质期
        private SysUser User;

        public frmInWorkShopList(frmMain _frmMain, SysUser _User)
        {
            InitializeComponent();
            frmMain = _frmMain;
            User = _User;
            //comKind.SelectedIndex = 0;//设置显示的item索引
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void frmInWorkShop_Load(object sender, EventArgs e)
        {


            MesWorkShopBLL WorkShopBLL = new MesWorkShopBLL();
            var rows = WorkShopBLL.GetList();
            for (int i = 0; i < rows.Count; i++)
            {
                cmbWorkShop.Items.Add(rows[i].W_Code);
            }

            MesStockBLL StockBLL = new MesStockBLL();
            var Stock_rows = StockBLL.GetList();
            for (int i = 0; i < Stock_rows.Count; i++)
            {
                cmbStock.Items.Add(Stock_rows[i].S_Code);
            }

            MesRecordBLL RecordBLL = new MesRecordBLL();
            var Record_rows = RecordBLL.GetList();
            for (int i = 0; i < Record_rows.Count; i++)
            {
                cmbRecord.Items.Add(Record_rows[i].R_Record);
            }

            //MesBasketBLL BasketBLL = new MesBasketBLL();
            //var Basket_rows = BasketBLL.GetList();
            //for (int i = 0; i < Basket_rows.Count; i++)
            //{
            //    cmbBasket.Items.Add(Record_rows[i].R_Record);
            //}

            //MesInventoryBLL InventoryBLL = new MesInventoryBLL();
            //var Goods_rows = InventoryBLL.GetList();
            //for (int i = 0; i < Goods_rows.Count; i++)
            //{
            //    cmbStock.Items.Add(Goods_rows[i].G_Code);
            //}
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cmbCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
    }
}
