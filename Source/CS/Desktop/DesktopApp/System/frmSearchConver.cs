using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Model;
using Business.System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmSearchConver : DockContent
    {
        public frmSearchConver()
        {
            InitializeComponent();
        }

        private void frmSearchConver_Load(object sender, EventArgs e)
        {
            txtStockCode.Text = Globels.strStockCode;
            MesStockBLL StockBLL = new MesStockBLL();
            var row = StockBLL.GetData(" where S_Code = '" + Globels.strStockCode + "'");
            txtStockName.Text = row[0].S_Name;

            //DateTime dt = DateTime.Now.Date.AddDays(-1);
            //string strSql = "SELECT DISTINCT b.O_SecGoodsCode,b.O_SecGoodsName FROM Mes_OrgResHead AS a LEFT JOIN dbo.Mes_OrgResDetail AS b ON a.O_OrgResNo = b.O_OrgResNo WHERE  a.O_StockCode = '" + Globels.strStockCode + "' AND a.O_UploadDate > '" + dt + "'";
            //DataSet ds = new DataSet();
            //Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
            //ds = ConvertBLL.GetList(strSql);
            //cmbGoodsName.Items.Clear();
            //int nLen = ds.Tables[0].Rows.Count;
            //for (int i = 0; i < nLen; i++)
            //{
            //    cmbGoodsName.Items.Add(ds.Tables[0].Rows[i]["O_SecGoodsName"].ToString());
            //}
        }

        private void btn_Conver_Click(object sender, EventArgs e)
        {
            SearchConver();
        }

        private void SearchConver()
        {
            

        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime dt = dt1.AddDays(1);
            string strSql = "SELECT DISTINCT b.O_SecGoodsCode,b.O_SecGoodsName FROM Mes_OrgResHead AS a LEFT JOIN dbo.Mes_OrgResDetail AS b ON a.O_OrgResNo = b.O_OrgResNo WHERE  a.O_StockCode = '" + Globels.strStockCode + "' AND a.O_UploadDate > '" + dt1 + "' and a.O_UploadDate < '"+ dt +"'";
            DataSet ds = new DataSet();
            Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
            ds = ConvertBLL.GetList(strSql);
            //cmbGoodsName.Items.Clear();
            listView1.Items.Clear();
            int nLen = ds.Tables[0].Rows.Count;
            this.listView1.BeginUpdate();
            for (int i = 0; i < nLen; i++)
            {
                //cmbGoodsName.Items.Add(ds.Tables[0].Rows[i]["O_SecGoodsName"].ToString());

                //DateTime dt = DateTime.Now.Date.AddDays(-1);
                strSql = "SELECT SUM(O_Qty) as O_Qty,SUM(O_SecQty) as O_SecQty,O_GoodsName,a.O_OrgResNo,b.O_Batch FROM Mes_OrgResHead AS a LEFT JOIN dbo.Mes_OrgResDetail AS b ON a.O_OrgResNo = b.O_OrgResNo WHERE  a.O_StockCode = '" + Globels.strStockCode + "' AND a.O_UploadDate > '" + dt1 + "' and a.O_UploadDate < '" + dt + "' and O_SecGoodsName = '" + ds.Tables[0].Rows[i]["O_SecGoodsName"].ToString() + "' group by O_GoodsName,a.O_OrgResNo,b.O_Batch";
                DataSet ds2 = new DataSet();
 
                ds2 = ConvertBLL.GetList(strSql);
                int nLen2 = ds2.Tables[0].Rows.Count;
                if (nLen2 > 0)
                {
                    List<string> OrderNoList = new List<string>();
                    //txtGoodsName.Text = ds2.Tables[0].Rows[0]["O_GoodsName"].ToString();
                    decimal dQty = 0;
                    decimal dQtySec = 0;
                    for (int j = 0; j < nLen2; j++)
                    {
                        if (!OrderNoList.Contains(ds2.Tables[0].Rows[j]["O_OrgResNo"].ToString()))
                        {
                            OrderNoList.Add(ds2.Tables[0].Rows[j]["O_OrgResNo"].ToString());
                            dQtySec = dQtySec + Convert.ToDecimal(ds2.Tables[0].Rows[j]["O_SecQty"].ToString());
                        }
                        dQty = dQty + Convert.ToDecimal(ds2.Tables[0].Rows[j]["O_Qty"].ToString());


                    }
                    txtQty.Text = dQty.ToString();
                    txtQtySec.Text = dQtySec.ToString();



                    decimal dConvert = dQtySec / dQty;
                    dConvert = Math.Round(dConvert, 6);
                    txtConvert.Text = dConvert.ToString();

                    ListViewItem lvi = new ListViewItem(ds2.Tables[0].Rows[0]["O_GoodsName"].ToString());
                    lvi.SubItems.Add(dQty.ToString());
                    lvi.SubItems.Add(ds.Tables[0].Rows[i]["O_SecGoodsName"].ToString());
                    lvi.SubItems.Add(dQtySec.ToString());


                    lvi.SubItems.Add(dConvert.ToString());
                    this.listView1.Items.Add(lvi);
                }
            }
            this.listView1.EndUpdate();
        }
    }
}
