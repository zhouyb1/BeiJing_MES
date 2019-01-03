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
    public partial class frmCompanyEdit : Form
    {
        private int OperationType = 1;//1新增、2修改、3明细
        private string PrimaryKey = "";
        private SysUser User;
        private frmCompanyList frmParent;

        public frmCompanyEdit(frmCompanyList _frmCompanyList,SysUser _User, string _PrimaryKey, int _OperationType)
        {
            InitializeComponent();

            PrimaryKey = _PrimaryKey;
            OperationType = _OperationType;
            User = _User;
            frmParent = _frmCompanyList;

            if (OperationType == 3)
            {
                getDetail();

                this.btnSave.Visible = false;
            }

            if (OperationType == 2)
            {
                getDetail();

                this.C_UpdateBy.Text = User.U_Code;
                this.C_Code.ReadOnly = true;
            }

            if (OperationType == 1)
            {
                C_CreateBy.Text = User.U_Code;
            }

        }

        private void getDetail()
        {
            try
            {
                SysCompanyBLL companybll = new SysCompanyBLL();
                SysCompany company = companybll.getDetail(PrimaryKey);

                C_Code.Text = company.C_Code;
                C_Name.Text = company.C_Name;
                C_Phone.Text = company.C_Phone;
                C_Fax.Text = company.C_Fax;
                C_Email.Text = company.C_Email;

                C_Address.Text = company.C_Address;
                C_Remark.Text = company.C_Remark;

                C_CreateBy.Text = company.C_CreateBy;
                C_UpdateBy.Text = company.C_UpdateBy;

                if (company.C_CreateDate.HasValue)
                    C_CreateDate.Value = company.C_CreateDate.Value;
                else
                {
                    C_CreateDate.Value = DateTime.Now;
                }
              
                if (company.C_UpdateDate.HasValue)
                    C_UpdateDate.Value = company.C_UpdateDate.Value;
                else
                {
                    C_UpdateDate.Value = DateTime.Now;
                    ;
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("公司管理加载数据异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        private void addCompany()
        {
            try
            {
                if (checkInput())
                {
                   
                    SysCompany company = new SysCompany();

                    company.C_Code = C_Code.Text;
                    company.C_Name = C_Name.Text;
                    company.C_Phone = C_Phone.Text;
                    company.C_Fax = C_Fax.Text;
                    company.C_Email = C_Email.Text;

                    company.C_Address = C_Address.Text;
                    company.C_Remark = C_Remark.Text;

                    company.C_CreateBy = C_CreateBy.Text;
                    company.C_CreateDate = C_CreateDate.Value;
                    company.C_UpdateBy = null;
                    company.C_UpdateDate = null;

                    SysCompanyBLL companybll = new SysCompanyBLL();

                    if (companybll.Exists(company.C_Code))
                    {
                        untCommon.InfoMsg("公司编号已存在！");
                    }
                    else
                    {
                        if (companybll.Add(company) > 0)
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
                 untCommon.ErrorMsg("公司管理添加数据异常："+ex.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void updateCompany()
        {
            try
            {
                if (checkInput())
                {
                    SysCompany company = new SysCompany();

                    company.C_Code = C_Code.Text;
                    company.C_Name = C_Name.Text;
                    company.C_Phone = C_Phone.Text;
                    company.C_Fax = C_Fax.Text;
                    company.C_Email = C_Email.Text;

                    company.C_Address = C_Address.Text;
                    company.C_Remark = C_Remark.Text;

                    company.C_CreateBy = C_CreateBy.Text;
                    company.C_CreateDate = C_CreateDate.Value;
                    company.C_UpdateBy = C_UpdateBy.Text;
                    company.C_UpdateDate = C_UpdateDate.Value;

                    SysCompanyBLL companybll = new SysCompanyBLL();
                    if (companybll.Edit(company) > 0)
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
                 untCommon.ErrorMsg("公司管理更新数据异常："+ex.Message);
            }
           
        }


        /// <summary>
        /// 校验输入
        /// </summary>
        /// <returns></returns>
        private bool checkInput()
        {
            if (string.IsNullOrEmpty(C_Code.Text))
            {
                untCommon.InfoMsg("公司编码不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(C_Name.Text))
            {
                untCommon.InfoMsg("公司名称不能为空！");
                return false;
            }
            return true;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (OperationType == 1)
            {
                addCompany();
            }
            else
            {
                updateCompany();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
