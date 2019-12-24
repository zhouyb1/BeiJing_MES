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
using Model;
using Tools;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmMain : Form 
    {

        /// <summary>
        /// 用户账号
        /// </summary>
        public SysUser User { get; set; }

        /// <summary>
        /// 用户模块
        /// </summary>
        public List<SysModule> Modules { get; set; }

        /// <summary>
        /// 登录窗口
        /// </summary>
        public frmLogin frmLogin { get; set; }

        private bool iscancle = false;

        public frmMain(frmLogin _frmLogin)
        {
            InitializeComponent();

            frmLogin = _frmLogin;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            loadModule();
            //lbVer.Text = Tools.ConfigManager.ReadValueByKey(ConfigurationFile.AppConfig, "version");
            lbUser.Text = "[" + User.F_Account + "]" + User.F_RealName;
        }


        /// <summary>
        /// 加载模块
        /// </summary>
        private void loadModule()
        {
            try
            {
                SysModuleBLL modulebll = new SysModuleBLL();
                Modules = modulebll.LoadRoleModule(User.R_CSCode);

                cmsBaseManager.Visible = false;
                cmsCompany.Visible = false;
                cmsDepartment.Visible = false;
                cmsRole.Visible = false;
                cmsArea.Visible = false;
                cmsDictionary.Visible = false;

                //cmsEquipmentManager.Visible = false;
                //cmsEquipmentBox.Visible = false;
                //cmsHistory.Visible = false;

                cmsUserManager.Visible = false;
                cmsUser.Visible = false;
                cmsPower.Visible = false;
                cmsPassword.Visible = false;

                cmsSystemManager.Visible = false;
                cmsLog.Visible = false;
                cmsDatabase.Visible = false;

                //cmsExitManager.Visible = false;
                //btnExit.Visible = false;
                //btnOutLogin.Visible = false;

                cmsOtherManager.Visible = false;
                cmsSkin.Visible = false;
                cmsAbout.Visible = false;

                原物料入库ToolStripMenuItem.Visible = false;
                补打标签ToolStripMenuItem.Visible = false; ;

                车间扫描ToolStripMenuItem.Visible = false;;
                车间入库ToolStripMenuItem.Visible = false;;
                车间出库ToolStripMenuItem.Visible = false; ;
                人脸ToolStripMenuItem.Visible = false;;
                
                rFID管理ToolStripMenuItem.Visible = false;;
                
                车间设置ToolStripMenuItem.Visible = false;

                //cmsDictionary.Visible = true;

                foreach (var row in Modules)
                {
                    switch (row.M_Code)
                    {
                        case "01":
                            {
                                cmsBaseManager.Visible = true;
                                break;
                            }
                        case "0101":
                            {
                                cmsCompany.Visible = true;
                                break;
                            }
                        case "0102":
                            {
                                cmsDepartment.Visible = true;
                                break;
                            }
                        case "0103":
                            {
                                cmsRole.Visible = true;
                                break;
                            }
                        case "0104":
                            {
                                cmsArea.Visible = true;
                                break;
                            }
                        case "0105":
                            {
                                cmsDictionary.Visible = true;
                                break;
                            }


                        //case "02":
                        //    {
                        //        cmsEquipmentManager.Visible = true;
                        //        break;
                        //    }
                        //case "0201":
                        //    {
                        //        cmsEquipmentBox.Visible = true;
                        //        break;
                        //    }
                        //case "0202":
                        //    {
                        //        cmsHistory.Visible = true;
                        //        break;
                        //    }

                        case "03":
                            {
                                cmsUserManager.Visible = true;
                                break;
                            }
                        case "0301":
                            {
                                cmsUser.Visible = true;
                                break;
                            }
                        case "0302":
                            {
                                cmsPower.Visible = true;
                                break;
                            }
                        case "0303":
                            {
                                cmsPassword.Visible = true;
                                break;
                            }


                        case "04":
                            {
                                cmsSystemManager.Visible = true;
                                break;
                            }
                        case "0401":
                            {
                                cmsLog.Visible = true;
                                break;
                            }
                        case "0402":
                            {
                                cmsDatabase.Visible = true;
                                break;
                            }


                        //case "05":
                        //    {
                        //        cmsExitManager.Visible = true;
                        //        break;
                        //    }
                        //case "0501":
                        //    {
                        //        btnExit.Visible = true;
                        //        break;
                        //    }
                        //case "0502":
                        //    {
                        //        btnOutLogin.Visible = true;
                        //        break;
                        //    }

                        case "06":
                            {
                                cmsOtherManager.Visible = true;
                                break;
                            }
                        case "0601":
                            {
                                cmsSkin.Visible = true;
                                break;
                            }
                        case "0602":
                            {
                                cmsAbout.Visible = true;
                                break;
                            }
                        case "10":
                            {
                                原物料入库ToolStripMenuItem.Visible = true;
                                break;
                            }
                        case "11":
                            {
                                补打标签ToolStripMenuItem.Visible = true;
                                break;
                            }
                            
       
                        case "12":
                            {
                                车间扫描ToolStripMenuItem.Visible = true;
                                break;
                            }
                        case "13":
                            {
                                车间入库ToolStripMenuItem.Visible = true;
                                break;
                            }
                        case "14":
                            {
                                车间出库ToolStripMenuItem.Visible = true;
                                break;
                            }
                        case "15":
                            {
                                人脸ToolStripMenuItem.Visible = true;
                                break;
                            }
                        case "16":
                            {
                                rFID管理ToolStripMenuItem.Visible = true;
                                break;
                            }

                    }
                }
            }
            catch (Exception ex)
            {
               untCommon.ErrorMsg("加载用户模块权限异常："+ex.Message);
               Application.Exit();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOutLogin_Click(object sender, EventArgs e)
        {
            if (untCommon.QuestionMsg("您确定要注销系统吗？"))
            {
                iscancle = true;
                frmLogin.Show();
                this.Close();
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!iscancle)
            {
                if (untCommon.QuestionMsg("您确定要关闭系统吗？"))
                {
                    e.Cancel = false; //关闭窗体
                }
                else
                {
                    e.Cancel = true;//不执行操作
                } 
            }
           
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!iscancle)
            {
                Application.Exit();//退出系统
            }
          
        }

        #region 打开功能模块



        private void cmsDatabase_Click(object sender, EventArgs e)
        {
            frmBackup frmBackup=new frmBackup();
            frmBackup.ShowDialog();
        }

        private void cmsPasswordEdit_Click(object sender, EventArgs e)
        {
            frmPasswordEdit frmPasswordEdit = new frmPasswordEdit(this);
            frmPasswordEdit.ShowDialog();
        }

        private void cmsSkinSet_Click(object sender, EventArgs e)
        {
            frmConfig frmConfig = new frmConfig(this);
            frmConfig.ShowDialog();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            frmAbout frmAbout = new frmAbout();
            frmAbout.ShowDialog();
        }

        private void cmsEquipmentBox_Click(object sender, EventArgs e)
        {
            frmEquipmentList frmEquipmentBox = new frmEquipmentList(this);
            frmEquipmentBox.TopLevel = false;
            frmEquipmentBox.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmEquipmentBox.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmEquipmentBox.Show(this.panMain);
        }

        private void cmsCompany_Click(object sender, EventArgs e)
        {
            frmCompanyList frmCompany = new frmCompanyList(this);
            frmCompany.TopLevel = false;
            frmCompany.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmCompany.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmCompany.Show(this.panMain);

        }

        private void cmsDictionary_Click(object sender, EventArgs e)
        {

            frmDictionaryList frmDictionary = new frmDictionaryList(this);
            frmDictionary.TopLevel = false;
            frmDictionary.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmDictionary.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmDictionary.Show(this.panMain);
        }

        public void cmsRole_Click(object sender, EventArgs e)
        {
            frmRoleList frmRole = new frmRoleList(this);
            frmRole.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmRole.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmRole.Show(this.panMain);
        }

        private void cmsDepartment_Click(object sender, EventArgs e)
        {
            frmDepartmentList frmDepartment = new frmDepartmentList(this);
            frmDepartment.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmDepartment.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmDepartment.Show(this.panMain);
        }

        public void cmsUser_Click(object sender, EventArgs e)
        {
            frmUserList frmUser = new frmUserList(this);
            frmUser.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmUser.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmUser.Show(this.panMain);
        }

        private void cmsPower_Click(object sender, EventArgs e)
        {
            frmPowerSet frmUser = new frmPowerSet(this);
            frmUser.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmUser.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmUser.Show(this.panMain);
        }

        private void cmsArea_Click(object sender, EventArgs e)
        {
            frmAreaSet frmArea = new frmAreaSet(this);
            frmArea.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmArea.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmArea.Show(this.panMain);
        }

        private void cmsHistory_Click(object sender, EventArgs e)
        {
            frmInspectList frmInspect = new frmInspectList(this);
            frmInspect.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmInspect.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmInspect.Show(this.panMain);
        }

        #endregion
        /// <summary>
        /// 显示时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            lbDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void 原物料入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStorageList frmStorage = new frmStorageList(this);
            frmStorage.TopLevel = false;
            frmStorage.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmStorage.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmStorage.Show(this.panMain);
        }

        private void 车间扫描ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOrgres frmMeatProcess = new frmOrgres(this, User);
            frmMeatProcess.TopLevel = false;
            frmMeatProcess.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmMeatProcess.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmMeatProcess.Show(this.panMain);
        }

        private void 车间入库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmVegetableProcessList frmVegetableProcess = new frmVegetableProcessList(this,User);
            //frmVegetableProcess.TopLevel = false;
            //frmVegetableProcess.WindowState = FormWindowState.Maximized;

            //foreach (DockContent frm in this.panMain.Contents)
            //{
            //    if (frm.Name == frmVegetableProcess.Name)
            //    {
            //        frm.Activate();//激活
            //        return;
            //    }
            //}

            //frmVegetableProcess.Show(this.panMain);

            frmInWorkShopList frmInWorkShop = new frmInWorkShopList(this, User);
            frmInWorkShop.TopLevel = false;
            frmInWorkShop.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmInWorkShop.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmInWorkShop.Show(this.panMain);
        }

        public void 设备管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDevice frmDevice = new frmDevice(this);
            frmDevice.TopLevel = false;
            frmDevice.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmDevice.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmDevice.Show(this.panMain);
        }

        private void 记录查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRecordQuery frmRecordQuery = new frmRecordQuery();
            frmRecordQuery.TopLevel = false;
            frmRecordQuery.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmRecordQuery.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmRecordQuery.Show(this.panMain);
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRoleList frmRole = new frmRoleList(this);
            frmRole.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmRole.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmRole.Show(this.panMain);
        }

        private void 考勤管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmAttendanceManagement frmAttendanceManagement = new frmAttendanceManagement();
            frmAttendanceManagement.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmAttendanceManagement.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmAttendanceManagement.Show(this.panMain);
        }

        private void 系统配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSystemConfiguration frmSystemConfiguration = new frmSystemConfiguration();
            frmSystemConfiguration.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmSystemConfiguration.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmSystemConfiguration.Show(this.panMain);
        }

        private void 人脸ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void 人员管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserList frmUser = new frmUserList(this);
            frmUser.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmUser.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmUser.Show(this.panMain);
        }

        private void 设备配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 照片管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmImageQuery frmImageQuery = new frmImageQuery(this, User);
            frmImageQuery.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmImageQuery.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmImageQuery.Show(this.panMain);
        }

        private void panMain_ActiveContentChanged(object sender, EventArgs e)
        {

        }

        private void 车间出库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOutWorkShopList frmInWorkShop = new frmOutWorkShopList(this, User);
            frmInWorkShop.TopLevel = false;
            frmInWorkShop.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmInWorkShop.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmInWorkShop.Show(this.panMain);
        }

        private void 补打标签ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BracodePrintf frmBarcodePrintf = new BracodePrintf(this, User);
            frmBarcodePrintf.TopLevel = false;
            frmBarcodePrintf.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmBarcodePrintf.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmBarcodePrintf.Show(this.panMain);
        }

        private void 程序更新ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            
        }

        private void 报废单ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScrap frmBarcodePrintf = new frmScrap();
            frmBarcodePrintf.TopLevel = false;
            frmBarcodePrintf.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmBarcodePrintf.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmBarcodePrintf.Show(this.panMain);
        }

        private void 领料单ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 出成率制作ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 库存查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInventory frmInventory = new frmInventory();
            frmInventory.TopLevel = false;
            frmInventory.WindowState = FormWindowState.Maximized;

            foreach (DockContent frm in this.panMain.Contents)
            {
                if (frm.Name == frmInventory.Name)
                {
                    frm.Activate();//激活
                    return;
                }
            }

            frmInventory.Show(this.panMain);
        }


    }
}
