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
    public partial class frmOrgres : DockContent
    {
        public frmMain frmMain { get; set; }
        //decimal Period; //保质期
        private SysUser User;
        int n_Row = 0;
        NfcTag nfcTag;
        Bmp2Bmp2Data b2d;
        Bmp2BmpProduct bp;

        public frmOrgres(frmMain _frmMain, SysUser _User)
        {
            InitializeComponent();
            frmMain = _frmMain;
            User = _User;
            //comKind.SelectedIndex = 0;//设置显示的item索引
        }

        private void frmOrgres_Load(object sender, EventArgs e)
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
                cmbWorkShopName.Items.Add(rows[i].W_Name);
            }

            if (cmbWorkShop.Items.Contains(Globels.strWorkShop))
            {
                cmbWorkShop.Text = Globels.strWorkShop;
            }

            MesRecordBLL RecordBLL = new MesRecordBLL();
            var Record_rows = RecordBLL.GetList();
            for (int i = 0; i < Record_rows.Count; i++)
            {
                cmbRecord.Items.Add(Record_rows[i].R_Record);
                cmbRecordName.Items.Add(Record_rows[i].R_Name);
            }


            Mes_ProceBLL ProceBLL = new Mes_ProceBLL();
            var row = ProceBLL.GetList_Proce(" where 1 = 1 ");
            for (int i = 0; i < row.Count; i++)
            {
                cmbProce.Items.Add(row[i].P_ProNo);
                cmbProceName.Items.Add(row[i].P_ProName);
            }
            if (cmbProce.Items.Contains(Globels.strProce))
            {
                cmbProce.Text = Globels.strProce;
            }
            


            Mes_TeamBLL TeamBLL = new Mes_TeamBLL();
            var Team_rows = TeamBLL.GetList_Team(" where 1 = 1 ");
            for (int i = 0; i < Team_rows.Count; i++)
            {
                cmbTeam.Items.Add(Team_rows[i].T_Code);
                cmbTeamName.Items.Add(Team_rows[i].T_Name);
            }
            if (cmbTeam.Items.Contains(Globels.strTeam))
            {
                cmbTeam.Text = Globels.strTeam;
            }

            string strSql = "Select distinct W_SecGoodsName from Mes_WorkShopWeight where W_WorkShopCode = '"+ cmbWorkShop.Text +"'";
            Mes_WorkShopWeightBLL WorkShopWeightBL = new Mes_WorkShopWeightBLL();
            DataSet ds = WorkShopWeightBL.GetList_WorkShop(strSql);
            int nLen = ds.Tables[0].Rows.Count;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("");
            for (int i = 0; i < nLen;i++ )
            {
                comboBox1.Items.Add(ds.Tables[0].Rows[i]["W_SecGoodsName"].ToString());
            }
                Search();
        }

        private void comOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesProductOrderHeadBLL ProductOrderHeadBLL = new MesProductOrderHeadBLL();
            var row = ProductOrderHeadBLL.GetList(comOrderNo.Text);
            txtOrderDate.Text = row[0].P_OrderDate.ToString();
        }

        private void cmbWorkShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesWorkShopBLL WorkShopBLL = new MesWorkShopBLL();
            var row = WorkShopBLL.GetData("where W_Code = '" + cmbWorkShop.Text + "'");
            cmbWorkShopName.Text = row[0].W_Name;
        }

        private void cmbRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesRecordBLL RecordBLL = new MesRecordBLL();
            var Record_rows = RecordBLL.GetData(" where R_Record = '" + cmbRecord.Text + "'");
            cmbRecordName.Text = Record_rows[0].R_Name;
                //txt .Items.Add(Record_rows[0].R_Name);
            
        }


        private void cmbProce_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mes_ProceBLL ProceBLL = new Mes_ProceBLL();
            var row = ProceBLL.GetList_Proce("where P_ProNo = '" + cmbProce.Text + "'");
            cmbProceName.Text = row[0].P_ProName;
        }

        
        private void btnScan_Click(object sender, EventArgs e)
        {
            if (comOrderNo.Text == "")
            {
                //MessageBox.Show("请先选择订单号");
                lblTS.Text = "系统提示：请先选择订单号";
                return;
            }
            if (cmbWorkShop.Text == "")
            {
                //MessageBox.Show("请先选择车间");
                lblTS.Text = "系统提示：请先选择车间";
                return;
            }
            if (cmbRecord.Text == "")
            {
                //MessageBox.Show("请先选择工艺代码");
                lblTS.Text = "系统提示：请先选择工艺代码";
                return;
            }
            if (cmbProce.Text == "")
            {
                //MessageBox.Show("请先选择工序");
                lblTS.Text = "系统提示：请先选择工序";
                return;
            }
            Globels.strOrderNo = comOrderNo.Text;
            Globels.strWorkShop = cmbWorkShop.Text;
            Globels.strWorkShopName = cmbWorkShopName.Text;
            Globels.strRecord = cmbRecord.Text;
            Globels.strRecordName = cmbRecordName.Text;
            Globels.strProce = cmbProce.Text;
            Globels.strProceName = cmbProceName.Text;

            frmWorkShopScanList frm = new frmWorkShopScanList(frmMain, frmMain.User);
            frm.ShowDialog();
            frm.Dispose();
        }

        private void btn_Weight_Click(object sender, EventArgs e)
        {

            //if (comOrderNo.Text == "")
            //{
            //    //MessageBox.Show("请先选择订单号");
            //    lblTS.Text = "系统提示：请先选择订单号";
            //    return;
            //}
            if (cmbWorkShop.Text == "")
            {
                //MessageBox.Show("请先选择车间");
                lblTS.Text = "系统提示：请先选择车间";
                return;
            }
            if (cmbTeamName.Text == "")
            {
                //MessageBox.Show("请先选择车间");
                lblTS.Text = "系统提示：请先选择班组";
                return;
            }
            //if (cmbRecord.Text == "")
            //{
            //    //MessageBox.Show("请先选择工艺代码");
            //    lblTS.Text = "系统提示：请先选择工艺代码";
            //    return;
            //}
            //if (cmbProce.Text == "")
            //{
            //    //MessageBox.Show("请先选择工序");
            //    lblTS.Text = "系统提示：请先选择工序";
            //    return;
            //}
            Globels.strOrderNo = comOrderNo.Text;
            Globels.strWorkShop = cmbWorkShop.Text;
            Globels.strWorkShopName = cmbWorkShopName.Text;
            //Globels.strRecord = cmbRecord.Text;
            //Globels.strRecordName = cmbRecordName.Text;
            Globels.strProce = cmbProce.Text;
            Globels.strProceName = cmbProceName.Text;
            Globels.strTeamName = cmbTeamName.Text;

            frmWorkShopWeightList frm = new frmWorkShopWeightList();
            frm.ShowDialog();
            frm.Dispose();

            UpdateGoods();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {

            Search();
        }

        private void Search()
        {
            UpdateGoods();
        }

        /// <summary>
        /// 二维码生成
        /// </summary>
        /// <param name="strHZ"></param>
        /// <param name="strGoodsName"></param>
        /// <param name="strQty"></param>
        public void GetImg(string strHZ, string strGoodsName, string strQty, string strGoodsCode, string strBatch,string strBarcode)
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
                int nBZQ = BZQ(strGoodsCode);
                g.DrawString("名称：" + strGoodsName, f4, b, 4, 10);//设置位置
                g.DrawString("数量：" + strQty, f4, b, 4, 30);//设置位置
                g.DrawString("保质期：" + nBZQ.ToString() + "小时", f4, b, 4, 50);//设置位置
                g.DrawString("负责人：" + Globels.strName, f4, b, 4, 70);//设置位置
                string strTeamName = "";
                if (Globels.strStockCode == "0401")
                {
                    strTeamName = "蔬菜";
                }
                else if (Globels.strStockCode == "0402")
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

        private void UpdateGoods()
        {
            //Mes_WorkShopScanBLL WorkShopScanBLL = new Mes_WorkShopScanBLL();
            Mes_WorkShopWeightBLL WorkShopWeightBL = new Mes_WorkShopWeightBLL();

            //string strSql = " where W_WorkShop = '" + cmbWorkShop.Text + "'";
            //var row = WorkShopScanBLL.GetList_WorkShopScan(strSql);
            ////if (row == null || row.Count < 1)
            ////{
            ////    untCommon.InfoMsg("没有任何数据！");
            ////    return;
            ////}
            //dataGridView1.DataSource = row;
            //int nLen = dataGridView1.Rows.Count;
            //for (int i = 0; i < nLen; i++ )
            //{
            //    dataGridView1.Rows[i].Cells["实用数量"].Value = dataGridView1.Rows[i].Cells["数量"].Value;
            //}



            string strSql = "";
            if (comboBox1.Text == "")
            {
                strSql = " where W_RecordCode = '" + cmbRecord.Text + "' and W_ProceCode = '" + cmbProce.Text + "' and W_WorkShopCode = '" + cmbWorkShop.Text + "' and W_OrderNo = '" + comOrderNo.Text + "' order by W_CreateDate desc";
            }
            else
            {
                strSql = " where W_RecordCode = '" + cmbRecord.Text + "' and W_ProceCode = '" + cmbProce.Text + "' and W_WorkShopCode = '" + cmbWorkShop.Text + "' and W_OrderNo = '" + comOrderNo.Text + "' and W_SecGoodsName = '"+ comboBox1.Text+"' order by W_CreateDate desc";
            }
            var row2 = WorkShopWeightBL.GetList_WorkShopWeight(strSql);
            //if (row2 == null || row2.Count < 1)
            //{
            //    untCommon.InfoMsg("没有任何数据！");
            //    return;
            //}
            dataGridView2.DataSource = row2;
            int nLen = dataGridView2.Columns.Count;
            for(int i = 0; i < dataGridView2.Columns.Count - 1; i++)
            {
                string strQty = dataGridView2.Rows[i].Cells["数量2"].Value.ToString();
                dataGridView2.Rows[i].Cells["数量2"].Value = Delete0(strQty);
            }
        }

        private string Delete0(string strQty)
        {
            string strTemp = "";
            string strreturn = "";
            for (int i = 0; i < strQty.Length; i++)
            {
                string str = strQty.Substring(strQty.Length - i - 1, 1);
                if(str == ".")
                {
                    strTemp = strQty.Substring(0, strQty.Length - i - 1);

                    break;
                }
                else if (str == "0")
                {
                    ;
                }
                else
                {
                    strTemp = strQty.Substring(0, strQty.Length - i);
                    break;
                }
            }
            strreturn = strTemp;
                return strreturn;
        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否要提交", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (cmbTeam.Text == "")
                    {
                        lblTS.Text = "系统提示：先选择班组";
                        return;
                    }
                    Mes_WorkShopScanBLL WorkShopScanBLL = new Mes_WorkShopScanBLL();
                    Mes_WorkShopWeightBLL WorkShopWeightBL = new Mes_WorkShopWeightBLL();

                    List<string> insertList = new List<string>();
                    string strSecGoods = "";
                    string strSecPc = "";
                    string strSecName = "";
                    string strSecUnit = "";
                    decimal dSecQty = 0;
                    int nLen2 = dataGridView2.Rows.Count;
                    for (int i = 0; i < nLen2; i++)
                    {
                        object obj = dataGridView2.Rows[i].Cells["选择2"].Value;
                        if (Convert.ToString(obj) == "True" || Convert.ToString(obj) == "1")
                        {
                            strSecGoods = dataGridView2.Rows[i].Cells["物料2"].Value.ToString();
                            strSecPc = dataGridView2.Rows[i].Cells["批次2"].Value.ToString();
                            strSecName = dataGridView2.Rows[i].Cells["物料名称2"].Value.ToString();
                            strSecUnit = dataGridView2.Rows[i].Cells["单位2"].Value.ToString();

                            string strSecQty = dataGridView2.Rows[i].Cells["数量2"].Value.ToString();
                            string strTemp = dataGridView2.Rows[i].Cells["物料2"].Value.ToString() + "," + dataGridView2.Rows[i].Cells["批次2"].Value.ToString() + "," + dataGridView2.Rows[i].Cells["数量2"].Value.ToString();
                            if (insertList.Count > 0)
                            {
                                string[] str = insertList[0].ToString().Split(',');
                                if (str[0].ToString() == strSecGoods)
                                {

                                    if (str[1].ToString() == strSecPc)
                                    {
                                        dSecQty = dSecQty + Convert.ToDecimal(strSecQty);
                                        
                                    }
                                    else
                                    {
                                        lblTS.Text = "系统提示：生成的物料只允许同一物料，同一批次";
                                        return;
                                    }
                                }
                                else
                                {
                                    lblTS.Text = "系统提示：生成的物料只允许同一物料，同一批次";
                                    return;
                                    
                                }
                            }
                            else
                            {
                                dSecQty = dSecQty + Convert.ToDecimal(strSecQty);
                                insertList.Add(strTemp);
                            }
                        }
                    }

                    List<string> Goods = new List<string>();
                    List<string> strOldGoods = new List<string>();
                    int nKind = 0; //计算有几种原物料
                    int nLen = dataGridView1.Rows.Count;
                    for (int i = 0; i < nLen; i++)
                    {
                        object obj = dataGridView1.Rows[i].Cells["选择"].Value;
                        if (Convert.ToString(obj) == "True" || Convert.ToString(obj) == "1")
                        {
                            string strGoods = dataGridView1.Rows[i].Cells["物料"].Value.ToString();
                            string strQty = dataGridView1.Rows[i].Cells["实用数量"].Value.ToString();
                            string strYLQty = dataGridView1.Rows[i].Cells["数量"].Value.ToString();

                            string strPc = dataGridView1.Rows[i].Cells["批次"].Value.ToString();
                            string strPrice = dataGridView1.Rows[i].Cells["价格"].Value.ToString();
                            string strName = dataGridView1.Rows[i].Cells["物料名称"].Value.ToString();
                            string strUnit = dataGridView1.Rows[i].Cells["单位"].Value.ToString();
                            if (Jud(strGoods, strSecGoods))
                            {
                                if(strOldGoods.Contains(strGoods))
                                {

                                }
                                else
                                {
                                    nKind = nKind + 1;
                                    strOldGoods.Add(strGoods);
                                }
                                bool bRet = false;
                                for (int j = 0; j < Goods.Count; j++)
                                {
                                    string strTemp = Goods[j].ToString();
                                    string[] str = strTemp.Split(',');
                                    string strTempGoods = str[0].ToString();
                                    string strTempPc = str[2].ToString();
                                    string strTempQty = str[3].ToString();
                                    if (strTempGoods == strGoods && strTempPc == strPc)
                                    {
                                        decimal dQty = Convert.ToDecimal(strQty) + Convert.ToDecimal(strTempQty);
                                        Goods[j] = strGoods + "," + dQty.ToString() + "," + strPc + "," + strPrice + "," + strName + "," + strUnit;
                                        bRet = true;
                                    }
                                }
                                if (bRet == false)
                                {
                                    Goods.Add(strGoods + "," + strQty + "," + strPc + "," + strPrice + "," + strName + "," + strUnit);
                                }
                            }
                            else
                            {
                                lblTS.Text = "系统提示：选择的物料生成不了下一个物料，请核对";
                                return;
                            }
                        }
                    }

                    if (!Jud2(nKind,strSecGoods))
                    {
                        lblTS.Text = "系统提示：选择的转换前的物料不够";
                        return;
                    }

                    if (insertList.Count == 0 || Goods.Count == 0)
                    {
                        lblTS.Text = "系统提示：请选择物料";
                        return;
                    }
                    Mes_OrgResHeadBLL OrgResHeadBLL = new Mes_OrgResHeadBLL();
                    Mes_OrgResDetailBLL OrgResDetailBLL = new Mes_OrgResDetailBLL();
                    Mes_OrgResHeadEntity OrgResHeadEntity = new Mes_OrgResHeadEntity();
                    Mes_OrgResDetailEntity OrgResDetailEntity = new Mes_OrgResDetailEntity();

                    string strIn_No = "";

                    MesMaterInHeadBLL MaterInHeadBLL = new MesMaterInHeadBLL();
                    strIn_No = MaterInHeadBLL.GetDH("组装与拆分单");

                    OrgResHeadEntity.O_OrgResNo = strIn_No;
                    OrgResHeadEntity.O_OrderNo = comOrderNo.Text;

                    OrgResHeadEntity.O_CreateBy = Globels.strUser;
                    OrgResHeadEntity.O_CreateDate = DateTime.Now;
                    OrgResHeadEntity.O_OrderDate = txtOrderDate.Text;
                    OrgResHeadEntity.O_Remark = "";
                    OrgResHeadEntity.O_Status = 1;
                    OrgResHeadEntity.O_WorkShopCode = cmbWorkShop.Text;
                    OrgResHeadEntity.O_WorkShopName = cmbWorkShopName.Text;
                    OrgResHeadEntity.O_Record = cmbRecord.Text;
                    OrgResHeadEntity.O_ProCode = cmbProce.Text;
                    OrgResHeadEntity.O_TeamCode = cmbTeam.Text;
                    OrgResHeadEntity.O_TeamName = cmbTeamName.Text;

                    int nRow = OrgResHeadBLL.SaveEntity("", OrgResHeadEntity);
                    decimal dSecPrice = 0;
                    decimal dTotal = 0;
                    for (int i = 0; i < Goods.Count; i++)
                    {
                        string[] strTemp = Goods[i].ToString().Split(',');
                        dTotal = dTotal + (Convert.ToDecimal(strTemp[3].ToString()) * Convert.ToDecimal(strTemp[1].ToString()));
                    }
                    dSecPrice = dTotal / dSecQty;


                    for (int i = 0; i < Goods.Count; i++)
                    {
                        OrgResDetailEntity.O_OrgResNo = strIn_No;
                        OrgResDetailEntity.O_SecGoodsCode = strSecGoods;
                        OrgResDetailEntity.O_SecGoodsName = strSecName;
                        OrgResDetailEntity.O_SecPrice = 0;
                        OrgResDetailEntity.O_SecQty = dSecQty;
                        OrgResDetailEntity.O_SecUnit = strSecUnit;
                        OrgResDetailEntity.O_SecBatch = strSecPc;

                        string[] strTemp = Goods[i].ToString().Split(',');

                        string strGoodsCode = strTemp[0].ToString();
                        Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
                        var Convert_rows = ConvertBLL.GetList_Mes_Convert(" where C_SecCode = '" + strGoodsCode + "'");
                        string strC_GoodsCode = "";
                        if (Convert_rows.Count > 0)
                        {
                            strC_GoodsCode = Convert_rows[0].C_Code;
                        }

                        OrgResDetailEntity.O_GoodsCode = strTemp[0].ToString();
                        OrgResDetailEntity.O_GoodsName = strTemp[4].ToString();
                        OrgResDetailEntity.O_Price = Convert.ToDecimal(strTemp[3].ToString());
                        OrgResDetailEntity.O_Qty = Convert.ToDecimal(strTemp[1].ToString());
                        OrgResDetailEntity.O_Unit = strTemp[5].ToString();
                        OrgResDetailEntity.O_Batch = strTemp[2].ToString();
                        OrgResDetailEntity.O_SecPrice = dSecPrice;
                        nRow = OrgResDetailBLL.SaveEntity("", OrgResDetailEntity);

                    }
                    Upload(strIn_No);
                    MessageBox.Show("保存成功");
                    lblTS.Text = "";
                    //UpdatePrice(strSecGoods, dSecPrice);  已经在存储过程中处理
                    Delete();
                    UpdateGoods();
                }
                catch(Exception ex)
                {
                    lblTS.Text = "系统提示：" + ex.ToString();
                }
            }
            
        }

        /// <summary>
        /// 审核，提交单据 以前在网页端
        /// </summary>
        private void Upload(string strDH)
        {
            Mes_OrgResHeadBLL OrgResHeadBLL = new Mes_OrgResHeadBLL();
            OrgResHeadBLL.SH(strDH);
            OrgResHeadBLL.UPLOAD(strDH, Globels.strUser);
        }

        /// <summary>
        /// 判断单个物料是否是否匹配
        /// </summary>
        /// <param name="strGoods"></param>
        /// <param name="strSecGoods"></param>
        /// <returns></returns>
        private bool Jud(string strGoods,string strSecGoods)
        {
            Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
            var row = ConvertBLL.GetList_Mes_Convert("where C_Code = '" + strGoods + "' and C_SecCode = '"+ strSecGoods +"'");
            if (row.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 判断生成品是否由几个商品生成的数量
        /// </summary>
        /// <param name="strGoods"></param>
        /// <param name="strSecGoods"></param>
        /// <returns></returns>
        private bool Jud2(int nKind, string strSecGoods)
        {
            
            Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
            var row = ConvertBLL.GetList_Mes_Convert("where C_SecCode = '" + strSecGoods + "'");
            if (row.Count > 0)
            {
                if (nKind == row.Count)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private void cmbTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mes_TeamBLL TeamBLL = new Mes_TeamBLL();
            var Team_rows = TeamBLL.GetList_Team(" where T_Code = '"+ cmbTeam.Text +"'");
            if(Team_rows.Count > 0)
            {
                cmbTeamName.Text = Team_rows[0].T_Name;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Globels.strWorkShop = cmbWorkShop.Text;
            Globels.strWorkShopName = cmbWorkShopName.Text;
            frmBackStock frm = new frmBackStock();
            frm.ShowDialog();
            frm.Dispose();


            //Globels.strWorkShop = cmbWorkShop.Text;

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
            BarcodeBLL.SaveEntity("", BarcodeEntity);

        }

        private bool Delete()
        {
            try
            {
                int nLen = dataGridView1.Rows.Count;
                for (int i = 0; i < nLen; i++)
                {
                    object obj = dataGridView1.Rows[i].Cells["选择"].Value;
                    if (Convert.ToString(obj) == "True" || Convert.ToString(obj) == "1")
                    {
                        decimal dsyQty = Convert.ToDecimal(dataGridView1.Rows[i].Cells["实用数量"].Value.ToString());
                        decimal dylQty = Convert.ToDecimal(dataGridView1.Rows[i].Cells["数量"].Value.ToString());
                        if (dsyQty == dylQty)
                        {
                            string strID = dataGridView1.Rows[i].Cells["ID"].Value.ToString();
                            DeleteData(strID);
                        }
                        else
                        {
                            decimal dQty = dylQty - dsyQty;
                            string strID = dataGridView1.Rows[i].Cells["ID"].Value.ToString();
                            UpdateData(strID, dQty);
                        }
                    }
                }

                int nLen2 = dataGridView2.Rows.Count;
                for (int i = 0; i < nLen2; i++)
                {
                    object obj = dataGridView2.Rows[i].Cells["选择2"].Value;
                    if (Convert.ToString(obj) == "True" || Convert.ToString(obj) == "1")
                    {
                        string strID = dataGridView2.Rows[i].Cells["ID2"].Value.ToString();
                        DeleteWeightData(strID);
                    }
                }

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        private void UpdatePrice(string GoodsCode, decimal dPrice)
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
            catch(Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 删除ESL标签条码
        /// </summary>
        /// <param name="strId"></param>
        /// <returns></returns>
        private bool DeleteBarcode(string strBarcode)
        {
            try
            {
                string strSql = "delete from Mes_Barcode where B_Barcode = '" + strBarcode + "'";
                Mes_BarcodeBLL BarcodeBLL = new Mes_BarcodeBLL();
                int nRow = BarcodeBLL.Update(strSql);
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

        private bool DeleteWeightData(string strId)
        {
            try
            {
                Mes_WorkShopWeightBLL WorkShopWeightBLL = new Mes_WorkShopWeightBLL();
                int nRow = WorkShopWeightBLL.DeleteEntity(strId);
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

        private bool UpdateData(string strId,decimal dQty)
        {
            try
            {
                Mes_WorkShopScanBLL WorkShopScanBLL = new Mes_WorkShopScanBLL();
                int nRow = WorkShopScanBLL.UpdateEntity(strId, dQty);
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

        private void cmbWorkShopName_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesWorkShopBLL WorkShopBLL = new MesWorkShopBLL();
            var row = WorkShopBLL.GetData("where W_Name = '" + cmbWorkShopName.Text + "'");
            cmbWorkShop.Text = row[0].W_Code;
        }

        private void cmbTeamName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mes_TeamBLL TeamBLL = new Mes_TeamBLL();
            var Team_rows = TeamBLL.GetList_Team(" where T_Name = '" + cmbTeamName.Text + "'");
            if (Team_rows.Count > 0)
            {
                cmbTeam.Text = Team_rows[0].T_Code;
            }
        }

        private void cmbRecordName_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesRecordBLL RecordBLL = new MesRecordBLL();
            var Record_rows = RecordBLL.GetData(" where R_Name = '" + cmbRecordName.Text + "'");
            cmbRecord.Text = Record_rows[0].R_Record;
        }

        private void cmbProceName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mes_ProceBLL ProceBLL = new Mes_ProceBLL();
            var row = ProceBLL.GetList_Proce("where P_ProName = '" + cmbProceName.Text + "'");
            cmbProce.Text = row[0].P_ProNo;
        }

        private void btnPrintf_Click(object sender, EventArgs e)
        {
            try
            {
                


                string strOrderNo = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["生产订单2"].Value.ToString();
                string strWorkShop = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["车间2"].Value.ToString();
                string strGoodsCode = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["物料2"].Value.ToString();
                string strBatch = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["批次2"].Value.ToString();
                string strQty = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["数量2"].Value.ToString();
                decimal d = Convert.ToDecimal(strQty);
                strQty = d.ToString("0.####");
                //string strPrice = dataGridView2.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["价格2"].Value.ToString();
                string strGoodsName = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["物料名称2"].Value.ToString();
                string strUnit = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["单位2"].Value.ToString();
                string strId = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["Id2"].Value.ToString();
                string strBarcode = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["备注2"].Value.ToString();
                //string strDate = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["W_Price"].Value.ToString();

                if (MessageBox.Show("物料是否要补写?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        this.Enabled = false;
                        Cursor.Current = Cursors.WaitCursor;
                        
                        GetImg("物料" + strGoodsCode + "批次" + strBatch + "单号" + Globels.strOrderNo, strGoodsName, strQty, strGoodsCode, strBatch,strBarcode);
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
            catch (Exception ex)
            {
                //MessageBox.Show("请选中某一行进行退仓库");
                lblTS.Text = "系统提示：请选中某一行进行补写";
            }
        }

        private int BZQ(string strGoodsCode)
        {
            int dd = 0;
            MesGoodsBLL GoodsBLL = new MesGoodsBLL();
            var Goods_rows = GoodsBLL.GetListCondit("where G_Code = '" + strGoodsCode + "'");
            int nLen = Goods_rows.Count;
            if (nLen > 0)
            {
                dd = Goods_rows[0].G_Period * 24;

            }
            return dd;
        }

        private void btnResolve_Click(object sender, EventArgs e)
        {
            frmResolve frm = new frmResolve();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            

        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            //n_Row = dataGridView2.SelectedCells[0].RowIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mes_WorkShopScanBLL WorkShopScanBLL = new Mes_WorkShopScanBLL();
            Mes_WorkShopWeightBLL WorkShopWeightBL = new Mes_WorkShopWeightBLL();

            List<string> insertList = new List<string>();
            string strID = "";
            string strSecGoods = "";
            string strSecPc = "";
            string strSecName = "";
            string strSecUnit = "";
            decimal dSecQty = 0;
            int nLen2 = dataGridView2.Rows.Count;
            for (int i = 0; i < nLen2; i++)
            {
                object obj = dataGridView2.Rows[i].Cells["选择2"].Value;
                if (Convert.ToString(obj) == "True" || Convert.ToString(obj) == "1")
                {
                    if (strID == "")
                    {
                        strID = dataGridView2.Rows[i].Cells["ID2"].Value.ToString();
                    }
                    else
                    {
                        strID = strID + "," + dataGridView2.Rows[i].Cells["ID2"].Value.ToString();
                    }
                    strSecGoods = dataGridView2.Rows[i].Cells["物料2"].Value.ToString();
                    strSecPc = dataGridView2.Rows[i].Cells["批次2"].Value.ToString();
                    strSecName = dataGridView2.Rows[i].Cells["物料名称2"].Value.ToString();
                    strSecUnit = dataGridView2.Rows[i].Cells["单位2"].Value.ToString();

                    string strSecQty = dataGridView2.Rows[i].Cells["数量2"].Value.ToString();
                    string strTemp = dataGridView2.Rows[i].Cells["物料2"].Value.ToString() +"," + dataGridView2.Rows[i].Cells["批次2"].Value.ToString() + "," + dataGridView2.Rows[i].Cells["数量2"].Value.ToString();
                    if (insertList.Count > 0)
                    {
                        string[] str = insertList[0].ToString().Split(',');
                        if (str[0].ToString() == strSecGoods)
                        {

                            if (str[1].ToString() == strSecPc)
                            {
                                dSecQty = dSecQty + Convert.ToDecimal(strSecQty);

                            }
                            else
                            {
                                lblTS.Text = "系统提示：生成的物料只允许同一物料，同一批次";
                                return;
                            }
                        }
                        else
                        {
                            lblTS.Text = "系统提示：生成的物料只允许同一物料，同一批次";
                            return;

                        }
                    }
                    else
                    {
                        dSecQty = dSecQty + Convert.ToDecimal(strSecQty);
                        insertList.Add(strTemp);
                    }
                }
            }
            Globels.strGoodsMessase = strSecGoods + "," + strSecName + "," + strSecPc + "," + dSecQty + "," + strSecUnit ;
            Globels.strID = strID;

            Globels.strWorkShop = cmbWorkShop.Text;
            Globels.strWorkShopName = cmbWorkShopName.Text;
            Globels.strProce = cmbProce.Text;
            Globels.strTeam = cmbTeam.Text;
            Globels.strTeamName = cmbTeamName.Text;

            frmGoodsConvet frm = new frmGoodsConvet();
            frm.ShowDialog();
            frm.Dispose();

            UpdateGoods();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {



                
                string strId = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["Id2"].Value.ToString();
                string strBarcode = dataGridView2.Rows[dataGridView2.SelectedCells[0].RowIndex].Cells["备注2"].Value.ToString();
                //string strDate = dataGridView1.Rows[dataGridView1.SelectedCells[0].RowIndex].Cells["W_Price"].Value.ToString();

                if (MessageBox.Show("物料是否要删除?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        this.Enabled = false;
                        Cursor.Current = Cursors.WaitCursor;
                        DeleteWeightData(strId);
                        DeleteBarcode(strBarcode);

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
                lblTS.Text = "系统提示：请选中某一行进行补写";
            }
        }

     


      

        
    }
}
