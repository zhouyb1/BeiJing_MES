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
using WeifenLuo.WinFormsUI.Docking;

namespace DesktopApp
{
    public partial class frmPowerSet : DockContent
    {
        private frmMain frmParent;
        private string Role_Code = "";
        private List<SysModule> Modules=new List<SysModule>();

        public frmPowerSet(frmMain _frmMain)
        {
            InitializeComponent();
            frmParent = _frmMain;
        }

        private void frmPowerSet_Load(object sender, EventArgs e)
        {
            loadRole();
        }

        private void loadRole()
        {
            try
            {
                listRole.Items.Clear();
                listRole.BeginUpdate();

                listRole.View = View.List;
                listRole.SmallImageList = imageList;
                listRole.LargeImageList = imageList;

                SysRoleBLL rolebll = new SysRoleBLL();
                var roles = rolebll.loadData("");

                foreach (var role in roles)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = role.R_Name;
                    lvi.Tag = role.R_Code;
                    lvi.ImageIndex = 0;

                    listRole.Items.Add(lvi);
                }

                listRole.Show();
                listRole.EndUpdate();
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("权限管理加载角色数据异常："+ex.Message);
            }
        }

        /// <summary>
        /// 加载模块
        /// </summary>
        private void loadModule(string role)
        {
            try
            {
                Role_Code = role;

                treeMenu.Nodes.Clear();
                treeMenu.BeginUpdate();
                treeMenu.CheckBoxes = true;

                SysModuleBLL modulebll = new SysModuleBLL();
                var modules = modulebll.loadData(role);

                if (modules == null || modules.Count < 1)
                    return;

                TreeNode tnRoot = new TreeNode();
                tnRoot.Text = "所有模块";
                tnRoot.Tag = "00";


                foreach (var row in modules.Where(r => string.IsNullOrEmpty(r.M_ParentCode)))
                {
                    TreeNode tnNode = new TreeNode();
                    tnNode.Text = row.M_Name;
                    tnNode.Tag = row.M_Code;
                    tnNode.Checked = row.M_Choice;
                    

                    FillTree(tnNode, modules);

                    tnRoot.Nodes.Add(tnNode);
                }

                treeMenu.Nodes.Add(tnRoot);
                tnRoot.ExpandAll();

                
                treeMenu.Show();
                treeMenu.EndUpdate();
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("权限管理加载模块数据异常：" + ex.Message);
            }
        }


        private void FillTree(TreeNode node, List<SysModule> modules)
        {
            var rows = modules.Where(r => r.M_ParentCode == node.Tag.ToString()).ToList();
            if (rows.Count > 0)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    TreeNode tn = new TreeNode();
                    tn.Text = rows[i].M_Name;
                    tn.Tag = rows[i].M_Code;
                    tn.Checked = rows[i].M_Choice;

                    if (rows[i].M_ParentCode == node.Tag.ToString())
                    {
                        FillTree(tn, modules);
                    }

                    node.Nodes.Add(tn);
                }
            }
        }


        private void getAllChildNodes(TreeNode treeNode)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                //表示选择了
                if (node.Checked)
                {
                    SysModule module=new SysModule();
                    module.M_Code = node.Tag.ToString();

                    Modules.Add(module);
                }

                if (node.Nodes.Count > 0)
                {
                    this.getAllChildNodes(node);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listRole.SelectedItems.Count <  1)
                return;

            var role = listRole.SelectedItems[0].Tag.ToString();
            loadModule(role);
        }

        private void treeMenu_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeViewCheck.CheckControl(e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Modules.Clear();

                TreeNode tnRoot = treeMenu.TopNode;
                getAllChildNodes(tnRoot);

                SysModuleBLL modulebll = new SysModuleBLL();
                int result = modulebll.saveData(Role_Code, Modules);

                if (result > 0)
                {
                    untCommon.InfoMsg("权限设置成功！");
                }
                else
                {
                    untCommon.InfoMsg("权限设置失败！");
                }
            }
            catch (Exception ex)
            {
               untCommon.ErrorMsg("权限管理权限设置异常："+ex.Message);
            }
        }

        
    }

    /// <summary>
    /// 节点选择类
    /// </summary>
    public static class TreeViewCheck
    {
        /// <summary>
        /// 系列节点 Checked 属性控制
        /// </summary>
        /// <param name="e"></param>
        public static void CheckControl(TreeViewEventArgs e)
        {
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node != null && !Convert.IsDBNull(e.Node))
                {
                    CheckParentNode(e.Node);
                    if (e.Node.Nodes.Count > 0)
                    {
                        CheckAllChildNodes(e.Node, e.Node.Checked);
                    }
                }
            }
        }

        #region 私有方法

        //改变所有子节点的状态
        private static void CheckAllChildNodes(TreeNode pn, bool IsChecked)
        {
            foreach (TreeNode tn in pn.Nodes)
            {
                tn.Checked = IsChecked;

                if (tn.Nodes.Count > 0)
                {
                    CheckAllChildNodes(tn, IsChecked);
                }
            }
        }

        //改变父节点的选中状态，此处为所有子节点不选中时才取消父节点选中，可以根据需要修改
        private static void CheckParentNode(TreeNode curNode)
        {
            bool bChecked = false;

            if (curNode.Parent != null)
            {
                foreach (TreeNode node in curNode.Parent.Nodes)
                {
                    if (node.Checked)
                    {
                        bChecked = true;
                        break;
                    }
                }

                if (bChecked)
                {
                    curNode.Parent.Checked = true;
                    CheckParentNode(curNode.Parent);
                }
                else
                {
                    curNode.Parent.Checked = false;
                    CheckParentNode(curNode.Parent);
                }
            }
        }

        #endregion
    }
}
