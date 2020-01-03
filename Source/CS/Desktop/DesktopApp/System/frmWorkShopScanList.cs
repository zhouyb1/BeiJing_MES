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
        //Double Period; //保质期
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
                    if (strBarcode.IndexOf('*') > 0)
                    {
                        string[] strTemp = strBarcode.Split('*');
                        cmbGoodsCode.Text = strTemp[0].ToString();
                        cmbPc.Text = strTemp[1].ToString();
                        txtQty.Text = strTemp[2].ToString();

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
                    else
                    {
                        string[] strTemp = strBarcode.Split(',');

                        cmbGoodsCode.Text = Resolve(strTemp[0].ToString());
                        cmbPc.Text = Resolve(strTemp[1].ToString());
                        txtQty.Text = Resolve(strTemp[2].ToString());

                        MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                        var Goods_rows = GoodsBLL.GetList(cmbGoodsCode.Text, "");
                        //MessageBox.Show(Goods_rows.Count.ToString());
                        int nLen = Goods_rows.Count;
                        if (nLen > 0)
                        {
                            txtName.Text = Goods_rows[0].G_Name;
                            txtUnit.Text = Goods_rows[0].G_Unit;
                            txtPrice.Text = Goods_rows[0].G_Price.ToString();

                        }
                    }
                    //txtPrice.Text = strTemp[3].ToString();

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("请扫描正确的条码");
            }
        }

        private string Resolve(string strTemp)
        {
            try
            {
                string[] str = strTemp.Split(':');
                return str[1];
            }
            catch(Exception ex)
            {
                return "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if(MessageBox.Show("是否保存","",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                Mes_WorkShopScanBLL WorkShopScanBLL = new Mes_WorkShopScanBLL();
                Mes_WorkShopScanEntity WorkShopScanEntity = new Mes_WorkShopScanEntity();


                WorkShopScanEntity.W_GoodsName = txtName.Text;
                WorkShopScanEntity.W_WorkShop = Globels.strWorkShop;
                WorkShopScanEntity.W_RecordCode = Globels.strRecord;

                WorkShopScanEntity.W_GoodsCode = cmbGoodsCode.Text;
                WorkShopScanEntity.W_Batch = cmbPc.Text;
                WorkShopScanEntity.W_Price = Convert.ToDouble(txtPrice.Text);

                WorkShopScanEntity.W_Status = 1;
                WorkShopScanEntity.W_Qty = Convert.ToDouble(txtQty.Text);
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
            cmbGoodsCode.Items.Clear();
            MesInventoryBLL InventoryBLL = new MesInventoryBLL();
            var row2 = InventoryBLL.GetData("where I_StockCode = '" + Globels.strStockCode + "' and I_Qty > 0 ");
            for (int i = 0; i < row2.Count; i++)
            {
                cmbGoodsCode.Items.Add(row2[i].I_GoodsCode);

            }
            if (row2.Count == 1)
            {
                cmbGoodsCode.Text = row2[0].I_GoodsCode;
            }

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

        private void cmbGoodsCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbPc.Items.Clear();
                MesInventoryBLL InventoryBLL = new MesInventoryBLL();
                var row = InventoryBLL.GetData("where I_StockCode = '" + Globels.strStockCode + "' and I_GoodsCode = '" + cmbGoodsCode.Text + "'");
                for (int i = 0; i < row.Count; i++)
                {
                    cmbPc.Items.Add(row[i].I_Batch);

                }
                if (row.Count == 1)
                {
                    cmbPc.Text = row[0].I_Batch;
                }



                MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                var Goods_rows = GoodsBLL.GetList(cmbGoodsCode.Text, "");
                int nLen = Goods_rows.Count;
                if (nLen > 0)
                {
                    txtName.Text = Goods_rows[0].G_Name;
                    txtPrice.Text = Goods_rows[0].G_Price.ToString();
                    txtUnit.Text = Goods_rows[0].G_Unit.ToString();
                    //int nKind = Goods_rows[0].G_Kind;
                    //if (nKind == 1)
                    //{
                    //    label17.Visible = true;
                    //    cmbSupplyName.Visible = true;


                    //}
                    //else
                    //{
                    //    ;
                    //}

                }


            }
            catch (Exception ex)
            {
                //lblTS.Text = "ex.ToString()";
            }
        }
    }
}
