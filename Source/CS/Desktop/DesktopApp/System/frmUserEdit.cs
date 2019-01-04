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

                this.U_UpdateBy.Text = User.F_Account;
                this.U_Code.ReadOnly = true;
            }

            if (OperationType == 1)
            {
                U_CreateBy.Text = User.F_Account;
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

                U_Code.Text=user.F_Account;
                U_Name.Text=user.F_RealName;
                U_Pwd.Text=user.F_Password ;
                U_Sex.SelectedItem=user.F_Gender;
                department = user.D_Code;
                role = user.R_Code;

                U_Phone.Text=user.F_Mobile;
                U_Email.Text=user.F_Email;
                U_QQ.Text=user.F_OICQ;
                U_WeChat.Text=user.F_WeChat;

                U_Address.Text=user.U_Address;
                U_Remark.Text=user.F_Description;
                U_Active.Checked=user.F_EnabledMark;

                U_CreateBy.Text = user.F_CreateUserName;
                U_UpdateBy.Text = user.F_ModifyUserName;

                if (user.F_CreateDate.HasValue)
                    U_CreateDate.Value = user.F_CreateDate.Value;
                else
                {
                    U_CreateDate.Value = DateTime.Now;
                }

                if (user.F_ModifyDate.HasValue)
                    U_UpdateDate.Value = user.F_ModifyDate.Value;
                else
                {
                    U_UpdateDate.Value = DateTime.Now;
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

                    user.F_Account = U_Code.Text;
                    user.F_RealName = U_Name.Text;
                    user.F_Password = U_Pwd.Text;
                    user.F_Gender = U_Sex.Text;
                    user.D_Code = D_Code.SelectedValue.ToString();

                    user.R_Code = R_Code.SelectedValue.ToString();
                    user.F_Mobile = U_Phone.Text;
                    user.F_Email = U_Email.Text;
                    user.F_OICQ = U_QQ.Text;
                    user.F_WeChat = U_WeChat.Text;

                    user.U_Address = U_Address.Text;
                    user.F_Description = U_Remark.Text;
                    user.F_EnabledMark = U_Active.Checked;

                    user.F_CreateUserName = U_CreateBy.Text;
                    user.F_CreateDate = U_CreateDate.Value;
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

                    user.F_Account = U_Code.Text;
                    user.F_RealName = U_Name.Text;
                    user.F_Password = U_Pwd.Text;
                    user.F_Gender = U_Sex.Text;
                    user.D_Code = D_Code.SelectedValue.ToString();

                    user.R_Code = R_Code.SelectedValue.ToString();
                    user.F_Mobile = U_Phone.Text;
                    user.F_Email = U_Email.Text;
                    user.F_OICQ = U_QQ.Text;
                    user.F_WeChat = U_WeChat.Text;

                    user.U_Address = U_Address.Text;
                    user.F_Description = U_Remark.Text;
                    user.F_EnabledMark = U_Active.Checked;

                    user.F_CreateUserName = U_CreateBy.Text;
                    user.F_CreateDate = U_CreateDate.Value;

                    user.F_ModifyUserName = U_UpdateBy.Text;
                    user.F_ModifyDate = U_UpdateDate.Value;

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
            if (string.IsNullOrEmpty(U_Code.Text))
            {
                untCommon.InfoMsg("用户编码不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(U_Name.Text))
            {
                untCommon.InfoMsg("用户名称不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(U_Pwd.Text))
            {
                untCommon.InfoMsg("用户密码不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(U_Sex.Text))
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
