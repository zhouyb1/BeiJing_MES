using Business.System;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public frmWorkShopWeightList()
        {
            InitializeComponent();
        }

        private void frmWorkShopWeightList_Load(object sender, EventArgs e)
        {
            

            Mes_WorkShopScanBLL WorkShopScanBLL = new Mes_WorkShopScanBLL();

            var Scan_rows = WorkShopScanBLL.GetList_WorkShopScan(" where W_WorkShop = '"+ Globels.strWorkShop +"'");
            for (int i = 0; i < Scan_rows.Count; i++)
            {
                string strGoodsCode = Scan_rows[i].W_GoodsCode;
                Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
                var Convert_rows = ConvertBLL.GetList_Mes_Convert(" where C_Code = '"+ strGoodsCode +"'");
                for (int j = 0; j < Convert_rows.Count; j++)
                {
                    cmbGoodsCode.Items.Add(Convert_rows[j].C_SecCode);
                }
            }


        }

        private void cmbGoodsCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesGoodsBLL GoodsBLL = new MesGoodsBLL();
            var Goods_rows = GoodsBLL.GetListCondit("where G_Code = '" + cmbGoodsCode.Text + "'");
            int nLen = Goods_rows.Count;
            if (nLen > 0)
            {
                txtName.Text = Goods_rows[0].G_Name;
                txtUnit.Text = Goods_rows[0].G_Unit;
            }
            txtBatch.Text = DateTime.Now.ToString("yyyyMMdd");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbGoodsCode.Text == "")
            {
                MessageBox.Show("请先选择物料");

                return;

            }
            else
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

                try
                {
                    this.Enabled = false;
                    Cursor.Current = Cursors.WaitCursor;
                    

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
                    WorkShopWeightEntity.W_SecGoodsCode = cmbGoodsCode.Text;
                    WorkShopWeightEntity.W_SecGoodsName = txtName.Text;
                    WorkShopWeightEntity.W_SecQty = Convert.ToDecimal(txtQty.Text) - Convert.ToDecimal(txtRQQty.Text);
                    WorkShopWeightEntity.W_SecUnit = txtUnit.Text;
                    WorkShopWeightEntity.W_Status = 1;
                    WorkShopWeightEntity.W_WorkShopCode = Globels.strWorkShop;

                    int nCount = WorkShopWeightBLL.SaveEntity("", WorkShopWeightEntity);
                    if (nCount > 0)
                    {
                        Decimal dTemp = Convert.ToDecimal(txtQty.Text) - Convert.ToDecimal(txtRQQty.Text);
                        GetImg("物料" + cmbGoodsCode.Text.Trim() + "批次" + txtBatch.Text.Trim() + "单号" + Globels.strOrderNo, txtName.Text.Trim(), dTemp.ToString());
                        MessageBox.Show("添加成功");
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

        private bool IsNumberic(string oText)
        {
            try
            {
                Decimal var1 = Convert.ToDecimal(oText);
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
                            char[] arr = strWeight[nLen - 1].Substring(0,7).ToCharArray();
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
        public void GetImg(string strHZ, string strGoodsName, string strQty)
        {
            try
            {
                b2d = new Bmp2Bmp2Data();
                nfcTag = new NfcTag(new WI());

                string strPath = Application.StartupPath + "\\img\\" + strHZ + ".bmp";
                strHZ = "物料:" + cmbGoodsCode.Text.Trim() + ",批次:" + txtBatch.Text.Trim() + ",数量:" + strQty + ",单号:" + Globels.strOrderNo;
                int nLen = strHZ.Length;
                byte[] fileData = Encoding.GetEncoding("GB2312").GetBytes(strHZ);
                int nLen2 = fileData.Length;

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

                g.DrawString("名称：" + strGoodsName, f4, b, 4, 10);//设置位置
                g.DrawString("数量：" + strQty, f4, b, 4, 30);//设置位置
                g.DrawString("保质期：" + "24小时", f4, b, 4, 50);//设置位置
                g.DrawString("负责人：" + Globels.strUser, f4, b, 4, 70);//设置位置
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
    }
}
