using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
using Common;
using Model;
using Tools;

namespace DesktopApp
{
    public partial class frmSerialPortConfig : Form
    {
        private SerialPort ComDevice = new SerialPort();//COM通讯
        private frmEquipmentList frmFather;

        string password = "00000000";
        private int ferrorcode;
        private int frmcomportindex = 4;
        private byte fComAdr = 0xff; //当前操作的ComAdr
        private int fCmdRet = 30; //所有执行指令的返回值

        public frmSerialPortConfig(frmEquipmentList father)
        {
            InitializeComponent();
            frmFather = father;
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        public void Init()
        {
            btnSend.Enabled = false;
            btnReceive.Enabled = false;

            //cbbComList.Items.AddRange(SerialPort.GetPortNames());//获取现有串口
            //if (cbbComList.Items.Count < 1)
            //{
            //    MessageBox.Show("请先连接COM通讯设备！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    this.Close();
            //}

            if (File.Exists(Application.StartupPath + @"\\comConfig.lsf"))
            {
                KBFile kbFile = new KBFile();
                kbFile.path = Application.StartupPath + @"\\comConfig.lsf";
                COMInfo com = kbFile.readObjectFromFile() as COMInfo;


                cbbComList.SelectedIndex = cbbComList.Items.IndexOf(com.PortName);

                cbbBaudRate.SelectedIndex = cbbBaudRate.Items.IndexOf(com.BaudRate.ToString());

                cbbDataBits.SelectedIndex = cbbDataBits.Items.IndexOf(com.DataBits.ToString());

                cbbParity.SelectedIndex = cbbParity.Items.IndexOf(com.Parity.ToString());

                cbbStopBits.SelectedIndex = cbbStopBits.Items.IndexOf( ((int)com.StopBits).ToString() );
            }
            else
            {
                cbbComList.SelectedIndex = 0;
                cbbBaudRate.SelectedIndex = 5;
                cbbDataBits.SelectedIndex = 0;
                cbbParity.SelectedIndex = 0;
                cbbStopBits.SelectedIndex = 0;
            }

            imgTip.BackgroundImage = DesktopApp.Properties.Resources.red;

            ComDevice.DataReceived += new SerialDataReceivedEventHandler(Com_DataReceived);//绑定事件
        }

        /// <summary>
        /// 接收COM数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] ReDatas = new byte[ComDevice.BytesToRead];
            ComDevice.Read(ReDatas, 0, ReDatas.Length);//读取数据

            AddData(ReDatas);//输出数据
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data">字节数组</param>
        public void AddData(byte[] data)
        {
            if (rbtnHex.Checked)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sb.AppendFormat("{0:x2}" + " ", data[i]);
                }
                AddContent(sb.ToString().ToUpper());
            }
            else if (rbtnASCII.Checked)
            {
                AddContent(new ASCIIEncoding().GetString(data));
            }
            else if (rbtnUTF8.Checked)
            {
                AddContent(new UTF8Encoding().GetString(data));
            }
            else if (rbtnUnicode.Checked)
            {
                AddContent(new UnicodeEncoding().GetString(data));
            }
            else
            {

            }

            lblRevCount.Invoke(new MethodInvoker(delegate
            {
                lblRevCount.Text = (int.Parse(lblRevCount.Text) + data.Length).ToString();
            }));
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


        public static string byteToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();

        }

        /// <summary>
        /// 输入到显示区域
        /// </summary>
        /// <param name="content"></param>
        private void AddContent(string content)
        {
            this.BeginInvoke(new MethodInvoker(delegate
            {
                if (chkAutoLine.Checked && txtShowData.Text.Length > 0)
                {
                    txtShowData.AppendText("\r\n");
                }
                txtShowData.AppendText(content);
            }));
        }

        //清空缓存区
        private void btnClearRev_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtShowData.Clear();
        }

        //情况发送区
        private void btnClearSend_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtSendData.Clear();
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="data"></param>
        public bool SendData(byte[] data)
        {
            if (ComDevice.IsOpen)
            {
                try
                {
                    ComDevice.Write(data, 0, data.Length);//发送数据
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("串口未打开！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }



        /// <summary>
        /// 错误代码
        /// </summary>
        /// <param name="cmdRet"></param>
        /// <returns></returns>

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
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (btnOpen.Text == "打开串口")
            {
                byte fBaud;
                int portNum = cbbComList.SelectedIndex + 1;
                int FrmPortIndex = 0;
                string strException = string.Empty;
                fBaud = Convert.ToByte(cbbBaudRate.SelectedIndex);
                if (fBaud > 2)
                    fBaud = Convert.ToByte(fBaud + 2);

                fComAdr = 255;//广播地址打开设备
                fCmdRet = RWDev.OpenComPort(portNum, ref fComAdr, fBaud, ref FrmPortIndex);

                string strLog = "";
                if (fCmdRet != 0)
                {
                    btnSend.Enabled = false;
                    btnReceive.Enabled = false;

                    cbbComList.Enabled = true;
                    cbbBaudRate.Enabled = true;
                    cbbParity.Enabled = true;
                    cbbDataBits.Enabled = true;
                    cbbStopBits.Enabled = true;
                    strLog = "连接读写器失败，失败原因： " + GetReturnCodeDesc(fCmdRet);

                    btnOpen.Text = "打开串口";
                    imgTip.BackgroundImage = Properties.Resources.red;
                    untCommon.ErrorMsg(strLog);
                }
                else
                {
                    btnSend.Enabled = true;
                    btnReceive.Enabled = true;

                    cbbComList.Enabled = false;
                    cbbBaudRate.Enabled = false;
                    cbbParity.Enabled = false;
                    cbbDataBits.Enabled = false;
                    cbbStopBits.Enabled = false;

                    frmcomportindex = FrmPortIndex;
                    //strLog = "连接读写器 " + cbbComList.Text + "@" + cbbBaudRate.Text + " 成功";

                    btnOpen.Text = "关闭串口";
                    imgTip.BackgroundImage = Properties.Resources.green;
                }
            }
            else
            {
                if (frmcomportindex > 0)
                    fCmdRet = RWDev.CloseSpecComPort(frmcomportindex);
                if (fCmdRet == 0)
                    frmcomportindex = -1;

                btnSend.Enabled = false;
                btnReceive.Enabled = false;

                cbbComList.Enabled = true;
                cbbBaudRate.Enabled = true;
                cbbParity.Enabled = true;
                cbbDataBits.Enabled = true;
                cbbStopBits.Enabled = true;

                btnOpen.Text = "打开串口";
                imgTip.BackgroundImage = Properties.Resources.red;
            }
          
        }

        //private void btnOpen_Click(object sender, EventArgs e)
        //{
            //if (cbbComList.Items.Count <= 0)
            //{
            //    //MessageBox.Show("没有发现串口,请检查线路！");
            //    MessageBox.Show("没有发现串口,请检查线路！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //if (ComDevice.IsOpen == false)
            //{
            //    ComDevice.PortName = cbbComList.SelectedItem.ToString();
            //    ComDevice.BaudRate = Convert.ToInt32(cbbBaudRate.SelectedItem.ToString());
            //    ComDevice.Parity = (Parity)Convert.ToInt32(cbbParity.SelectedIndex.ToString());
            //    ComDevice.DataBits = Convert.ToInt32(cbbDataBits.SelectedItem.ToString());
            //    ComDevice.StopBits = (StopBits)Convert.ToInt32(cbbStopBits.SelectedItem.ToString());
            //    try
            //    {
            //        ComDevice.Open();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //    btnOpen.Text = "关闭串口";
            //    imgTip.BackgroundImage = Properties.Resources.green;
            //}
            //else
            //{
            //    try
            //    {
            //        ComDevice.Close();
            //        btnSend.Enabled = false;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    btnOpen.Text = "打开串口";
            //    imgTip.BackgroundImage = Properties.Resources.red;
            //}

            //btnSend.Enabled = ComDevice.IsOpen; 
            //btnReceive.Enabled = ComDevice.IsOpen; 

            //cbbComList.Enabled = !ComDevice.IsOpen;
            //cbbBaudRate.Enabled = !ComDevice.IsOpen;
            //cbbParity.Enabled = !ComDevice.IsOpen;
            //cbbDataBits.Enabled = !ComDevice.IsOpen;
            //cbbStopBits.Enabled = !ComDevice.IsOpen;
        //}

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                COMInfo com = new COMInfo();
                com.PortName = cbbComList.SelectedItem.ToString();
                com.BaudRate = Convert.ToInt32(cbbBaudRate.SelectedItem.ToString());
                com.Parity = (Parity)Convert.ToInt32(cbbParity.SelectedIndex.ToString());
                com.DataBits = Convert.ToInt32(cbbDataBits.SelectedItem.ToString());
                com.StopBits = (StopBits)Convert.ToInt32(cbbStopBits.SelectedItem.ToString());

                

                /*删除旧的配置文件*/
                if (File.Exists(Application.StartupPath + @"\\comConfig.lsf"))
                    File.Delete(Application.StartupPath + @"\\comConfig.lsf");

                KBFile kbFile = new KBFile();
                kbFile.path = Application.StartupPath + @"\\comConfig.lsf";
                if (kbFile.writeObjectToFile(com) < 1)
                {
                    MessageBox.Show("保存COM通讯配置文件失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("保存COM通讯配置文件成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmFather.ComInfo = com;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmSerialPortConfig_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
          
            byte[] sendData = null;
            byte[] passwordData = null;

            if (rbtnSendHex.Checked)
            {
                if ((txtSendData.Text.Length % 4) != 0 || txtSendData.Text.Length == 0)
                {
                    untCommon.ErrorMsg("无效的写入数据，长度必须为4的倍数");
                    return;
                }

                sendData = strToHexByte(txtSendData.Text.Trim());
                passwordData = strToHexByte(password);
            }
            else if (rbtnSendASCII.Checked)
            {
                sendData = Encoding.ASCII.GetBytes(txtSendData.Text.Trim());
                passwordData = Encoding.ASCII.GetBytes(password);
            }
            else if (rbtnSendUTF8.Checked)
            {
                sendData = Encoding.UTF8.GetBytes(txtSendData.Text.Trim());
                passwordData = Encoding.UTF8.GetBytes(password);
            }
            else if (rbtnSendUnicode.Checked)
            {
                sendData = Encoding.Unicode.GetBytes(txtSendData.Text.Trim());
                passwordData = Encoding.Unicode.GetBytes(password);
            }
            else
            {
                sendData = Encoding.ASCII.GetBytes(txtSendData.Text.Trim());
                passwordData = Encoding.ASCII.GetBytes(password);
            }

            if (sendData == null || sendData.Length < 1 || passwordData == null || passwordData.Length < 1)
            {
                untCommon.ErrorMsg("无效的写入数据");
                return;
            }

            byte ENum;
            string writeData = txtSendData.Text;
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
                untCommon.InfoMsg("写入成功");
            }


            //if (this.SendData(sendData))//发送数据成功计数
            //{
            //    lblSendCount.Invoke(new MethodInvoker(delegate
            //    {
            //        lblSendCount.Text = (int.Parse(lblSendCount.Text) + txtSendData.Text.Length).ToString();
            //    }));
            //}
            //else
            //{

            //}

        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            byte Qvalue = 0;
            byte Session = 0;
            byte TIDFlag = 0;
            byte Target = 0;
            byte FastFlag = 0;
            byte Ant = 0;
            byte InAnt = 0;
            int CardNum = 0;
            byte Scantime = 0;
            int Totallen = 0;
            int EPClen, m;
            byte[] EPC = new byte[50000];
            int CardIndex;
            string temps, temp;
            string sEPC;
            byte MaskMem = 0;
            byte[] MaskAdr = new byte[2];
            byte MaskLen = 0;
            byte[] MaskData = new byte[100];
            byte MaskFlag = 0;
            byte AdrTID = 0;
            byte LenTID = 0;
            AdrTID = 0;
            LenTID = 6;
            MaskFlag = 0;
            int cbtime = System.Environment.TickCount;
            DataGridViewRow rows = new DataGridViewRow();
            CardNum = 0;
            fCmdRet = RWDev.Inventory_G2(ref fComAdr, Qvalue, Session, MaskMem, MaskAdr, MaskLen, MaskData, MaskFlag, AdrTID, LenTID, TIDFlag, Target, InAnt, Scantime, FastFlag, EPC, ref Ant, ref Totallen, ref CardNum, frmcomportindex);
            ///////////设置网络断线重连
            if ((fCmdRet == 1) | (fCmdRet == 2) | (fCmdRet == 3) | (fCmdRet == 4))//代表已查找结束，
            {
                byte[] daw = new byte[Totallen];
                Array.Copy(EPC, daw, Totallen);

                temps = byteToHexString(daw);
                m = 0;
               
                if (CardNum == 0)
                {
                   untCommon.ErrorMsg("请放入要读取的RFID卡片,一次一张");
                   return;    
                }
                else
                {
                    EPClen = daw[m] + 1;
                    temp = temps.Substring(m * 2 + 2, EPClen * 2);
                    sEPC = temp.Substring(0, temp.Length - 2);
                    //string RSSI = Convert.ToInt32(temp.Substring(temp.Length - 2, 2), 16).ToString();

                    string showdata = "";
                    if (chkAutoLine.Checked)
                    {
                        showdata=txtShowData.Text + "\r\n" + sEPC;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtShowData.Text))
                        {
                            showdata = sEPC;
                        }
                        else
                        {
                            showdata = txtShowData.Text + " " + sEPC;
                        }
                        
                    }
                    txtShowData.Text = showdata;
                }

                //for (CardIndex = 0; CardIndex < CardNum; CardIndex++)
                //{


                //    m = m + EPClen + 1;
                //    if (sEPC.Length != (EPClen - 1) * 2)
                //    {
                //        return;
                //    }
                //    bool isonlistview = false;

                //    for (int i = 0; i < dataGridView1.RowCount; i++)
                //    {
                //        if ((dataGridView1.Rows[i].Cells[1].Value != null) && (sEPC == dataGridView1.Rows[i].Cells[1].Value.ToString()))
                //        {
                //            rows = dataGridView1.Rows[i];
                //            int ntime = Convert.ToInt32(rows.Cells[2].Value.ToString());
                //            ntime = ntime + 1;
                //            if (ntime == 99999) ntime = 1;
                //            rows.Cells[2].Value = ntime;
                //            rows.Cells[3].Value = RSSI;
                //            isonlistview = true;
                //            break;
                //        }
                //    }

                //    if (!isonlistview)
                //    {
                //        string[] arr = new string[4];
                //        arr[0] = (dataGridView1.RowCount + 1).ToString();
                //        arr[1] = sEPC;
                //        arr[2] = "1";
                //        arr[3] = RSSI;
                //        dataGridView1.Rows.Insert(dataGridView1.RowCount, arr);
                //        if (rb_epc.Checked)
                //            comboBox_EPC.Items.Add(sEPC);
                //    }
                //}
            }
        }

        private void FrmSerialPortConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (frmcomportindex > 0)
                fCmdRet = RWDev.CloseSpecComPort(frmcomportindex);
            if (fCmdRet == 0)
                frmcomportindex = -1;
        }
    }
}
