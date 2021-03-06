﻿namespace DesktopApp
{
    partial class frmCompanyList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompanyList));
            this.panTop = new System.Windows.Forms.Panel();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panMain = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.C_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Phone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Fax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_CreateBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_UpdateBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C_UpdateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.cmsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panTop.SuspendLayout();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.btnDetail);
            this.panTop.Controls.Add(this.btnClose);
            this.panTop.Controls.Add(this.btnFind);
            this.panTop.Controls.Add(this.txtKey);
            this.panTop.Controls.Add(this.btnDelete);
            this.panTop.Controls.Add(this.btnEdit);
            this.panTop.Controls.Add(this.btnAdd);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(1119, 62);
            this.panTop.TabIndex = 0;
            this.panTop.Paint += new System.Windows.Forms.PaintEventHandler(this.panTop_Paint);
            // 
            // btnDetail
            // 
            this.btnDetail.Image = global::DesktopApp.Properties.Resources.open;
            this.btnDetail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetail.Location = new System.Drawing.Point(512, 16);
            this.btnDetail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(70, 29);
            this.btnDetail.TabIndex = 11;
            this.btnDetail.Text = "明细";
            this.btnDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDetail.UseVisualStyleBackColor = true;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::DesktopApp.Properties.Resources.clear;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(690, 16);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 29);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "关闭";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnFind
            // 
            this.btnFind.Image = global::DesktopApp.Properties.Resources.search1;
            this.btnFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFind.Location = new System.Drawing.Point(261, 16);
            this.btnFind.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(70, 29);
            this.btnFind.TabIndex = 9;
            this.btnFind.Text = "查询";
            this.btnFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(15, 19);
            this.txtKey.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(236, 25);
            this.txtKey.TabIndex = 8;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::DesktopApp.Properties.Resources.edit_remove;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(590, 16);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 29);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "删除";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::DesktopApp.Properties.Resources.edit1;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(435, 16);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 29);
            this.btnEdit.TabIndex = 6;
            this.btnEdit.Text = "修改";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::DesktopApp.Properties.Resources.edit_add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(358, 16);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 29);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "添加";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.dataGridView);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 62);
            this.panMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(1119, 539);
            this.panMain.TabIndex = 2;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.C_Code,
            this.C_Name,
            this.C_Phone,
            this.C_Fax,
            this.C_Email,
            this.C_Address,
            this.C_Remark,
            this.C_CreateBy,
            this.C_CreateDate,
            this.C_UpdateBy,
            this.C_UpdateDate});
            this.dataGridView.ContextMenuStrip = this.contextMenuStrip;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1119, 539);
            this.dataGridView.TabIndex = 0;
            // 
            // C_Code
            // 
            this.C_Code.DataPropertyName = "C_Code";
            this.C_Code.HeaderText = "公司编码";
            this.C_Code.Name = "C_Code";
            this.C_Code.Width = 120;
            // 
            // C_Name
            // 
            this.C_Name.DataPropertyName = "C_Name";
            this.C_Name.HeaderText = "公司名称";
            this.C_Name.Name = "C_Name";
            this.C_Name.Width = 240;
            // 
            // C_Phone
            // 
            this.C_Phone.DataPropertyName = "C_Phone";
            this.C_Phone.HeaderText = "联系方式";
            this.C_Phone.Name = "C_Phone";
            this.C_Phone.Width = 120;
            // 
            // C_Fax
            // 
            this.C_Fax.DataPropertyName = "C_Fax";
            this.C_Fax.HeaderText = "公司传真";
            this.C_Fax.Name = "C_Fax";
            this.C_Fax.Width = 120;
            // 
            // C_Email
            // 
            this.C_Email.DataPropertyName = "C_Email";
            this.C_Email.HeaderText = "电子邮箱";
            this.C_Email.Name = "C_Email";
            this.C_Email.Width = 120;
            // 
            // C_Address
            // 
            this.C_Address.DataPropertyName = "C_Address";
            this.C_Address.HeaderText = "公司地址";
            this.C_Address.Name = "C_Address";
            this.C_Address.Width = 240;
            // 
            // C_Remark
            // 
            this.C_Remark.DataPropertyName = "C_Remark";
            this.C_Remark.HeaderText = "备注说明";
            this.C_Remark.Name = "C_Remark";
            this.C_Remark.Width = 300;
            // 
            // C_CreateBy
            // 
            this.C_CreateBy.DataPropertyName = "C_CreateBy";
            this.C_CreateBy.HeaderText = "创建人";
            this.C_CreateBy.Name = "C_CreateBy";
            this.C_CreateBy.Width = 120;
            // 
            // C_CreateDate
            // 
            this.C_CreateDate.DataPropertyName = "C_CreateDate";
            this.C_CreateDate.HeaderText = "创建日期";
            this.C_CreateDate.Name = "C_CreateDate";
            this.C_CreateDate.Width = 150;
            // 
            // C_UpdateBy
            // 
            this.C_UpdateBy.DataPropertyName = "C_UpdateBy";
            this.C_UpdateBy.HeaderText = "更新人";
            this.C_UpdateBy.Name = "C_UpdateBy";
            this.C_UpdateBy.Width = 120;
            // 
            // C_UpdateDate
            // 
            this.C_UpdateDate.DataPropertyName = "C_UpdateDate";
            this.C_UpdateDate.HeaderText = "更新日期";
            this.C_UpdateDate.Name = "C_UpdateDate";
            this.C_UpdateDate.Width = 150;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsAdd,
            this.cmsEdit,
            this.cmsDetail,
            this.toolStripMenuItem,
            this.cmsDelete});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(113, 114);
            // 
            // cmsAdd
            // 
            this.cmsAdd.Image = global::DesktopApp.Properties.Resources.edit_add;
            this.cmsAdd.Name = "cmsAdd";
            this.cmsAdd.Size = new System.Drawing.Size(112, 26);
            this.cmsAdd.Text = "添加";
            this.cmsAdd.Click += new System.EventHandler(this.cmsAdd_Click);
            // 
            // cmsEdit
            // 
            this.cmsEdit.Image = global::DesktopApp.Properties.Resources.edit1;
            this.cmsEdit.Name = "cmsEdit";
            this.cmsEdit.Size = new System.Drawing.Size(112, 26);
            this.cmsEdit.Text = "修改";
            this.cmsEdit.Click += new System.EventHandler(this.cmsEdit_Click);
            // 
            // cmsDetail
            // 
            this.cmsDetail.Image = global::DesktopApp.Properties.Resources.open;
            this.cmsDetail.Name = "cmsDetail";
            this.cmsDetail.Size = new System.Drawing.Size(112, 26);
            this.cmsDetail.Text = "明细";
            this.cmsDetail.Click += new System.EventHandler(this.cmsDetail_Click);
            // 
            // toolStripMenuItem
            // 
            this.toolStripMenuItem.Name = "toolStripMenuItem";
            this.toolStripMenuItem.Size = new System.Drawing.Size(109, 6);
            // 
            // cmsDelete
            // 
            this.cmsDelete.Image = global::DesktopApp.Properties.Resources.edit_remove;
            this.cmsDelete.Name = "cmsDelete";
            this.cmsDelete.Size = new System.Drawing.Size(112, 26);
            this.cmsDelete.Text = "删除";
            this.cmsDelete.Click += new System.EventHandler(this.cmsDelete_Click);
            // 
            // frmCompanyList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1119, 601);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.panTop);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmCompanyList";
            this.Text = "公司管理";
            this.Load += new System.EventHandler(this.frmCompanyList_Load);
            this.panTop.ResumeLayout(false);
            this.panTop.PerformLayout();
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem cmsAdd;
        private System.Windows.Forms.ToolStripMenuItem cmsEdit;
        private System.Windows.Forms.ToolStripMenuItem cmsDetail;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmsDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Phone;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Fax;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_Remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_CreateBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_UpdateBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn C_UpdateDate;
    }
}