namespace DesktopApp
{
    partial class frmParameterConfiguration
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.网络参数 = new System.Windows.Forms.TabPage();
            this.基本参数 = new System.Windows.Forms.TabPage();
            this.回调参数 = new System.Windows.Forms.TabPage();
            this.显示参数 = new System.Windows.Forms.TabPage();
            this.远程操作 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1291, 688);
            this.splitContainer1.SplitterDistance = 260;
            this.splitContainer1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(260, 688);
            this.dataGridView1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(3, 559);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1021, 126);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "信息";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1027, 688);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.网络参数);
            this.tabControl1.Controls.Add(this.基本参数);
            this.tabControl1.Controls.Add(this.回调参数);
            this.tabControl1.Controls.Add(this.显示参数);
            this.tabControl1.Controls.Add(this.远程操作);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 21);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1021, 664);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // 网络参数
            // 
            this.网络参数.Location = new System.Drawing.Point(4, 25);
            this.网络参数.Name = "网络参数";
            this.网络参数.Padding = new System.Windows.Forms.Padding(3);
            this.网络参数.Size = new System.Drawing.Size(1013, 635);
            this.网络参数.TabIndex = 0;
            this.网络参数.Tag = "DesktopApp.frmNetworkParameters";
            this.网络参数.Text = "网络参数";
            this.网络参数.UseVisualStyleBackColor = true;
            // 
            // 基本参数
            // 
            this.基本参数.Location = new System.Drawing.Point(4, 25);
            this.基本参数.Name = "基本参数";
            this.基本参数.Padding = new System.Windows.Forms.Padding(3);
            this.基本参数.Size = new System.Drawing.Size(1013, 635);
            this.基本参数.TabIndex = 1;
            this.基本参数.Tag = "DesktopApp.frmBasicParameters";
            this.基本参数.Text = "基本参数";
            this.基本参数.UseVisualStyleBackColor = true;
            // 
            // 回调参数
            // 
            this.回调参数.Location = new System.Drawing.Point(4, 25);
            this.回调参数.Name = "回调参数";
            this.回调参数.Size = new System.Drawing.Size(1013, 635);
            this.回调参数.TabIndex = 2;
            this.回调参数.Tag = "DesktopApp.frmCallbackParameter";
            this.回调参数.Text = "回调参数";
            this.回调参数.UseVisualStyleBackColor = true;
            // 
            // 显示参数
            // 
            this.显示参数.Location = new System.Drawing.Point(4, 25);
            this.显示参数.Name = "显示参数";
            this.显示参数.Size = new System.Drawing.Size(1013, 635);
            this.显示参数.TabIndex = 3;
            this.显示参数.Tag = "DesktopApp.frmDisplayParameter";
            this.显示参数.Text = "显示参数";
            this.显示参数.UseVisualStyleBackColor = true;
            // 
            // 远程操作
            // 
            this.远程操作.Location = new System.Drawing.Point(4, 25);
            this.远程操作.Name = "远程操作";
            this.远程操作.Size = new System.Drawing.Size(1013, 635);
            this.远程操作.TabIndex = 4;
            this.远程操作.Tag = "DesktopApp.frmRemoteOperation";
            this.远程操作.Text = "远程操作";
            this.远程操作.UseVisualStyleBackColor = true;
            // 
            // frmParameterConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 688);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmParameterConfiguration";
            this.Text = "参数配置";
            this.Load += new System.EventHandler(this.frmParameterConfiguration_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage 网络参数;
        private System.Windows.Forms.TabPage 基本参数;
        private System.Windows.Forms.TabPage 回调参数;
        private System.Windows.Forms.TabPage 显示参数;
        private System.Windows.Forms.TabPage 远程操作;
    }
}