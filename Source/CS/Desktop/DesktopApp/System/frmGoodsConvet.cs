using Business.System;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmGoodsConvet : DockContent
    {
        string m_strUnit = "";
        public frmGoodsConvet()
        {
            InitializeComponent();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            UpdateGoods();
        }

        private void frmGoodsConvet_Load(object sender, EventArgs e)
        {
            string strGoods = Globels.strGoodsMessase;
            string[] str = strGoods.Split(',');
            txtGoodsCode.Text = str[0];
            txtGoodsName.Text = str[1];
            txtPc.Text = str[2];
            txtQty.Text = str[3];
            m_strUnit = str[4];
            Thread.Sleep(100);
            UpdateGoods();

        }

        private void UpdateGoods()
        {
            Mes_WorkShopScanBLL WorkShopScanBLL = new Mes_WorkShopScanBLL();
            string strSql = "select a.I_GoodsCode,a.I_GoodsName,SUM(a.I_Qty) as I_Qty,a.I_Unit,a.I_StockCode,a.I_StockName,c.G_Price from Mes_Inventory as a left join Mes_Convert as b on b.C_Code = a.I_GoodsCode left join Mes_Goods as c on a.I_GoodsCode = c.G_Code where C_SecCode = '" + txtGoodsCode.Text + "' and a.I_StockCode = '" + Globels.strStockCode + "' and a.I_Qty > 0 GROUP BY a.I_GoodsCode,a.I_GoodsName,a.I_Unit,a.I_StockCode,a.I_StockName,c.G_Price";
            //string strSql = "select a.I_GoodsCode,a.I_GoodsName,a.I_Batch,a.I_Qty,a.I_Unit,a.ID,a.I_StockCode,a.I_StockName,c.G_Price from Mes_Inventory as a left join Mes_Convert as b on b.C_Code = a.I_GoodsCode left join Mes_Goods as c on a.I_GoodsCode = c.G_Code where C_SecCode = '" + txtGoodsCode.Text + "' and a.I_StockCode = '" + Globels.strStockCode + "' and a.I_Qty > 0";
            DataSet ds = new DataSet();
            ds = WorkShopScanBLL.GetList_WorkShopScan2(strSql);


            dataGridView1.DataSource = ds.Tables[0];

            int nLen = dataGridView1.Rows.Count;
            for (int i = 0; i < nLen - 1; i++)
            {
                string strQty = dataGridView1.Rows[i].Cells["数量"].Value.ToString();
                dataGridView1.Rows[i].Cells["数量"].Value = Delete0(strQty);

                dataGridView1.Rows[i].Cells["实用数量"].Value = "0";
                dataGridView1.Rows[i].Cells["车间"].Value = Globels.strWorkShopName;
            }
        }

        private string Delete0(string strQty)
        {
            string strTemp = "";
            string strreturn = "";
            for (int i = 0; i < strQty.Length; i++)
            {
                string str = strQty.Substring(strQty.Length - i - 1, 1);
                if (str == ".")
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

        private void btn_Convet_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> Goods = new List<string>();
                List<string> strOldGoods = new List<string>();
                int nKind = 0; //计算有几种原物料
                int nLen = dataGridView1.Rows.Count;
                string strStockName = "";
                string strStockCode = "";

                for (int i = 0; i < nLen; i++)
                {
                    object obj = dataGridView1.Rows[i].Cells["选择"].Value;
                    //if (Convert.ToString(obj) == "True" || Convert.ToString(obj) == "1")
                    //{
                    string strGoods = dataGridView1.Rows[i].Cells["物料"].Value.ToString();
                    string strQty = dataGridView1.Rows[i].Cells["实用数量"].Value.ToString();
                    string strYLQty = dataGridView1.Rows[i].Cells["数量"].Value.ToString();
                    strStockCode = dataGridView1.Rows[i].Cells["仓库编码"].Value.ToString();
                    strStockName = dataGridView1.Rows[i].Cells["仓库名称"].Value.ToString();

                    //string strPc = dataGridView1.Rows[i].Cells["批次"].Value.ToString();
                    string strPrice = dataGridView1.Rows[i].Cells["价格"].Value.ToString();
                    string strName = dataGridView1.Rows[i].Cells["物料名称"].Value.ToString();
                    string strUnit = dataGridView1.Rows[i].Cells["单位"].Value.ToString();

                    if (Convert.ToDecimal(strQty) > Convert.ToDecimal(strYLQty))
                    {
                        lblTS.Text = "系统提示：物料：" + strName + "实用数量不能大于库存数量";
                        return;
                    }
                    if (Convert.ToDecimal(strQty) == 0 || Convert.ToDecimal(strQty) < 0)
                    {
                        lblTS.Text = "系统提示：物料：" + strName + "实用数量不能为0或者负数";
                        return;
                    }

                    if (Jud(strGoods, txtGoodsCode.Text))
                    {
                        if (strOldGoods.Contains(strGoods))
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
                            if (strTempGoods == strGoods)
                            {
                                decimal dQty = Convert.ToDecimal(strQty) + Convert.ToDecimal(strTempQty);
                                Goods[j] = strGoods + "," + dQty.ToString() + "," + strPrice + "," + strName + "," + strUnit;
                                bRet = true;
                            }
                        }
                        if (bRet == false)
                        {
                            Goods.Add(strGoods + "," + strQty + "," + strPrice + "," + strName + "," + strUnit);
                        }
                        //}
                        else
                        {
                            lblTS.Text = "系统提示：选择的物料生成不了下一个物料，请核对";
                            return;
                        }
                    }
                }

                if (!Jud2(nKind, txtGoodsCode.Text))
                {
                    lblTS.Text = "系统提示：选择的转换前的物料不够";
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
                OrgResHeadEntity.O_OrderNo = "";

                OrgResHeadEntity.O_CreateBy = Globels.strUser;
                OrgResHeadEntity.O_CreateDate = DateTime.Now;
                OrgResHeadEntity.O_OrderDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                OrgResHeadEntity.O_Remark = "";
                OrgResHeadEntity.O_Status = 1;
                OrgResHeadEntity.O_StockCode = strStockCode;
                OrgResHeadEntity.O_StockName = strStockName;
                OrgResHeadEntity.O_WorkShopCode = Globels.strWorkShop;
                OrgResHeadEntity.O_WorkShopName = Globels.strWorkShopName;
                OrgResHeadEntity.O_Record = "";
                OrgResHeadEntity.O_ProCode = Globels.strProce;
                OrgResHeadEntity.O_TeamCode = Globels.strTeam;
                OrgResHeadEntity.O_TeamName = Globels.strTeamName;

                int nRow = OrgResHeadBLL.SaveEntity("", OrgResHeadEntity);
                decimal dSecPrice = 0;
                decimal dTotal = 0;
                for (int i = 0; i < Goods.Count; i++)
                {
                    string[] strTemp = Goods[i].ToString().Split(',');
                    dTotal = dTotal + (Convert.ToDecimal(strTemp[2].ToString()) * Convert.ToDecimal(strTemp[1].ToString()));
                }
                dSecPrice = dTotal / Convert.ToDecimal(txtQty.Text);


                for (int i = 0; i < Goods.Count; i++)
                {
                    OrgResDetailEntity.O_OrgResNo = strIn_No;
                    OrgResDetailEntity.O_SecGoodsCode = txtGoodsCode.Text;
                    OrgResDetailEntity.O_SecGoodsName = txtGoodsName.Text;
                    OrgResDetailEntity.O_SecPrice = 0;
                    OrgResDetailEntity.O_SecQty = Convert.ToDecimal(txtQty.Text);
                    OrgResDetailEntity.O_SecUnit = m_strUnit;
                    OrgResDetailEntity.O_SecBatch = txtPc.Text;

                    string[] strTemp = Goods[i].ToString().Split(',');

                    string strGoodsCode = strTemp[0].ToString();
                    //Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
                    //var Convert_rows = ConvertBLL.GetList_Mes_Convert(" where C_SecCode = '" + strGoodsCode + "'");
                    //string strC_GoodsCode = "";
                    //if (Convert_rows.Count > 0)
                    //{
                    //    strC_GoodsCode = Convert_rows[0].C_Code;
                    //}
                    MesInventoryBLL InventoryBLL = new MesInventoryBLL();
                    var Inventory_row = InventoryBLL.GetData(" where I_StockCode = '" + Globels.strStockCode + "' and I_GoodsCode = '" + strGoodsCode + "' and I_Qty > 0 order by I_Batch");
                    if (Inventory_row.Count > 0)
                    {
                        Decimal dTotalQty = Convert.ToDecimal(strTemp[1].ToString());
                        for (int j = 0; j < Inventory_row.Count; j++)
                        {
                            if (dTotalQty > 0)
                            {
                                if (dTotalQty < Inventory_row[j].I_Qty)
                                {

                                    OrgResDetailEntity.O_GoodsCode = strTemp[0].ToString();
                                    OrgResDetailEntity.O_GoodsName = strTemp[3].ToString();
                                    OrgResDetailEntity.O_Price = Convert.ToDecimal(strTemp[2].ToString());
                                    OrgResDetailEntity.O_Qty = dTotalQty;
                                    OrgResDetailEntity.O_Unit = strTemp[4].ToString();
                                    OrgResDetailEntity.O_Batch = Inventory_row[j].I_Batch;
                                    OrgResDetailEntity.O_SecPrice = dSecPrice;
                                    nRow = OrgResDetailBLL.SaveEntity("", OrgResDetailEntity);
                                    //UpdateData(Inventory_row[j].ID, Inventory_row[j].I_Qty - dTotalQty);
                                    break;
                                }
                                else
                                {
                                    OrgResDetailEntity.O_GoodsCode = strTemp[0].ToString();
                                    OrgResDetailEntity.O_GoodsName = strTemp[3].ToString();
                                    OrgResDetailEntity.O_Price = Convert.ToDecimal(strTemp[2].ToString());
                                    OrgResDetailEntity.O_Qty = Inventory_row[j].I_Qty;
                                    OrgResDetailEntity.O_Unit = strTemp[4].ToString();
                                    OrgResDetailEntity.O_Batch = Inventory_row[j].I_Batch;
                                    OrgResDetailEntity.O_SecPrice = dSecPrice;
                                    nRow = OrgResDetailBLL.SaveEntity("", OrgResDetailEntity);
                                    dTotalQty = dTotalQty - Inventory_row[j].I_Qty;
                                    //DeleteData(Inventory_row[j].ID);
                                }
                            }

                        }
                    }

                    //OrgResDetailEntity.O_GoodsCode = strTemp[0].ToString();
                    //OrgResDetailEntity.O_GoodsName = strTemp[4].ToString();
                    //OrgResDetailEntity.O_Price = Convert.ToDecimal(strTemp[3].ToString());
                    //OrgResDetailEntity.O_Qty = Convert.ToDecimal(strTemp[1].ToString());
                    //OrgResDetailEntity.O_Unit = strTemp[5].ToString();
                    //OrgResDetailEntity.O_Batch = strTemp[2].ToString();
                    //OrgResDetailEntity.O_SecPrice = dSecPrice;
                    //nRow = OrgResDetailBLL.SaveEntity("", OrgResDetailEntity);

                }
                Upload(strIn_No);
                MessageBox.Show("保存成功");
                lblTS.Text = "";
                //UpdatePrice(strSecGoods, dSecPrice);  已经在存储过程中处理
                Delete();
                //Update();
                this.Close();
            }
            catch (Exception ex)
            {
                lblTS.Text = ex.ToString();
                return;
            }
        }

        private bool Delete()
        {
            try
            {
                //int nLen = dataGridView1.Rows.Count;
                //for (int i = 0; i < nLen; i++)
                //{
                //    object obj = dataGridView1.Rows[i].Cells["选择"].Value;
                //    if (Convert.ToString(obj) == "True" || Convert.ToString(obj) == "1")
                //    {
                //        decimal dsyQty = Convert.ToDecimal(dataGridView1.Rows[i].Cells["实用数量"].Value.ToString());
                //        decimal dylQty = Convert.ToDecimal(dataGridView1.Rows[i].Cells["数量"].Value.ToString());
                //        if (dsyQty == dylQty)
                //        {
                //            string strID = dataGridView1.Rows[i].Cells["ID"].Value.ToString();
                //            DeleteData(strID);
                //        }
                //        else
                //        {
                //            decimal dQty = dylQty - dsyQty;
                //            string strID = dataGridView1.Rows[i].Cells["ID"].Value.ToString();
                //            UpdateData(strID, dQty);
                //        }
                //    }
                //}

                string strTemp = Globels.strID;
                string[] str = strTemp.Split(',');
                int nLen2 = str.Length;
                for (int i = 0; i < nLen2; i++)
                {

                    string strID = str[i];
                    DeleteWeightData(strID);

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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

        private bool UpdateData(string strId, decimal dQty)
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
        private bool Jud(string strGoods, string strSecGoods)
        {
            Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
            var row = ConvertBLL.GetList_Mes_Convert("where C_Code = '" + strGoods + "' and C_SecCode = '" + strSecGoods + "'");
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


    }
}
