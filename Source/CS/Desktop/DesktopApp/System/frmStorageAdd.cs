using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;
using WeifenLuo.WinFormsUI.Docking;
using Business.System;
using Model;

namespace DesktopApp
{
    public partial class frmStorageAdd : DockContent
    {
        private string M_MaterInNo = "";//生产订单号
        private SysUser User;
        private frmStorageList frmStorage;
        private int rowindex;//获得当前选中的行的索引
        public frmStorageAdd(frmStorageList _frmStorageList, SysUser _User, string _M_MaterInNo)
        {
            InitializeComponent();
            M_MaterInNo = _M_MaterInNo;
            User = _User;
            frmStorage = _frmStorageList;

            getDetail();
        }

        private void getDetail()
        {
            try
            {
                //M_MaterInNo = M_MaterInNo.Text.Trim();
                txtMaterInNo.Text = M_MaterInNo.ToString();
                txtCreateBy.Text = User.F_Account.ToString();
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("物料入库编辑页面加载数据异常：" + ex.Message);
            }
        }

        //private void txtStockCode_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
        //    {
        //        MesStockBLL MesStockBLL = new MesStockBLL();
        //        var rows = MesStockBLL.GetList(comStock.Text.Trim(), "");
        //        if (rows == null || rows.Count < 1)
        //        {
        //            untCommon.InfoMsg("输入的仓库编码错误，请重新输入！");
        //            txtStockCode.Focus();
        //            txtStockCode.SelectAll();
        //        }
        //        else
        //        {
        //            txtStockCode.Text = rows[0].S_Code;
        //            txtStockName.Text = rows[0].S_Name;
        //        }
        //    }
        //}

        //private void txtStockName_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
        //    {
        //        MesStockBLL MesStockBLL = new MesStockBLL();
        //        var rows = MesStockBLL.GetList("", txtStockName.Text.Trim());
        //        if (rows == null || rows.Count < 1)
        //        {
        //            untCommon.InfoMsg("输入的仓库名称错误，请重新输入！");
        //            txtStockName.Focus();
        //            txtStockName.SelectAll();
        //        }
        //        else
        //        {
        //            txtStockCode.Text = rows[0].S_Code;
        //            txtStockName.Text = rows[0].S_Name;
        //        }
        //    }
        //}

        //private void txtOrderNo_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.Enter)//如果输入的是回车键  
        //    {
        //        MesProductOrderHeadBLL MesProductOrderHeadBLL = new MesProductOrderHeadBLL();
        //        var rows = MesProductOrderHeadBLL.GetList(txtOrderNo.Text.Trim());
        //        if (rows == null || rows.Count < 1)
        //        {
        //            untCommon.InfoMsg("输入的生产订单号错误，请重新输入！");
        //            txtOrderNo.Focus();
        //            txtOrderNo.SelectAll();
        //        }
        //    }
        //}

        private void btnSave_Click(object sender, EventArgs e)
        {
            addStorage();
        }

        /// <summary>
        /// 校验输入
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            if (string.IsNullOrEmpty(comStock.Text.Trim()))
            {
                untCommon.InfoMsg("仓库编码不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(comProductNo.Text))
            {
                untCommon.InfoMsg("生产订单号不能为空！");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void addStorage()
        {
            try
            {
                //MesProductOrderHeadBLL MesProductOrderHeadBLL = new MesProductOrderHeadBLL();
                //var rows = MesProductOrderHeadBLL.GetList(txtOrderNo.Text.Trim());
                //if (rows == null || rows.Count < 1)
                //{
                //    untCommon.InfoMsg("输入的生产订单号错误，请重新输入！");
                //    txtOrderNo.Focus();
                //    txtOrderNo.SelectAll();
                //    return;
                //}

                string[] strStock = comStock.Text.ToString().Split('$');
                string[] strProduct = comProductNo.Text.ToString().Split('$');

                MesMaterInHeadBLL MesMaterInHeadBLL = new MesMaterInHeadBLL();
                if (checkInput())
                {
                    MesMaterInHeadEntity MesMaterInHead = new MesMaterInHeadEntity();

                    MesMaterInHead.M_MaterInNo = txtMaterInNo.Text;
                    MesMaterInHead.M_OrderKind = 0;
                    MesMaterInHead.M_StockCode = strStock[0];
                    MesMaterInHead.M_Kind = "0";
                    MesMaterInHead.M_StockName = strStock[1];
                    MesMaterInHead.M_OrderNo = strProduct[0];
                    MesMaterInHead.M_OrderDate = DateTime.Parse(strProduct[1].ToString());
                    MesMaterInHead.M_Status = 1;
                    MesMaterInHead.M_CreateBy = txtCreateBy.Text;
                    MesMaterInHead.M_CreateDate = DateTime.Now;
                    MesMaterInHead.M_UpdateDate = null;
                    MesMaterInHead.M_DeleteDate = null;
                    MesMaterInHead.M_UploadDate = null;

                    

                    if (MesMaterInHeadBLL.SaveEntity("", MesMaterInHead) > 0)
                    {
                        untCommon.InfoMsg("添加成功！");
                        string strTime = DateTime.Now.ToString("yyyy-MM-dd ");
                        string strStartTime = strTime + "00:00:00";
                        string strEndTime = strTime + "23:59:59";
                        string strCondit = " and M_CreateDate > '" + strStartTime + "' and M_CreateDate < '" + strEndTime + "'";
                        frmStorage.loadData(strCondit);
                        Close();
                        //frmStorage.Refresh();
                    }
                    else
                    {
                        untCommon.InfoMsg("添加失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("设备管理添加数据异常：" + ex.Message);
            }
        }

        private void frmStorageAdd_Load(object sender, EventArgs e)
        {
            comStock.Items.Clear();
            comProductNo.Items.Clear();
            MesStockBLL StockBLL = new MesStockBLL();
            var row = StockBLL.GetData(" where s_Kind = 1");
            int nCount = row.Count;
            for(int i = 0; i < nCount; i++)
            {
                comStock.Items.Add(row[i].S_Code.ToString() + "$" + row[i].S_Name.ToString());

            }

            MesProductOrderHeadBLL ProductOrderHeadBLL = new MesProductOrderHeadBLL();
            var Product = ProductOrderHeadBLL.GetList("");

            int nCount2 = Product.Count;
            for (int i = 0; i < nCount2; i++)
            {
                comProductNo.Items.Add(Product[i].P_OrderNo.ToString() + "$" + Product[i].P_OrderDate.ToString());

            }
        }
    }
}
