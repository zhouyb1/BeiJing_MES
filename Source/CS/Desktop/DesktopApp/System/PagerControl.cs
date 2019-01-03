using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserDefineControl
{
    public partial class PagerControl : UserControl
    {
        #region 构造函数

        public PagerControl()
        {
            InitializeComponent();
        }

        #endregion

        #region 分页字段和属性

        private int pageIndex = 1;
        /// <summary>
        /// 当前页面
        /// </summary>
        public virtual int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }

        private int pageSize = 100;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public virtual int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int recordCount = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public virtual int RecordCount
        {
            get { return recordCount; }
            set { recordCount = value; }
        }

        private int pageCount = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (pageSize != 0)
                {
                    pageCount = GetPageCount();
                }
                return pageCount;
            }
        }
        
        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <returns></returns>
        private int GetPageCount()
        {
            if (PageSize == 0)
            {
                return 0;
            }
            int pageCount = RecordCount / PageSize;
            if (RecordCount % PageSize == 0)
            {
                pageCount = RecordCount / PageSize;
            }
            else
            {
                pageCount = RecordCount / PageSize + 1;
            }
            return pageCount;
        }
        #endregion

        /// <summary>
        /// 页面改变触发
        /// </summary>

        public event EventHandler OnPageChanged;
        
        #region 控制页数
        private void lnkFirst_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PageIndex = 1;
            DrawControl(true);
        }

        private void lnkPrev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PageIndex = Math.Max(1, PageIndex - 1);
            DrawControl(true);
        }

        private void lnkNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PageIndex = Math.Min(PageCount, PageIndex + 1);
            DrawControl(true);
        }

        private void lnkLast_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PageIndex = PageCount;
            DrawControl(true);
        }

       
        #endregion


        /// <summary>
        /// 外部调用
        /// </summary>
        public void DrawControl(int count)
        {
            recordCount = count;
            DrawControl(false);
        }
      
        /// <summary>
        /// 页面控件呈现
        /// </summary>
        private void DrawControl(bool callEvent)
        {
            lblCurrentPage.Text = PageIndex.ToString();
            lblPageCount.Text = PageCount.ToString();
            lblTotalCount.Text = RecordCount.ToString();
            cmbPageSize.Text = PageSize.ToString();

            if (callEvent && OnPageChanged != null)
            {
                OnPageChanged(this, null);//当前分页数字改变时，触发委托事件
            }
            SetFormCtrEnabled();
            if (PageCount == 1)//有且仅有一页
            {
                lnkFirst.Enabled = false;
                lnkPrev.Enabled = false;
                lnkNext.Enabled = false;
                lnkLast.Enabled = false;
                btnGo.Enabled = false;
            }
            else if (PageIndex == 1)//第一页
            {
                lnkFirst.Enabled = false;
                lnkPrev.Enabled = false;
            }
            else if (PageIndex == PageCount)//最后一页
            {
                lnkNext.Enabled = false;
                lnkLast.Enabled = false;
            }
        }

        private void SetFormCtrEnabled()
        {
            lnkFirst.Enabled = true;
            lnkPrev.Enabled = true;
            lnkNext.Enabled = true;
            lnkLast.Enabled = true;
            btnGo.Enabled = true;
        }

        #region 输入页码跳转
        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int num = 0;
            if (int.TryParse(txtPageNum.Text.Trim(), out num) && num > 0)
            {
                PageIndex = num;
                DrawControl(true);
            }
        } 

        /// <summary>
        /// 跳转文本框控制
        /// </summary>
        private void txtPageNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            //控制只能数值
            if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 && (int)e.KeyChar != 46)
                e.Handled = true;


            //触发回车
            if (e.KeyChar == 13)
            {
                btnGo_Click(null, null);
                return;
            }
        }

        /// <summary>
        /// 跳转页数限制
        /// </summary>
        private void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int num = 0;
            if (int.TryParse(txtPageNum.Text.Trim(), out num) && num > 0)
            {
                if (num > PageCount)
                {
                    txtPageNum.Text = PageCount.ToString();
                }
            }
        } 
        #endregion

        #region 调整每页记录数
        private void cmbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPageSize.SelectedIndex < 0)
            {
                cmbPageSize.SelectedIndex = 2;
            }
            else
            {
                int num = int.Parse(cmbPageSize.Items[cmbPageSize.SelectedIndex].ToString());
                //if (!int.TryParse(size, out num) || num <= 0)
                //{
                //    num = 100;
                //    cmbPageSize.SelectedIndex = 2;
                //}
                pageSize = num;

                lnkFirst_LinkClicked(null, null);
            }
        } 
        #endregion

        /// <summary>
        /// 默认100条每页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PagerControl_Load(object sender, EventArgs e)
        {
            cmbPageSize.SelectedIndex = 2;
        }
    }
}
