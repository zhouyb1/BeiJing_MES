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
using Business.System;
using Model;
using System.Threading;
using System.IO;
using System.Drawing.Imaging;
using Tag;
using USC.BmpLib.BMP;
using USC.Tools;
using USC.PCD;

namespace DesktopApp
{
    public partial class frmVegetableProcessList : DockContent 
    {
        public frmMain frmMain { get; set; }
        string Period; //保质期
        private SysUser User;
        NfcTag nfcTag;
        Bmp2Bmp2Data b2d;
        Bmp2BmpProduct bp;

        public frmVegetableProcessList(frmMain _frmMain, SysUser _User)
        {
            InitializeComponent();
            frmMain = _frmMain;
            User = _User;
            comKind.SelectedIndex = 0;//设置显示的item索引
        }

        private void frmVegetableProcessList_Load(object sender, EventArgs e)
        {
            b2d = new Bmp2Bmp2Data();
            nfcTag = new NfcTag(new WI());
        }

        private void btnWeigh_Click(object sender, EventArgs e)
        {
            Random ran = new Random();
            int RandKey = ran.Next(0, 9999);
            txtQty.Text = RandKey.ToString();
            if (comKind.Text.Trim() == "蔬菜去皮前")
            {
                GetImg("物料" + txtGoodsCode.Text.Trim() + "批次" + txtBatch.Text.Trim() + "单号" + txtOrderNo.Text.Trim(), txtGoodsName.Text.Trim(), txtQty.Text.Trim());
            }
            addWeighStorage();
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
                string strPath = Application.StartupPath + "\\img\\" + strHZ + ".bmp";
                strHZ = "物料：" + txtGoodsCode.Text.Trim() + ",批次：" + txtBatch.Text.Trim() + ",单号：" + txtOrderNo.Text.Trim();
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
                g.DrawString("保质期：" + Period, f4, b, 4, 50);//设置位置
                g.DrawString("负责人：" + User.F_Account, f4, b, 4, 70);//设置位置
                g.DrawString("订单：" + txtOrderNo.Text.Trim(), f4, b, 4, 90);//设置位置

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

        /// <summary>
        /// 称重记录保存
        /// </summary>
        private void addWeighStorage()
        {
            try
            {
                MesWeightRecordBLL MesWeightRecordBLL = new MesWeightRecordBLL();
                if (checkInput())
                {
                    MesMaterInDetailBLL MaterInDetailBLL = new MesMaterInDetailBLL();
                    var rows = MaterInDetailBLL.GetList_GoodsCode(txtGoodsCode.Text);
                    if (rows[0].M_GoodsCode == txtGoodsCode.Text && rows[0].M_GoodsName == txtGoodsName.Text && rows[0].M_Qty.ToString() == txtQty.Text)
                    {
                        untCommon.InfoMsg("称重记录数据错误！");
                        return;
                    }

                    MesWeightRecordEntity MesWeightRecord = new MesWeightRecordEntity();

                    MesWeightRecord.P_OrderNo = txtOrderNo.Text;
                    MesWeightRecord.W_Kind = comKind.Text;
                    MesWeightRecord.W_Date = DateTime.Now;
                    MesWeightRecord.W_GoodsCode = txtGoodsCode.Text;
                    MesWeightRecord.W_GoodsName = txtGoodsName.Text;
                    MesWeightRecord.W_Batch = txtBatch.Text;
                    MesWeightRecord.W_Qty = decimal.Parse(txtQty.Text);
                    MesWeightRecord.W_Unit = txtUnit.Text;

                    if (MesWeightRecordBLL.SaveEntity("", MesWeightRecord) > 0)
                    {
                        untCommon.InfoMsg("称重记录添加成功！");
                    }
                    else
                    {
                        untCommon.InfoMsg("称重记录添加失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("设备管理添加数据异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 校验输入
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            if (string.IsNullOrEmpty(txtGoodsCode.Text.Trim()))
            {
                untCommon.InfoMsg("物料编码不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(txtGoodsName.Text))
            {
                untCommon.InfoMsg("物料名称不能为空！");
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

            if (string.IsNullOrEmpty(comKind.Text))
            {
                untCommon.InfoMsg("称重类型不能为空！");
                return false;
            }

            return true;
        }

        private void txtOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
            {
                MesProductOrderHeadBLL MesProductOrderHeadBLL = new MesProductOrderHeadBLL();
                var rows = MesProductOrderHeadBLL.GetList(txtOrderNo.Text.Trim());
                if (rows == null || rows.Count < 1)
                {
                    untCommon.InfoMsg("输入的生产订单号错误，请重新输入！");
                    txtOrderNo.Focus();
                    txtOrderNo.SelectAll();
                }
                else
                {
                    txtGoodsCode.Focus();
                }
            }
        }

        private void txtGoodsCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
            {
                MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                //MesMaterInDetailEntity MaterInDetail = new MesMaterInDetailEntity();
                var Goods_rows = GoodsBLL.GetList(txtGoodsCode.Text.Trim(), "");
                if (Goods_rows == null || Goods_rows.Count < 1)
                {
                    untCommon.InfoMsg("输入的物料编码错误，请重新输入！");
                }
                else
                {
                    txtGoodsCode.Text = Goods_rows[0].G_Code;
                    txtGoodsName.Text = Goods_rows[0].G_Name;
                    txtUnit.Text = Goods_rows[0].G_Unit;
                    Period = Goods_rows[0].G_Period;
                    txtBatch.Focus();
                }
            }
        }

        private void txtGoodsName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
            {
                MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                //MesMaterInDetailEntity MaterInDetail = new MesMaterInDetailEntity();
                var Goods_rows = GoodsBLL.GetList("", txtGoodsName.Text.Trim());
                if (Goods_rows == null || Goods_rows.Count < 1)
                {
                    untCommon.InfoMsg("输入的物料名称错误，请重新输入！");
                }
                else
                {
                    txtGoodsCode.Text = Goods_rows[0].G_Code;
                    txtGoodsName.Text = Goods_rows[0].G_Name;
                    txtUnit.Text = Goods_rows[0].G_Unit;
                    Period = Goods_rows[0].G_Period;
                    txtBatch.Focus();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            addWeighStorage();
        }
    }
}
