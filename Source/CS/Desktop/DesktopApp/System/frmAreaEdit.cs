using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.System;
using Model;

namespace DesktopApp
{
    public partial class frmAreaEdit : Form
    {


        private SysUser User;
        private int OperationType = 1;//1新增、2修改
        private string ParentCode = "";
        private string PrimaryKey = "";
        private frmAreaSet frmParent;

        public frmAreaEdit(frmAreaSet _frmAreaSet, SysUser _User, string _ParentCode, string _PrimaryKey, int _OperationType)
        {
            InitializeComponent();

            PrimaryKey = _PrimaryKey;
            OperationType = _OperationType;
            User = _User;
            frmParent = _frmAreaSet;
            ParentCode = _ParentCode;

            loadDropdownData();

            //新增
            if (OperationType == 1)
            {
                if (string.IsNullOrEmpty(ParentCode) || ParentCode == "00")
                {
                    A_Parent.SelectedIndex = -1;
                }
                else
                {
                    A_Parent.SelectedValue = ParentCode;
                }
            }

            //修改
            if (OperationType == 2)
            {
                getDetail();
                this.A_Code.ReadOnly = true;
            }
        }

        //仅允许添加天河区以下级别
        private void loadDropdownData()
        {
            try
            {
                //AreaBLL areabll = new AreaBLL();
                //var areas = areabll.loadData();

                AreaBLL areabll = new AreaBLL();
                var rows = areabll.loadData();

                //街道办
                var E_City_Datas = rows.Where(r => r.A_Parent == "440106" || r.A_Code == "440106").OrderBy(r=>r.A_Code).ToList();

                A_Parent.DataSource = E_City_Datas;
                A_Parent.DisplayMember = "A_Name";
                A_Parent.ValueMember = "A_Code";

                A_Parent.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("区域信息加载异常：" + ex.Message);
            }
        }

        private void getDetail()
        {
            try
            {
                AreaBLL areabll = new AreaBLL();
                var area = areabll.getDetail(PrimaryKey);

                A_Code.Text = area.A_Code;
                A_Name.Text = area.A_Name;

                if(string.IsNullOrEmpty(area.A_Parent))
                  A_Parent.SelectedIndex =-1;
                else
                {
                    A_Parent.SelectedValue = area.A_Parent;
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("区域信息加载异常：" + ex.Message);
            }
        }


        /// <summary>
        /// 添加
        /// </summary>
        private void add()
        {
            try
            {
                if (check())
                {
                    Area area = new Area();

                    area.A_Code = A_Code.Text;
                    area.A_Name = A_Name.Text;
                    area.A_Parent = A_Parent.SelectedIndex < 0 ? "" : A_Parent.SelectedValue.ToString();

                    AreaBLL areabll = new AreaBLL();
                    if (areabll.Exists(area.A_Code))
                    {
                        untCommon.InfoMsg("区域编码已存在！");
                    }
                    else
                    {
                        if (areabll.Add(area) > 0)
                        {
                            frmParent.loadArea();
                            untCommon.InfoMsg("添加成功！");

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
                untCommon.InfoMsg("区域管理添加数据异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void update()
        {
            try
            {
                if (check())
                {
                    Area area = new Area();

                    area.A_Code = A_Code.Text;
                    area.A_Name = A_Name.Text;
                    area.A_Parent = A_Parent.SelectedIndex < 0 ? "" : A_Parent.SelectedValue.ToString();

                    AreaBLL areabll = new AreaBLL();
                    if (areabll.Edit(area) > 0)
                    {
                        frmParent.loadArea();
                        untCommon.InfoMsg("修改成功！");

                    }
                    else
                    {
                        untCommon.InfoMsg("修改失败！");
                    }
                }
            }
            catch (Exception ex)
            {
               untCommon.InfoMsg("区域管理更新数据异常："+ex.Message);
            }
           
        }


        /// <summary>
        /// 校验输入
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            if (string.IsNullOrEmpty(A_Code.Text))
            {
                untCommon.InfoMsg("区域编码不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(A_Name.Text))
            {
                untCommon.InfoMsg("区域名称不能为空！");
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (OperationType == 1)
            {
                add();
            }

            if (OperationType == 2)
            {
                update();
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
