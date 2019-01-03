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
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmAreaSet : DockContent
    {
        private frmMain frmParent;
        public frmAreaSet(frmMain _frmMain)
        {
            InitializeComponent();
            frmParent = _frmMain;
        }

        private void frmPowerSet_Load(object sender, EventArgs e)
        {
            loadArea();
        }

        /// <summary>
        /// 加载区域
        /// </summary>
        public void loadArea()
        {
            try
            {

                treeMenu.Nodes.Clear();

                AreaBLL areabll = new AreaBLL();
                var areas = areabll.loadData();

                if (areas == null || areas.Count < 1)
                    return;


                TreeNode tnRoot = new TreeNode();
                tnRoot.Text = "所有区域";
                tnRoot.Tag = "00";


                foreach (var row in areas.Where(r => string.IsNullOrEmpty(r.A_Parent)))
                {
                    TreeNode tnNode = new TreeNode();
                    tnNode.Text = row.A_Name;
                    tnNode.Tag = row.A_Code;

                    FillTree(tnNode, areas);

                    tnRoot.Nodes.Add(tnNode);
                }
                
                treeMenu.Nodes.Add(tnRoot);
                tnRoot.ExpandAll();
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("区域管理加载异常："+ex.Message);
            }
        }

        private void FillTree(TreeNode node,List<Area> areas)
        {
            var rows = areas.Where(r => r.A_Parent == node.Tag.ToString()).ToList();
            if (rows.Count > 0)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = rows[i].A_Name;
                    tn.Tag = rows[i].A_Code;
               

                    if (rows[i].A_Parent == node.Tag.ToString())
                    {
                        FillTree(tn, areas);
                    }

                    node.Nodes.Add(tn);
                }
            }
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void cmsAdd_Click(object sender, EventArgs e)
        {
           TreeNode tn =treeMenu.SelectedNode;
           string _ParentCode = "00";
           if (tn != null)
                _ParentCode = tn.Tag.ToString();

            frmAreaEdit frmAreaEdit = new frmAreaEdit(this, frmParent.User,_ParentCode,"" , 1);
            frmAreaEdit.ShowDialog();
        }

        private void cmsEdit_Click(object sender, EventArgs e)
        {
            TreeNode tn = treeMenu.SelectedNode;
            string A_Code = "";
            if (tn == null || tn.Tag.ToString()=="00")
            {
                return;
            }
            else
            {
                A_Code = tn.Tag.ToString();
            }

            frmAreaEdit frmAreaEdit = new frmAreaEdit(this, frmParent.User, "", A_Code, 2);
            frmAreaEdit.ShowDialog();
        }

        private void cmsDelete_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode tn = treeMenu.SelectedNode;
                string A_Code = "";
                string A_Name = "";

                if (tn == null || tn.Tag.ToString() == "00")
                {
                    return;
                }
                else
                {
                    if (tn.Nodes.Count > 0)
                    {
                        untCommon.InfoMsg("请先删除子区域！");
                        return;
                    }
                    A_Name = tn.Text.ToString();
                    A_Code = tn.Tag.ToString();
                }

                if (untCommon.QuestionMsg("您确定删除 " + A_Name + " 该项吗？"))
                {
                    AreaBLL areabll = new AreaBLL();
                    if (areabll.Del(A_Code) > 0)
                    {
                        loadArea();
                        untCommon.InfoMsg("删除成功！");
                    }
                    else
                    {
                        untCommon.InfoMsg("修改失败！");
                    }
                }
            }
            catch (Exception ex)
            {
               untCommon.InfoMsg("区域管理删除数据异常："+ex.Message);
            }

        }
    }
}
