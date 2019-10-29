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
using System.Threading;
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
        string strUnit = "";

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
                cmbWorkshopName.Items.Add(rows[i].W_Name);
            }

            if (cmbWorkShop.Items.Contains(Globels.strWorkShop))
            {
                cmbWorkShop.Text = Globels.strWorkShop;
            }

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
            cmbWorkshopName.Text = row[0].W_Name;
        }

        private void cmbStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesStockBLL StockBLL = new MesStockBLL();
            var row = StockBLL.GetData(" where S_Code = '" + cmbStock.Text + "'");
            cmbStockName.Text = row[0].S_Name;

            cmbGoodsCode.Items.Clear();
            MesInventoryBLL InventoryBLL = new MesInventoryBLL();
            var row2 = InventoryBLL.GetData("where I_StockCode = '" + cmbStock.Text + "' and I_Qty > 0 ");
            for (int i = 0; i < row2.Count; i++)
            {
                cmbGoodsCode.Items.Add(row2[i].I_GoodsCode);

            }
            if(row2.Count == 1)
            {
                cmbGoodsCode.Text = row2[0].I_GoodsCode;
            }

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
                        if (strBarcode.IndexOf('*') > 0)
                        {
                            string[] strTemp = strBarcode.Split('*');
                            string strGoodsCode = strTemp[0].ToString();
                            if (cmbGoodsCode.Items.Contains(strGoodsCode))
                            {
                                cmbGoodsCode.Text = strTemp[0].ToString();
                                cmbPc.Text = strTemp[1].ToString();
                                txtQty.Text = strTemp[2].ToString();

                                MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                                var Goods_rows = GoodsBLL.GetList(strTemp[0].ToString(), "");
                                int nLen = Goods_rows.Count;
                                if (nLen > 0)
                                {
                                    txtName.Text = Goods_rows[0].G_Name;
                                    txtPrice.Text = Goods_rows[0].G_Price.ToString();
                                    strUnit = Goods_rows[0].G_Unit.ToString();

                                }
                            }
                        }
                        else
                        {
                            string[] strTemp = strBarcode.Split(',');


                            string strGoodsCode = Resolve(strTemp[0].ToString());
                            if (cmbGoodsCode.Items.Contains(strGoodsCode))
                            {
                                cmbGoodsCode.Text = Resolve(strTemp[0].ToString());
                                cmbPc.Text = Resolve(strTemp[1].ToString());
                                txtQty.Text = Resolve(strTemp[2].ToString());

                                MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                                var Goods_rows = GoodsBLL.GetList(cmbGoodsCode.Text, "");
                                int nLen = Goods_rows.Count;
                                if (nLen > 0)
                                {
                                    txtName.Text = Goods_rows[0].G_Name;
                                    txtPrice.Text = Goods_rows[0].G_Price.ToString();
                                    strUnit = Goods_rows[0].G_Unit.ToString();

                                }
                            }
                        }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string Resolve(string strTemp)
        {
            try
            {
                string[] str = strTemp.Split(':');
                return str[1];
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void Update()
        {
            Mes_OutWorkShopTempBLL OutWorkShopTempBLL = new Mes_OutWorkShopTempBLL();
            var rows = OutWorkShopTempBLL.GetList_OutWorkShopTemp("where O_StockCode = '" + cmbStock.Text + "' and O_WorkShop = '" + cmbWorkShop.Text + "' and O_OrderNo = '" + comOrderNo.Text + "'");
            //if (rows == null || rows.Count < 1)
            //{
            //    //untCommon.InfoMsg("没有任何数据！");
            //    return;
            //}
            dataGridView1.DataSource = rows;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("是否保存","",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    Mes_OutWorkShopTempEntity OutWorkShopTempEntity = new Mes_OutWorkShopTempEntity();
                    OutWorkShopTempEntity.O_StockCode = cmbStock.Text;
                    OutWorkShopTempEntity.O_StockName = cmbStockName.Text;
                    OutWorkShopTempEntity.O_WorkShop = cmbWorkShop.Text;
                    OutWorkShopTempEntity.O_WorkShopName = cmbWorkshopName.Text;
                    OutWorkShopTempEntity.O_OrderNo = comOrderNo.Text;
                    OutWorkShopTempEntity.O_Status = 1;
                    OutWorkShopTempEntity.O_CreateBy = Globels.strUser;
                    OutWorkShopTempEntity.O_CreateDate = DateTime.Now;
                    OutWorkShopTempEntity.O_GoodsCode = cmbGoodsCode.Text;
                    OutWorkShopTempEntity.O_GoodsName = txtName.Text;
                    OutWorkShopTempEntity.O_Unit = strUnit;
                    OutWorkShopTempEntity.O_Qty = Convert.ToDecimal(txtQty.Text);
                    OutWorkShopTempEntity.O_Batch = cmbPc.Text;
                    OutWorkShopTempEntity.O_Remark = "";
                    OutWorkShopTempEntity.O_Barcode = txtBarcode.Text;
                    OutWorkShopTempEntity.O_Price = Convert.ToDecimal(txtPrice.Text);
                    OutWorkShopTempEntity.O_Record = cmbRecord.Text;

                    Mes_OutWorkShopTempBLL OutWorkShopTempBLL = new Mes_OutWorkShopTempBLL();


                    if (OutWorkShopTempBLL.SaveEntity("", OutWorkShopTempEntity) > 0)
                    {
                        untCommon.InfoMsg("添加成功！");
                        Update();
                        cls();
                        txtBarcode.SelectAll();
                        txtBarcode.Focus();
                        //frmParent.loadData();
                    }
                    else
                    {
                        //untCommon.InfoMsg("添加失败！");
                        lblTS.Text = "添加失败!";
                    }
                }
                catch(Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    lblTS.Text = "系统提示："+ ex.ToString();
                }
            }
        }

        private void cls()
        {
            txtBarcode.Text = "";
            //txtCode.Text = "";
            txtName.Text = "";
            //txtPc.Text = "";
            txtQty.Text = "";
            txtPrice.Text = "";

        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("是否要完工?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    Mes_OutWorkShopTempBLL OutWorkShopTempBLL = new Mes_OutWorkShopTempBLL();
                    var rows = OutWorkShopTempBLL.GetList_OutWorkShopTemp("where O_StockCode = '" + cmbStock.Text + "' and O_WorkShop = '" + cmbWorkShop.Text + "' and O_OrderNo = '" + comOrderNo.Text + "'");
                    if (rows == null || rows.Count < 1)
                    {
                        //untCommon.InfoMsg("没有任何数据！");
                        lblTS.Text = "没有任何数据！";
                        return;
                    }

                    Mes_OutWorkShopHeadBLL OutWorkShopHeadBLL = new Mes_OutWorkShopHeadBLL();
                    Mes_OutWorkShopDetailBLL OutWorkShopDetailBLL = new Mes_OutWorkShopDetailBLL();
                    Mes_OutWorkShopHeadEntity OutWorkShopHeadEntity = new Mes_OutWorkShopHeadEntity();
                    Mes_OutWorkShopDetailEntity OutWorkShopDetailEntity = new Mes_OutWorkShopDetailEntity();

                    string strIn_No = "";
                    MesMaterInHeadBLL MaterInHeadBLL = new MesMaterInHeadBLL();
                    strIn_No = MaterInHeadBLL.GetDH("线边仓出库到车间单");
                    /*var rowsHead = OutWorkShopHeadBLL.GetList_OutWorkShopHead("where 1 = 1 order by O_OutNo DESC");
                    if (rowsHead == null || rowsHead.Count < 1)
                    {
                        strIn_No = "OW" + DateTime.Now.ToString("yyyyMMdd") + "000001";
                    }
                    else
                    {
                        string strDate = rowsHead[0].O_OutNo.Substring(2, 8);
                        if (strDate == DateTime.Now.ToString("yyyyMMdd"))
                        {
                            string strList = rowsHead[0].O_OutNo.Substring(10, 6);
                            int nList = Convert.ToInt32(strList) + 1;
                            strIn_No = "OW" + DateTime.Now.ToString("yyyyMMdd") + nList.ToString().PadLeft(6, '0');
                        }
                        else
                        {
                            strIn_No = "OW" + DateTime.Now.ToString("yyyyMMdd") + "000001";
                        }

                    }*/

                    OutWorkShopHeadEntity.O_OutNo = strIn_No;
                    OutWorkShopHeadEntity.O_OrderNo = comOrderNo.Text;
                    OutWorkShopHeadEntity.O_StockCode = cmbStock.Text;
                    OutWorkShopHeadEntity.O_StockName = cmbStockName.Text;
                    OutWorkShopHeadEntity.O_CreateBy = Globels.strUser;
                    OutWorkShopHeadEntity.O_CreateDate = DateTime.Now;
                    OutWorkShopHeadEntity.O_OrderDate = txtOrderDate.Text;
                    OutWorkShopHeadEntity.O_Remark = "";
                    OutWorkShopHeadEntity.O_Status = 1;
                    OutWorkShopHeadEntity.O_WorkShop = cmbWorkShop.Text;
                    OutWorkShopHeadEntity.O_Kind = 1;



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
                    DeleteData();
                    Update();

                }

            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                lblTS.Text = ex.ToString();
            }
        }

        private void DeleteData()
        {
            Mes_OutWorkShopTempBLL OutWorkShopTempBLL = new Mes_OutWorkShopTempBLL();
            OutWorkShopTempBLL.DeleteData("where O_StockCode = '" + cmbStock.Text + "' and O_WorkShop = '" + cmbWorkShop.Text + "' and O_OrderNo = '" + comOrderNo.Text + "'");
            
        }

        private void cmbProce_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mes_ProceBLL ProceBLL = new Mes_ProceBLL();
            var row = ProceBLL.GetList_Proce("where P_RecordCode = '" + cmbRecord.Text + "' and P_ProNo = '" + cmbProce.Text + "'");
            txtProceName.Text = row[0].P_ProName;
        }

        private void btn_Weight_Click(object sender, EventArgs e)
        {
            if(Open())
            {
                Thread.Sleep(100);
                if (serialPort1.IsOpen)
                {
                    byte[] byRead = null;
                    byRead = new byte[2048];
                    int nReadLen;
                    nReadLen = serialPort1.Read(byRead, 0, byRead.Length);
                    string str = System.Text.Encoding.Default.GetString(byRead);
                    string[] strWeight = str.Split('=');
                    int nLen = strWeight.Length;
                    if (strWeight[nLen - 3].ToString() == strWeight[nLen - 2].ToString() && strWeight[nLen - 1].ToString() == strWeight[nLen - 2].ToString())
                    {
                        
                    }
                    else
                    {
                        //MessageBox.Show("串口没有打开");
                        lblTS.Text = "串口没有打开！";
                    }
                    //MessageBox.Show(str);
                }
                else
                {
                    //MessageBox.Show("串口没有打开");
                    lblTS.Text = "串口没有打开！";
                }

                Close();

            }
            else
            {
                ;
            }
        }

        private bool Open()
        {
            try
            {
                serialPort1.BaudRate = 1200;
                serialPort1.Open();
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                lblTS.Text = "ex.ToString()";
                return false;
            }
        }

        private void Close()
        {
            serialPort1.Close();
        }

        private void comOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesProductOrderHeadBLL ProductOrderHeadBLL = new MesProductOrderHeadBLL();
            var row = ProductOrderHeadBLL.GetList(comOrderNo.Text);
            
            txtOrderDate.Text = row[0].P_OrderDate.ToString();
        }

        private void cmbGoodsCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbPc.Items.Clear();
                MesInventoryBLL InventoryBLL = new MesInventoryBLL();
                var row = InventoryBLL.GetData("where I_StockCode = '" + cmbStock.Text + "' and I_GoodsCode = '" + cmbGoodsCode.Text + "' and I_Qty > 0");
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
                    strUnit = Goods_rows[0].G_Unit.ToString();
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
            catch(Exception ex)
            {
                lblTS.Text = "ex.ToString()";
            }
        }

        private void cmbWorkshopName_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesWorkShopBLL WorkShopBLL = new MesWorkShopBLL();
            var row = WorkShopBLL.GetData(" where W_Name = '" + cmbWorkshopName.Text + "'");
            cmbWorkShop.Text = row[0].W_Code;
        }

        private void cmbStockName_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesStockBLL StockBLL = new MesStockBLL();
            var row = StockBLL.GetData(" where S_Name = '" + cmbStockName.Text + "'");
            cmbStock.Text = row[0].S_Code;
        }


    }
}
