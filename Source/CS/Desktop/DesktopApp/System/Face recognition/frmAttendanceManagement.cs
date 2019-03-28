using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmAttendanceManagement : DockContent
    {
        private int[] s = { 0, 0, 0, 0};//用来记录窗体是否打开过  
        public frmAttendanceManagement()
        {
            InitializeComponent();
        }

        private void frmAttendanceManagement_Load(object sender, EventArgs e)
        {
            //初始打开时就加载frmAttendanceCheckDetail  
            string frmAttendanceCheckDetail = "DesktopApp.frmAttendanceCheckDetail";
            GenerateForm(frmAttendanceCheckDetail, tabControl1);

            ////初始打开时就加载frmAttendanceCheckDetail  
            //string frmAttendanceWorkDay = "DesktopApp.frmAttendanceWorkDay";
            //GenerateForm(frmAttendanceWorkDay, tabControl1);

            ////初始打开时就加载frmAttendanceCheckDetail  
            //string frmAttendanceStatistics = "DesktopApp.frmAttendanceStatistics";
            //GenerateForm(frmAttendanceStatistics, tabControl1);

            ////初始打开时就加载frmAttendanceCheckDetail  
            //string frmAttendanceWorkMonthly = "DesktopApp.frmAttendanceWorkMonthly";
            //GenerateForm(frmAttendanceWorkMonthly, tabControl1); 
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
    }  
}  
