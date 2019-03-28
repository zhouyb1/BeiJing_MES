namespace DesktopApp
{
    partial class frmRecordQuery
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.工号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.姓名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.部门 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.设备 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnFind = new System.Windows.Forms.Button();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.txtImageFile = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_Image = new System.Windows.Forms.ComboBox();
            this.cmb_Device = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1063, 555);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.工号,
            this.姓名,
            this.部门,
            this.时间,
            this.设备,
            this.状态});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(200, 100);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 27;
            this.dataGridView.Size = new System.Drawing.Size(509, 455);
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
            // 时间
            // 
            this.时间.DataPropertyName = "K_Time";
            this.时间.HeaderText = "时间";
            this.时间.Name = "时间";
            this.时间.ReadOnly = true;
            // 
            // 设备
            // 
            this.设备.DataPropertyName = "K_Device";
            this.设备.HeaderText = "设备";
            this.设备.Name = "设备";
            this.设备.ReadOnly = true;
            // 
            // 状态
            // 
            this.状态.DataPropertyName = "K_Status";
            this.状态.HeaderText = "状态";
            this.状态.Name = "状态";
            this.状态.ReadOnly = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtImageFile);
            this.panel3.Controls.Add(this.label26);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.cmb_Image);
            this.panel3.Controls.Add(this.cmb_Device);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(709, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(354, 455);
            this.panel3.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Location = new System.Drawing.Point(24, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(312, 287);
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
            this.pictureBox1.Size = new System.Drawing.Size(306, 263);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 60;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnFind);
            this.panel2.Controls.Add(this.txt_name);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.dateTimePicker2);
            this.panel2.Controls.Add(this.dateTimePicker1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(200, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(863, 100);
            this.panel2.TabIndex = 2;
            // 
            // btnFind
            // 
            this.btnFind.Image = global::DesktopApp.Properties.Resources.search1;
            this.btnFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFind.Location = new System.Drawing.Point(388, 55);
            this.btnFind.Margin = new System.Windows.Forms.Padding(4);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(70, 29);
            this.btnFind.TabIndex = 10;
            this.btnFind.Text = "查询";
            this.btnFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(138, 59);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(217, 25);
            this.txt_name.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "工号或者姓名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(305, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "-";
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(325, 12);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(160, 25);
            this.dateTimePicker2.TabIndex = 2;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(138, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(160, 25);
            this.dateTimePicker1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(80, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "日期：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 555);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "部门列表";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 21);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(194, 531);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // txtImageFile
            // 
            this.txtImageFile.Location = new System.Drawing.Point(121, 114);
            this.txtImageFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtImageFile.Name = "txtImageFile";
            this.txtImageFile.ReadOnly = true;
            this.txtImageFile.Size = new System.Drawing.Size(212, 25);
            this.txtImageFile.TabIndex = 71;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(18, 117);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(82, 15);
            this.label26.TabIndex = 70;
            this.label26.Text = "照片路径：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 15);
            this.label4.TabIndex = 68;
            this.label4.Text = "照片名称：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 69;
            this.label5.Text = "设备名称：";
            // 
            // cmb_Image
            // 
            this.cmb_Image.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Image.FormattingEnabled = true;
            this.cmb_Image.Items.AddRange(new object[] {
            "照片1",
            "照片2",
            "照片3"});
            this.cmb_Image.Location = new System.Drawing.Point(121, 63);
            this.cmb_Image.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_Image.Name = "cmb_Image";
            this.cmb_Image.Size = new System.Drawing.Size(140, 23);
            this.cmb_Image.TabIndex = 66;
            this.cmb_Image.SelectedIndexChanged += new System.EventHandler(this.cmb_Image_SelectedIndexChanged);
            // 
            // cmb_Device
            // 
            this.cmb_Device.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Device.FormattingEnabled = true;
            this.cmb_Device.Location = new System.Drawing.Point(121, 32);
            this.cmb_Device.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_Device.Name = "cmb_Device";
            this.cmb_Device.Size = new System.Drawing.Size(140, 23);
            this.cmb_Device.TabIndex = 67;
            this.cmb_Device.SelectedIndexChanged += new System.EventHandler(this.cmb_Device_SelectedIndexChanged);
            // 
            // frmRecordQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1063, 555);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmRecordQuery";
            this.Text = "记录查询";
            this.Load += new System.EventHandler(this.frmRecordQuery_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 姓名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 部门;
        private System.Windows.Forms.DataGridViewTextBoxColumn 时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 设备;
        private System.Windows.Forms.DataGridViewTextBoxColumn 状态;
        private System.Windows.Forms.TextBox txtImageFile;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_Image;
        private System.Windows.Forms.ComboBox cmb_Device;
    }
}