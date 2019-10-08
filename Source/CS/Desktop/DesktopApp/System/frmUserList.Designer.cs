namespace DesktopApp
{
    partial class frmUserList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUserList));
            this.panTop = new System.Windows.Forms.Panel();
            this.btnFaceRecognition = new System.Windows.Forms.Button();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panMain = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.cmsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.F_Account = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_RealName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StrGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.所属班组 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_Mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.用户身份证 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_OICQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_WeChat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.U_Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.员工类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.用户民族 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.用户籍贯 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.用户学历 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.用户组别 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RFID编码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.用户照片1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.入职日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.离职日期 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_CreateUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_ModifyUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_ModifyDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F_EnabledMark = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panTop.SuspendLayout();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.btnFaceRecognition);
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
            // btnFaceRecognition
            // 
            this.btnFaceRecognition.Image = global::DesktopApp.Properties.Resources.user;
            this.btnFaceRecognition.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFaceRecognition.Location = new System.Drawing.Point(444, 13);
            this.btnFaceRecognition.Name = "btnFaceRecognition";
            this.btnFaceRecognition.Size = new System.Drawing.Size(105, 23);
            this.btnFaceRecognition.TabIndex = 12;
            this.btnFaceRecognition.Text = "人脸识别注册";
            this.btnFaceRecognition.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFaceRecognition.UseVisualStyleBackColor = true;
            this.btnFaceRecognition.Click += new System.EventHandler(this.btnFaceRecognition_Click);
            // 
            // btnDetail
            // 
            this.btnDetail.Image = global::DesktopApp.Properties.Resources.open;
            this.btnDetail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetail.Location = new System.Drawing.Point(829, 13);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(56, 23);
            this.btnDetail.TabIndex = 11;
            this.btnDetail.Text = "明细";
            this.btnDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDetail.UseVisualStyleBackColor = true;
            this.btnDetail.Visible = false;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // btnClose
            // 
            this.btnClose.Image = global::DesktopApp.Properties.Resources.clear;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(665, 12);
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
            this.txtKey.Location = new System.Drawing.Point(12, 15);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(190, 21);
            this.txtKey.TabIndex = 8;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::DesktopApp.Properties.Resources.edit_remove;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(369, 13);
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
            this.btnEdit.Location = new System.Drawing.Point(286, 13);
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
            this.btnAdd.Location = new System.Drawing.Point(766, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(56, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "添加";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Visible = false;
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
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.F_Account,
            this.F_RealName,
            this.F_Password,
            this.StrGender,
            this.D_Code,
            this.所属班组,
            this.R_Code,
            this.F_Mobile,
            this.用户身份证,
            this.F_OICQ,
            this.F_WeChat,
            this.F_Email,
            this.U_Address,
            this.员工类型,
            this.用户民族,
            this.用户籍贯,
            this.用户学历,
            this.用户组别,
            this.RFID编码,
            this.用户照片1,
            this.入职日期,
            this.离职日期,
            this.F_Description,
            this.F_CreateUserName,
            this.F_CreateDate,
            this.F_ModifyUserName,
            this.F_ModifyDate,
            this.F_EnabledMark});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(895, 431);
            this.dataGridView.TabIndex = 0;
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
            this.contextMenuStrip.Size = new System.Drawing.Size(105, 114);
            // 
            // cmsAdd
            // 
            this.cmsAdd.Image = global::DesktopApp.Properties.Resources.edit_add;
            this.cmsAdd.Name = "cmsAdd";
            this.cmsAdd.Size = new System.Drawing.Size(104, 26);
            this.cmsAdd.Text = "添加";
            this.cmsAdd.Visible = false;
            this.cmsAdd.Click += new System.EventHandler(this.cmsAdd_Click);
            // 
            // cmsEdit
            // 
            this.cmsEdit.Image = global::DesktopApp.Properties.Resources.edit1;
            this.cmsEdit.Name = "cmsEdit";
            this.cmsEdit.Size = new System.Drawing.Size(104, 26);
            this.cmsEdit.Text = "修改";
            this.cmsEdit.Click += new System.EventHandler(this.cmsEdit_Click);
            // 
            // cmsDetail
            // 
            this.cmsDetail.Image = global::DesktopApp.Properties.Resources.open;
            this.cmsDetail.Name = "cmsDetail";
            this.cmsDetail.Size = new System.Drawing.Size(104, 26);
            this.cmsDetail.Text = "明细";
            this.cmsDetail.Click += new System.EventHandler(this.cmsDetail_Click);
            // 
            // toolStripMenuItem
            // 
            this.toolStripMenuItem.Name = "toolStripMenuItem";
            this.toolStripMenuItem.Size = new System.Drawing.Size(101, 6);
            // 
            // cmsDelete
            // 
            this.cmsDelete.Image = global::DesktopApp.Properties.Resources.edit_remove;
            this.cmsDelete.Name = "cmsDelete";
            this.cmsDelete.Size = new System.Drawing.Size(104, 26);
            this.cmsDelete.Text = "删除";
            this.cmsDelete.Visible = false;
            this.cmsDelete.Click += new System.EventHandler(this.cmsDelete_Click);
            // 
            // F_Account
            // 
            this.F_Account.DataPropertyName = "F_Account";
            this.F_Account.HeaderText = "用户编码";
            this.F_Account.Name = "F_Account";
            this.F_Account.ReadOnly = true;
            this.F_Account.Width = 120;
            // 
            // F_RealName
            // 
            this.F_RealName.DataPropertyName = "F_RealName";
            this.F_RealName.HeaderText = "用户名称";
            this.F_RealName.Name = "F_RealName";
            this.F_RealName.ReadOnly = true;
            this.F_RealName.Width = 120;
            // 
            // F_Password
            // 
            this.F_Password.DataPropertyName = "F_Password";
            this.F_Password.HeaderText = "用户密码";
            this.F_Password.Name = "F_Password";
            this.F_Password.ReadOnly = true;
            this.F_Password.Width = 120;
            // 
            // StrGender
            // 
            this.StrGender.DataPropertyName = "StrGender";
            this.StrGender.HeaderText = "用户性别";
            this.StrGender.Name = "StrGender";
            this.StrGender.ReadOnly = true;
            // 
            // D_Code
            // 
            this.D_Code.DataPropertyName = "D_Code";
            this.D_Code.HeaderText = "所属部门";
            this.D_Code.Name = "D_Code";
            this.D_Code.ReadOnly = true;
            this.D_Code.Width = 120;
            // 
            // 所属班组
            // 
            this.所属班组.DataPropertyName = "F_TeamName";
            this.所属班组.HeaderText = "所属班组";
            this.所属班组.Name = "所属班组";
            this.所属班组.ReadOnly = true;
            // 
            // R_Code
            // 
            this.R_Code.DataPropertyName = "R_Code";
            this.R_Code.HeaderText = "用户角色";
            this.R_Code.Name = "R_Code";
            this.R_Code.ReadOnly = true;
            this.R_Code.Width = 120;
            // 
            // F_Mobile
            // 
            this.F_Mobile.DataPropertyName = "F_Mobile";
            this.F_Mobile.HeaderText = "用户手机";
            this.F_Mobile.Name = "F_Mobile";
            this.F_Mobile.ReadOnly = true;
            this.F_Mobile.Width = 120;
            // 
            // 用户身份证
            // 
            this.用户身份证.DataPropertyName = "F_Cert";
            this.用户身份证.HeaderText = "用户身份证";
            this.用户身份证.Name = "用户身份证";
            this.用户身份证.ReadOnly = true;
            // 
            // F_OICQ
            // 
            this.F_OICQ.DataPropertyName = "F_OICQ";
            this.F_OICQ.HeaderText = "用户QQ";
            this.F_OICQ.Name = "F_OICQ";
            this.F_OICQ.ReadOnly = true;
            this.F_OICQ.Width = 120;
            // 
            // F_WeChat
            // 
            this.F_WeChat.DataPropertyName = "F_WeChat";
            this.F_WeChat.HeaderText = "用户微信";
            this.F_WeChat.Name = "F_WeChat";
            this.F_WeChat.ReadOnly = true;
            this.F_WeChat.Width = 120;
            // 
            // F_Email
            // 
            this.F_Email.DataPropertyName = "F_Email";
            this.F_Email.HeaderText = "用户邮箱";
            this.F_Email.Name = "F_Email";
            this.F_Email.ReadOnly = true;
            this.F_Email.Width = 120;
            // 
            // U_Address
            // 
            this.U_Address.DataPropertyName = "U_Address";
            this.U_Address.HeaderText = "通讯地址";
            this.U_Address.Name = "U_Address";
            this.U_Address.ReadOnly = true;
            this.U_Address.Width = 300;
            // 
            // 员工类型
            // 
            this.员工类型.DataPropertyName = "F_Kind";
            this.员工类型.HeaderText = "员工类型";
            this.员工类型.Name = "员工类型";
            this.员工类型.ReadOnly = true;
            // 
            // 用户民族
            // 
            this.用户民族.DataPropertyName = "F_Nation";
            this.用户民族.HeaderText = "用户民族";
            this.用户民族.Name = "用户民族";
            this.用户民族.ReadOnly = true;
            // 
            // 用户籍贯
            // 
            this.用户籍贯.DataPropertyName = "F_Origin";
            this.用户籍贯.HeaderText = "用户籍贯";
            this.用户籍贯.Name = "用户籍贯";
            this.用户籍贯.ReadOnly = true;
            // 
            // 用户学历
            // 
            this.用户学历.DataPropertyName = "F_Record";
            this.用户学历.HeaderText = "用户学历";
            this.用户学历.Name = "用户学历";
            this.用户学历.ReadOnly = true;
            // 
            // 用户组别
            // 
            this.用户组别.DataPropertyName = "F_Group";
            this.用户组别.HeaderText = "用户组别";
            this.用户组别.Name = "用户组别";
            this.用户组别.ReadOnly = true;
            // 
            // RFID编码
            // 
            this.RFID编码.DataPropertyName = "F_RFIDCode";
            this.RFID编码.HeaderText = "RFID编码";
            this.RFID编码.Name = "RFID编码";
            this.RFID编码.ReadOnly = true;
            // 
            // 用户照片1
            // 
            this.用户照片1.DataPropertyName = "F_Picture1";
            this.用户照片1.HeaderText = "用户照片1";
            this.用户照片1.Name = "用户照片1";
            this.用户照片1.ReadOnly = true;
            // 
            // 入职日期
            // 
            this.入职日期.DataPropertyName = "F_Indate";
            this.入职日期.HeaderText = "入职日期";
            this.入职日期.Name = "入职日期";
            this.入职日期.ReadOnly = true;
            // 
            // 离职日期
            // 
            this.离职日期.DataPropertyName = "F_Outdate";
            this.离职日期.HeaderText = "离职日期";
            this.离职日期.Name = "离职日期";
            this.离职日期.ReadOnly = true;
            // 
            // F_Description
            // 
            this.F_Description.DataPropertyName = "F_Description";
            this.F_Description.HeaderText = "备注说明";
            this.F_Description.Name = "F_Description";
            this.F_Description.ReadOnly = true;
            this.F_Description.Width = 240;
            // 
            // F_CreateUserName
            // 
            this.F_CreateUserName.DataPropertyName = "F_CreateUserName";
            this.F_CreateUserName.HeaderText = "创建人";
            this.F_CreateUserName.Name = "F_CreateUserName";
            this.F_CreateUserName.ReadOnly = true;
            this.F_CreateUserName.Width = 120;
            // 
            // F_CreateDate
            // 
            this.F_CreateDate.DataPropertyName = "F_CreateDate";
            this.F_CreateDate.HeaderText = "创建日期";
            this.F_CreateDate.Name = "F_CreateDate";
            this.F_CreateDate.ReadOnly = true;
            this.F_CreateDate.Width = 150;
            // 
            // F_ModifyUserName
            // 
            this.F_ModifyUserName.DataPropertyName = "F_ModifyUserName";
            this.F_ModifyUserName.HeaderText = "更新人";
            this.F_ModifyUserName.Name = "F_ModifyUserName";
            this.F_ModifyUserName.ReadOnly = true;
            this.F_ModifyUserName.Width = 120;
            // 
            // F_ModifyDate
            // 
            this.F_ModifyDate.DataPropertyName = "F_ModifyDate";
            this.F_ModifyDate.HeaderText = "更新日期";
            this.F_ModifyDate.Name = "F_ModifyDate";
            this.F_ModifyDate.ReadOnly = true;
            this.F_ModifyDate.Width = 150;
            // 
            // F_EnabledMark
            // 
            this.F_EnabledMark.DataPropertyName = "F_EnabledMark";
            this.F_EnabledMark.HeaderText = "是否启用";
            this.F_EnabledMark.Name = "F_EnabledMark";
            this.F_EnabledMark.ReadOnly = true;
            this.F_EnabledMark.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.F_EnabledMark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // frmUserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(895, 481);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.panTop);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmUserList";
            this.Text = "用户管理";
            this.Load += new System.EventHandler(this.frmUserList_Load);
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
        private System.Windows.Forms.Button btnFaceRecognition;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_Account;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_RealName;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_Password;
        private System.Windows.Forms.DataGridViewTextBoxColumn StrGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn D_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn 所属班组;
        private System.Windows.Forms.DataGridViewTextBoxColumn R_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_Mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn 用户身份证;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_OICQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_WeChat;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn U_Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn 员工类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn 用户民族;
        private System.Windows.Forms.DataGridViewTextBoxColumn 用户籍贯;
        private System.Windows.Forms.DataGridViewTextBoxColumn 用户学历;
        private System.Windows.Forms.DataGridViewTextBoxColumn 用户组别;
        private System.Windows.Forms.DataGridViewTextBoxColumn RFID编码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 用户照片1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 入职日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn 离职日期;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_CreateUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_ModifyUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn F_ModifyDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn F_EnabledMark;
    }
}