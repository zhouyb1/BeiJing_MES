using Business.System;
using Model;
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
    public partial class frmOrgres : DockContent
    {
        public frmMain frmMain { get; set; }
        //decimal Period; //保质期
        private SysUser User;

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
            }

            MesRecordBLL RecordBLL = new MesRecordBLL();
            var Record_rows = RecordBLL.GetList();
            for (int i = 0; i < Record_rows.Count; i++)
            {
                cmbRecord.Items.Add(Record_rows[i].R_Record);
            }
        }

        private void comOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbWorkShop_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesWorkShopBLL WorkShopBLL = new MesWorkShopBLL();
            var row = WorkShopBLL.GetData("where W_Code = '" + cmbWorkShop.Text + "'");
            txtWorkShopName.Text = row[0].W_Name;
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
                //txt .Items.Add(Record_rows[0].R_Name);
            
        }


        private void cmbProce_SelectedIndexChanged(object sender, EventArgs e)
        {
            Mes_ProceBLL ProceBLL = new Mes_ProceBLL();
            var row = ProceBLL.GetList_Proce("where P_RecordCode = '" + cmbRecord.Text + "' and P_ProNo = '" + cmbProce.Text + "'");
            txtProceName.Text = row[0].P_ProNo;
        }

        
        private void btnScan_Click(object sender, EventArgs e)
        {
            if (comOrderNo.Text == "")
            {
                MessageBox.Show("请先选择订单号");
                return;
            }
            if (cmbWorkShop.Text == "")
            {
                MessageBox.Show("请先选择车间");
                return;
            }
            if (cmbRecord.Text == "")
            {
                MessageBox.Show("请先选择工艺代码");
                return;
            }
            if (cmbProce.Text == "")
            {
                MessageBox.Show("请先选择工序");
                return;
            }
            Globels.strOrderNo = comOrderNo.Text;
            Globels.strWorkShop = cmbWorkShop.Text;
            Globels.strWorkShopName = txtWorkShopName.Text;
            Globels.strRecord = cmbRecord.Text;
            Globels.strRecordName = txtRecordName.Text;
            Globels.strProce = cmbProce.Text;
            Globels.strProceName = txtProceName.Text;

            frmWorkShopScanList frm = new frmWorkShopScanList(frmMain, frmMain.User);
            frm.ShowDialog();
            frm.Dispose();
        }

        private void btn_Weight_Click(object sender, EventArgs e)
        {

            if (comOrderNo.Text == "")
            {
                MessageBox.Show("请先选择订单号");
                return;
            }
            if (cmbWorkShop.Text == "")
            {
                MessageBox.Show("请先选择车间");
                return;
            }
            if (cmbRecord.Text == "")
            {
                MessageBox.Show("请先选择工艺代码");
                return;
            }
            if (cmbProce.Text == "")
            {
                MessageBox.Show("请先选择工序");
                return;
            }
            Globels.strOrderNo = comOrderNo.Text;
            Globels.strWorkShop = cmbWorkShop.Text;
            Globels.strWorkShopName = txtWorkShopName.Text;
            Globels.strRecord = cmbRecord.Text;
            Globels.strRecordName = txtRecordName.Text;
            Globels.strProce = cmbProce.Text;
            Globels.strProceName = txtProceName.Text;

            frmWorkShopWeightList frm = new frmWorkShopWeightList();
            frm.ShowDialog();
            frm.Dispose();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            Mes_WorkShopScanBLL WorkShopScanBLL = new Mes_WorkShopScanBLL();
            Mes_WorkShopWeightBLL WorkShopWeightBL = new Mes_WorkShopWeightBLL();

            string strSql = " where W_RecordCode = '"+ cmbRecord.Text +"' and W_ProceCode = '"+ cmbProce.Text +"' and W_WorkShop = '"+ cmbWorkShop.Text +"' and W_OrderNo = '"+ comOrderNo.Text +"'";
            var row = WorkShopScanBLL.GetList_WorkShopScan(strSql);
            if (row == null || row.Count < 1)
            {
                untCommon.InfoMsg("没有任何数据！");
                return;
            }
            dataGridView1.DataSource = row;


            strSql = " where W_RecordCode = '" + cmbRecord.Text + "' and W_ProceCode = '" + cmbProce.Text + "' and W_WorkShopCode = '" + cmbWorkShop.Text + "' and W_OrderNo = '" + comOrderNo.Text + "'";
            var row2 = WorkShopWeightBL.GetList_WorkShopWeight(strSql);
            if (row2 == null || row2.Count < 1)
            {
                untCommon.InfoMsg("没有任何数据！");
                return;
            }
            dataGridView2.DataSource = row2;

        }

        private void btn_upload_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("是否要提交","",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                Mes_WorkShopScanBLL WorkShopScanBLL = new Mes_WorkShopScanBLL();
                Mes_WorkShopWeightBLL WorkShopWeightBL = new Mes_WorkShopWeightBLL();

                string strSql = "where W_RecordCode = '" + cmbRecord.Text + "' and W_ProceCode = '" + cmbProce.Text + "' and W_WorkShopCode = '" + cmbWorkShop.Text + "' and W_OrderNo = '" + comOrderNo.Text + "'";
                var row2 = WorkShopWeightBL.GetList_WorkShopWeight(strSql);
                if (row2 == null || row2.Count < 1)
                {
                    untCommon.InfoMsg("没有任何数据！");
                    return;
                }

                Mes_OrgResHeadBLL OrgResHeadBLL = new Mes_OrgResHeadBLL();
                Mes_OrgResDetailBLL OrgResDetailBLL = new Mes_OrgResDetailBLL();
                Mes_OrgResHeadEntity OrgResHeadEntity = new Mes_OrgResHeadEntity();
                Mes_OrgResDetailEntity OrgResDetailEntity = new Mes_OrgResDetailEntity();

                string strIn_No = "";

                var rowsHead = OrgResHeadBLL.GetList_OrgResHead("where 1 = 1 order by O_OrgResNo DESC");
                if (rowsHead == null || rowsHead.Count < 1)
                {
                    strIn_No = "OR" + DateTime.Now.ToString("yyyyMMdd") + "0001";
                }
                else
                {
                    string strDate = rowsHead[0].O_OrgResNo.Substring(2, 8);
                    if (strDate == DateTime.Now.ToString("yyyyMMdd"))
                    {
                        string strList = rowsHead[0].O_OrgResNo.Substring(10, 4);
                        int nList = Convert.ToInt32(strList) + 1;
                        strIn_No = "OR" + DateTime.Now.ToString("yyyyMMdd") + nList.ToString().PadLeft(4, '0');
                    }
                    else
                    {
                        strIn_No = "OR" + DateTime.Now.ToString("yyyyMMdd") + "0001";
                    }

                }

                OrgResHeadEntity.O_OrgResNo = strIn_No;
                OrgResHeadEntity.O_OrderNo = comOrderNo.Text;

                OrgResHeadEntity.O_CreateBy = "";
                OrgResHeadEntity.O_CreateDate = DateTime.Now;
                OrgResHeadEntity.O_OrderDate = "";
                OrgResHeadEntity.O_Remark = "";
                OrgResHeadEntity.O_Status = 1;
                OrgResHeadEntity.O_WorkShopCode = cmbWorkShop.Text;
                OrgResHeadEntity.O_Record = cmbRecord.Text;
                OrgResHeadEntity.O_ProCode = cmbProce.Text;



                int nRow = OrgResHeadBLL.SaveEntity("", OrgResHeadEntity);

                for (int i = 0; i < row2.Count; i++)
                {
                    OrgResDetailEntity.O_OrgResNo = strIn_No;
                    OrgResDetailEntity.O_SecGoodsCode = row2[i].W_SecGoodsCode;
                    OrgResDetailEntity.O_SecGoodsName = row2[i].W_SecGoodsName;
                    OrgResDetailEntity.O_SecPrice = 0;
                    OrgResDetailEntity.O_SecQty = row2[i].W_SecQty;
                    OrgResDetailEntity.O_SecUnit = row2[i].W_SecUnit;
                    OrgResDetailEntity.O_SecBatch = row2[i].W_SecBatch;


                    string strGoodsCode = row2[i].W_SecGoodsCode;
                    Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
                    var Convert_rows = ConvertBLL.GetList_Mes_Convert(" where C_SecCode = '" + strGoodsCode + "'");
                    string strC_GoodsCode = "";
                    if (Convert_rows.Count > 0)
                    {
                        strC_GoodsCode = Convert_rows[0].C_Code;
                    }

                    strSql = "where W_RecordCode = '" + cmbRecord.Text + "' and W_ProceCode = '" + cmbProce.Text + "' and W_WorkShop = '" + cmbWorkShop.Text + "' and W_OrderNo = '" + comOrderNo.Text + "' and W_GoodsCode = '" + strC_GoodsCode + "'";
                    var row = WorkShopScanBLL.GetList_WorkShopScan(strSql);
                    if (row == null || row.Count < 1)
                    {
                        untCommon.InfoMsg("没有任何数据！");
                        return;
                    }
                    else
                    {

                        OrgResDetailEntity.O_GoodsCode = row[0].W_GoodsCode;
                        OrgResDetailEntity.O_GoodsName = row[0].W_GoodsName;
                        OrgResDetailEntity.O_Price = row[0].W_Price;
                        OrgResDetailEntity.O_Qty = row[0].W_Qty;
                        OrgResDetailEntity.O_Unit = row[0].W_Unit;
                        OrgResDetailEntity.O_Batch = row[0].W_Batch;

                        OrgResDetailEntity.O_SecPrice = (row[0].W_Price * row[0].W_Qty) / row2[i].W_SecQty;
                    }


                    nRow = OrgResDetailBLL.SaveEntity("", OrgResDetailEntity);

                }
                MessageBox.Show("保存成功");
   
            }
        }

        
    }
}
