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
    }
}
