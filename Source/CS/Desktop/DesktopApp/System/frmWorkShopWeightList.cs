using Business.System;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tag;
using USC.BmpLib.BMP;
using USC.PCD;
using USC.Tools;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmWorkShopWeightList : DockContent
    {
        NfcTag nfcTag;
        Bmp2Bmp2Data b2d;
        Bmp2BmpProduct bp;
        string strBZQ; //保质期
        string m_strBarcode = "补写";
        public frmWorkShopWeightList()
        {
            InitializeComponent();
        }

        //private void cmbGoodsName_DrawItem(object sender, DrawItemEventArgs e)
        //{
        //    if (e.Index < 0)
        //    {
        //        return;
        //    }
        //    e.DrawBackground();
        //    e.DrawFocusRectangle();
        //    e.Graphics.DrawString(cmbGoodsName.GetItemText(cmbGoodsName.Items[e.Index]).ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.X, e.Bounds.Y + 0);
        //}
        private void frmWorkShopWeightList_Load(object sender, EventArgs e)
        {

            List<string> insertList = new List<string>();
            MesInventoryBLL InventoryBLL = new MesInventoryBLL();
            this.listView1.Items.Clear();
            this.listView1.BeginUpdate();
            var Scan_rows = InventoryBLL.GetData(" where I_StockCode = '" + Globels.strStockCode + "' and I_Qty > 0 order by I_GoodsCode");
            for (int i = 0; i < Scan_rows.Count; i++)
            {
                string strGoodsCode = Scan_rows[i].I_GoodsCode;
                Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
                var Convert_rows = ConvertBLL.GetList_Mes_Convert(" where C_Code = '" + strGoodsCode + "' and C_ProNo = '" + Globels.strProce + "' order by C_SecName");
                
                for (int j = 0; j < Convert_rows.Count; j++)
                {
                    if (!insertList.Contains(Convert_rows[j].C_SecName))
                    {
                        insertList.Add(Convert_rows[j].C_SecName);
                        ListViewItem lvi = new ListViewItem(Convert_rows[j].C_SecName);
                        this.listView1.Items.Add(lvi);
                    }

                    //if (!cmbGoodsName.Items.Contains(Convert_rows[j].C_SecName))
                    //{
                    //    cmbGoodsName.Items.Add(Convert_rows[j].C_SecName);
                    //}

                }
                
            }
            this.listView1.EndUpdate();

        }

        private void txtRQQty_GotFocus(object sender, EventArgs e)
        {
            ShowInputPanel();
        }
        //显示屏幕键盘
        public static int ShowInputPanel()
        {
            try
            {
                dynamic file = "C:\\Program Files\\Common Files\\microsoft shared\\ink\\TabTip.exe";
                if (!System.IO.File.Exists(file))
                    return -1;
                Process.Start(file);
                //return SetUnDock(); //不知SetUnDock()是什么，所以直接注释返回1
                return 1;
            }
            catch (Exception)
            {
                return 255;
            }
        }

        private void cmbGoodsCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesGoodsBLL GoodsBLL = new MesGoodsBLL();
            var Goods_rows = GoodsBLL.GetListCondit("where G_Name = '" + txtGoodsName.Text + "'");
            int nLen = Goods_rows.Count;
            if (nLen > 0)
            {
                txtCode.Text = Goods_rows[0].G_Code;
                txtUnit.Text = Goods_rows[0].G_Unit;
                int dd = Goods_rows[0].G_Period * 24;
                strBZQ = dd.ToString();
            }
            txtBatch.Text = DateTime.Now.ToString("yyyyMMdd");
            

            //ShowYWL(txtCode.Text);
        }

        private void ShowYWL(string strCode)
        {
            Mes_YWLBLL YWLBL = new Mes_YWLBLL();


            string strSql = "select a.C_Code,a.C_Name,b.W_Batch, b.W_Qty from Mes_Convert as a left join Mes_WorkShopScan as b on a.C_Code = b.W_GoodsCode where C_SecCode = '" + strCode + "'";
            var row = YWLBL.GetList_Mes_YWL(strSql);
            //if (row == null || row.Count < 1)
            //{
            //    untCommon.InfoMsg("没有任何数据！");
            //    return;
            //}
            dataGridView1.DataSource = row;
            int nLen = dataGridView1.Rows.Count;
            for (int i = 0; i < nLen; i++)
            {
                dataGridView1.Rows[i].Cells["使用数量"].Value = "0";
            }
            //listView1.Items.Clear();
            //Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
            //var Conv_row = ConvertBLL.GetList_Mes_Convert(" where C_SecCode = '" + strCode + "'");
            //int nLen = Conv_row.Count;
            //this.listView1.BeginUpdate();
            //for (int i = 0; i < nLen; i++)
            //{
            //    ListViewItem lvi = new ListViewItem(Conv_row[i].C_Code);
            //    lvi.SubItems.Add(Conv_row[i].C_Name);

            //    Mes_WorkShopScanBLL WorkShopScanBLL = new Mes_WorkShopScanBLL();
            //    var Work_row = WorkShopScanBLL.GetList_WorkShopScan(" where W_WorkShop = '" + Globels.strWorkShop + "' and W_GoodsCode = '" + Conv_row[i].C_Code + "' and W_Qty > 0 order by W_GoodsCode,W_Batch");
            //    int nLen2 = Work_row.Count;
            //    for (int j = 0; j < nLen2; j++ )
            //    {
            //        lvi.SubItems.Add(Work_row[j].W_Batch);
            //        lvi.SubItems.Add(Work_row[j].W_Qty.ToString());
            //        lvi.SubItems.Add("0");
            //        this.listView1.Items.Add(lvi);
            //    }
                    


            //}

            //this.listView1.EndUpdate();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtGoodsName.Text == "")
            {
                MessageBox.Show("请先选择物料");

                return;

            }
            else
            {
                try
                {
                
                   if(txtQty.Text == "")
                   {
                       MessageBox.Show("请先称重");
                       return;
                   }
                    if(txtRQQty.Text == "")
                    {
                        MessageBox.Show("请先输入容器重量");
                        return;
                    }
                    if(IsNumberic(txtRQQty.Text) == false)
                    {
                        MessageBox.Show("容器重量应该为数字");
                        return;
                    }
                    if (Convert.ToDecimal(txtRQQty.Text) > Convert.ToDecimal(txtQty.Text))
                    {
                        MessageBox.Show("容器重量不能大于称重重量");
                        return;
                    }

                
                    this.Enabled = false;
                    Cursor.Current = Cursors.WaitCursor;
                    string strBarcode = txtCode.Text + DateTime.Now.ToString("yyyyMMddHHmmss");

                    Mes_WorkShopWeightBLL WorkShopWeightBLL = new Mes_WorkShopWeightBLL();
                    Mes_WorkShopWeightEntity WorkShopWeightEntity = new Mes_WorkShopWeightEntity();
                    WorkShopWeightEntity.W_CreateBy = "";
                    WorkShopWeightEntity.W_CreateDate = DateTime.Now;
                    WorkShopWeightEntity.W_OrderNo = Globels.strOrderNo;
                    WorkShopWeightEntity.W_ProceCode = Globels.strProce;
                    WorkShopWeightEntity.W_ProceName = Globels.strProceName;
                    WorkShopWeightEntity.W_RecordName = Globels.strRecordName;
                    WorkShopWeightEntity.W_WorkShopName = Globels.strWorkShopName;
                    WorkShopWeightEntity.W_RecordCode = Globels.strRecord;
                    WorkShopWeightEntity.W_Remark = "";
                    WorkShopWeightEntity.W_SecBatch = txtBatch.Text;
                    WorkShopWeightEntity.W_SecGoodsCode = txtCode.Text;
                    WorkShopWeightEntity.W_SecGoodsName = txtGoodsName.Text;
                    WorkShopWeightEntity.W_SecQty = Convert.ToDecimal(txtQty.Text) - Convert.ToDecimal(txtRQQty.Text);
                    WorkShopWeightEntity.W_SecUnit = txtUnit.Text;
                    WorkShopWeightEntity.W_Status = 1;
                    WorkShopWeightEntity.W_WorkShopCode = Globels.strWorkShop;
                    WorkShopWeightEntity.W_Remark = strBarcode;

                    int nCount = WorkShopWeightBLL.SaveEntity("", WorkShopWeightEntity);
                    if (nCount > 0)
                    {
                        string Barcode = txtCode.Text + DateTime.Now.ToString("yyyyMMddHHmmss");
                        m_strBarcode = Barcode;
                        decimal dTemp = Convert.ToDecimal(txtQty.Text) - Convert.ToDecimal(txtRQQty.Text);
                        GetImg("物料" + txtCode.Text + "批次" + txtBatch.Text.Trim() + "单号" + Globels.strOrderNo, txtGoodsName.Text, dTemp.ToString(), strBZQ, strBarcode);
                        MessageBox.Show("添加成功");
                        SaveBarcode(Barcode, txtCode.Text, txtGoodsName.Text, dTemp, Globels.strWorkShop);
                    }

                    this.Enabled = true;
                    Cursor.Current = Cursors.Default;
                }
                catch(Exception ex)
                {
                    this.Enabled = true;
                    Cursor.Current = Cursors.Default;
                }
            }



        }
        /// <summary>
        /// 保存标签条码
        /// </summary>
        /// <param name="B_Barcode"></param>
        /// <param name="B_Code"></param>
        /// <param name="B_Name"></param>
        /// <param name="B_Qty"></param>
        /// <param name="B_WorkShopCode"></param>
        private void SaveBarcode(string B_Barcode, string B_Code, string B_Name, decimal B_Qty, string B_WorkShopCode)
        {
            Mes_BarcodeEntity BarcodeEntity = new Mes_BarcodeEntity();
            Mes_BarcodeBLL BarcodeBLL = new Mes_BarcodeBLL();
            BarcodeEntity.B_Barcode = B_Barcode;
            BarcodeEntity.B_Code = B_Code;
            BarcodeEntity.B_Name = B_Name;
            BarcodeEntity.B_Qty = B_Qty;
            BarcodeEntity.B_WorkShopCode = B_WorkShopCode;
            DateTime dt = DateTime.Now;
            BarcodeEntity.B_Ptime = dt;
            BarcodeEntity.B_Itime = dt;
            BarcodeEntity.B_Otime = dt;
            BarcodeEntity.B_Utime = dt;
            BarcodeEntity.B_Status = 1;
            BarcodeBLL.SaveEntity("",BarcodeEntity);

        }

        private bool IsNumberic(string oText)
        {
            try
            {
                decimal var1 = Convert.ToDecimal(oText);
                return true;
            }
            catch
            {
                return false;
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
                        //byte[] byRead = null;
                        //byRead = new byte[2048];
                        //int nReadLen;
                        //Thread.Sleep(100);
                        //nReadLen = serialPort1.Read(byRead, 0, byRead.Length);
                        //string str = System.Text.Encoding.Default.GetString(byRead);
                        //string[] strWeight = str.Split('=');
                        //int nLen = strWeight.Length;
                        //if (nLen > 1)
                        //{
                        //    //if (strWeight[nLen - 3].ToString() == strWeight[nLen - 2].ToString() && strWeight[nLen - 4].ToString() == strWeight[nLen - 2].ToString())
                        //    //{
                        //    txtQty.Text = ZH(strWeight[nLen - 1].ToString());
                        //    //}
                        //    //else
                        //    //{
                        //    //MessageBox.Show("串口没有打开");

                        //    //}
                        //    Close();
                        //    Thread.Sleep(100);
                        //    this.Enabled = true;
                        //    Cursor.Current = Cursors.Default;
                        //}
                        //else
                        //{
                        //    MessageBox.Show(str);
                        //    Close();
                        //    Thread.Sleep(100);
                        //    this.Enabled = true;
                        //    Cursor.Current = Cursors.Default;
                        //}

                        //MessageBox.Show(str);
                    }
                    else
                    {
                        MessageBox.Show("串口没有打开");
                        this.Enabled = true;
                        Cursor.Current = Cursors.Default;
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
                Close();
                MessageBox.Show(ex.ToString());
                this.Enabled = true;
                Cursor.Current = Cursors.Default;
            }
        }

        private string ZH(string strTemp)
        {
            string strReturn = "";
            int nLen = strTemp.Length;
            for (int i = 0; i < nLen; i++)
            {
                strReturn = strReturn + strTemp.Substring(nLen - i - 1, 1);
            }
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 二维码生成
        /// </summary>
        /// <param name="strHZ"></param>
        /// <param name="strGoodsName"></param>
        /// <param name="strQty"></param>
        public void GetImg(string strHZ, string strGoodsName, string strQty,string strBZQ,string strBarcode)
        {
            try
            {
                b2d = new Bmp2Bmp2Data();
                nfcTag = new NfcTag(new WI());

                string strPath = Application.StartupPath + "\\img\\" + strHZ + ".bmp";
                strHZ = "物料:" + txtCode.Text.Trim() + ",批次:" + txtBatch.Text.Trim() + ",数量:" + strQty + ",单号:" + Globels.strOrderNo + "," + strBarcode;
                int nLen = strHZ.Length;
                byte[] fileData = Encoding.GetEncoding("GB2312").GetBytes(strHZ);
                int nLen2 = fileData.Length;
                //MessageBox.Show(strHZ);
                Barcode.Make(fileData, nLen2, 0, 0, 0, strPath, 2);
                Thread.Sleep(100);
                FileStream fs = new FileStream(strPath, FileMode.Open, FileAccess.Read);
                Byte[] mybyte = new byte[fs.Length];
                fs.Read(mybyte, 0, mybyte.Length);
                fs.Close();

                MemoryStream ms = new MemoryStream(mybyte);
                Bitmap myimge = new Bitmap(ms);

                Bitmap image = new Bitmap(296, 128);//初始化大小
                Graphics g = Graphics.FromImage(image);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置图片质量

                g.Clear(Color.White);
                g.DrawImage(myimge, 190, 15, 80, 80);
                Font f1 = new Font("Arial ", 40);//, System.Drawing.FontStyle.Bold);//设置字体样式，大小
                Font f2 = new Font("Arial ", 30);//, System.Drawing.FontStyle.Bold);//设置字体样式，大小
                Font f3 = new Font("Arial ", 12);//, System.Drawing.FontStyle.Bold);//设置字体样式，大小
                Font f4 = new Font("Arial ", 10);//, System.Drawing.FontStyle.Bold);//设置字体样式，大小
                Brush b = new SolidBrush(Color.Black);
                Brush r = new SolidBrush(Color.White);
                //g.DrawString(strHZ, f3, b, 15, 60);//设置位置
                //int nBZQ = BZQ(strGoodsName);
                g.DrawString("名称：" + strGoodsName, f4, b, 4, 10);//设置位置
                g.DrawString("数量：" + strQty, f4, b, 4, 30);//设置位置
                g.DrawString("保质期：" + strBZQ + "小时", f4, b, 4, 50);//设置位置
                g.DrawString("负责人：" + Globels.strName, f4, b, 4, 70);//设置位置

                string strTeamName = "";
                if(Globels.strStockCode == "0401")
                {
                    strTeamName = "蔬菜";
                }
                else if(Globels.strStockCode == "0402")
                {
                    strTeamName = "肉食";
                }
                else
                {
                    strTeamName = "热厨";
                }
                g.DrawString("班组：" + strTeamName, f4, b, 4, 90);//设置位置

                g.DrawString("日期：" + DateTime.Now.ToString("yyyy-MM-dd"), f4, b, 178, 105);//设置位置

                image.Save(strPath, ImageFormat.Jpeg);//自己创建一个文件夹，放入生成的图片（根目录下）

                //二维码图片nfc写入
                bp = new Bmp2BmpProduct(new TagViewSize((EnumTagViewSizeID)(0x21), 0x00),
                        new Bitmap(image)
                        );
                b2d.ImageYuLan(bp);

                byte[] _nfcTagBmpData = b2d.GetDataToSend(bp);
                int _nfcTagBmpDataLength = b2d.SendLength;

                nfcTag.SendBmp2NfcTag(_nfcTagBmpData, _nfcTagBmpDataLength);

                //MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("二维码生成异常！：" + ex.Message);
            }
        }

        public void GetImg2(string strHZ, string strGoodsName, string strQty, string strBZQ)
        {
            try
            {
                b2d = new Bmp2Bmp2Data();
                nfcTag = new NfcTag(new WI());

                string strPath = Application.StartupPath + "\\img\\" + strHZ + ".bmp";
                strHZ = "物料:" + "0200075" + ",批次:" + "20191219" + ",数量:" + "100" + ",单号:" + Globels.strOrderNo;
                int nLen = strHZ.Length;
                byte[] fileData = Encoding.GetEncoding("GB2312").GetBytes(strHZ);
                int nLen2 = fileData.Length;
                //MessageBox.Show(strHZ);
                Barcode.Make(fileData, nLen2, 0, 0, 0, strPath, 2);
                Thread.Sleep(100);
                FileStream fs = new FileStream(strPath, FileMode.Open, FileAccess.Read);
                Byte[] mybyte = new byte[fs.Length];
                fs.Read(mybyte, 0, mybyte.Length);
                fs.Close();

                MemoryStream ms = new MemoryStream(mybyte);
                Bitmap myimge = new Bitmap(ms);

                Bitmap image = new Bitmap(296, 128);//初始化大小
                Graphics g = Graphics.FromImage(image);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;//设置图片质量

                g.Clear(Color.White);
                g.DrawImage(myimge, 190, 15, 80, 80);
                Font f1 = new Font("Arial ", 40);//, System.Drawing.FontStyle.Bold);//设置字体样式，大小
                Font f2 = new Font("Arial ", 30);//, System.Drawing.FontStyle.Bold);//设置字体样式，大小
                Font f3 = new Font("Arial ", 12);//, System.Drawing.FontStyle.Bold);//设置字体样式，大小
                Font f4 = new Font("Arial ", 10);//, System.Drawing.FontStyle.Bold);//设置字体样式，大小
                Brush b = new SolidBrush(Color.Black);
                Brush r = new SolidBrush(Color.White);
                //g.DrawString(strHZ, f3, b, 15, 60);//设置位置

                g.DrawString("名称：" + "香葱末", f4, b, 4, 10);//设置位置
                g.DrawString("数量：" + strQty, f4, b, 4, 30);//设置位置
                g.DrawString("保质期：" + strBZQ + "小时", f4, b, 4, 50);//设置位置
                g.DrawString("负责人：" + Globels.strName, f4, b, 4, 70);//设置位置
                g.DrawString("订单：" + Globels.strOrderNo, f4, b, 4, 90);//设置位置

                g.DrawString("日期：" + DateTime.Now.ToString("yyyy-MM-dd"), f4, b, 178, 105);//设置位置

                image.Save(strPath, ImageFormat.Jpeg);//自己创建一个文件夹，放入生成的图片（根目录下）

                //二维码图片nfc写入
                bp = new Bmp2BmpProduct(new TagViewSize((EnumTagViewSizeID)(0x21), 0x00),
                        new Bitmap(image)
                        );
                b2d.ImageYuLan(bp);

                byte[] _nfcTagBmpData = b2d.GetDataToSend(bp);
                int _nfcTagBmpDataLength = b2d.SendLength;

                nfcTag.SendBmp2NfcTag(_nfcTagBmpData, _nfcTagBmpDataLength);

                //MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("二维码生成异常！：" + ex.Message);
            }
        }

        /// <summary>
        /// 照片写入
        /// </summary>
        private void NFC()
        {
            byte[] _nfcTagBmpData = b2d.GetDataToSend(bp);
            int _nfcTagBmpDataLength = b2d.SendLength;

            nfcTag.SendBmp2NfcTag(_nfcTagBmpData, _nfcTagBmpDataLength);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetImg2("物料" + txtCode.Text + "批次" + txtBatch.Text.Trim() + "单号" + Globels.strOrderNo, txtCode.Text.Trim(), "100", "72");
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            
        }

        private void btn_Conver_Click(object sender, EventArgs e)
        {
            int nLen = dataGridView1.Rows.Count;
            for(int i = 0 ; i < nLen; i++)
            {

            }
        }

        private void txtRQQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal dSJ = Convert.ToDecimal(txtQty.Text) - Convert.ToDecimal(txtRQQty.Text);
                txtSJ.Text = dSJ.ToString();
            }
            catch
            {
                ;
            }
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal dSJ = Convert.ToDecimal(txtQty.Text) - Convert.ToDecimal(txtRQQty.Text);
                txtSJ.Text = dSJ.ToString();
            }
            catch
            {
                ;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string strSql = "select * from Mes_WorkShopWeight where W_Remark = '"+ m_strBarcode +"'";
                Mes_WorkShopWeightBLL WorkShopWeightBLL = new Mes_WorkShopWeightBLL();
                Mes_WorkShopWeightEntity WorkShopWeightEntity = new Mes_WorkShopWeightEntity();
                DataSet ds = new DataSet();
                ds = WorkShopWeightBLL.GetList_WorkShop(strSql);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string strOrderNo = ds.Tables[0].Rows[0]["W_OrderNo"].ToString();
                    string strGoodsCode = ds.Tables[0].Rows[0]["W_SecGoodsCode"].ToString();
                    string strBatch = ds.Tables[0].Rows[0]["W_SecBatch"].ToString();
                    string strQty = ds.Tables[0].Rows[0]["W_SecQty"].ToString();
                    decimal d = Convert.ToDecimal(strQty);
                    strQty = d.ToString("0.####");
                    //string strPrice = dataGridView2.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["价格2"].Value.ToString();
                    string strGoodsName = ds.Tables[0].Rows[0]["W_SecGoodsName"].ToString();
                    string strBarcode = ds.Tables[0].Rows[0]["W_Remark"].ToString();
                    //string strDate = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["W_Price"].Value.ToString();

                    if (MessageBox.Show("物料是否要补写?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            this.Enabled = false;
                            Cursor.Current = Cursors.WaitCursor;

                            GetImg("物料" + strGoodsCode + "批次" + strBatch + "单号" + Globels.strOrderNo, strGoodsName, strQty, strBZQ, strBarcode);
                            //DeleteData(strId);
                            //SaveBarcode(strBarcode, strGoodsCode, strGoodsName, Convert.ToDecimal(strQty), strWorkShop);
                            this.Enabled = true;
                            Cursor.Current = Cursors.Default;

                        }
                        catch (Exception ex)
                        {
                            this.Enabled = true;
                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("不好意思，您还没有打印过任何标签");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //lblTS.Text = "系统提示：请选中某一行进行补写";
            }
        
        
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int nLen2 = listView1.SelectedIndices[0];
                string strGoodsName = listView1.Items[nLen2].SubItems[0].Text.ToString();
                MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                var Goods_rows = GoodsBLL.GetListCondit("where G_Name = '" + strGoodsName + "'");
                int nLen = Goods_rows.Count;
                if (nLen > 0)
                {
                    txtCode.Text = Goods_rows[0].G_Code;
                    txtGoodsName.Text = Goods_rows[0].G_Name;
                    txtUnit.Text = Goods_rows[0].G_Unit;
                    int dd = Goods_rows[0].G_Period * 24;
                    strBZQ = dd.ToString();
                }
                txtBatch.Text = DateTime.Now.ToString("yyyyMMdd");
            }
            catch(Exception ex)
            {
                ;
            }
            
        }

        private void frmWorkShopWeightList_Activated(object sender, EventArgs e)
        {
            txtQty.Focus();
        }
    }
}
