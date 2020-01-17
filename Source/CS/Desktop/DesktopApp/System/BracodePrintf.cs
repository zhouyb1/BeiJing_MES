using Business.System;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class BracodePrintf : DockContent
    {
        //public BracodePrintf()
        //{
        //    InitializeComponent();
        //}

        public frmMain frmMain { get; set; }
        //decimal Period; //保质期
        private SysUser User;

        string strUnit = "";
        decimal drqQty = 0;
        public BracodePrintf(frmMain _frmMain, SysUser _User)
        {
            InitializeComponent();
            frmMain = _frmMain;
            User = _User;
            //comKind.SelectedIndex = 0;//设置显示的item索引
        }

        private void BracodePrintf_Load(object sender, EventArgs e)
        {
            MesBasketBLL BasketBLL = new MesBasketBLL();
            var Basket_rows = BasketBLL.GetList();
            for (int i = 0; i < Basket_rows.Count; i++)
            {
                comBasketType.Items.Add(Basket_rows[i].B_BasketName);
            }

            checkBox1.Checked = true;
            comGoods.Items.Clear();
            MesGoodsBLL GoodsBLL = new MesGoodsBLL();
            var row = GoodsBLL.GetList("", "");
            for (int i = 0; i < row.Count; i++)
            {
                comGoods.Items.Add(row[i].G_Code + "$" + row[i].G_Name);
            }

            txtBatch.Text = DateTime.Now.ToString("yyyyMMdd");
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
                            char[] arr = strWeight[nLen - 1].Substring(0, 7).ToCharArray();
                            Array.Reverse(arr);
                            txtQty.Text = decimal.Parse(new string(arr)).ToString();
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
            catch (Exception ex)
            {
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
                MessageBox.Show(ex.ToString());
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
                txtUnit.Text = Goods_rows[0].G_Unit;
                txtPrice.Text = Goods_rows[0].G_Price.ToString();
                if (Goods_rows[0].G_Kind == 1)
                {
                    txtKind.Text = "原物料";
                }
                txtBatch.Text = DateTime.Now.ToString("yyyyMMdd");

                cmbSupply.Items.Clear();
                Mes_InPriceBLL InPriceBLL = new Mes_InPriceBLL();
                var InPrice_row = InPriceBLL.GetList_Mes_Price("where P_GoodsCode = '" + strGoods[0] + "'");
                if (InPrice_row.Count > 0)
                {
                    for (int i = 0; i < InPrice_row.Count; i++)
                    {
                        cmbSupply.Items.Add(InPrice_row[i].P_SupplyCode);
                    }
                    if (InPrice_row.Count == 1)
                    {
                        cmbSupply.Text = InPrice_row[0].P_SupplyCode;
                    }
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

        private void btnWeigh_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                //MesBasketBLL BasketBLL = new MesBasketBLL();
                //var Basket_rows = BasketBLL.GetList_BasketName(comBasketType.Text);
                drqQty = decimal.Parse(txtBasketQty.Text);
            }

            Print();
            clear();
        }

        private void Print()
        {
            try
            {
                //nLen = dataGridView2.RowCount;
                PrintDocument pd = new PrintDocument();
                PaperSize pageSize = new PaperSize("First custom size", 400, 200);
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

            decimal strQty = 0;
            if (checkBox1.Checked == true)
            {
                strQty = Convert.ToDecimal(txtQty.Text.Trim()) - drqQty;
            }
            else
            {
                strQty = Convert.ToDecimal(txtQty.Text.Trim());
            }
            g.DrawString("物料编码：" + strGoods[0], fontLiShu3, Brushes.Black, 10, 20);
            g.DrawString("物料名称：" + strGoods[1], fontLiShu3, Brushes.Black, 10, 40);
            //if (checkBox1.Checked == true)
            g.DrawString("    数量：" + strQty.ToString(), fontLiShu3, Brushes.Black, 10, 60);
            g.DrawString("    批次：" + txtBatch.Text.Trim(), fontLiShu3, Brushes.Black, 10, 80);
            g.DrawString("    时间：" + DateTime.Now, fontLiShu3, Brushes.Black, 10, 100);

            string str = strGoods[0] + "*" + txtBatch.Text.Trim() + "*" + txtQty.Text.Trim();

            prin1.barcodeControl1.Data = str;
            prin1.barcodeControl1.Caption = str;
            Rectangle rect = prin1.barcodeControl1.ClientRectangle;
            rect = new Rectangle(10, 120, 280, 80);
            prin1.barcodeControl1.Draw(g, rect, GraphicsUnit.Inch, 0.01f, 0, null);


        }

        private void clear()
        {
            //txtGoodsCode.Text = "";
            //txtGoodsName.Text = "";
            txtBatch.Text = "";
            txtQty.Text = "";
            txtUnit.Text = "";
            txtKind.Text = "";
        }

        private void comBasketType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesBasketBLL BasketBLL = new MesBasketBLL();
            var Basket_rows = BasketBLL.GetList_BasketName(comBasketType.Text);
            txtBasketQty.Text = Basket_rows[0].M_Weight.ToString();
        }

        private void cmbSupply_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
