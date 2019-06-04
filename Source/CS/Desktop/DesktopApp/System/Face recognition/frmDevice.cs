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
    public partial class frmDevice : DockContent
    {
        private frmMain frmMain { get; set; }
        public frmDevice(frmMain _frmMain)//, frmFaceRecognition _frmFaceRecognition)
        {
            InitializeComponent();
            //frmFaceRecognition = _frmFaceRecognition;
            frmMain = _frmMain;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {

        }

        private void frmDeviceManagement_Load(object sender, EventArgs e)
        {
            frmDeviceManagementList frmDeviceManagementList = new frmDeviceManagementList();
            frmDeviceManagementList.TopLevel = false;
            frmDeviceManagementList.Dock = DockStyle.Fill;//把子窗体设置为控件
            frmDeviceManagementList.FormBorderStyle = FormBorderStyle.None;
            splitContainer1.Panel2.Controls.Add(frmDeviceManagementList);
            frmDeviceManagementList.Show();

            panel1.Visible = false;

            //BeginInvoke(new Action(load));

            string frmParameterConfiguration = "DesktopApp.frmParameterConfiguration";
            GenerateForm(frmParameterConfiguration);
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Node.Text)
            {
                case "设备管理":
                    panel1.Visible = false;
                    //splitContainer1.Panel2.Visible = false;
                    break;
                case "参数配置":
                    //splitContainer1.Panel2.Visible = true;
                    panel1.Visible = true;
                    break;
                default:
                    break;
            }

        }

        public void GenerateForm(string form)
        {
            // 反射生成窗体
            Form fm = (Form)Assembly.GetExecutingAssembly().CreateInstance(form);

            fm.TopLevel = false;
            fm.ControlBox = false;
            fm.Dock = DockStyle.Fill;//把子窗体设置为控件
            fm.FormBorderStyle = FormBorderStyle.None;
            panel1.Controls.Add(fm);
            fm.Show();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }


    }
}
