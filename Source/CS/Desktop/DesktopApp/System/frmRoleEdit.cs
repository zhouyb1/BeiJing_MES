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

namespace DesktopApp
{
    public partial class frmRoleEdit : Form
    {
        private int OperationType = 1;//1新增、2修改、3明细
        private string PrimaryKey = "";
        private SysUser User;
        private frmRoleList frmParent;

        public frmRoleEdit(frmRoleList _frmRoleList, SysUser _User, string _PrimaryKey, int _OperationType)
        {
            InitializeComponent();

            PrimaryKey = _PrimaryKey;
            OperationType = _OperationType;
            User = _User;
            frmParent = _frmRoleList;

            if (OperationType == 3)
            {
                getDetail();

                this.btnSave.Visible = false;
            }

            if (OperationType == 2)
            {
                getDetail();

                this.R_UpdateBy.Text = User.F_Account;
                this.R_Code.ReadOnly = true;
            }

            if (OperationType == 1)
            {
                R_CreateBy.Text = User.F_Account;
            }

        }

        private void getDetail()
        {
            try
            {
                SysRoleBLL rolebll = new SysRoleBLL();
                SysRole role = rolebll.getDetail(PrimaryKey);

                R_Code.Text = role.R_Code;
                R_Name.Text = role.R_Name;
                R_Remark.Text = role.R_Remark;

                R_CreateBy.Text = role.R_CreateBy;
                R_UpdateBy.Text = role.R_UpdateBy;

                if (role.R_CreateDate.HasValue)
                    R_CreateDate.Value = role.R_CreateDate.Value;
                else
                {
                    R_CreateDate.Value = DateTime.Now;
                }

                if (role.R_UpdateDate.HasValue)
                    R_UpdateDate.Value = role.R_UpdateDate.Value;
                else
                {
                    R_UpdateDate.Value = DateTime.Now;
                    ;
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("角色管理加载数据异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        private void addRole()
        {
            try
            {
                if (checkInput())
                {
                    SysRole role = new SysRole();

                    role.R_Code = R_Code.Text;
                    role.R_Name = R_Name.Text;
                    role.R_Remark = R_Remark.Text;

                    role.R_CreateBy = R_CreateBy.Text;
                    role.R_CreateDate = R_CreateDate.Value;
                    role.R_UpdateBy = null;
                    role.R_UpdateDate = null;

                    SysRoleBLL rolebll = new SysRoleBLL();
                    if (rolebll.Exists(role.R_Code))
                    {
                        untCommon.InfoMsg("角色编号已存在！");
                    }
                    else
                    {
                        if (rolebll.Add(role) > 0)
                        {
                            untCommon.InfoMsg("添加成功！");
                            frmParent.loadData();
                        }
                        else
                        {
                            untCommon.InfoMsg("添加失败！");
                        }
                    }
                   
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("角色管理添加数据异常：" + ex);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void updateRole()
        {
            try
            {
                SysRole role = new SysRole();

                role.R_Code = R_Code.Text;
                role.R_Name = R_Name.Text;
                role.R_Remark = R_Remark.Text;

                role.R_CreateBy = R_CreateBy.Text;
                role.R_CreateDate = R_CreateDate.Value;

                role.R_UpdateBy = R_UpdateBy.Text;
                role.R_UpdateDate = R_UpdateDate.Value;

                SysRoleBLL rolebll = new SysRoleBLL();
                if (rolebll.Edit(role) > 0)
                {
                    untCommon.InfoMsg("修改成功！");
                    frmParent.loadData();
                }
                else
                {
                    untCommon.InfoMsg("修改失败！");
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("角色管理更新数据异常：" + ex);
            }
        }


        /// <summary>
        /// 校验输入
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            if (string.IsNullOrEmpty(R_Code.Text))
            {
                untCommon.InfoMsg("角色编码不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(R_Name.Text))
            {
                untCommon.InfoMsg("角色名称不能为空！");
                return false;
            }
            return true;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (OperationType == 1)
            {
                addRole();
            }
            else
            {
                updateRole();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
