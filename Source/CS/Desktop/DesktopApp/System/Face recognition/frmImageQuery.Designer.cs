namespace DesktopApp
{
    partial class frmImageQuery
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
            this.btnFind = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.工号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.账号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.姓名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.部门 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.设备 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmb_Image = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtImageFile = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_Device = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txt_RealName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFind
            // 
            this.btnFind.Image = global::DesktopApp.Properties.Resources.search1;
            this.btnFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFind.Location = new System.Drawing.Point(389, 32);
            this.btnFind.Margin = new System.Windows.Forms.Padding(4);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(70, 29);
            this.btnFind.TabIndex = 10;
            this.btnFind.Text = "查询";
            this.btnFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.工号,
            this.账号,
            this.姓名,
            this.部门,
            this.设备});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 100);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 27;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(484, 453);
            this.dataGridView.TabIndex = 5;
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            // 
            // 工号
            // 
            this.工号.DataPropertyName = "F_EnCode";
            this.工号.HeaderText = "工号";
            this.工号.Name = "工号";
            this.工号.ReadOnly = true;
            // 
            // 账号
            // 
            this.账号.DataPropertyName = "F_Account";
            this.账号.HeaderText = "账号";
            this.账号.Name = "账号";
            this.账号.ReadOnly = true;
            // 
            // 姓名
            // 
            this.姓名.DataPropertyName = "F_RealName";
            this.姓名.HeaderText = "姓名";
            this.姓名.Name = "姓名";
            this.姓名.ReadOnly = true;
            // 
            // 部门
            // 
            this.部门.DataPropertyName = "D_Code";
            this.部门.HeaderText = "部门";
            this.部门.Name = "部门";
            this.部门.ReadOnly = true;
            // 
            // 设备
            // 
            this.设备.HeaderText = "设备";
            this.设备.Name = "设备";
            this.设备.ReadOnly = true;
            this.设备.Visible = false;
            // 
            // cmb_Image
            // 
            this.cmb_Image.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Image.FormattingEnabled = true;
            this.cmb_Image.Items.AddRange(new object[] {
            "照片1",
            "照片2",
            "照片3"});
            this.cmb_Image.Location = new System.Drawing.Point(132, 68);
            this.cmb_Image.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_Image.Name = "cmb_Image";
            this.cmb_Image.Size = new System.Drawing.Size(140, 23);
            this.cmb_Image.TabIndex = 62;
            this.cmb_Image.SelectedIndexChanged += new System.EventHandler(this.cmb_Image_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtImageFile);
            this.panel3.Controls.Add(this.label26);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.cmb_Image);
            this.panel3.Controls.Add(this.cmb_Device);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(484, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(389, 453);
            this.panel3.TabIndex = 4;
            // 
            // txtImageFile
            // 
            this.txtImageFile.Location = new System.Drawing.Point(132, 119);
            this.txtImageFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtImageFile.Name = "txtImageFile";
            this.txtImageFile.ReadOnly = true;
            this.txtImageFile.Size = new System.Drawing.Size(212, 25);
            this.txtImageFile.TabIndex = 65;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(29, 122);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(82, 15);
            this.label26.TabIndex = 64;
            this.label26.Text = "照片路径：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 63;
            this.label2.Text = "照片名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 63;
            this.label1.Text = "设备名称：";
            // 
            // cmb_Device
            // 
            this.cmb_Device.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Device.FormattingEnabled = true;
            this.cmb_Device.Location = new System.Drawing.Point(132, 37);
            this.cmb_Device.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_Device.Name = "cmb_Device";
            this.cmb_Device.Size = new System.Drawing.Size(140, 23);
            this.cmb_Device.TabIndex = 62;
            this.cmb_Device.SelectedIndexChanged += new System.EventHandler(this.cmb_Device_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Location = new System.Drawing.Point(32, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(321, 280);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "识别照片";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(315, 256);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 60;
            this.pictureBox1.TabStop = false;
            // 
            // txt_RealName
            // 
            this.txt_RealName.Location = new System.Drawing.Point(139, 36);
            this.txt_RealName.Name = "txt_RealName";
            this.txt_RealName.Size = new System.Drawing.Size(217, 25);
            this.txt_RealName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "姓名或者工号：";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnFind);
            this.panel2.Controls.Add(this.txt_RealName);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(873, 100);
            this.panel2.TabIndex = 2;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::DesktopApp.Properties.Resources.edit_remove;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(596, 32);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 29);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "删除";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Image = global::DesktopApp.Properties.Resources.edit1;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(488, 32);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 29);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "修改";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(873, 553);
            this.panel1.TabIndex = 1;
            // 
            // frmImageQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(873, 553);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmImageQuery";
            this.Text = "照片管理";
            this.Load += new System.EventHandler(this.frmImageQuery_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ComboBox cmb_Image;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txt_RealName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_Device;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 账号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 姓名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 部门;
        private System.Windows.Forms.DataGridViewTextBoxColumn 设备;
        private System.Windows.Forms.TextBox txtImageFile;
        private System.Windows.Forms.Label label26;
    }
}