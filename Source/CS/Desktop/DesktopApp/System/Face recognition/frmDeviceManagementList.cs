using Business.System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmDeviceManagementList : DockContent
    {
        public frmMain frmMain { get; set; }
        private int rowindex;//获得当前选中的行的索引
        public frmDeviceManagementList()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmDeviceManagementEdit frmDeviceManagementEdit = new frmDeviceManagementEdit("","","","","");
            if (frmDeviceManagementEdit.ShowDialog() == DialogResult.OK)
            {
                frmDeviceManagementEdit.Dispose();
                loadData();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
            MesDeviceBLL.GetList_Name(txtKey.Text.Trim());
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (rowindex < 0)
            {
                return;
            }
            frmDeviceManagementEdit frmDeviceManagementEdit = new frmDeviceManagementEdit(dataGridView.Rows[rowindex].Cells["设备ID"].Value.ToString(), dataGridView.Rows[rowindex].Cells["设备名称"].Value.ToString(), dataGridView.Rows[rowindex].Cells["部门"].Value.ToString(), dataGridView.Rows[rowindex].Cells["IP地址"].Value.ToString(), dataGridView.Rows[rowindex].Cells["备注"].Value.ToString());
            if (frmDeviceManagementEdit.ShowDialog() == DialogResult.OK)
            {
                frmDeviceManagementEdit.Dispose();
                loadData();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmDevice frmDevice = new frmDevice(frmMain);
            frmDevice.Close();
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
            var rows = MesDeviceBLL.DeleteEntity(dataGridView.Rows[rowindex].Cells["设备ID"].Value.ToString());

            if (rows > 0)
            {
                untCommon.InfoMsg("删除成功！");
                loadData();
                return;
            }
        }

        private void frmDeviceManagementList_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            loadData();
        }

        private void loadData()
        {
            try
            {
                MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
                var rows = MesDeviceBLL.GetList();

                if (rows == null || rows.Count < 1)
                {
                    untCommon.InfoMsg("没有任何设备信息数据！");
                    dataGridView.DataSource = rows;
                    return;
                }
                dataGridView.DataSource = rows;
                for (int i = 0; i < rows.Count; i++)
                {
                    string url = "http://" + rows[i].D_IP + ":8090/getDeviceKey";

                    string postData = "";

                    string strtemp = FaceRecognition.Port(url, postData);

                    if (FaceRecognition.json(strtemp))
                    {
                        JObject joModel = (JObject)JsonConvert.DeserializeObject(strtemp);
                        string strData;
                        if (bool.Parse(joModel["success"].ToString()))
                        {
                            strData = joModel["result"].ToString();
                        }
                        else
                        {
                            strData = joModel["msg"].ToString();
                        }
                        if (strData == "1")
                        {
                            this.dataGridView.Rows[i].Cells["设备状态"].Value = "在线";
                        }
                        else
                        {
                            this.dataGridView.Rows[i].Cells["设备状态"].Value = "离线";
                        }
                    }
                    else
                    {
                        //untCommon.InfoMsg("该IP地址不可用！");
                        this.dataGridView.Rows[i].Cells["设备状态"].Value = "离线";
                    }
                }
                       
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("设备信息加载数据异常：" + ex.Message);
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rowindex = e.RowIndex;//获取当前行数
        }
    }
}
