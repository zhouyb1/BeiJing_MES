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
    public partial class frmUserEdit : Form
    {
        private int OperationType = 1;//1新增、2修改、3明细
        private string PrimaryKey = "";
        private SysUser User;
        private frmUserList frmParent;
        private string department = "";
        private string role = "";
        
        public frmUserEdit(frmUserList _frmUserList, SysUser _User, string _PrimaryKey, int _OperationType)
        {
            InitializeComponent();

            PrimaryKey = _PrimaryKey;
            OperationType = _OperationType;
            User = _User;
            frmParent = _frmUserList;

            if (OperationType == 3)
            {
                getDetail();

                this.btnSave.Visible = false;
            }

            if (OperationType == 2)
            {
                getDetail();

                this.F_ModifyUserName.Text = User.F_Account;
                this.F_Account.ReadOnly = true;
            }

            if (OperationType == 1)
            {
                F_CreateUserName.Text = User.F_Account;
            }

            loadDroplistData();
        }

        private void loadDroplistData()
        {
            try
            {
                SysDepartmentBLL departmentbll = new SysDepartmentBLL();
                var rows = departmentbll.loadData("");
                D_Code.DataSource = rows;
                D_Code.ValueMember = "D_Code";
                D_Code.DisplayMember = "D_Name";
                if (string.IsNullOrEmpty(department))
                {
                    D_Code.SelectedIndex = 0;
                }
                else
                {
                    D_Code.SelectedValue = department;
                }


                SysRoleBLL rolebll = new SysRoleBLL();
                var datas = rolebll.loadData("");
                R_Code.DataSource = datas;
                R_Code.ValueMember = "R_Code";
                R_Code.DisplayMember = "R_Name";
                if (string.IsNullOrEmpty(role))
                {
                    R_Code.SelectedIndex = 0;
                }
                else
                {
                    R_Code.SelectedValue = role;
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("用户管理加载数据异常：" + ex.Message);
            }
        }

        private void getDetail()
        {
            try
            {
                SysUserBLL userbll = new SysUserBLL();
                SysUser user = userbll.getDetail(PrimaryKey);

                F_Account.Text=user.F_Account;
                F_RealName.Text=user.F_RealName;
                F_Password.Text = "******";//user.F_Password ;
                F_Gender.SelectedItem=user.F_Gender==1?"男":"女";
                department = user.D_Code;
                role = user.R_Code;

                F_Mobile.Text=user.F_Mobile;
                F_Email.Text=user.F_Email;
                F_OICQ.Text=user.F_OICQ;
                F_WeChat.Text=user.F_WeChat;

                U_Address.Text=user.U_Address;
                F_Description.Text=user.F_Description;
                F_EnabledMark.Checked=user.F_EnabledMark;

                F_CreateUserName.Text = user.F_CreateUserName;
                F_ModifyUserName.Text = user.F_ModifyUserName;

                if (user.F_CreateDate.HasValue)
                    F_CreateDate.Value = user.F_CreateDate.Value;
                else
                {
                    F_CreateDate.Value = DateTime.Now;
                }

                if (user.F_ModifyDate.HasValue)
                    F_ModifyDate.Value = user.F_ModifyDate.Value;
                else
                {
                    F_ModifyDate.Value = DateTime.Now;
                    ;
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("用户管理加载数据异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        private void addUser()
        {
            try
            {
                if (checkInput())
                {
                    SysUser user = new SysUser();

                    user.F_Account = F_Account.Text;
                    user.F_RealName = F_RealName.Text;
                    user.F_Password = F_Password.Text;
                    user.F_Gender = F_Gender.Text=="男"?1:0;
                    user.D_Code = D_Code.SelectedValue.ToString();

                    user.R_Code = R_Code.SelectedValue.ToString();
                    user.F_Mobile = F_Mobile.Text;
                    user.F_Email = F_Email.Text;
                    user.F_OICQ = F_OICQ.Text;
                    user.F_WeChat = F_WeChat.Text;

                    user.U_Address = U_Address.Text;
                    user.F_Description = F_Description.Text;
                    user.F_EnabledMark = F_EnabledMark.Checked;

                    user.F_CreateUserName = F_CreateUserName.Text;
                    user.F_CreateDate = F_CreateDate.Value;
                    user.F_ModifyUserName = null;
                    user.F_ModifyDate = null;

                    SysUserBLL userbll = new SysUserBLL();
                    if (userbll.Add(user) > 0)
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
            catch (Exception ex)
            {

                untCommon.ErrorMsg("角色管理添加数据异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void updateUser()
        {
            try
            {
                if (checkInput())
                {
                    SysUser user = new SysUser();

                    user.F_Account = F_Account.Text;
                    user.F_RealName = F_RealName.Text;
                    //user.F_Password = U_Pwd.Text;
                    user.F_Gender = F_Gender.Text=="男"?1:0;
                    user.D_Code = D_Code.SelectedValue.ToString();

                    user.R_Code = R_Code.SelectedValue.ToString();
                    user.F_Mobile = F_Mobile.Text;
                    user.F_Email = F_Email.Text;
                    user.F_OICQ = F_OICQ.Text;
                    user.F_WeChat = F_WeChat.Text;

                    user.U_Address = U_Address.Text;
                    user.F_Description = F_Description.Text;
                    user.F_EnabledMark = F_EnabledMark.Checked;

                    user.F_CreateUserName = F_CreateUserName.Text;
                    user.F_CreateDate = F_CreateDate.Value;

                    user.F_ModifyUserName = F_ModifyUserName.Text;
                    user.F_ModifyDate = F_ModifyDate.Value;

                    SysUserBLL userbll = new SysUserBLL();
                    if (userbll.Edit(user) > 0)
                    {
                        untCommon.InfoMsg("修改成功！");
                        frmParent.loadData();
                    }
                    else
                    {
                        untCommon.InfoMsg("修改失败！");
                    }
                }
            }
            catch (Exception ex)
            {
                
                untCommon.ErrorMsg("角色管理更新数据异常："+ex.Message);
            }
        }


        /// <summary>
        /// 校验输入
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            if (string.IsNullOrEmpty(F_Account.Text))
            {
                untCommon.InfoMsg("用户编码不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(F_RealName.Text))
            {
                untCommon.InfoMsg("用户名称不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(F_Password.Text))
            {
                untCommon.InfoMsg("用户密码不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(F_Gender.Text))
            {
                untCommon.InfoMsg("用户性别不能为空！");
                return false;
            }

            if (D_Code.SelectedIndex < 0)
            {
                untCommon.InfoMsg("所属部门不能为空！");
                return false;
            }

            if (R_Code.SelectedIndex < 0)
            {
                untCommon.InfoMsg("用户角色不能为空！");
                return false;
            }

            return true;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (OperationType == 1)
            {
                addUser();
            }
            else
            {
                updateUser();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
