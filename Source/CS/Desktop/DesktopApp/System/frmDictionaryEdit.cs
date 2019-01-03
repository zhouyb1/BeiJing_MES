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
    public partial class frmDictionaryEdit : Form
    {
        private int OperationType = 1;//1新增、2修改、3明细
        private string PrimaryKey = "";
        private SysUser User;
        private frmDictionaryList frmParent;

        public frmDictionaryEdit(frmDictionaryList _frmDictionaryList, SysUser _User, string _PrimaryKey, int _OperationType)
        {
            InitializeComponent();

            PrimaryKey = _PrimaryKey;
            OperationType = _OperationType;
            User = _User;
            frmParent = _frmDictionaryList;

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
        }


        private void getDetail()
        {
            try
            {
                SysDictionaryBLL Dictionarybll = new SysDictionaryBLL();
                SysDictionary Dictionary = Dictionarybll.getDetail(PrimaryKey);

                D_Code.Text = Dictionary.D_Code;
                D_Name.Text = Dictionary.D_Name;
                D_Type.SelectedItem = Dictionary.D_Type;
                D_Seq.Value = Dictionary.D_Seq;


                D_CreateBy.Text = Dictionary.D_CreateBy;
                D_UpdateBy.Text = Dictionary.D_UpdateBy;

                if (Dictionary.D_CreateDate.HasValue)
                    D_CreateDate.Value = Dictionary.D_CreateDate.Value;
                else
                {
                    D_CreateDate.Value = DateTime.Now;
                }

                if (Dictionary.D_UpdateDate.HasValue)
                    D_UpdateDate.Value = Dictionary.D_UpdateDate.Value;
                else
                {
                    D_UpdateDate.Value = DateTime.Now;
                    ;
                }
            }
            catch (Exception ex)
            {
                untCommon.ErrorMsg("信息管理加载数据异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        private void addDictionary()
        {
            try
            {
                if (checkInput())
                {
                    SysDictionary Dictionary = new SysDictionary();

                    Dictionary.D_Code = D_Code.Text;
                    Dictionary.D_Name = D_Name.Text;
                    Dictionary.D_Type = D_Type.SelectedItem.ToString();
                    Dictionary.D_Seq = int.Parse(D_Seq.Value.ToString());

                    Dictionary.D_CreateBy = D_CreateBy.Text;
                    Dictionary.D_CreateDate = D_CreateDate.Value;
                    Dictionary.D_UpdateBy = null;
                    Dictionary.D_UpdateDate = null;

                    SysDictionaryBLL Dictionarybll = new SysDictionaryBLL();
                    if (Dictionarybll.Exists(Dictionary.D_Code))
                    {
                        untCommon.InfoMsg("信息编码已存在！");
                    }
                    else
                    {
                        if (Dictionarybll.Add(Dictionary) > 0)
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
                untCommon.ErrorMsg("常规信息管理添加数据异常：" + ex.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        private void updateDictionary()
        {
            try
            {
                SysDictionary Dictionary = new SysDictionary();

                Dictionary.D_Code = D_Code.Text;
                Dictionary.D_Name = D_Name.Text;
                Dictionary.D_Type = D_Type.SelectedItem.ToString();
                Dictionary.D_Seq = int.Parse(D_Seq.Value.ToString());


                Dictionary.D_CreateBy = D_CreateBy.Text;
                Dictionary.D_CreateDate = D_CreateDate.Value;

                Dictionary.D_UpdateBy = D_UpdateBy.Text;
                Dictionary.D_UpdateDate = D_UpdateDate.Value;

                SysDictionaryBLL Dictionarybll = new SysDictionaryBLL();
                if (Dictionarybll.Edit(Dictionary) > 0)
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
                untCommon.ErrorMsg("常规信息管理更新数据异常：" + ex.Message);
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
                untCommon.InfoMsg("信息编码不能为空！");
                return false;
            }

            if (D_Type.SelectedIndex < 0)
            {
                untCommon.InfoMsg("信息类型不能为空！");
                return false;
            }

            if (string.IsNullOrEmpty(D_Name.Text))
            {
                untCommon.InfoMsg("信息名称不能为空！");
                return false;
            }

            return true;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (OperationType == 1)
            {
                addDictionary();
            }
            else
            {
                updateDictionary();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDictionaryEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
