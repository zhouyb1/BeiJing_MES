namespace DesktopApp
{
    partial class frmDictionaryList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDictionaryList));
            this.panTop = new System.Windows.Forms.Panel();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panMain = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.D_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D_Seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D_CreateBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D_CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D_UpdateBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D_UpdateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.panTop.Controls.Add(this.cbType);
            this.panTop.Controls.Add(this.btnDetail);
            this.panTop.Controls.Add(this.btnClose);
            this.panTop.Controls.Add(this.btnFind);
            this.panTop.Controls.Add(this.txtKey);
            this.panTop.Controls.Add(this.btnDelete);
            this.panTop.Controls.Add(this.btnEdit);
            this.panTop.Controls.Add(this.btnAdd);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(895, 50);
            this.panTop.TabIndex = 0;
            // 
            // cbType
            // 
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "巡检类型",
            "巡检反馈",
            "摄像机类型",
            "安装方式",
            "取电方式"});
            this.cbType.Location = new System.Drawing.Point(16, 15);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(90, 20);
            this.cbType.TabIndex = 12;
            // 
            // btnDetail
            // 
            this.btnDetail.Image = global::DesktopApp.Properties.Resources.open;
            this.btnDetail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetail.Location = new System.Drawing.Point(410, 13);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(56, 23);
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
            this.btnClose.Location = new System.Drawing.Point(552, 13);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(56, 23);
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
            this.btnFind.Location = new System.Drawing.Point(209, 13);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(56, 23);
            this.btnFind.TabIndex = 9;
            this.btnFind.Text = "查询";
            this.btnFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(112, 15);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(90, 21);
            this.txtKey.TabIndex = 8;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::DesktopApp.Properties.Resources.edit_remove;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(472, 13);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(56, 23);
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
            this.btnEdit.Location = new System.Drawing.Point(348, 13);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(56, 23);
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
            this.btnAdd.Location = new System.Drawing.Point(286, 13);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 23);
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
            this.panMain.Location = new System.Drawing.Point(0, 50);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(895, 431);
            this.panMain.TabIndex = 2;
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.D_Code,
            this.D_Type,
            this.D_Name,
            this.D_Seq,
            this.D_CreateBy,
            this.D_CreateDate,
            this.D_UpdateBy,
            this.D_UpdateDate});
            this.dataGridView.ContextMenuStrip = this.contextMenuStrip;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(895, 431);
            this.dataGridView.TabIndex = 0;
            // 
            // D_Code
            // 
            this.D_Code.DataPropertyName = "D_Code";
            this.D_Code.HeaderText = "信息编码";
            this.D_Code.Name = "D_Code";
            this.D_Code.Width = 120;
            // 
            // D_Type
            // 
            this.D_Type.DataPropertyName = "D_Type";
            this.D_Type.HeaderText = "信息类型";
            this.D_Type.Name = "D_Type";
            this.D_Type.Width = 120;
            // 
            // D_Name
            // 
            this.D_Name.DataPropertyName = "D_Name";
            this.D_Name.HeaderText = "信息内容";
            this.D_Name.Name = "D_Name";
            this.D_Name.Width = 300;
            // 
            // D_Seq
            // 
            this.D_Seq.DataPropertyName = "D_Seq";
            this.D_Seq.HeaderText = "信息排序";
            this.D_Seq.Name = "D_Seq";
            // 
            // D_CreateBy
            // 
            this.D_CreateBy.DataPropertyName = "D_CreateBy";
            this.D_CreateBy.HeaderText = "创建人";
            this.D_CreateBy.Name = "D_CreateBy";
            this.D_CreateBy.Width = 120;
            // 
            // D_CreateDate
            // 
            this.D_CreateDate.DataPropertyName = "D_CreateDate";
            this.D_CreateDate.HeaderText = "创建日期";
            this.D_CreateDate.Name = "D_CreateDate";
            this.D_CreateDate.Width = 150;
            // 
            // D_UpdateBy
            // 
            this.D_UpdateBy.DataPropertyName = "D_UpdateBy";
            this.D_UpdateBy.HeaderText = "更新人";
            this.D_UpdateBy.Name = "D_UpdateBy";
            this.D_UpdateBy.Width = 120;
            // 
            // D_UpdateDate
            // 
            this.D_UpdateDate.DataPropertyName = "D_UpdateDate";
            this.D_UpdateDate.HeaderText = "更新日期";
            this.D_UpdateDate.Name = "D_UpdateDate";
            this.D_UpdateDate.Width = 150;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsAdd,
            this.cmsEdit,
            this.cmsDetail,
            this.toolStripMenuItem,
            this.cmsDelete});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(101, 98);
            // 
            // cmsAdd
            // 
            this.cmsAdd.Image = global::DesktopApp.Properties.Resources.edit_add;
            this.cmsAdd.Name = "cmsAdd";
            this.cmsAdd.Size = new System.Drawing.Size(100, 22);
            this.cmsAdd.Text = "添加";
            this.cmsAdd.Click += new System.EventHandler(this.cmsAdd_Click);
            // 
            // cmsEdit
            // 
            this.cmsEdit.Image = global::DesktopApp.Properties.Resources.edit1;
            this.cmsEdit.Name = "cmsEdit";
            this.cmsEdit.Size = new System.Drawing.Size(100, 22);
            this.cmsEdit.Text = "修改";
            this.cmsEdit.Click += new System.EventHandler(this.cmsEdit_Click);
            // 
            // cmsDetail
            // 
            this.cmsDetail.Image = global::DesktopApp.Properties.Resources.open;
            this.cmsDetail.Name = "cmsDetail";
            this.cmsDetail.Size = new System.Drawing.Size(100, 22);
            this.cmsDetail.Text = "明细";
            this.cmsDetail.Click += new System.EventHandler(this.cmsDetail_Click);
            // 
            // toolStripMenuItem
            // 
            this.toolStripMenuItem.Name = "toolStripMenuItem";
            this.toolStripMenuItem.Size = new System.Drawing.Size(97, 6);
            // 
            // cmsDelete
            // 
            this.cmsDelete.Image = global::DesktopApp.Properties.Resources.edit_remove;
            this.cmsDelete.Name = "cmsDelete";
            this.cmsDelete.Size = new System.Drawing.Size(100, 22);
            this.cmsDelete.Text = "删除";
            this.cmsDelete.Click += new System.EventHandler(this.cmsDelete_Click);
            // 
            // frmDictionaryList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(895, 481);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.panTop);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDictionaryList";
            this.Text = "常规信息";
            this.Load += new System.EventHandler(this.frmDictionaryList_Load);
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
        private System.Windows.Forms.DataGridViewTextBoxColumn D_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn D_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn D_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn D_Seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn D_CreateBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn D_CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn D_UpdateBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn D_UpdateDate;
        private System.Windows.Forms.ComboBox cbType;
    }
}