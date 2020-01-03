using System;
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
using Model;
using System.Drawing.Printing;
using System.Threading;

namespace DesktopApp
{
    public partial class frmStorageEdit : DockContent
    {
        private string M_MaterInNo = "";//入库单号
        private string P_OrderNo = "";//生产订单号
        private string M_Status = "";//单据状态
        private string M_StockCode = ""; //仓库
        private Double Itox = 0;
        private SysUser User;
        private frmStorageList frmStorage;
        private string W_Kind;//称重类型
        private int rowindex;//获得当前选中的行的索引
        int whether;//是否打印 2:打印 
        Double drqQty = 0;
        public frmStorageEdit(frmStorageList _frmStorageList, SysUser _User, string _M_MaterInNo, string _P_OrderNo, string _M_Status,string stockcode)
        {
            InitializeComponent();
            M_MaterInNo = _M_MaterInNo;
            P_OrderNo = _P_OrderNo;
            User = _User;
            frmStorage = _frmStorageList;
            M_Status = _M_Status;
            M_StockCode = stockcode;
            getDetail();
        }

        private void getDetail()
        {
            try
            {
                //M_MaterInNo = M_MaterInNo.Text.Trim();
                txtMaterInNo.Text = M_MaterInNo.ToString();
                MesMaterInDetailBLL MaterInDetailBLL = new MesMaterInDetailBLL();
                var rows = MaterInDetailBLL.GetList(M_MaterInNo);
                MesBasketBLL BasketBLL = new MesBasketBLL();
                var Basket_rows = BasketBLL.GetList();
                for (int i = 0; i < Basket_rows.Count; i++)
                {
                    comBasketType.Items.Add(Basket_rows[i].B_BasketName);
                }
                if (rows == null || rows.Count < 1)
                {
                    //untCommon.InfoMsg("该入库单没有任何数据！");
                    return;
                }
                dataGridView.DataSource = rows;
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("物料入库编辑页面加载数据异常：" + ex.Message);
            }
        }

        private void frmStorageEdit_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            comGoods.Items.Clear();
            MesGoodsBLL GoodsBLL = new MesGoodsBLL();
            var row = GoodsBLL.GetData(" and G_Kind = 1 and G_StockCode = '"+ M_StockCode +"'");
            for (int i = 0; i < row.Count; i++)
            {
                comGoods.Items.Add(row[i].G_Code + "$" + row[i].G_Name);
            }

                txtBatch.Text = DateTime.Now.ToString("yyyyMMdd");

        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //获得当前选中的行的索引 
            rowindex = e.RowIndex;
        }
        /// <summary>
        /// 称重保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWeigh_Click(object sender, EventArgs e)
        {
            if(M_Status == "2")
            {
                untCommon.InfoMsg("该单据已经审核，不能再添加！");
                return;
            }
            if (M_Status == "3")
            {
                untCommon.InfoMsg("该单据已经完成，不能再添加！");
                return;
            }
            if (M_Status == "-1")
            {
                untCommon.InfoMsg("该单据已经删除，不能再添加！");
                return;
            }
            W_Kind = "入库称重";
            //addWeighStorage();
             addStorage();
            if (whether == 2)
            {
                Print();
                clear();
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print()
        {
            try
            {
                    //nLen = dataGridView2.RowCount;
                    PrintDocument pd = new PrintDocument();
                    PaperSize pageSize = new PaperSize("First custom size", 400, 200 );
                    pd.DefaultPageSettings.PaperSize = pageSize;
                    pd.PrintPage += new PrintPageEventHandler(printf);
                    PrintPreviewDialog cppd = new PrintPreviewDialog();
                    cppd.Document = pd;
                    PrintController printController = new StandardPrintController();
                    pd.PrintController = printController;

                    try
                    {
                        //pd.PrinterSettings.PrinterName = ("打印机名称");
                        cppd.ShowDialog();//打印预览

                        //pd.Print();  
                    }
                    catch (Exception)
                    {
                        untCommon.InfoMsg("请在系统里面设置打印机！");
                    }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("打印失败！");
            }
        }

        private void printf(object sender, PrintPageEventArgs e)
        {
           // nLen = dataGridView2.RowCount;
            string[] strGoods = comGoods.Text.ToString().Split('$');
            Graphics g = e.Graphics;
            pd prin1 = new pd();

            System.Drawing.Pen pen;
            pen = new Pen(Color.Black, 1f);

            System.Drawing.Font fontLiShu3;
            fontLiShu3 = new System.Drawing.Font("宋体", 9, System.Drawing.FontStyle.Bold);

            Double strQty = 0;
            if (checkBox1.Checked == true)
            {
                strQty = Convert.ToDouble(txtQty.Text.Trim()) - drqQty;
            }
            else
            {
                strQty = Convert.ToDouble(txtQty.Text.Trim());
            }
            g.DrawString("物料编码：" + strGoods[0], fontLiShu3, Brushes.Black, 30, 10);
            g.DrawString("物料名称：" + strGoods[1], fontLiShu3, Brushes.Black, 30, 25);
            //if (checkBox1.Checked == true)
            g.DrawString("    数量：" + strQty.ToString(), fontLiShu3, Brushes.Black, 30, 40);
            g.DrawString("    批次：" + txtBatch.Text.Trim(), fontLiShu3, Brushes.Black, 30, 55);
            g.DrawString("    时间：" + DateTime.Now, fontLiShu3, Brushes.Black, 30, 70);

            string str = strGoods[0] + "*" + txtBatch.Text.Trim() + "*" + txtQty.Text.Trim();

            prin1.barcodeControl1.Data = str;
            prin1.barcodeControl1.Caption = str;
            Rectangle rect = prin1.barcodeControl1.ClientRectangle;
            rect = new Rectangle(40, 80, 80, 40);
            prin1.barcodeControl1.Draw(g, rect, GraphicsUnit.Inch, 0.01f, 0, null);


        }

        /// <summary>
        /// 校验输入
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            if (string.IsNullOrEmpty(comGoods.Text.Trim()))
            {
                untCommon.InfoMsg("物料编码不能为空！");
                return false;
            }
  

            if (string.IsNullOrEmpty(txtBatch.Text))
            {
                untCommon.InfoMsg("批次不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(txtQty.Text))
            {
                untCommon.InfoMsg("入库数量不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(txtUnit.Text))
            {
                untCommon.InfoMsg("单位不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(txtKind.Text))
            {
                untCommon.InfoMsg("物料类型不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(txtPrice.Text))
            {
                untCommon.InfoMsg("入库单价不能为空！");
                return false;
            }

            return true;
        }
        /// <summary>
        /// 称重记录保存
        /// </summary>
        //private void addWeighStorage()
        //{
        //    try
        //    {
        //        MesWeightRecordBLL MesWeightRecordBLL = new MesWeightRecordBLL();
        //        MesMaterInDetailBLL MaterInDetailBLL = new MesMaterInDetailBLL();
        //        var rows = MaterInDetailBLL.GetList_GoodsCode("","");
        //        if (checkInput())
        //        {
        //            rows = MaterInDetailBLL.GetList_GoodsCode(txtGoodsCode.Text,M_MaterInNo);
        //            if (rows[0].M_GoodsCode == txtGoodsCode.Text && rows[0].M_GoodsName == txtGoodsName.Text && rows[0].M_Qty.ToString() == txtQty.Text)
        //            {
        //                untCommon.InfoMsg("称重记录数据错误！");
        //                return;
        //            }


                    
        //            MesWeightRecordEntity MesWeightRecord = new MesWeightRecordEntity();

        //            MesWeightRecord.P_OrderNo = P_OrderNo;
        //            MesWeightRecord.W_Kind = W_Kind;
        //            MesWeightRecord.W_Date = DateTime.Now;
        //            MesWeightRecord.W_GoodsCode = txtGoodsCode.Text;
        //            MesWeightRecord.W_GoodsName = txtGoodsName.Text;
        //            MesWeightRecord.W_Batch = txtBatch.Text;
        //            MesWeightRecord.W_Qty = Double.Parse(txtQty.Text);
        //            MesWeightRecord.W_Unit = txtUnit.Text;

        //            if (MesWeightRecordBLL.SaveEntity("", MesWeightRecord) > 0)
        //            {
        //                untCommon.InfoMsg("称重记录添加成功！");
        //                whether = 2;
        //                //frmParent.loadData();
        //                frmStorage.Refresh();
        //            }
        //            else
        //            {
        //                untCommon.InfoMsg("称重记录添加失败！");
        //            }
        //        }
        //        rows = MaterInDetailBLL.GetList(M_MaterInNo);
        //        dataGridView.DataSource = rows;
        //    }
        //    catch (Exception ex)
        //    {
        //        untCommon.ErrorMsg("称重数据异常：" + ex.Message);
        //    }
        //}

        /// <summary>
        /// 保存
        /// </summary>
        private void addStorage()
        {
            try
            {
                string[] strGoods = comGoods.Text.ToString().Split('$');
                MesWeightRecordBLL MesWeightRecordBLL = new MesWeightRecordBLL();
                MesMaterInDetailBLL MaterInDetailBLL = new MesMaterInDetailBLL();
                MesWeightRecordEntity MesWeightRecord = new MesWeightRecordEntity();
                var rows = MaterInDetailBLL.GetList_GoodsCode("","");
                if (checkInput())
                {
                    //rows = MaterInDetailBLL.GetList_GoodsCode(txtGoodsCode.Text,M_MaterInNo);
                    //if (rows[0].M_GoodsCode == txtGoodsCode.Text && rows[0].M_GoodsName == txtGoodsName.Text && rows[0].M_Qty.ToString() == txtQty.Text)
                    //{
                    //    untCommon.InfoMsg("称重记录数据错误！");
                    //    return;
                    //}

                    MesWeightRecord.P_OrderNo = P_OrderNo;
                    MesWeightRecord.W_Kind = W_Kind;
                    MesWeightRecord.W_Date = DateTime.Now;
                    MesWeightRecord.W_GoodsCode = strGoods[0];
                    MesWeightRecord.W_GoodsName = strGoods[1];
                    MesWeightRecord.W_Batch = txtBatch.Text;
                    MesWeightRecord.W_Qty = Double.Parse(txtQty.Text);
                    MesWeightRecord.W_Unit = txtUnit.Text;

                    MesMaterInDetailEntity MaterInDetail = new MesMaterInDetailEntity();
                    MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                    //MesMaterInDetailEntity MaterInDetail = new MesMaterInDetailEntity();
                    var Goods_rows = GoodsBLL.GetList(strGoods[0], "");
                    
                    string cz = "";//是否存在相同物料

                    MaterInDetail.M_MaterInNo = txtMaterInNo.Text;
                    MaterInDetail.M_GoodsCode = strGoods[0];
                    MaterInDetail.M_GoodsName = strGoods[1];
                    MaterInDetail.M_Batch = txtBatch.Text;
                    MaterInDetail.M_Price = Convert.ToDouble(txtPrice.Text);
                    MaterInDetail.M_GoodsItax = Itox;
                    
                    if (checkBox1.Checked == true)
                    {
                        drqQty = Double.Parse(txtBasketQty.Text);
                        MaterInDetail.M_Qty = Double.Parse(txtQty.Text) - drqQty;
                     }
                    else
                    {
                        MaterInDetail.M_Qty = Double.Parse(txtQty.Text);
                    }
                    MaterInDetail.M_Unit = txtUnit.Text;
                    MaterInDetail.M_Kind = Convert.ToString(Goods_rows[0].G_Kind);

                    // MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                    //var Goods_rows = GoodsBLL.GetList(MaterInDetail.M_GoodsCode, MaterInDetail.M_GoodsName);

                    if (Goods_rows == null || Goods_rows.Count < 1)
                    {
                        untCommon.InfoMsg("输入的物料名称错误，请重新输入！");
                        return;
                    }
                    else if (Goods_rows[0].G_Kind == 1)
                    {
                        //txtGoodsCode.Text = Goods_rows[0].G_Code;
                        //txtGoodsName.Text = Goods_rows[0].G_Name;
                        txtUnit.Text = Goods_rows[0].G_Unit;
                    }
                    else
                    {
                        untCommon.InfoMsg("输入的物料名称错误，请重新输入！");
                        return;
                    }

                    int i = dataGridView.Rows.Count;
                    if (dataGridView.RowCount > 0 && dataGridView.DataSource != null)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (dataGridView.Rows[j].Cells["物料编码"].Value.ToString() == strGoods[0])
                            {
                                if (dataGridView.Rows[j].Cells["批次"].Value.ToString() == txtBatch.Text.Trim())
                                {
                                    cz = "存在";
                                }
                            }
                        }
                    }
                    if (cz == "存在")
                    {
                        var MaterInDetai_rows = MaterInDetailBLL.GetList_GoodsCode(MaterInDetail.M_GoodsCode,M_MaterInNo);
                        var rowData = MaterInDetailBLL.GetEntity(MaterInDetai_rows[0].ID);
                        
                        //

                        Double dPrice = (rowData.M_Price * rowData.M_Qty + MaterInDetail.M_Price * MaterInDetail.M_Qty) / (rowData.M_Qty + MaterInDetail.M_Qty);
                        rowData.M_Price = dPrice;
                        rowData.M_Qty += MaterInDetail.M_Qty;
                        if (MaterInDetailBLL.SaveEntityTrans(rowData.ID, rowData, "", MesWeightRecord) > 0)//事务
                        {
                            untCommon.InfoMsg("修改成功！");
                            whether = 2;
                            //frmParent.loadData();
                            frmStorage.Refresh();
                        }
                        else
                        {
                            untCommon.InfoMsg("修改失败！");
                        }
                        //if (MaterInDetailBLL.SaveEntity(rowData.ID, rowData) > 0)
                        //{
                        //    untCommon.InfoMsg("修改失败！");
                        //    whether = 2;
                        //    //frmParent.loadData();
                        //    frmStorage.Refresh();
                        //    clear();
                        //}
                        //else
                        //{
                        //    untCommon.InfoMsg("修改失败！");
                        //}
                    }
                    else
                    {
                        if (MaterInDetailBLL.SaveEntityTrans("", MaterInDetail, "", MesWeightRecord) > 0)
                        {
                            untCommon.InfoMsg("添加成功！");
                            //frmParent.loadData();
                            whether = 2;
                            frmStorage.Refresh();
                        }
                        else
                        {
                            untCommon.InfoMsg("添加失败！");
                        }
                    }
                }
                rows = MaterInDetailBLL.GetList(M_MaterInNo);
                dataGridView.DataSource = rows;
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("称重保存数据异常：" + ex.Message);
            }
        }

        //private void txtGoodsCode_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
        //    {
                
        //    }
        //}

        //private void txtGoodsName_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
        //    {
                
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            addStorage();
        }

        private void btnPeeling_Click(object sender, EventArgs e)
        {
            W_Kind = "入库去框重量";
            //addWeighStorage();
        }
        /// <summary>
        /// 清空文本框
        /// </summary>
        private void clear()
        {
            //txtGoodsCode.Text = "";
            //txtGoodsName.Text = "";
            txtBatch.Text = "";
            txtQty.Text = "";
            txtUnit.Text = "";
            txtKind.Text = "";
            txtPrice.Text = "";
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView.Rows[0].Cells[0].Value != null)
            {
                if (e.ColumnIndex == 3)
                {
                    if (e.Value != null)
                    {
                        string stringValue = e.Value.ToString();
                        switch (stringValue)
                        {
                            case "0": e.Value = "原材料";
                                break;
                            case "1": e.Value = "半成品";
                                break;
                            case "2": e.Value = "成品";
                                break;
                        }
                    }
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtUnit_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
            {
                txtQty.Focus();
            }
        }

        private void btn_Weight_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                if (Open())
                {
                    Thread.Sleep(100);
                    if (serialPort1.IsOpen)
                    {
                     
                    
                        //int nReadLen;
                        //Thread.Sleep(200);
                        //nReadLen = serialPort1.Read(byRead, 0, byRead.Length);

                        //string str = System.Text.Encoding.Default.GetString(byRead);
                        Thread.Sleep(100);
                        byte[] byRead = new byte[serialPort1.ReadBufferSize];
                        int nReadLen = serialPort1.Read(byRead, 0, byRead.Length);
                        string str = System.Text.Encoding.Default.GetString(byRead);
                        //MessageBox.Show(str);
                        //MessageBox.Show(str.Length.ToString());
                        string[] strWeight = str.Split('=');
                        int nLen = strWeight.Length;
                        //MessageBox.Show(nLen.ToString());
                        if (nLen > 1)
                        {
                            //if (strWeight[nLen - 3].ToString() == strWeight[nLen - 2].ToString() && strWeight[nLen - 4].ToString() == strWeight[nLen - 2].ToString())
                            //{
                            char[] arr = strWeight[nLen - 1].Substring(0,7).ToCharArray();
                            Array.Reverse(arr);
                            txtQty.Text = Double.Parse(new string(arr)).ToString();
                                ; 
                                //ZH(strWeight[nLen - 1].ToString());

                            //}
                            //else
                            //{
                            //MessageBox.Show("串口没有打开");

                            //}
                            Close();
                            Thread.Sleep(100);
                            this.Enabled = true;
                            Cursor.Current = Cursors.Default;
                        }
                        else
                        {
                            MessageBox.Show(str);
                            Close();
                            Thread.Sleep(100);
                            this.Enabled = true;
                            Cursor.Current = Cursors.Default;
                        }

                        //MessageBox.Show(str);
                    }
                    else
                    {
                        this.Enabled = true;
                        Cursor.Current = Cursors.Default;
                        MessageBox.Show("串口没有打开");

                    }

                    Close();
                    this.Enabled = true;
                    Cursor.Current = Cursors.Default;

                }
                else
                {
                    this.Enabled = true;
                    Cursor.Current = Cursors.Default;
                }
            }
            catch(Exception ex)
            {
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.ToString());
            }
        }

        private string ZH(string strTemp)
        {
            strTemp = strTemp.Replace(" ","");
             string strReturn = "";
            if (string.IsNullOrEmpty(strTemp))
                return "0";
            MessageBox.Show(strTemp);
            int nLen = strTemp.Length;
            MessageBox.Show(strTemp.Length.ToString());
            for (int i = 0; i < nLen; i++)
            {
                strReturn = strReturn + strTemp.Substring(nLen - i - 1);
            }
            /*char[] datas = strTemp.ToCharArray();
            StringBuilder sb = new StringBuilder();
            
            MessageBox.Show(datas.Length.ToString());
            for (int i = datas.Length - 1; i >= 0; i--)
            {
                sb.Append( datas[i].ToString() );

                
    
            }*/

            return strReturn;
               
        }

        private bool Open()
        {
            try
            {
                serialPort1.PortName = Globels.strCom;
                serialPort1.BaudRate = 1200;
                serialPort1.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        private void Close()
        {
            try
            {
                serialPort1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void comGoods_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] strGoods = comGoods.Text.ToString().Split('$');
                MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                //MesMaterInDetailEntity MaterInDetail = new MesMaterInDetailEntity();
                var Goods_rows = GoodsBLL.GetList(strGoods[0], strGoods[1]);
                if (Goods_rows == null || Goods_rows.Count < 1 || Goods_rows[0].G_Kind == 2)
                {
                    untCommon.InfoMsg("输入的物料编码错误，请重新输入！");
                    return;
                }
                if (Goods_rows[0].G_Kind == 1)
                {
                    //txtGoodsCode.Text = Goods_rows[0].G_Code;
                    //txtGoodsName.Text = Goods_rows[0].G_Name;
                    txtUnit.Text = Goods_rows[0].G_Unit.ToString();
                    Itox = Goods_rows[0].G_Itax;
                    //txtPrice.Text = Goods_rows[0].G_Price.ToString();
                    if (Goods_rows[0].G_Kind == 1)
                    {
                        txtKind.Text = "原物料";
                    }
                    txtBatch.Text = DateTime.Now.ToString("yyyyMMdd");


                    Mes_InPriceBLL InPriceBLL = new Mes_InPriceBLL();
                    var InPrice_row = InPriceBLL.GetList_Mes_Price(" where P_GoodsCode = '" + strGoods[0] + "' and P_SupplyCode = '"+ Globels.strSupplyCode +"'");
                    if (InPrice_row.Count > 0)
                    {
                        
                            txtPrice.Text = InPrice_row[0].P_InPrice.ToString();
                        
                    }
                    else
                    {
                        untCommon.InfoMsg("请先维护商品的入库价格");
                        return;
                    }

                    txtQty.Focus();


                }
                else
                {
                    untCommon.InfoMsg("输入的物料编码不是原物料，请重新输入！");
                    //txtGoodsCode.Text = "";
                    //txtGoodsCode.Focus();
                }
            }
            catch (Exception Exception)
            {
                MessageBox.Show(Exception.ToString());
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comBasketType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MesBasketBLL BasketBLL = new MesBasketBLL();
                var Basket_rows = BasketBLL.GetList_BasketName(comBasketType.Text);
                txtBasketQty.Text = Basket_rows[0].M_Weight.ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        

     
    }
}
