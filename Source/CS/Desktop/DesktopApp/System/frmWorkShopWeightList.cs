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
    public partial class frmWorkShopWeightList : DockContent
    {
        public frmWorkShopWeightList()
        {
            InitializeComponent();
        }

        private void frmWorkShopWeightList_Load(object sender, EventArgs e)
        {
            Mes_WorkShopScanBLL WorkShopScanBLL = new Mes_WorkShopScanBLL();

            var Scan_rows = WorkShopScanBLL.GetList_WorkShopScan(" where W_OrderNo = '"+ Globels.strOrderNo +"' and W_WorkShop = '"+ Globels.strWorkShop +"' and W_RecordCode = '"+ Globels.strRecord +"' and W_ProceCode = '"+ Globels.strProce +"'");
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
            //MesMaterInDetailEntity MaterInDetail = new MesMaterInDetailEntity();
            var Goods_rows = GoodsBLL.GetList(cmbGoodsCode.Text.Trim(), "");
            txtName.Text = Goods_rows[0].G_Name;
            txtUnit.Text = Goods_rows[0].G_Unit;
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
                WorkShopWeightEntity.W_SecQty = Convert.ToDecimal(txtQty.Text); ;
                WorkShopWeightEntity.W_SecUnit = txtUnit.Text;
                WorkShopWeightEntity.W_Status = 1;
                WorkShopWeightEntity.W_WorkShopCode = Globels.strWorkShop;

                int nCount = WorkShopWeightBLL.SaveEntity("", WorkShopWeightEntity);
                if (nCount > 0)
                {
                    MessageBox.Show("添加成功");
                }
            }



        }
    }
}
