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
    public partial class frmScrap : DockContent
    {
        decimal dPrice = 0;
        public frmScrap()
        {
            InitializeComponent();
        }

        private void frmScrap_Load(object sender, EventArgs e)
        {
            MesStockBLL StockBLL = new MesStockBLL();
            var Stock_rows = StockBLL.GetData(" where S_Kind = '4'");
            for (int i = 0; i < Stock_rows.Count; i++)
            {
                cmbStock.Items.Add(Stock_rows[i].S_Code);
                cmbStockName.Items.Add(Stock_rows[i].S_Name);
            }
            if (cmbStock.Items.Contains(Globels.strStockCode))
            {
                cmbStock.Text = Globels.strStockCode;
            }
            listView1.Items.Clear();
        }

        

        

        private void cmbStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesStockBLL StockBLL = new MesStockBLL();
            var row = StockBLL.GetData(" where S_Code = '" + cmbStock.Text + "'");
            cmbStockName.Text = row[0].S_Name;

            cmbGoodsCode.Items.Clear();
            MesInventoryBLL InventoryBLL = new MesInventoryBLL();
            var row2 = InventoryBLL.GetData("where I_StockCode = '" + cmbStock.Text + "' and I_Qty > 0 ");
            for (int i = 0; i < row2.Count; i++)
            {
                cmbGoodsCode.Items.Add(row2[i].I_GoodsCode);

            }
            if (row2.Count == 1)
            {
                cmbGoodsCode.Text = row2[0].I_GoodsCode;
            }
        }

        private void cmbStockName_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesStockBLL StockBLL = new MesStockBLL();
            var row = StockBLL.GetData(" where S_Name = '" + cmbStockName.Text + "'");
            cmbStock.Text = row[0].S_Code;

            cmbGoodsCode.Items.Clear();
            MesInventoryBLL InventoryBLL = new MesInventoryBLL();
            var row2 = InventoryBLL.GetData("where I_StockCode = '" + cmbStock.Text + "' and I_Qty > 0 ");
            for (int i = 0; i < row2.Count; i++)
            {
                cmbGoodsCode.Items.Add(row2[i].I_GoodsCode);

            }
            if (row2.Count == 1)
            {
                cmbGoodsCode.Text = row2[0].I_GoodsCode;
            }
        }

        private void cmbGoodsCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbBatch.Items.Clear();
                MesInventoryBLL InventoryBLL = new MesInventoryBLL();
                var row = InventoryBLL.GetData("where I_StockCode = '" + cmbStock.Text + "' and I_GoodsCode = '" + cmbGoodsCode.Text + "' and I_Qty > 0");
                for (int i = 0; i < row.Count; i++)
                {
                    cmbBatch.Items.Add(row[i].I_Batch);

                }
                if (row.Count == 1)
                {
                    cmbBatch.Text = row[0].I_Batch;
                    txtQty.Text = row[0].I_Qty.ToString();
                }



                MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                var Goods_rows = GoodsBLL.GetList(cmbGoodsCode.Text, "");
                int nLen = Goods_rows.Count;
                if (nLen > 0)
                {
                    cmbGoodsName.Text = Goods_rows[0].G_Name;
                    
                    txtUnit.Text = Goods_rows[0].G_Unit.ToString();
                    dPrice = Goods_rows[0].G_Price;
                    //int nKind = Goods_rows[0].G_Kind;
                    //if (nKind == 1)
                    //{
                    //    label17.Visible = true;
                    //    cmbSupplyName.Visible = true;


                    //}
                    //else
                    //{
                    //    ;
                    //}

                }


            }
            catch (Exception ex)
            {
                //lblTS.Text = "ex.ToString()";
            }
        }

        private void cmbGoodsName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbBatch.Items.Clear();
                MesInventoryBLL InventoryBLL = new MesInventoryBLL();
                var row = InventoryBLL.GetData("where I_StockCode = '" + cmbStock.Text + "' and I_GoodsName = '" + cmbGoodsName.Text + "' and I_Qty > 0");
                for (int i = 0; i < row.Count; i++)
                {
                    cmbBatch.Items.Add(row[i].I_Batch);

                }
                if (row.Count == 1)
                {
                    cmbBatch.Text = row[0].I_Batch;
                    txtQty.Text = row[0].I_Qty.ToString();
                    //MesInventoryBLL 
                }



                MesGoodsBLL GoodsBLL = new MesGoodsBLL();
                var Goods_rows = GoodsBLL.GetList(cmbGoodsCode.Text, "");
                int nLen = Goods_rows.Count;
                if (nLen > 0)
                {
                    cmbGoodsName.Text = Goods_rows[0].G_Name;

                    txtUnit.Text = Goods_rows[0].G_Unit.ToString();
                    //int nKind = Goods_rows[0].G_Kind;
                    //if (nKind == 1)
                    //{
                    //    label17.Visible = true;
                    //    cmbSupplyName.Visible = true;


                    //}
                    //else
                    //{
                    //    ;
                    //}

                }


            }
            catch (Exception ex)
            {
                //lblTS.Text = "ex.ToString()";
            }
        }

        private void cmbBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            MesInventoryBLL InventoryBLL = new MesInventoryBLL();
            var row = InventoryBLL.GetData("where I_StockCode = '" + cmbStock.Text + "' and I_GoodsName = '" + cmbGoodsName.Text + "' and I_Batch = '"+ cmbBatch.Text +"' and I_Qty > 0");
            
            if (row.Count > 1)
            {
                txtQty.Text = row[0].I_Qty.ToString();
                //MesInventoryBLL 
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否保存", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    decimal dQty = Convert.ToDecimal(txtQty.Text);
                    decimal dScrap = Convert.ToDecimal(txtScrapQty.Text);
                    if(dScrap > dQty)
                    {
                        lblTS.Text = "系统提示：报废数量不能大于库存数量！";
                    }
                    else
                    {
                        int nCount = listView1.Items.Count;
                        bool bRet = false;
                        for(int i = 0; i < nCount; i++)
                        {
                            string strCode = listView1.Items[i].SubItems[2].Text;
                            if (strCode == cmbGoodsCode.Text)
                            {
                                dScrap = dScrap + Convert.ToDecimal(listView1.Items[i].SubItems[5].Text);
                                listView1.Items[i].SubItems[5].Text = dScrap.ToString();
                                bRet = true;
                                break;
                            }
                        }
                        if(bRet == false)
                        {
                            this.listView1.BeginUpdate();
                            
                                ListViewItem lvi = new ListViewItem(cmbStock.Text);
                                lvi.SubItems.Add(cmbStockName.Text);
                                lvi.SubItems.Add(cmbGoodsCode.Text);
                                lvi.SubItems.Add(cmbGoodsName.Text);
                                lvi.SubItems.Add(cmbBatch.Text);
                                lvi.SubItems.Add(dScrap.ToString());
                                lvi.SubItems.Add(txtUnit.Text);//dPrice
                                lvi.SubItems.Add(dPrice.ToString());
                                this.listView1.Items.Add(lvi);
                            
                            this.listView1.EndUpdate();
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    lblTS.Text = "系统提示：" + ex.ToString();
                    
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    int nSelectIndex = listView1.SelectedIndices[0];
                    listView1.Items.RemoveAt(nSelectIndex);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    lblTS.Text = "系统提示：" + ex.ToString();
                }
            }
        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否提交", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    Mes_ScrapHeadEntity ScrapHeadEntity = new Mes_ScrapHeadEntity();
                    Mes_ScrapHeadBLL ScrapHeadBLL = new Mes_ScrapHeadBLL();
                    Mes_ScrapDetailEntity ScrapDetailEntity = new Mes_ScrapDetailEntity();
                    Mes_ScrapDetailBLL ScrapDetailBLL = new Mes_ScrapDetailBLL();


                    MesMaterInHeadBLL MaterInHeadBLL = new MesMaterInHeadBLL();
                    string strScrapNo = MaterInHeadBLL.GetDH("报废单");
                    ScrapHeadEntity.S_ScrapNo = strScrapNo;
                    ScrapHeadEntity.S_StockCode = cmbStock.Text;
                    ScrapHeadEntity.S_StockName = cmbStockName.Text;
                    ScrapHeadEntity.S_CreateBy = Globels.strUser;
                    ScrapHeadEntity.S_CreateDate = DateTime.Now;
                    ScrapHeadEntity.S_OrderDate = DateTime.Now;
                    ScrapHeadEntity.S_Remark = "";
                    ScrapHeadEntity.S_Status = 1;

                    ScrapHeadBLL.SaveEntity("", ScrapHeadEntity);
                    int nCount = listView1.Items.Count;
                    for(int i = 0; i < nCount; i++)
                    {
                        ScrapDetailEntity.S_ScrapNo = strScrapNo;
                        ScrapDetailEntity.S_Batch = listView1.Items[i].SubItems[4].Text;
                        ScrapDetailEntity.S_GoodsCode = listView1.Items[i].SubItems[2].Text;
                        ScrapDetailEntity.S_GoodsName = listView1.Items[i].SubItems[3].Text;
                        ScrapDetailEntity.S_Price = Convert.ToDecimal(listView1.Items[i].SubItems[7].Text);
                        ScrapDetailEntity.S_Unit = listView1.Items[i].SubItems[6].Text;
                        ScrapDetailEntity.S_Qty = Convert.ToDecimal(listView1.Items[i].SubItems[5].Text);
                        ScrapDetailEntity.S_Remark = "";

                        ScrapDetailBLL.SaveEntity("",ScrapDetailEntity);


                    }

                    MessageBox.Show("报废提交成功");
                    
                    listView1.Items.Clear();

                    
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    lblTS.Text = "系统提示：" + ex.ToString();
                }
            }
        }
        
    }
}
