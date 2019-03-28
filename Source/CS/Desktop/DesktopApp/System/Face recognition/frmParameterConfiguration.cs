using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmParameterConfiguration : DockContent
    {
        private frmMain frmMain { get; set; }

        private int[] s = { 0, 0, 0, 0, 0};//用来记录窗体是否打开过  
        public frmParameterConfiguration()//frmMain _frmMain
        {
            InitializeComponent();
            //frmMain = _frmMain;
        }

        private void frmParameterConfiguration_Load(object sender, EventArgs e)
        {
            //初始打开时就加载frmNetworkParameters  
            string frmNetworkParameters = "DesktopApp.frmNetworkParameters";
            GenerateForm(frmNetworkParameters, tabControl1);
        }   

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //只生成一次
            if (s[tabControl1.SelectedIndex] == 0)
            {
                btn_Click(sender, e);
            }
        }

        /// <summary>  
        /// 通用按钮点击选项卡 在选项卡上显示对应的窗体  
        /// </summary>  
        private void btn_Click(object sender, EventArgs e)
        {
            string formClass = ((TabControl)sender).SelectedTab.Tag.ToString();
            GenerateForm(formClass, sender);
        }

        //在选项卡中生成窗体
        public void GenerateForm(string form, object sender)
        {
            // 反射生成窗体
            Form fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(form);

            //设置窗体没有边框 加入到选项卡中
            fm.FormBorderStyle = FormBorderStyle.None;
            fm.TopLevel = false;
            fm.Parent = ((TabControl)sender).SelectedTab;
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;
            fm.Show();

            s[((TabControl)sender).SelectedIndex] = 1;
        }

        private void load()
       {
        //    //基本参数
        //    frmBasicParameters frmBasicParameters = new frmBasicParameters();

        //    frmBasicParameters.TopLevel = false;
        //    frmBasicParameters.Dock = DockStyle.Fill;//把子窗体设置为控件
        //    frmBasicParameters.FormBorderStyle = FormBorderStyle.None;
        //    panel2.Controls.Add(frmBasicParameters);
        //    frmBasicParameters.Show();

        //    //回调参数
        //    frmCallbackParameter frmCallbackParameter = new frmCallbackParameter();

        //    frmCallbackParameter.TopLevel = false;
        //    frmCallbackParameter.Dock = DockStyle.Fill;//把子窗体设置为控件
        //    frmCallbackParameter.FormBorderStyle = FormBorderStyle.None;
        //    panel3.Controls.Add(frmCallbackParameter);
        //    frmCallbackParameter.Show();

        //    //显示参数
        //    frmDisplayParameter frmDisplayParameter = new frmDisplayParameter();

        //    frmDisplayParameter.TopLevel = false;
        //    frmDisplayParameter.Dock = DockStyle.Fill;//把子窗体设置为控件
        //    frmDisplayParameter.FormBorderStyle = FormBorderStyle.None;
        //    panel4.Controls.Add(frmDisplayParameter);
        //    frmDisplayParameter.Show();
            
        //    //远程操作

        //    frmRemoteOperation frmRemoteOperation = new frmRemoteOperation();

        //    frmRemoteOperation.TopLevel = false;
        //    frmRemoteOperation.Dock = DockStyle.Fill;//把子窗体设置为控件
        //    frmRemoteOperation.FormBorderStyle = FormBorderStyle.None;
        //    panel5.Controls.Add(frmRemoteOperation);
        //    frmRemoteOperation.Show();

           //private void listView1_Click(object sender, EventArgs e)
           //{
           //    panel1.Visible = false;
           //    panel2.Visible = false;
           //    panel3.Visible = false;
           //    panel4.Visible = false;
           //    panel5.Visible = false;
           //    switch (listView1.SelectedItems[0].Index)
           //    {
           //        case 0:
           //            panel1.Visible = true;
           //            break;
           //        case 1:
           //            panel2.Visible = true;
           //            break;
           //        case 2:
           //            panel3.Visible = true;
           //            break;
           //        case 3:
           //            panel4.Visible = true;
           //            break;
           //        case 4:
           //            panel5.Visible = true;
           //            break;
           //    }
           //}

           //ThreadStart threadStart = new ThreadStart(load);//通过ThreadStart委托告诉子线程讲执行什么方法,这里执行一个计算圆周长的方法
           //Thread thread = new Thread(threadStart);
           //thread.Start(); //启动新线程
           //thread.Abort();

           //网络参数
           //panel1.BringToFront();//将panel1置顶
           //frmNetworkParameters frmNetworkParameters = new frmNetworkParameters();
           //frmNetworkParameters.TopLevel = false;
           //frmNetworkParameters.Dock = DockStyle.Fill;//把子窗体设置为控件
           //frmNetworkParameters.FormBorderStyle = FormBorderStyle.None;
           //panel1.Controls.Add(frmNetworkParameters);
           //frmNetworkParameters.Show();

           //BeginInvoke(new Action(load));
        }
    }
}
