using Business;
using Business.System;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmDeviceManagementEdit : DockContent
    {
        string ID;
        string DeviceName;//设备名称
        string IP;//IP地址
        string Remarks;//备注
        string Department;//部门
        public frmDeviceManagementEdit(string _ID,string _DeviceName,string _Department, string _IP, string _Remarks)
        {
            InitializeComponent();
            ID = _ID;
            DeviceName = _DeviceName;
            IP = _IP;
            Remarks = _Remarks;
            Department = _Department;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkInput())
                {
                    MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
                    MesDeviceEntity MesDevice = new MesDeviceEntity();
                    if (string.IsNullOrEmpty(ID))
                    {
                        MesDevice.D_Name = txt_DeviceName.Text;
                        MesDevice.D_Department = cmb_Department.Text;
                        MesDevice.D_IP = txt_IP.Text;
                        MesDevice.D_Remark = txt_Remarks.Text;
                        MesDevice.D_TeamCode = cmbTeam.SelectedValue.ToString();
                        MesDevice.D_TeamName = cmbTeam.Text.ToString();

                        if (MesDeviceBLL.SaveEntity("", MesDevice) > 0)
                        {
                            untCommon.InfoMsg("设备管理信息添加成功！");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            // dataTable.AcceptChanges();
                        }
                        else
                        {
                            untCommon.InfoMsg("设备管理信息添加失败！");
                        }
                    }
                    else
                    {
                        MesDevice.D_Name = txt_DeviceName.Text;
                        MesDevice.D_Department = cmb_Department.Text;
                        MesDevice.D_IP = txt_IP.Text;
                        MesDevice.D_Remark = txt_Remarks.Text;

                        if (MesDeviceBLL.SaveEntity(ID, MesDevice) > 0)
                        {
                            untCommon.InfoMsg("设备管理信息修改成功！");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            // dataTable.AcceptChanges();
                        }
                        else
                        {
                            untCommon.InfoMsg("设备管理信息修改失败！");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("设备管理添加数据异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 校验输入
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            if (string.IsNullOrEmpty(txt_DeviceName.Text.Trim()))
            {
                untCommon.InfoMsg("设备名称不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(cmb_Department.Text))
            {
                untCommon.InfoMsg("部门不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(txt_IP.Text))
            {
                untCommon.InfoMsg("IP地址不能为空！");
                return false;
            }
            else
            {
                string url = "http://" + txt_IP.Text + ":8090/getDeviceKey";

                string postData = "";

                string strtemp = FaceRecognition.Port(url, postData);

                if (!FaceRecognition.json(strtemp))
                {
                    untCommon.InfoMsg("该IP地址不可用！");
                    return false;
                }
                JObject joModel = (JObject)JsonConvert.DeserializeObject(strtemp);
                if (!bool.Parse(joModel["success"].ToString()))
                {
                    untCommon.InfoMsg("该IP地址不可用！");
                    return false;
                }
            }
            return true;
        }

        private void frmDeviceManagementEdit_Load(object sender, EventArgs e)
        {
            AMBaseDepartmentBLL AMBaseDepartmentBLL = new AMBaseDepartmentBLL();
            List<string> list = new List<string>();
            var rows = AMBaseDepartmentBLL.GetList_D_Name();
            for (int i = 0; i < rows.Count; i++)
            {
                list.Add(rows[i].F_FullName);
            }
            cmb_Department.DataSource = list;

            if (string.IsNullOrEmpty(ID))
            {
                return;
            }
            txt_DeviceName.Text = DeviceName;
            txt_IP.Text = IP;
            cmb_Department.Text = Department;
            txt_Remarks.Text = Remarks;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmb_Department_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_Department.Text == "生产车间")
            {
                Mes_TeamBLL TeamBLL = new Mes_TeamBLL();
                var Teams = TeamBLL.GetList_Team("");
                cmbTeam.DataSource = Teams;
                cmbTeam.ValueMember = "T_Code";
                cmbTeam.DisplayMember = "T_Name";
            }
        }
    }
}
