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
            DateTime dt = DateTime.Now.Date.AddDays(-1);
            string strSql = "SELECT DISTINCT b.O_SecGoodsCode,b.O_SecGoodsName FROM Mes_OrgResHead AS a LEFT JOIN dbo.Mes_OrgResDetail AS b ON a.O_OrgResNo = b.O_OrgResNo WHERE  a.O_StockCode = '" + Globels.strStockCode + "' AND a.O_UploadDate > '" + dt + "'";
            DataSet ds = new DataSet();
            Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
            ds = ConvertBLL.GetList(strSql);
            cmbGoodsName.Items.Clear();
            int nLen = ds.Tables[0].Rows.Count;
            for (int i = 0; i < nLen; i++)
            {
                cmbGoodsName.Items.Add(ds.Tables[0].Rows[i]["O_SecGoodsName"].ToString());
            }
        }

        private void btn_Conver_Click(object sender, EventArgs e)
        {
            SearchConver();
        }

        private void SearchConver()
        {
            DateTime dt = DateTime.Now.Date.AddDays(-1);
            string strSql = "SELECT SUM(O_Qty) as O_Qty,SUM(O_SecQty) as O_SecQty,O_GoodsName,a.O_OrgResNo,b.O_Batch FROM Mes_OrgResHead AS a LEFT JOIN dbo.Mes_OrgResDetail AS b ON a.O_OrgResNo = b.O_OrgResNo WHERE  a.O_StockCode = '" + Globels.strStockCode + "' AND a.O_UploadDate > '" + dt + "' and O_SecGoodsName = '" + cmbGoodsName.Text + "' group by O_GoodsName,a.O_OrgResNo,b.O_Batch";
            DataSet ds = new DataSet();
            Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
            ds = ConvertBLL.GetList(strSql);
            int nLen = ds.Tables[0].Rows.Count;
            if(nLen > 0)
            {
                List<string> OrderNoList = new List<string>();
                txtGoodsName.Text = ds.Tables[0].Rows[0]["O_GoodsName"].ToString();
                decimal dQty = 0;
                decimal dQtySec = 0;
                for (int i = 0; i < nLen; i++)
                {
                    if (!OrderNoList.Contains(ds.Tables[0].Rows[i]["O_OrgResNo"].ToString()))
                    {
                        OrderNoList.Add(ds.Tables[0].Rows[i]["O_OrgResNo"].ToString());
                        dQtySec = dQtySec + Convert.ToDecimal(ds.Tables[0].Rows[i]["O_SecQty"].ToString());
                    }
                    dQty = dQty + Convert.ToDecimal(ds.Tables[0].Rows[i]["O_Qty"].ToString());
                    

                }
                txtQty.Text = dQty.ToString();
                txtQtySec.Text = dQtySec.ToString();



                decimal dConvert = dQtySec / dQty;
                txtConvert.Text = dConvert.ToString();
            }

        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now.Date.AddDays(-1);
            string strSql = "SELECT DISTINCT b.O_SecGoodsCode,b.O_SecGoodsName FROM Mes_OrgResHead AS a LEFT JOIN dbo.Mes_OrgResDetail AS b ON a.O_OrgResNo = b.O_OrgResNo WHERE  a.O_StockCode = '" + Globels.strStockCode + "' AND a.O_UploadDate > '" + dt + "'";
            DataSet ds = new DataSet();
            Mes_ConvertBLL ConvertBLL = new Mes_ConvertBLL();
            ds = ConvertBLL.GetList(strSql);
            cmbGoodsName.Items.Clear();
            int nLen = ds.Tables[0].Rows.Count;
            for (int i = 0; i < nLen; i++)
            {
                cmbGoodsName.Items.Add(ds.Tables[0].Rows[i]["O_SecGoodsName"].ToString());
            }
        }
    }
}
