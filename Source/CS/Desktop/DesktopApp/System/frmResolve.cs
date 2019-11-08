﻿using Business.System;
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

namespace DesktopApp
{
    public partial class frmResolve : Form
    {

        NfcTag nfcTag;
        Bmp2Bmp2Data b2d;
        Bmp2BmpProduct bp;

        public frmResolve()
        {
            InitializeComponent();
        }

        

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyValue == 13)
                {
                    string strBarcode = txtBarcode.Text;
                    
                        string[] strTemp = strBarcode.Split(',');


                        //MessageBox.Show(strTemp.Length.ToString());
                        txtCode.Text = Resolve(strTemp[0].ToString());
                        txtBatch.Text = Resolve(strTemp[1].ToString());
                        txtQty.Text = Resolve(strTemp[2].ToString());

                        MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                        var Goods_rows = GoodsBLL.GetListCondit("where G_Code = '" + txtCode.Text + "'");
                        int nLen = Goods_rows.Count;
                        if (nLen > 0)
                        {
                            txtName.Text = Goods_rows[0].G_Name;
                            txtUnit.Text = Goods_rows[0].G_Unit;
                            //txtPrice.Text = Goods_rows[0].G_Price.ToString();

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

        private void btnResolve_Click(object sender, EventArgs e)
        {
            if(txtResolveQty.Text == "")
            {
                lblTS.Text = "请输入分写数量";
            }
            try
            {
                decimal dQty = Convert.ToDecimal(txtQty.Text);
                decimal dResolveQty = Convert.ToDecimal(txtResolveQty.Text);
                decimal dNextQty = dQty - dResolveQty;
                if(dNextQty < 0)
                {
                    lblTS.Text = "补写标签的数量不能大于原来标签数量";
                }

                GetImg("物料" + txtCode.Text + "批次" + txtBatch.Text + "单号" + Globels.strOrderNo, txtName.Text, dResolveQty.ToString(), txtCode.Text, txtBatch.Text);

                MessageBox.Show("请拿开第一张卡，放置第二张卡");

                GetImg("物料" + txtCode.Text + "批次" + txtBatch.Text + "单号" + Globels.strOrderNo, txtName.Text, dNextQty.ToString(), txtCode.Text, txtBatch.Text);

            }
            catch(Exception ex)
            {
                lblTS.Text = ex.ToString();
            }

            //decimal dQty = 
        }

        /// <summary>
        /// 二维码生成
        /// </summary>
        /// <param name="strHZ"></param>
        /// <param name="strGoodsName"></param>
        /// <param name="strQty"></param>
        public void GetImg(string strHZ, string strGoodsName, string strQty, string strGoodsCode, string strBatch)
        {
            try
            {
                b2d = new Bmp2Bmp2Data();
                nfcTag = new NfcTag(new WI());

                string strPath = Application.StartupPath + "\\img\\" + strHZ + ".bmp";

                strHZ = "物料:" + strGoodsCode + ",批次:" + strBatch + ",数量:" + strQty + ",单号:" + Globels.strOrderNo;
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
                g.DrawString("订单：", f4, b, 4, 90);//设置位置

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
                //untCommon.ErrorMsg("二维码生成异常！：" + ex.Message);
                lblTS.Text = "系统提示：" + "二维码生成异常！：" + ex.Message;
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