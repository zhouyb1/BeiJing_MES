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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmRecordQuery : DockContent
    {
        public frmRecordQuery()
        {
            InitializeComponent();
        }

        private void frmRecordQuery_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            load();
        }

        private void load()
        {
            SysUserBLL userbll = new SysUserBLL();
            var user = userbll.loadData("");

            AMBaseDepartmentBLL AMBaseDepartmentBLL = new AMBaseDepartmentBLL();
            // var rows = AMBaseDepartmentBLL.GetList_ID(user.F_DepartmentId);

            List<string> list = new List<string>();
            var rows = AMBaseDepartmentBLL.GetList_D_Name();
            for (int i = 0; i < rows.Count; i++)
            {
                list.Add(rows[i].F_FullName);
            }
            var topNode = new TreeNode();
            topNode.Name = "0";
            topNode.Text = "全公司";
            treeView1.Nodes.Add(topNode);
            Bind(topNode, list, 0);
        }

        private void Bind(TreeNode parNode, List<string> list, int nodeId)
        {
            var childList = list;// list.FindAll(t => t.ParentId == nodeId).OrderBy(t => t.Id);

            for (int i = 0; i < list.Count; i++)
            {
                var node = new TreeNode();
                node.Name = (i + 1).ToString();
                node.Text = childList[i];
                parNode.Nodes.Add(node);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            AMBaseDepartmentBLL AMBaseDepartmentBLL = new AMBaseDepartmentBLL();
            SysUserBLL SysUserBLL = new SysUserBLL();
            SysUser SysUser = new SysUser(); List<SysUser> user = new List<SysUser>();
            user.Clear();
            string tree = treeView1.SelectedNode.Text;
            var rows =AMBaseDepartmentBLL.GetList_F_ID(tree);
            if (rows == null || rows.Count < 1)
            {
                untCommon.InfoMsg("该部门暂无人员,请选择正确的部门！");
                return;
            }
            for (int i = 0; i < rows.Count; i++)
            {
                MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
                SysUser = SysUserBLL.getDetail_F_DepartmentId(rows[i].F_DepartmentId);
                if (SysUser != null && !string.IsNullOrEmpty(SysUser.ToString()))
                {
                    var MesDeviceID = MesDeviceBLL.GetList_Deparemaent(tree);
                    string personId = "";
                    if (string.IsNullOrEmpty(txt_name.Text))
                    {
                        personId = "-1";
                    }
                    else
                    {
                        personId = txt_name.Text.Trim();//SysUser.F_EnCode
                    }
                    for (int j = 0; i < rows.Count; i++)
                    {
                        SysUser.K_Device = MesDeviceID[j].D_Name;

                        string url = "http://" + MesDeviceID[j].D_IP + ":8090/findRecords";

                        string postData = "pass=12345678&personId=" + personId + "&length=-1&index=0&startTime=0&endTime=0";

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
                                untCommon.InfoMsg(strData);
                            }
                            if (joModel["result"].ToString() == "1")
                            {
                                //this.dataGridView.Rows[i].Cells["状态"].Value = "在线";
                                SysUser.K_Status = "签到成功";
                                var data = joModel["data"];
                                var records = (JArray)data["records"];
                                var jObj = (JObject)records[0];
                                var strTime = (string)jObj["time"].ToString();
                                DateTime time = FaceRecognition.ConvertToDateTime(strTime);
                                SysUser.K_Time = time.ToString();
                            }
                            else
                            {
                                //this.dataGridView.Rows[i].Cells["状态"].Value = "离线";
                                SysUser.K_Status = "签到失败";
                            }
                        }
                        else
                        {
                            untCommon.InfoMsg("该IP地址不可用！");
                            //this.dataGridView.Rows[i].Cells["状态"].Value = "离线";
                            SysUser.K_Status = "签到失败";
                        }
                        user.Add(SysUser);
                    }
                }
            }
            if (user == null || user.Count < 1)
            {
                untCommon.InfoMsg("该部门暂无人员！");
                return;
            }
            dataGridView.DataSource = user;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(e.Node.Name) < 1)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("用户管理加载数据异常：" + ex.Message);
            }
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            photo();
        }

        private void photo()
        {
            try
            {
                string D_Code = dataGridView.SelectedRows[0].Cells["部门"].Value.ToString();
                MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
                var MesDevice = MesDeviceBLL.GetList_Deparemaent(D_Code);
                cmb_Device.DataSource = MesDevice;
                cmb_Device.DisplayMember = "D_Name";
                cmb_Image.Text = "照片1";
            }
            catch (Exception)
            {
                untCommon.InfoMsg("该员工照片显示错误！");
            }
        }

        private void cmb_Device_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                if (dataGridView.SelectedRows.Count < 1)
                    return;
                string EnCode = dataGridView.SelectedRows[0].Cells["工号"].Value.ToString();

                MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
                var MesDevice = MesDeviceBLL.GetList_Name(cmb_Device.Text);
                if (MesDevice.Count < 1)
                {
                    return;
                }
                string url = "http://" + MesDevice[0].D_IP + ":8090/face/find";

                string postData = "pass=" + 12345678 + "&personId=" + EnCode + "";

                string strtemp = FaceRecognition.Port(url, postData);
                if (!FaceRecognition.json(strtemp))
                {
                    untCommon.InfoMsg("该IP地址不可用！");
                    return;
                }
                JObject joModel = (JObject)JsonConvert.DeserializeObject(strtemp);
                if (!bool.Parse(joModel["success"].ToString()))
                {
                    untCommon.InfoMsg(joModel["msg"].ToString());
                    return;
                }
                else
                {
                    //untCommon.InfoMsg("人脸识别注册成功！");
                    var jData = (JArray)joModel["data"];
                    string strUrl = "";
                    List<string> list = new List<string>();
                    for (var i = 0; i < jData.Count; i++)
                    {
                        var jObj = (JObject)jData[i];
                        strUrl = (string)jObj["path"].ToString();
                        list.Add(strUrl);
                    }
                    Stream stream = FaceRecognition.Info(list[0].ToString());
                    pictureBox1.Image = Image.FromStream(stream);
                }
            }
            catch (Exception)
            {
                untCommon.InfoMsg("照片显示错误！");
            }
        }

        private void cmb_Image_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                if (dataGridView.SelectedRows.Count < 1)
                    return;
                string EnCode = dataGridView.SelectedRows[0].Cells["工号"].Value.ToString();

                MesDeviceBLL MesDeviceBLL = new MesDeviceBLL();
                var MesDevice = MesDeviceBLL.GetList_Name(cmb_Device.Text);

                string url = "http://" + MesDevice[0].D_IP + ":8090/face/find";

                string postData = "pass=" + 12345678 + "&personId=" + EnCode + "";

                string strtemp = FaceRecognition.Port(url, postData);
                if (!FaceRecognition.json(strtemp))
                {
                    untCommon.InfoMsg("该IP地址不可用！");
                    return;
                }
                JObject joModel = (JObject)JsonConvert.DeserializeObject(strtemp);
                if (!bool.Parse(joModel["success"].ToString()))
                {
                    untCommon.InfoMsg(joModel["msg"].ToString());
                    return;
                }
                else
                {
                    //untCommon.InfoMsg("人脸识别注册成功！");
                    var jData = (JArray)joModel["data"];
                    string strUrl = "";
                    int number = 0;
                    List<string> list = new List<string>();
                    for (var i = 0; i < jData.Count; i++)
                    {
                        var jObj = (JObject)jData[i];
                        strUrl = (string)jObj["path"].ToString();
                        list.Add(strUrl);
                    }
                    switch (cmb_Image.Text)
                    {
                        case "照片1":
                            number = 0;
                            break;
                        case "照片2":
                            number = 1;
                            break;
                        case "照片3":
                            number = 2;
                            break;
                    }
                    Stream stream = FaceRecognition.Info(list[number]);
                    pictureBox1.Image = Image.FromStream(stream);
                }
            }
            catch (Exception)
            {
                untCommon.InfoMsg("照片显示错误！");
            }
        }
    }
}
