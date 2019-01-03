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
    public partial class frmDepartmentEdit : Form
    {
        private int OperationType = 1;//1新增、2修改、3明细
        private string PrimaryKey = "";
        private SysUser User;
        private frmDepartmentList frmParent;
        private string company = "";

        public frmDepartmentEdit(frmDepartmentList _frmDepartmentList, SysUser _User, string _PrimaryKey, int _OperationType)
        {
            InitializeComponent();

            PrimaryKey = _PrimaryKey;
            OperationType = _OperationType;
            User = _User;
            frmParent = _frmDepartmentList;

            if (OperationType == 3)
            {
                getDetail();

                this.btnSave.Visible = false;
            }

            if (OperationType == 2)
            {
                getDetail();

                this.D_UpdateBy.Text = User.U_Code;
                this.D_Code.ReadOnly = true;
            }

            if (OperationType == 1)
            {
                D_CreateBy.Text = User.U_Code;
            }

            loadDroplistData();
        }

        private void loadDroplistData()
        {
            try
            {
                SysCompanyBLL companybll = new SysCompanyBLL();
                var rows = companybll.loadData("");
                C_Code.DataSource = rows;
                C_Code.ValueMember = "C_Code";
                C_Code.DisplayMember = "C_Name";
                if (string.IsNullOrEmpty(company))
                {
                    C_Code.SelectedIndex = 0;
                }
                else
                {
                    C_Code.SelectedValue = company;
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("部门管理加载数据异常：" + ex.Message);
            }
           
        }

        private void getDetail()
        {
            try
            {
                SysDepartmentBLL departmentbll = new SysDepartmentBLL();
                SysDepartment department = departmentbll.getDetail(PrimaryKey);

                D_Code.Text = department.D_Code;
                D_Name.Text = department.D_Name;
                company = department.C_Code;
                D_Remark.Text = department.D_Remark;

                D_CreateBy.Text = department.D_CreateBy;
                D_UpdateBy.Text = department.D_UpdateBy;

                if (department.D_CreateDate.HasValue)
                    D_CreateDate.Value = department.D_CreateDate.Value;
                else
                {
                    D_CreateDate.Value = DateTime.Now;
                }

                if (department.D_UpdateDate.HasValue)
                    D_UpdateDate.Value = department.D_UpdateDate.Value;
                else
                {
                    D_UpdateDate.Value = DateTime.Now;
                    ;
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("部门管理加载数据异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        private void addDepartment()
        {
            try
            {
                if (checkInput())
                {
                    SysDepartment department = new SysDepartment();

                    department.D_Code = D_Code.Text;
                    department.D_Name = D_Name.Text;
                    department.C_Code = C_Code.SelectedValue.ToString();
                    department.D_Remark = D_Remark.Text;

                    department.D_CreateBy = D_CreateBy.Text;
                    department.D_CreateDate = D_CreateDate.Value;
                    department.D_UpdateBy = null;
                    department.D_UpdateDate = null;

                    SysDepartmentBLL departmentbll = new SysDepartmentBLL();

                    if (departmentbll.Exists(department.D_Code))
                    {
                        untCommon.InfoMsg("部门编号已存在！");
                    }
                    else
                    {
                        if (departmentbll.Add(department) > 0)
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
                untCommon.ErrorMsg("部门管理添加数据异常："+ex);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void updateDepartment()
        {
            try
            {
                SysDepartment department = new SysDepartment();

                department.D_Code = D_Code.Text;
                department.D_Name = D_Name.Text;
                department.C_Code = C_Code.SelectedValue.ToString();
                department.D_Remark = D_Remark.Text;

                department.D_CreateBy = D_CreateBy.Text;
                department.D_CreateDate = D_CreateDate.Value;

                department.D_UpdateBy = D_UpdateBy.Text;
                department.D_UpdateDate = D_UpdateDate.Value;

                SysDepartmentBLL departmentbll = new SysDepartmentBLL();
                if (departmentbll.Edit(department) > 0)
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
                 untCommon.ErrorMsg("部门管理更新数据异常："+ex);
            }
        }


        /// <summary>
        /// 校验输入
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            if (string.IsNullOrEmpty(D_Code.Text))
            {
                untCommon.InfoMsg("部门编码不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(D_Name.Text))
            {
                untCommon.InfoMsg("部门名称不能为空！");
                return false;
            }

            if (C_Code.SelectedIndex < 0)
            {
                untCommon.InfoMsg("所属公司不能为空！");
                return false;
            }
            return true;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (OperationType == 1)
            {
                addDepartment();
            }
            else
            {
                updateDepartment();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDepartmentEdit_Load(object sender, EventArgs e)
        {

        }

    }
}
