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
using Business.System;
using Model;

namespace DesktopApp
{
    public partial class frmEquipmentEdit : Form
    {
        private int OperationType = 1;//1新增、2修改、3明细
        private string PrimaryKey = "";
        private SysUser User;
        private frmEquipmentList frmParent;
        private List<Area> dropdownAreaData;
        private List<SysDictionary> dropdownDictionaryData; 
        

        public frmEquipmentEdit(frmEquipmentList _frmEquipmentList, SysUser _User, string _PrimaryKey, int _OperationType)
        {
            InitializeComponent();

            PrimaryKey = _PrimaryKey;
            OperationType = _OperationType;
            User = _User;
            frmParent = _frmEquipmentList;

            loadDropdown();

            if (OperationType == 3)
            {
                getDetail();

                this.btnSave.Visible = false;
            }

            if (OperationType == 2)
            {
                getDetail();

                this.E_UpdateBy.Text = User.U_Code;
                this.E_Code.ReadOnly = true;
                this.E_City.Enabled = false;
                this.E_MonitorNumber.ReadOnly = true;
            }

            if (OperationType == 1)
            {
                E_CreateBy.Text = User.U_Code;
            }

        }

        private void getDetail()
        {
            try
            {
                EquipmentBLL EquipmentBLL = new EquipmentBLL();
                Equipment Equipment = EquipmentBLL.getDetail(PrimaryKey);

                E_Code.Text = Equipment.E_Code;
                E_BoxCode.Text = Equipment.E_BoxCode;
                E_City.SelectedValue = Equipment.E_City;
                E_Village.SelectedValue = Equipment.E_Village;
                E_Address.Text = Equipment.E_Address;

                E_IP.Text = Equipment.E_IP;
                E_MonitorNumber.Text = Equipment.E_MonitorNumber;
                E_CameraType.SelectedValue = Equipment.E_CameraType;
                E_CameraQty.Value = Equipment.E_CameraQty;
                E_Direction.Text = Equipment.E_Direction;

                E_Range.Text = Equipment.E_Range;
                if (string.IsNullOrEmpty(Equipment.E_InstallType))
                {
                    E_InstallType.SelectedIndex = -1;
                }
                else
                {
                    E_InstallType.SelectedValue = Equipment.E_InstallType;
                }
               
                E_Height.Value = Equipment.E_Height.HasValue ? Equipment.E_Height.Value : 0;
                E_Width.Value = Equipment.E_Width.HasValue ? Equipment.E_Width.Value : 0;

                if (string.IsNullOrEmpty(Equipment.E_Longitude))
                {
                    E_Longitude_D.Value= 0;
                    E_Longitude_F.Value = 0;
                    E_Longitude_M.Value = 0;
                }
                else
                {
                    int d_index = Equipment.E_Longitude.IndexOf("°");
                    int f_index = Equipment.E_Longitude.IndexOf("′");
                    int m_index = Equipment.E_Longitude.IndexOf("″");

                    E_Longitude_D.Value = decimal.Parse(Equipment.E_Longitude.Substring(0, d_index));
                    E_Longitude_F.Value = decimal.Parse(Equipment.E_Longitude.Substring(d_index + 1, f_index-d_index-1));
                    E_Longitude_M.Value = decimal.Parse(Equipment.E_Longitude.Substring(f_index + 1, m_index - f_index-1));
                }

                if (string.IsNullOrEmpty(Equipment.E_Latitude))
                {
                    E_Latitude_D.Value = 0;
                    E_Latitude_F.Value = 0;
                    E_Latitude_M.Value = 0;
                }
                else
                {
                    int d_index = Equipment.E_Latitude.IndexOf("°");
                    int f_index = Equipment.E_Latitude.IndexOf("′");
                    int m_index = Equipment.E_Latitude.IndexOf("″");

                    E_Latitude_D.Value = decimal.Parse(Equipment.E_Latitude.Substring(0, d_index));
                    E_Latitude_F.Value = decimal.Parse(Equipment.E_Latitude.Substring(d_index + 1, f_index - d_index-1));
                    E_Latitude_M.Value = decimal.Parse(Equipment.E_Latitude.Substring(f_index + 1, m_index - f_index-1));
                }


                if (string.IsNullOrEmpty(Equipment.E_ElectricityType))
                {
                    E_ElectricityType.SelectedIndex = -1;
                }
                else
                {
                    E_ElectricityType.SelectedValue = Equipment.E_ElectricityType;
                }
               
                E_EquipmentBoxQty.Value = Equipment.E_EquipmentBoxQty.HasValue ? Equipment.E_EquipmentBoxQty.Value : 0;
                E_OpticalFiberQty1.Value = Equipment.E_OpticalFiberQty1.HasValue? Equipment.E_OpticalFiberQty1.Value: 0;
                E_OpticalFiberQty2.Value = Equipment.E_OpticalFiberQty2.HasValue ? Equipment.E_OpticalFiberQty2.Value : 0;


                U_Active.Checked = Equipment.E_Active;

                E_CreateBy.Text = Equipment.E_CreateBy;
                E_UpdateBy.Text = Equipment.E_UpdateBy;

                if (Equipment.E_CreateDate.HasValue)
                    E_CreateDate.Value = Equipment.E_CreateDate.Value;
                else
                {
                    E_CreateDate.Value = DateTime.Now;
                }

                if (Equipment.E_UpdateDate.HasValue)
                    E_UpdateDate.Value = Equipment.E_UpdateDate.Value;
                else
                {
                    E_UpdateDate.Value = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("设备管理加载数据异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 加载下拉框数据
        /// </summary>
        private void loadDropdown()
        {
            try
            {
                AreaBLL areabll = new AreaBLL();
                dropdownAreaData = areabll.loadData();

                SysDictionaryBLL dictionarybll = new SysDictionaryBLL();
                dropdownDictionaryData = dictionarybll.loadData("", "");

                //街道办
                var E_City_Datas = dropdownAreaData.Where(r => r.A_Parent == "440106").ToList();
                E_City.DataSource = E_City_Datas;
                E_City.ValueMember = "A_Code";
                E_City.DisplayMember = "A_Name";
                E_City.SelectedIndex = -1;

                //摄像机类型
                var E_CameraType_Datas = dropdownDictionaryData.Where(r => r.D_Type == "摄像机类型").ToList();
                E_CameraType.DataSource = E_CameraType_Datas;
                E_CameraType.ValueMember = "D_Name";
                E_CameraType.DisplayMember = "D_Name";
                E_CameraType.SelectedIndex = -1;

                //安装方式
                var E_InstallType_Datas = dropdownDictionaryData.Where(r => r.D_Type == "安装方式").ToList();
                E_InstallType.DataSource = E_InstallType_Datas;
                E_InstallType.ValueMember = "D_Name";
                E_InstallType.DisplayMember = "D_Name";
                E_InstallType.SelectedIndex = -1;

                //取电方式
                var E_ElectricityType_Datas = dropdownDictionaryData.Where(r => r.D_Type == "取电方式").ToList();
                E_ElectricityType.DataSource = E_ElectricityType_Datas;
                E_ElectricityType.ValueMember = "D_Name";
                E_ElectricityType.DisplayMember = "D_Name";
                E_ElectricityType.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("设备管理加载下拉框数据异常："+ex.Message);
            }
        }

        private void E_City_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (E_City.SelectedIndex < 0)
            {
                E_Code.Text = "";
                E_BoxCode.Text = "";
            }
            else
            {
                var E_City_Code = E_City.SelectedValue.ToString();

                //社区
                var E_Village_Datas = dropdownAreaData.Where(r => r.A_Parent == E_City_Code).ToList();
                E_Village.DataSource = E_Village_Datas;
                E_Village.ValueMember = "A_Code";
                E_Village.DisplayMember = "A_Name";
                E_Village.SelectedIndex = -1;

                //string E_Code_TEMP = "44010601071320000";
                //string E_BoxCode_TEMP = E_City_Code;

                string E_Code_TEMP =E_City_Code+"011320000";
                string E_BoxCode_TEMP = E_City_Code.Replace("4401", "") + "000";

                string E_MonitorNumber_TEMP = E_MonitorNumber.Text.Trim();

                E_Code.Text = E_Code_TEMP + E_MonitorNumber_TEMP;
                E_BoxCode.Text = E_BoxCode_TEMP + E_MonitorNumber_TEMP;
            }
    
         
        }


        /// <summary>
        /// 添加
        /// </summary>
        private void addEquipment()
        {
            try
            {
                if (checkInput())
                {
                    Equipment Equipment = new Equipment();

                    Equipment.E_Code = E_Code.Text;
                    Equipment.E_BoxCode = E_BoxCode.Text;
                    Equipment.E_City = E_City.SelectedValue.ToString();
                    Equipment.E_Village = E_Village.SelectedIndex < 0 ? "" : E_Village.SelectedValue.ToString();
                    Equipment.E_Address = E_Address.Text;

                    Equipment.E_IP = E_IP.Text;
                    Equipment.E_MonitorNumber = E_MonitorNumber.Text;
                    Equipment.E_CameraType = E_CameraType.SelectedValue.ToString();
                    Equipment.E_CameraQty = int.Parse(E_CameraQty.Value.ToString());
                    Equipment.E_Direction = E_Direction.Text;

                    Equipment.E_Range = E_Range.Text;
                    Equipment.E_InstallType = E_InstallType.SelectedIndex < 0 ? "" : E_InstallType.SelectedValue.ToString();
                    Equipment.E_Height = E_Height.Value;
                    Equipment.E_Width = E_Width.Value;

                    Equipment.E_Longitude = string.Format("{0}°{1}′{2}″", E_Longitude_D.Value, E_Longitude_F.Value,
                        E_Longitude_M.Value);
                    Equipment.E_Latitude = string.Format("{0}°{1}′{2}″", E_Latitude_D.Value, E_Latitude_F.Value,
                         E_Latitude_M.Value);



                    Equipment.E_ElectricityType = E_ElectricityType.SelectedIndex < 0 ? "" : E_ElectricityType.SelectedValue.ToString();
                    Equipment.E_EquipmentBoxQty = int.Parse(E_EquipmentBoxQty.Value.ToString());
                    Equipment.E_OpticalFiberQty1 = int.Parse(E_OpticalFiberQty1.Value.ToString());
                    Equipment.E_OpticalFiberQty2 = int.Parse(E_OpticalFiberQty2.Value.ToString());
                    Equipment.E_Active = U_Active.Checked;

                    Equipment.E_CreateBy = E_CreateBy.Text;
                    Equipment.E_CreateDate = E_CreateDate.Value;

                    Equipment.E_UpdateBy = null;
                    Equipment.E_UpdateDate = null;

      
                    EquipmentBLL EquipmentBLL = new EquipmentBLL();
                     if (EquipmentBLL.Exists(Equipment.E_Code))
                    {
                        untCommon.InfoMsg("设备编码已存在！");
                    }
                    else
                    {
                        if (EquipmentBLL.Add(Equipment) > 0)
                        {
                            untCommon.InfoMsg("添加成功！");
                            //frmParent.loadData();
                            frmParent.Refresh();


                            SysLog log = new SysLog();
                            log.L_Date = DateTime.Now;
                            log.L_User = User.U_Code;
                            log.L_Module = "设备管理";
                            log.L_Button = "添加";
                            log.L_Key = Equipment.E_Code;
                            log.L_Describe = "添加设备";
                            log.L_Result = "成功";

                            SysLogBLL logBll = new SysLogBLL();
                            logBll.WriteLog(log);
                        }
                        else
                        {
                            untCommon.InfoMsg("添加失败！");

                            SysLog log = new SysLog();
                            log.L_Date = DateTime.Now;
                            log.L_User = User.U_Code;
                            log.L_Module = "设备管理";
                            log.L_Button = "添加";
                            log.L_Key = Equipment.E_Code;
                            log.L_Describe = "添加设备";
                            log.L_Result = "失败";

                            SysLogBLL logBll = new SysLogBLL();
                            logBll.WriteLog(log);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("设备管理添加数据异常："+ex.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void updateEquipment()
        {
            if (checkInput())
            {
                Equipment Equipment = new Equipment();

                Equipment.E_Code = E_Code.Text;
                Equipment.E_BoxCode = E_BoxCode.Text;
                Equipment.E_City = E_City.SelectedValue.ToString();
                Equipment.E_Village =E_Village.SelectedIndex<0?"": E_Village.SelectedValue.ToString();
                Equipment.E_Address = E_Address.Text;

                Equipment.E_IP = E_IP.Text;
                Equipment.E_MonitorNumber = E_MonitorNumber.Text;
                Equipment.E_CameraType = E_CameraType.SelectedValue.ToString();
                Equipment.E_CameraQty = int.Parse(E_CameraQty.Value.ToString());
                Equipment.E_Direction = E_Direction.Text;

                Equipment.E_Range = E_Range.Text;
                Equipment.E_InstallType = E_InstallType.SelectedIndex < 0 ? "" : E_InstallType.SelectedValue.ToString();
                Equipment.E_Height = E_Height.Value;
                Equipment.E_Width = E_Width.Value;

                Equipment.E_Longitude = string.Format("{0}°{1}′{2}″", E_Longitude_D.Value, E_Longitude_F.Value,
                    E_Longitude_M.Value);
                Equipment.E_Latitude = string.Format("{0}°{1}′{2}″", E_Latitude_D.Value, E_Latitude_F.Value,
                     E_Latitude_M.Value);


                Equipment.E_ElectricityType = E_ElectricityType.SelectedIndex < 0 ? "" : E_ElectricityType.SelectedValue.ToString();
                Equipment.E_EquipmentBoxQty = int.Parse(E_EquipmentBoxQty.Value.ToString());
                Equipment.E_OpticalFiberQty1 = int.Parse(E_OpticalFiberQty1.Value.ToString());
                Equipment.E_OpticalFiberQty2 = int.Parse(E_OpticalFiberQty2.Value.ToString());
                Equipment.E_Active = U_Active.Checked;

                Equipment.E_CreateBy = E_CreateBy.Text;
                Equipment.E_UpdateBy = E_UpdateBy.Text;
                Equipment.E_CreateDate = E_CreateDate.Value;
                Equipment.E_UpdateDate = E_UpdateDate.Value;

                EquipmentBLL EquipmentBLL = new EquipmentBLL();
                if (EquipmentBLL.Edit(Equipment) > 0)
                {
                    untCommon.InfoMsg("修改成功！");
                    //frmParent.loadData();
                    frmParent.Refresh();


                    SysLog log = new SysLog();
                    log.L_Date = DateTime.Now;
                    log.L_User = User.U_Code;
                    log.L_Module = "设备管理";
                    log.L_Button = "修改";
                    log.L_Key = Equipment.E_Code;
                    log.L_Describe = "添加设备";
                    log.L_Result = "成功";

                    SysLogBLL logBll = new SysLogBLL();
                    logBll.WriteLog(log);
                }
                else
                {
                    untCommon.InfoMsg("修改失败！");

                    SysLog log = new SysLog();
                    log.L_Date = DateTime.Now;
                    log.L_User = User.U_Code;
                    log.L_Module = "设备管理";
                    log.L_Button = "修改";
                    log.L_Key = Equipment.E_Code;
                    log.L_Describe = "添加设备";
                    log.L_Result = "失败";

                    SysLogBLL logBll = new SysLogBLL();
                    logBll.WriteLog(log);
                }
            }
        }


        /// <summary>
        /// 校验输入
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            if (string.IsNullOrEmpty(E_Code.Text))
            {
                untCommon.InfoMsg("设备编码不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(E_Code.Text))
            {
                untCommon.InfoMsg("设备编码不能为空！");
                return false;
            }

            if (E_City.SelectedIndex<0)
            {
                untCommon.InfoMsg("街道办不能为空！");
                return false;
            }

            //if (E_Village.SelectedIndex < 0)
            //{
            //    untCommon.InfoMsg("改制村不能为空！");
            //    return false;
            //}

            if (string.IsNullOrEmpty(E_Address.Text))
            {
                untCommon.InfoMsg("安装地点不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(E_IP.Text))
            {
                untCommon.InfoMsg("IP地址不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(E_MonitorNumber.Text))
            {
                untCommon.InfoMsg("监控点编号不能为空！");
                return false;
            }

            if (E_CameraType.SelectedIndex<0)
            {
                untCommon.InfoMsg("摄像机类型不能为空！");
                return false;
            }
            return true;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (OperationType == 1)
            {
                addEquipment();
            }
            else
            {
                updateEquipment();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void E_MonitorNumber_TextChanged(object sender, EventArgs e)
        {
            if (E_City.SelectedIndex < 0)
                return;

            var E_City_Code = E_City.SelectedValue.ToString();

            //string E_Code_TEMP = "4401060107132000";
            //string E_BoxCode_TEMP = E_City_Code.Replace("4401","")+"01";
            string E_Code_TEMP = E_City_Code + "011320000";
            string E_BoxCode_TEMP = E_City_Code.Replace("4401", "") + "000";

            string E_MonitorNumber_TEMP = E_MonitorNumber.Text.Trim();
            E_MonitorNumber_TEMP = E_MonitorNumber_TEMP.PadLeft(3, '0');
            E_Code.Text = E_Code_TEMP + E_MonitorNumber_TEMP;
            E_BoxCode.Text = E_BoxCode_TEMP + E_MonitorNumber_TEMP;
        }

        private void E_MonitorNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果输入的不是数字键，也不是回车键、Backspace键，则取消该输入
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)13 && e.KeyChar != (char)8)
            {
                e.Handled = true;
            } 
        }

        private void frmEquipmentEdit_Load(object sender, EventArgs e)
        {

        }

      
    }
}
