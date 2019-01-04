using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business;
using Business.System;
using Common;
using Model;
using Tools;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmEquipmentList : DockContent 
    {

        string password = "00000000";
        private int ferrorcode;
        private int frmcomportindex = 4;
        private byte fComAdr = 0xff; //当前操作的ComAdr
        private int fCmdRet = 30; //所有执行指令的返回值

        private StringBuilder sqlwhere = new StringBuilder();

        private COMInfo COM; //COM串口配置信息

        public COMInfo ComInfo
        {
            get { return COM; }
            set { COM = value; }
        }

        public frmMain frmMain { get; set; }

        public frmEquipmentList(frmMain _frmMain)
        {
            InitializeComponent();

            frmMain = _frmMain;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            sqlwhere.Clear();

            string key = "";
            string city = "";
            if (cbCity.SelectedIndex>= 0)
            {
                city = cbCity.SelectedValue.ToString();
            }
            key = txtKey.Text.Trim();
            //loadData(city, key);

            if (!string.IsNullOrEmpty(key))
            {
                sqlwhere.Append(
                    string.Format(" AND (Base_Equipment.E_Code = '{0}' OR Base_Equipment.E_BoxCode = '{0}')",
                        key));
            }

            if (!string.IsNullOrEmpty(city))
            {
                sqlwhere.Append(
                    string.Format(" AND (Base_Equipment.E_City = '{0}' )",
                        city));
            }

            Refresh();
            
        }

        public void loadData(string city="",string key="")
        {
            try
            {
                EquipmentBLL EquipmentBoxbll = new EquipmentBLL();
                var rows = EquipmentBoxbll.loadData(city,key);

                if (rows == null || rows.Count < 1)
                {
                    untCommon.InfoMsg("没有任何数据！");
                    return;
                }

                dataGridView.DataSource = rows;
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("设备管理加载数据异常："+ex.Message);
            }
        }



        /// <summary>
        /// 页面改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pagerControl_OnPageChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public void Refresh()
        {
            try
            {
                EquipmentBLL bll = new EquipmentBLL();

                int count = bll.getRowCount(sqlwhere.ToString()); //获得总行数
                pagerControl.DrawControl(count);

                int star = (pagerControl.PageSize * pagerControl.PageIndex) - pagerControl.PageSize; //开始行
                int end = pagerControl.PageSize * pagerControl.PageIndex; //结束行

                var rows = bll.getPagerData(star, end, sqlwhere.ToString()); //加载数据

                if (rows == null || rows.Count < 1)
                {
                    untCommon.InfoMsg("没有任何数据！");
                    return;
                }

                dataGridView.DataSource = rows;
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("巡检记录查询加载数据异常：" + ex.Message);
            }
        }

        private void loadDropdown()
        {
            AreaBLL areabll = new AreaBLL();
            var rows = areabll.loadData();

            //街道办
            var E_City_Datas = rows.Where(r => r.A_Parent == "440106").ToList();
            cbCity.DataSource = E_City_Datas;
            cbCity.ValueMember = "A_Code";
            cbCity.DisplayMember = "A_Name";
            cbCity.SelectedIndex = -1;
        }

        /// <summary>
        /// 加载COM通讯配置
        /// </summary>
        public void loadCOMInfo()
        {
            try
            {
                if (File.Exists(Application.StartupPath + @"\\comConfig.lsf"))
                {
                    KBFile kbFile = new KBFile();
                    kbFile.path = Application.StartupPath + @"\\comConfig.lsf";
                    ComInfo = kbFile.readObjectFromFile() as COMInfo;
                }
                else
                {
                    MessageBox.Show("您COM通讯还未配置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmSerialPortConfig config = new frmSerialPortConfig(this);
                    config.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("加载COM通讯配置异常：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmEquipmentList_Load(object sender, EventArgs e)
        {
            //loadData();
            loadCOMInfo();
            loadDropdown();


            //绑定分页控件事件
            pagerControl.OnPageChanged += pagerControl_OnPageChanged;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addEquipmentBox();
        }

        private void cmsAdd_Click(object sender, EventArgs e)
        {
            addEquipmentBox();
        }

        private void addEquipmentBox()
        {
            frmEquipmentEdit frmEquipmentEdit=new frmEquipmentEdit(this,frmMain.User,"",1);
            frmEquipmentEdit.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            updateEquipmentBox();
        }

        private void cmsEdit_Click(object sender, EventArgs e)
        {
            updateEquipmentBox();
        }

        private void updateEquipmentBox()
        {
            if (dataGridView.SelectedRows.Count < 1)
                return;

            string E_Code = dataGridView.SelectedRows[0].Cells["E_Code"].Value.ToString();
            frmEquipmentEdit frmEquipmentEdit = new frmEquipmentEdit(this, frmMain.User, E_Code, 2);
            frmEquipmentEdit.ShowDialog();
        }

        private void btnDetail_Click(object sender, EventArgs e)
        {
            displayEquipmentBox();
        }

        private void cmsDetail_Click(object sender, EventArgs e)
        {
            displayEquipmentBox();
        }
       
        private void displayEquipmentBox()
        {
            if (dataGridView.SelectedRows.Count < 1)
                return;

            string E_Code = dataGridView.SelectedRows[0].Cells["E_Code"].Value.ToString();
            frmEquipmentEdit frmEquipmentEdit = new frmEquipmentEdit(this, frmMain.User, E_Code, 3);
            frmEquipmentEdit.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            deleteEquipmentBox();
        }

        private void cmsDelete_Click(object sender, EventArgs e)
        {
            deleteEquipmentBox();
        }

        private void deleteEquipmentBox()
        {
            try
            {
                if (dataGridView.SelectedRows.Count < 1)
                    return;

                string E_Code = dataGridView.SelectedRows[0].Cells["E_Code"].Value.ToString();

                if (untCommon.QuestionMsg("您确定删除[" + E_Code + "]该项吗？"))
                {
                    EquipmentBLL EquipmentBoxbll = new EquipmentBLL();
                    if (EquipmentBoxbll.Delete(E_Code) > 0)
                    {
                        untCommon.InfoMsg("删除成功！");
                        //loadData();
                        Refresh();

                        SysLog log = new SysLog();
                        log.L_Date = DateTime.Now;
                        log.L_User = frmMain.User.F_Account;
                        log.L_Module = "设备管理";
                        log.L_Button = "删除";
                        log.L_Key = E_Code;
                        log.L_Describe = "删除设备";
                        log.L_Result = "成功";

                        SysLogBLL logBll=new SysLogBLL();
                        logBll.WriteLog(log);


                    }
                    else
                    {
                        untCommon.InfoMsg("删除失败！");

                        SysLog log = new SysLog();
                        log.L_Date = DateTime.Now;
                        log.L_User = frmMain.User.F_Account;
                        log.L_Module = "设备管理";
                        log.L_Button = "删除";
                        log.L_Key = E_Code;
                        log.L_Describe = "删除设备";
                        log.L_Result = "失败";

                        SysLogBLL logBll = new SysLogBLL();
                        logBll.WriteLog(log);
                    }
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("设备管理删除数据异常："+ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }


        private void btnRfidSet_Click(object sender, EventArgs e)
        {
            frmSerialPortConfig frmSerialPortConfig=new frmSerialPortConfig(this);
            frmSerialPortConfig.ShowDialog();
        }

        /// <summary>
        /// 写入卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWriteRfid_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count < 1)
            {
                untCommon.InfoMsg("请选择要写入的行");
                return;
            }

            if (OpenPort())
            {
                Wirte();
            }
            ClosePort();

        }

        private bool OpenPort()
        {
            string[] coms = { "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9" };
            int[] ports = { 9600, 19200, 38400, 57600, 115200 };

            int portNum = 0;
            for (int i = 0; i < coms.Length; i++)
            {
                if (ComInfo.PortName == coms[i])
                {
                    portNum = i + 1;
                    break;
                }

            }
            byte fBaud = 0;
            for (int i = 0; i < ports.Length; i++)
            {
                if (ComInfo.BaudRate == ports[i])
                {
                    fBaud = Convert.ToByte(i);
                    break;
                }

            }


            int FrmPortIndex = 0;
            string strException = string.Empty;
            if (fBaud > 2)
                fBaud = Convert.ToByte(fBaud + 2);

            fComAdr = 255;//广播地址打开设备
            fCmdRet = RWDev.OpenComPort(portNum, ref fComAdr, fBaud, ref FrmPortIndex);
            if (fCmdRet != 0)
            {
                untCommon.ErrorMsg("连接读写器失败，失败原因： " + GetReturnCodeDesc(fCmdRet));
                return false;
            }
            else
            {
                frmcomportindex = FrmPortIndex;
                return true;
            }
        }

        private void ClosePort()
        {
            if (frmcomportindex > 0)
                fCmdRet = RWDev.CloseSpecComPort(frmcomportindex);
            if (fCmdRet == 0)
                frmcomportindex = -1;
        }

        /// <summary>
        /// 16进制数组字符串转换
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>

        public static byte[] hexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }



        private void Wirte()
        {
            byte[] sendData = null;
            byte[] passwordData = null;

            string eqNo = dataGridView.SelectedRows[0].Cells["E_BoxCode"].Value.ToString();

            string code = eqNo + "AF";
            if ((code.Length % 4) != 0 || code.Length == 0)
            {
                untCommon.ErrorMsg("无效的写入数据，长度必须为4的倍数");
                return;
            }

            sendData = strToHexByte(code);
            passwordData = strToHexByte(password);

            if (sendData == null || sendData.Length < 1 || passwordData == null || passwordData.Length < 1)
            {
                untCommon.ErrorMsg("无效的写入数据");
                return;
            }

            byte ENum;
            string writeData = code;
            ENum = Convert.ToByte(writeData.Length / 4);
            byte[] EPC = new byte[ENum];

            fCmdRet = RWDev.WriteEPC_G2(ref fComAdr, passwordData, sendData, ENum, ref ferrorcode, frmcomportindex);
            string strLog = "";
            if (fCmdRet != 0)
            {

                if (fCmdRet == 0xFC)
                    strLog = "写入数据失败，原因： " + "返回错误=0x" + Convert.ToString(ferrorcode, 16) + "(" + GetErrorCodeDesc(ferrorcode) + ")";
                else
                    strLog = "写入数据失败，原因： " + GetReturnCodeDesc(fCmdRet);

                untCommon.ErrorMsg(strLog);
            }
            else
            {
                untCommon.InfoMsg("设备箱码[" + eqNo + "]写入成功");
            }
        }

        private string GetReturnCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "操作成功";
                case 0x01:
                    return "询查时间结束前返回";
                case 0x02:
                    return "指定的询查时间溢出";
                case 0x03:
                    return "本条消息之后，还有消息";
                case 0x04:
                    return "读写模块存储空间已满";
                case 0x05:
                    return "访问密码错误";
                case 0x09:
                    return "销毁密码错误";
                case 0x0a:
                    return "销毁密码不能为全0";
                case 0x0b:
                    return "电子标签不支持该命令";
                case 0x0c:
                    return "对该命令，访问密码不能为全0";
                case 0x0d:
                    return "电子标签已经被设置了读保护，不能再次设置";
                case 0x0e:
                    return "电子标签没有被设置读保护，不需要解锁";
                case 0x10:
                    return "有字节空间被锁定，写入失败";
                case 0x11:
                    return "不能锁定";
                case 0x12:
                    return "已经锁定，不能再次锁定";
                case 0x13:
                    return "参数保存失败,但设置的值在读写模块断电前有效";
                case 0x14:
                    return "无法调整";
                case 0x15:
                    return "询查时间结束前返回";
                case 0x16:
                    return "指定的询查时间溢出";
                case 0x17:
                    return "本条消息之后，还有消息";
                case 0x18:
                    return "读写模块存储空间已满";
                case 0x19:
                    return "电子不支持该命令或者访问密码不能为0";
                case 0x1A:
                    return "标签自定义功能执行错误";
                case 0xF8:
                    return "检测天线错误";
                case 0xF9:
                    return "命令执行出错";
                case 0xFA:
                    return "有电子标签，但通信不畅，无法操作";
                case 0xFB:
                    return "无电子标签可操作";
                case 0xFC:
                    return "电子标签返回错误代码";
                case 0xFD:
                    return "命令长度错误";
                case 0xFE:
                    return "不合法的命令";
                case 0xFF:
                    return "参数错误";
                case 0x30:
                    return "通讯错误";
                case 0x31:
                    return "CRC校验错误";
                case 0x32:
                    return "返回数据长度有错误";
                case 0x33:
                    return "通讯繁忙，设备正在执行其他指令";
                case 0x34:
                    return "繁忙，指令正在执行";
                case 0x35:
                    return "端口已打开";
                case 0x36:
                    return "端口已关闭";
                case 0x37:
                    return "无效句柄";
                case 0x38:
                    return "无效端口";
                case 0xEE:
                    return "读写器处于主动模式";
                default:
                    return "";
            }
        }

        private string GetErrorCodeDesc(int cmdRet)
        {
            switch (cmdRet)
            {
                case 0x00:
                    return "其它错误";
                case 0x03:
                    return "存储器超限或不被支持的PC值";
                case 0x04:
                    return "存储器锁定";
                case 0x0b:
                    return "电源不足";
                case 0x0f:
                    return "非特定错误";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        private static byte[] strToHexByte(string hexString)
        {
            byte[] returnBytes = null;

            try
            {
                hexString = hexString.Replace(" ", "");
                if ((hexString.Length % 2) != 0)
                    hexString += " ";

                returnBytes = new byte[hexString.Length / 2];
                for (int i = 0; i < returnBytes.Length; i++)
                    returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);

                return returnBytes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView.RowCount < 1)
                    return;

                //保存文件
                string saveFileName = "";
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xls";
                saveDialog.Filter = "Excel文件|*.xls";
                saveDialog.FileName = "设备信息";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    saveFileName = saveDialog.FileName;
                    NPOIExcel excel = new NPOIExcel();

                    bool bl = excel.SaveToExcelNew(saveFileName, dataGridView);

                    if (bl)
                    {
                        untCommon.InfoMsg("导出成功！");
                    }
                    else
                    {
                        untCommon.InfoMsg("导出失败！");
                    }
                }



            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("设备信息导出异常：" + ex.Message);
            }
        }
    }
}
