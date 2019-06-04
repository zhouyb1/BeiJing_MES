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
            Mes_InWorkShopTempEntity InWorkShopTempEntity = new Mes_InWorkShopTempEntity();
            InWorkShopTempEntity.I_StockCode = cmbStock.Text;
            InWorkShopTempEntity.I_StockName = txtStockName.Text;
            InWorkShopTempEntity.I_WorkShop = cmbWorkShop.Text;
            InWorkShopTempEntity.I_WorkShopName = txtWorkShopName.Text;
            InWorkShopTempEntity.I_OrderNo = comOrderNo.Text;
            InWorkShopTempEntity.I_Status = 1;
            InWorkShopTempEntity.I_CreateBy = "";
            InWorkShopTempEntity.I_CreateDate = DateTime.Now;
            InWorkShopTempEntity.I_GoodsCode = txtCode.Text;
            InWorkShopTempEntity.I_GoodsName = txtName.Text;
            InWorkShopTempEntity.I_Unit = "";
            InWorkShopTempEntity.I_Qty = Convert.ToDecimal(txtQty.Text);
            InWorkShopTempEntity.I_Batch = txtPc.Text;
            InWorkShopTempEntity.I_Remark = "";
            InWorkShopTempEntity.I_Barcode = txtBarcode.Text;
            InWorkShopTempEntity.I_Price = Convert.ToDecimal(txtPrice.Text);
            InWorkShopTempEntity.I_Record = cmbRecord.Text;

            Mes_InWorkShopTempBLL InWorkShopTempBLL = new Mes_InWorkShopTempBLL();


            if (InWorkShopTempBLL.SaveEntity("", InWorkShopTempEntity) > 0)
                {
                    untCommon.InfoMsg("添加成功！");
                    //frmParent.loadData();
                }
                else
                {
                    untCommon.InfoMsg("添加失败！");
                }
            
        }

        private void frmInWorkShop_Load(object sender, EventArgs e)
        {
            MesProductOrderHeadBLL ProductOrderHeadBLL = new MesProductOrderHeadBLL();
            var row_ProductOrder = ProductOrderHeadBLL.GetListAll();
            for (int i = 0; i < row_ProductOrder.Count; i++)
            {
               //cmb  .Items.Add(row_ProductOrder[i].W_Code);
                comOrderNo.Items.Add(row_ProductOrder[i].P_OrderNo);
            }

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

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    string strBarcode = txtBarcode.Text;
                    string[] strTemp = strBarcode.Split('&');
                    txtCode.Text = strTemp[0].ToString();
                    txtPc.Text = strTemp[1].ToString();
                    txtQty.Text = strTemp[2].ToString();

                    MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                    var Goods_rows = GoodsBLL.GetList(strTemp[0].ToString(), "");
                    int nLen = Goods_rows.Count;
                    if (nLen > 0)
                    {
                        txtName.Text = Goods_rows[0].G_Name;
                        txtPrice.Text = Goods_rows[0].G_Price.ToString();

                    }
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void cmbWorkShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesWorkShopBLL WorkShopBLL = new MesWorkShopBLL();
            var row = WorkShopBLL.GetData("where W_Code = '"+ cmbWorkShop.Text +"'");
            txtWorkShopName.Text = row[0].W_Name;
        }

        private void cmbStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesStockBLL StockBLL = new MesStockBLL();
            var row = StockBLL.GetData("where S_Code = '" + cmbStock.Text + "'");
            txtStockName.Text = row[0].S_Name;
        }

        private void cmbRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mes_ProceBLL ProceBLL = new Mes_ProceBLL();
            var row = ProceBLL.GetList_Proce("where P_RecordCode = '" + cmbRecord.Text + "'");
            for (int i = 0; i < row.Count; i++)
            {
                
                cmbProce.Items.Add(row[i].P_ProNo);
            }

            MesRecordBLL RecordBLL = new MesRecordBLL();
            var Record_rows = RecordBLL.GetData(" where R_Record = '" + cmbRecord.Text + "'");
            txtRecordName.Text = Record_rows[0].R_Name;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Mes_InWorkShopTempBLL InWorkShopTempBLL = new Mes_InWorkShopTempBLL();
            var rows = InWorkShopTempBLL.GetList_InWorkShopTemp("where I_StockCode = '" + cmbStock.Text + "' and I_WorkShop = '" + cmbWorkShop.Text + "' and I_OrderNo = '" + comOrderNo.Text + "'");
            if (rows == null || rows.Count < 1)
            {
                untCommon.InfoMsg("没有任何数据！");
                return;
            }
            dataGridView1.DataSource = rows;
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否要完工?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    Mes_InWorkShopTempBLL InWorkShopTempBLL = new Mes_InWorkShopTempBLL();
                    var rows = InWorkShopTempBLL.GetList_InWorkShopTemp("where I_StockCode = '" + cmbStock.Text + "' and I_WorkShop = '" + cmbWorkShop.Text + "' and I_OrderNo = '" + comOrderNo.Text + "'");
                    if (rows == null || rows.Count < 1)
                    {
                        untCommon.InfoMsg("没有任何数据！");
                        return;
                    }

                    Mes_InWorkShopHeadBLL InWorkShopHeadBLL = new Mes_InWorkShopHeadBLL();
                    Mes_InWorkShopDetailBLL InWorkShopDetailBLL = new Mes_InWorkShopDetailBLL();
                    Mes_InWorkShopHeadEntity InWorkShopHeadEntity = new Mes_InWorkShopHeadEntity();
                    Mes_InWorkShopDetailEntity InWorkShopDetailEntity = new Mes_InWorkShopDetailEntity();

                    string strIn_No = "";

                    var rowsHead = InWorkShopHeadBLL.GetList_InWorkShopHead("where 1 = 1 order by I_InNo DESC");
                    if (rowsHead == null || rowsHead.Count < 1)
                    {
                        strIn_No = "IW" + DateTime.Now.ToString("yyyyMMdd") + "0001";
                    }
                    else
                    {
                        string strDate = rowsHead[0].I_InNo.Substring(2, 8);
                        if (strDate == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            string strList = rowsHead[0].I_InNo.Substring(10, 4);
                            int nList = Convert.ToInt32(strList) + 1;
                            strIn_No = "IW" + DateTime.Now.ToString("yyyyMMdd") + nList.ToString().PadLeft(4, '0');
                        }
                        else
                        {
                            strIn_No = "IW" + DateTime.Now.ToString("yyyyMMdd") + "0001";
                        }

                    }

                    InWorkShopHeadEntity.I_InNo = strIn_No;
                    InWorkShopHeadEntity.I_OrderNo = comOrderNo.Text;
                    InWorkShopHeadEntity.I_StockCode = cmbStock.Text;
                    InWorkShopHeadEntity.I_StockName = txtStockName.Text;
                    InWorkShopHeadEntity.I_CreateBy = "";
                    InWorkShopHeadEntity.I_CreateDate = DateTime.Now;
                    InWorkShopHeadEntity.I_OrderDate = "";
                    InWorkShopHeadEntity.I_Remark = "";
                    InWorkShopHeadEntity.I_Status = 1;
                    InWorkShopHeadEntity.I_WorkShop = cmbWorkShop.Text;



                    int nRow = InWorkShopHeadBLL.SaveEntity("", InWorkShopHeadEntity);

                    for (int i = 0; i < rows.Count; i++)
                    {
                        InWorkShopDetailEntity.I_GoodsCode = rows[i].I_GoodsCode;
                        InWorkShopDetailEntity.I_GoodsName = rows[i].I_GoodsName;
                        InWorkShopDetailEntity.I_InNo = strIn_No;
                        InWorkShopDetailEntity.I_Price = rows[i].I_Price;
                        InWorkShopDetailEntity.I_Qty = rows[i].I_Qty;
                        InWorkShopDetailEntity.I_Remark = rows[i].I_Remark;
                        InWorkShopDetailEntity.I_Unit = rows[i].I_Unit;
                        InWorkShopDetailEntity.I_Batch = rows[i].I_Batch;

                        nRow = InWorkShopDetailBLL.SaveEntity("", InWorkShopDetailEntity);

                    }

                    //更改临时数据状态

                    MessageBox.Show("保存成功");



                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
        }

        private void cmbProce_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mes_ProceBLL ProceBLL = new Mes_ProceBLL();
            var row = ProceBLL.GetList_Proce("where P_RecordCode = '" + cmbRecord.Text + "' and P_ProNo = '" + cmbProce.Text + "'");
            txtProceName.Text = row[0].P_ProName;
            
        }
        

        

        
    }
}
