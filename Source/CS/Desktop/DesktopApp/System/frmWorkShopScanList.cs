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
    public partial class frmWorkShopScanList : DockContent
    {
        public frmMain frmMain { get; set; }
        //decimal Period; //保质期
        private SysUser User;

        public frmWorkShopScanList(frmMain _frmMain, SysUser _User)
        {
            InitializeComponent();
            frmMain = _frmMain;
            User = _User;
            txtBarcode.Focus();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    string strBarcode = txtBarcode.Text;
                    string[] strTemp = strBarcode.Split('*');
                    txtCode.Text = strTemp[0].ToString();
                    txtPc.Text = strTemp[1].ToString();
                    txtQty.Text = strTemp[2].ToString();
                    //txtPrice.Text = strTemp[3].ToString();

                    MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                    var Goods_rows = GoodsBLL.GetList(strTemp[0].ToString(), "");
                    int nLen = Goods_rows.Count;
                    if (nLen > 0)
                    {
                        txtName.Text = Goods_rows[0].G_Name;
                        txtUnit.Text = Goods_rows[0].G_Unit;
                        txtPrice.Text = Goods_rows[0].G_Price.ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if(MessageBox.Show("是否保存","",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                Mes_WorkShopScanBLL WorkShopScanBLL = new Mes_WorkShopScanBLL();
                Mes_WorkShopScanEntity WorkShopScanEntity = new Mes_WorkShopScanEntity();

                WorkShopScanEntity.W_OrderNo = Globels.strOrderNo;
                WorkShopScanEntity.W_WorkShopName = Globels.strWorkShopName;
                WorkShopScanEntity.W_RecordName = Globels.strRecordName;
                WorkShopScanEntity.W_ProceName = Globels.strProceName;
                WorkShopScanEntity.W_GoodsName = txtName.Text;
                WorkShopScanEntity.W_WorkShop = Globels.strWorkShop;
                WorkShopScanEntity.W_RecordCode = Globels.strRecord;
                WorkShopScanEntity.W_ProceCode = Globels.strProce;
                WorkShopScanEntity.W_GoodsCode = txtCode.Text;
                WorkShopScanEntity.W_Batch = txtPc.Text;
                WorkShopScanEntity.W_Price = Convert.ToDecimal(txtPrice.Text);
                WorkShopScanEntity.W_CreateBy = "";
                WorkShopScanEntity.W_CreateDate = DateTime.Now;
                WorkShopScanEntity.W_Status = 1;
                WorkShopScanEntity.W_Qty = Convert.ToDecimal(txtQty.Text);
                WorkShopScanEntity.W_Unit = txtUnit.Text;
                WorkShopScanEntity.W_Remark = "";
                int nCount = WorkShopScanBLL.SaveEntity("", WorkShopScanEntity);
                if(nCount > 0)
                {
                    MessageBox.Show("添加成功");
                    Init();
                }

            }
        }

        private void frmWorkShopScanList_Load(object sender, EventArgs e)
        {
            txtBarcode.Focus();
        }

        private void Init()
        {
            txtBarcode.Text = "";
            //txtCode.Text = "";
            txtName.Text = "";
            //txtPc.Text = "";
            txtPrice.Text = "";
            txtQty.Text = "";
            txtUnit.Text = "";

            txtBarcode.Focus();
        }
    }
}
