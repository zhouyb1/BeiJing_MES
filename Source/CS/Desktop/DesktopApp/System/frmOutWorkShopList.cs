using Business.System;
using Model;
using Model.System;
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
    public partial class frmOutWorkShopList : DockContent
    {
        public frmMain frmMain { get; set; }
        //decimal Period; //保质期
        private SysUser User;

        public frmOutWorkShopList(frmMain _frmMain, SysUser _User)
        {
            InitializeComponent();
            frmMain = _frmMain;
            User = _User;
            //comKind.SelectedIndex = 0;//设置显示的item索引
        }

        private void frmOutWorkShopList_Load(object sender, EventArgs e)
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
        }

        private void cmbWorkShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesWorkShopBLL WorkShopBLL = new MesWorkShopBLL();
            var row = WorkShopBLL.GetData(" where W_Code = '" + cmbWorkShop.Text + "'");
            txtWorkShopName.Text = row[0].W_Name;
        }

        private void cmbStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesStockBLL StockBLL = new MesStockBLL();
            var row = StockBLL.GetData(" where S_Code = '" + cmbStock.Text + "'");
            txtStockName.Text = row[0].S_Name;
        }

        private void cmbRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mes_ProceBLL ProceBLL = new Mes_ProceBLL();
            var row = ProceBLL.GetList_Proce(" where P_RecordCode = '" + cmbRecord.Text + "'");
            for (int i = 0; i < row.Count; i++)
            {
                cmbProce.Items.Add(row[i].P_ProNo);
            }

            MesRecordBLL RecordBLL = new MesRecordBLL();
            var Record_rows = RecordBLL.GetData(" where R_Record = '" + cmbRecord.Text + "'");
            txtRecordName.Text = Record_rows[0].R_Name;
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Mes_OutWorkShopTempBLL OutWorkShopTempBLL = new Mes_OutWorkShopTempBLL();
            var rows = OutWorkShopTempBLL.GetList_OutWorkShopTemp("where O_StockCode = '" + cmbStock.Text + "' and O_WorkShop = '" + cmbWorkShop.Text + "' and O_OrderNo = '" + comOrderNo.Text + "'");
            if (rows == null || rows.Count < 1)
            {
                untCommon.InfoMsg("没有任何数据！");
                return;
            }
            dataGridView1.DataSource = rows;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("是否保存","",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                Mes_OutWorkShopTempEntity OutWorkShopTempEntity = new Mes_OutWorkShopTempEntity();
                OutWorkShopTempEntity.O_StockCode = cmbStock.Text;
                OutWorkShopTempEntity.O_StockName = txtStockName.Text;
                OutWorkShopTempEntity.O_WorkShop = cmbWorkShop.Text;
                OutWorkShopTempEntity.O_WorkShopName = txtWorkShopName.Text;
                OutWorkShopTempEntity.O_OrderNo = comOrderNo.Text;
                OutWorkShopTempEntity.O_Status = 1;
                OutWorkShopTempEntity.O_CreateBy = "";
                OutWorkShopTempEntity.O_CreateDate = DateTime.Now;
                OutWorkShopTempEntity.O_GoodsCode = txtCode.Text;
                OutWorkShopTempEntity.O_GoodsName = txtName.Text;
                OutWorkShopTempEntity.O_Unit = "";
                OutWorkShopTempEntity.O_Qty = Convert.ToDecimal(txtQty.Text);
                OutWorkShopTempEntity.O_Batch = txtPc.Text;
                OutWorkShopTempEntity.O_Remark = "";
                OutWorkShopTempEntity.O_Barcode = txtBarcode.Text;
                OutWorkShopTempEntity.O_Price = Convert.ToDecimal(txtPrice.Text);
                OutWorkShopTempEntity.O_Record = cmbRecord.Text;

                Mes_OutWorkShopTempBLL OutWorkShopTempBLL = new Mes_OutWorkShopTempBLL();


                if (OutWorkShopTempBLL.SaveEntity("", OutWorkShopTempEntity) > 0)
                {
                    untCommon.InfoMsg("添加成功！");
                    //frmParent.loadData();
                }
                else
                {
                    untCommon.InfoMsg("添加失败！");
                }
            }
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否要完工?", "温馨提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    Mes_OutWorkShopTempBLL OutWorkShopTempBLL = new Mes_OutWorkShopTempBLL();
                    var rows = OutWorkShopTempBLL.GetList_OutWorkShopTemp("where O_StockCode = '" + cmbStock.Text + "' and O_WorkShop = '" + cmbWorkShop.Text + "' and O_OrderNo = '" + comOrderNo.Text + "'");
                    if (rows == null || rows.Count < 1)
                    {
                        untCommon.InfoMsg("没有任何数据！");
                        return;
                    }

                    Mes_OutWorkShopHeadBLL OutWorkShopHeadBLL = new Mes_OutWorkShopHeadBLL();
                    Mes_OutWorkShopDetailBLL OutWorkShopDetailBLL = new Mes_OutWorkShopDetailBLL();
                    Mes_OutWorkShopHeadEntity OutWorkShopHeadEntity = new Mes_OutWorkShopHeadEntity();
                    Mes_OutWorkShopDetailEntity OutWorkShopDetailEntity = new Mes_OutWorkShopDetailEntity();

                    string strIn_No = "";

                    var rowsHead = OutWorkShopHeadBLL.GetList_OutWorkShopHead("where 1 = 1 order by O_OutNo DESC");
                    if (rowsHead == null || rowsHead.Count < 1)
                    {
                        strIn_No = "OW" + DateTime.Now.ToString("yyyyMMdd") + "0001";
                    }
                    else
                    {
                        string strDate = rowsHead[0].O_OutNo.Substring(2, 8);
                        if (strDate == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            string strList = rowsHead[0].O_OutNo.Substring(10, 4);
                            int nList = Convert.ToInt32(strList) + 1;
                            strIn_No = "OW" + DateTime.Now.ToString("yyyyMMdd") + nList.ToString().PadLeft(4, '0');
                        }
                        else
                        {
                            strIn_No = "OW" + DateTime.Now.ToString("yyyyMMdd") + "0001";
                        }

                    }

                    OutWorkShopHeadEntity.O_OutNo = strIn_No;
                    OutWorkShopHeadEntity.O_OrderNo = comOrderNo.Text;
                    OutWorkShopHeadEntity.O_StockCode = cmbStock.Text;
                    OutWorkShopHeadEntity.O_StockName = txtStockName.Text;
                    OutWorkShopHeadEntity.O_CreateBy = "";
                    OutWorkShopHeadEntity.O_CreateDate = DateTime.Now;
                    OutWorkShopHeadEntity.O_OrderDate = "";
                    OutWorkShopHeadEntity.O_Remark = "";
                    OutWorkShopHeadEntity.O_Status = 1;
                    OutWorkShopHeadEntity.O_WorkShop = cmbWorkShop.Text;



                    int nRow = OutWorkShopHeadBLL.SaveEntity("", OutWorkShopHeadEntity);

                    for (int i = 0; i < rows.Count; i++)
                    {
                        OutWorkShopDetailEntity.O_GoodsCode = rows[i].O_GoodsCode;
                        OutWorkShopDetailEntity.O_GoodsName = rows[i].O_GoodsName;
                        OutWorkShopDetailEntity.O_OutNo = strIn_No;
                        OutWorkShopDetailEntity.O_Price = rows[i].O_Price;
                        OutWorkShopDetailEntity.O_Qty = rows[i].O_Qty;
                        OutWorkShopDetailEntity.O_Remark = rows[i].O_Remark;
                        OutWorkShopDetailEntity.O_Unit = rows[i].O_Unit;
                        OutWorkShopDetailEntity.O_Batch = rows[i].O_Batch;

                        nRow = OutWorkShopDetailBLL.SaveEntity("", OutWorkShopDetailEntity);

                    }
                    MessageBox.Show("保存成功");

                }

            }
            catch (Exception ex)
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
