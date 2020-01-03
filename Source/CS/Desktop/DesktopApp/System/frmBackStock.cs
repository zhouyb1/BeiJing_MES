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
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmBackStock : DockContent
    {
        NfcTag nfcTag;
        Bmp2Bmp2Data b2d;
        Bmp2BmpProduct bp;
        public frmBackStock()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                //string strOrderNo = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["生产订单号"].Value.ToString();
                string strWorkShop = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["车间"].Value.ToString();
                string strGoodsCode = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["物料"].Value.ToString();
                string strBatch = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["批次"].Value.ToString();
                string strQty = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["数量"].Value.ToString();
                string strPrice = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["价格"].Value.ToString();
                string strGoodsName = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["物料名称"].Value.ToString();
                string strUnit = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["单位"].Value.ToString();
                string strId = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["Id"].Value.ToString();
                string strStockCode = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["仓库编码"].Value.ToString();
                string strStockName = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["仓库名称"].Value.ToString();
                //string strDate = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["W_Price"].Value.ToString();

                if (MessageBox.Show("物料是否要退回仓库?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    Mes_OutWorkShopHeadBLL OutWorkShopHeadBLL = new Mes_OutWorkShopHeadBLL();

                    try
                    {
                        this.Enabled = false;
                        Cursor.Current = Cursors.WaitCursor;



                        Mes_OutWorkShopDetailBLL OutWorkShopDetailBLL = new Mes_OutWorkShopDetailBLL();
                        Mes_OutWorkShopHeadEntity OutWorkShopHeadEntity = new Mes_OutWorkShopHeadEntity();
                        Mes_OutWorkShopDetailEntity OutWorkShopDetailEntity = new Mes_OutWorkShopDetailEntity();

                        string strIn_No = "";


                        MesMaterInHeadBLL MaterInHeadBLL = new MesMaterInHeadBLL();
                        strIn_No = MaterInHeadBLL.GetDH("线边仓出库到车间单");

                        //var rowsHead = OutWorkShopHeadBLL.GetList_OutWorkShopHead("where 1 = 1 order by O_OutNo DESC");
                        //if (rowsHead == null || rowsHead.Count < 1)
                        //{
                        //    strIn_No = "OW" + DateTime.Now.ToString("yyyyMMdd") + "000001";
                        //}
                        //else
                        //{
                        //    string strDate = rowsHead[0].O_OutNo.Substring(2, 8);
                        //    if (strDate == DateTime.Now.ToString("yyyyMMdd"))
                        //    {
                        //        string strList = rowsHead[0].O_OutNo.Substring(10, 4);
                        //        int nList = Convert.ToInt32(strList) + 1;
                        //        strIn_No = "OW" + DateTime.Now.ToString("yyyyMMdd") + nList.ToString().PadLeft(4, '0');
                        //    }
                        //    else
                        //    {
                        //        strIn_No = "OW" + DateTime.Now.ToString("yyyyMMdd") + "000001";
                        //    }

                        //}

                        OutWorkShopHeadEntity.O_OutNo = strIn_No;
                        OutWorkShopHeadEntity.O_OrderNo = "";
                        OutWorkShopHeadEntity.O_StockCode = strStockCode;
                        OutWorkShopHeadEntity.O_StockName = strStockName;
                        OutWorkShopHeadEntity.O_CreateBy = Globels.strUser;
                        OutWorkShopHeadEntity.O_CreateDate = DateTime.Now;
                        OutWorkShopHeadEntity.O_OrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        OutWorkShopHeadEntity.O_Remark = "";
                        OutWorkShopHeadEntity.O_Status = 1;
                        OutWorkShopHeadEntity.O_WorkShop = txtWorkShop.Text;
                        OutWorkShopHeadEntity.O_Kind = 2;



                        int nRow = OutWorkShopHeadBLL.SaveEntity("", OutWorkShopHeadEntity);

                        //for (int i = 0; i < rows.Count; i++)
                        //{
                        OutWorkShopDetailEntity.O_GoodsCode = strGoodsCode;
                        OutWorkShopDetailEntity.O_GoodsName = strGoodsName;
                        OutWorkShopDetailEntity.O_OutNo = strIn_No;
                        OutWorkShopDetailEntity.O_Price = Convert.ToDouble(strPrice);
                        OutWorkShopDetailEntity.O_Qty = Convert.ToDouble(strQty);
                        OutWorkShopDetailEntity.O_Remark = "";
                        OutWorkShopDetailEntity.O_Unit = strUnit;
                        OutWorkShopDetailEntity.O_Batch = strBatch;

                        nRow = OutWorkShopDetailBLL.SaveEntity("", OutWorkShopDetailEntity);

                        //}
                        MessageBox.Show("保存成功");
                        Upload(strIn_No);
                        string Barcode = strGoodsCode + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        //GetImg("物料" + strGoodsCode + "批次" + strBatch + "单号" + Globels.strOrderNo, strGoodsName, strQty, strGoodsCode, strBatch, Barcode);
                        //SaveBarcode(Barcode, strGoodsCode, strGoodsName, OutWorkShopDetailEntity.O_Qty, OutWorkShopHeadEntity.O_WorkShop);
                        DeleteData(strId);

                        UpdateGoods();

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
            catch (Exception ex)
            {
                //MessageBox.Show("请选中某一行进行退仓库");
                lblTS.Text = "系统提示：请选中某一行进行退仓库";
            }
        }

        /// <summary>
        /// 审核，提交单据 以前在网页端
        /// </summary>
        private void Upload(string strDH)
        {
            Mes_OutWorkShopHeadBLL OutWorkShopHeadBLL = new Mes_OutWorkShopHeadBLL();
            OutWorkShopHeadBLL.SH(strDH);
            OutWorkShopHeadBLL.UPLOAD(strDH, Globels.strUser);
        }

        /// <summary>
        /// 保存标签条码
        /// </summary>
        /// <param name="B_Barcode"></param>
        /// <param name="B_Code"></param>
        /// <param name="B_Name"></param>
        /// <param name="B_Qty"></param>
        /// <param name="B_WorkShopCode"></param>
        private void SaveBarcode(string B_Barcode, string B_Code, string B_Name, Double B_Qty, string B_WorkShopCode)
        {
            Mes_BarcodeEntity BarcodeEntity = new Mes_BarcodeEntity();
            Mes_BarcodeBLL BarcodeBLL = new Mes_BarcodeBLL();
            BarcodeEntity.B_Barcode = B_Barcode;
            BarcodeEntity.B_Code = B_Code;
            BarcodeEntity.B_Name = B_Name;
            BarcodeEntity.B_Qty = B_Qty;
            BarcodeEntity.B_WorkShopCode = B_WorkShopCode;
            BarcodeEntity.B_Ptime = DateTime.Now;
            BarcodeEntity.B_Status = 1;
            BarcodeBLL.SaveEntity("", BarcodeEntity);

        }

        

        private void UpdatePrice(string GoodsCode, Double dPrice)
        {
            //修改价格物价加权平局价格
            MesGoodsBLL GoodsBLL = new MesGoodsBLL();
            GoodsBLL.UpdateEntity(GoodsCode, dPrice);
        }


        private bool DeleteData(string strId)
        {
            try
            {
                Mes_WorkShopScanBLL WorkShopScanBLL = new Mes_WorkShopScanBLL();
                int nRow = WorkShopScanBLL.DeleteEntity(strId);
                if (nRow > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void frmBackStock_Load(object sender, EventArgs e)
        {
            txtWorkShop.Text = Globels.strWorkShop;
            txtWorkShopName.Text = Globels.strWorkShopName;

            Thread.Sleep(100);
            UpdateGoods();
        }

        private void UpdateGoods()
        {
            Mes_WorkShopScanBLL WorkShopScanBLL = new Mes_WorkShopScanBLL();

            string strSql = " where W_WorkShop = '" + txtWorkShop.Text + "'";
            var row = WorkShopScanBLL.GetList_WorkShopScan(strSql);
            //if (row == null || row.Count < 1)
            //{
            //    untCommon.InfoMsg("没有任何数据！");
            //    return;
            //}
            dataGridView1.DataSource = row;
            int nLen = dataGridView1.Rows.Count;
            for (int i = 0; i < nLen; i++)
            {
                dataGridView1.Rows[i].Cells["实用数量"].Value = dataGridView1.Rows[i].Cells["数量"].Value;
            }
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            UpdateGoods();
        }

        /// <summary>
        /// 二维码生成
        /// </summary>
        /// <param name="strHZ"></param>
        /// <param name="strGoodsName"></param>
        /// <param name="strQty"></param>
        public void GetImg(string strHZ, string strGoodsName, string strQty, string strGoodsCode, string strBatch, string strBarcode)
        {
            try
            {
                b2d = new Bmp2Bmp2Data();
                nfcTag = new NfcTag(new WI());

                string strPath = Application.StartupPath + "\\img\\" + strHZ + ".bmp";

                strHZ = "物料:" + strGoodsCode + ",批次:" + strBatch + ",数量:" + strQty + ",单号:" + Globels.strOrderNo + "," + strBarcode;
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
                g.DrawString("负责人：" + Globels.strName, f4, b, 4, 70);//设置位置
                //g.DrawString("订单：" + comOrderNo.Text, f4, b, 4, 90);//设置位置

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
