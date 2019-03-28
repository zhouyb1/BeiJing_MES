namespace DesktopApp
{
    partial class frmAttendanceManagement
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.签到明细表 = new System.Windows.Forms.TabPage();
            this.出勤工时日报表 = new System.Windows.Forms.TabPage();
            this.考勤统计表 = new System.Windows.Forms.TabPage();
            this.出勤工时月报表 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.签到明细表);
            this.tabControl1.Controls.Add(this.出勤工时日报表);
            this.tabControl1.Controls.Add(this.考勤统计表);
            this.tabControl1.Controls.Add(this.出勤工时月报表);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1045, 538);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // 签到明细表
            // 
            this.签到明细表.Location = new System.Drawing.Point(4, 25);
            this.签到明细表.Name = "签到明细表";
            this.签到明细表.Padding = new System.Windows.Forms.Padding(3);
            this.签到明细表.Size = new System.Drawing.Size(1037, 509);
            this.签到明细表.TabIndex = 0;
            this.签到明细表.Tag = "DesktopApp.frmAttendanceCheckDetail";
            this.签到明细表.Text = "签到明细表";
            this.签到明细表.UseVisualStyleBackColor = true;
            // 
            // 出勤工时日报表
            // 
            this.出勤工时日报表.Location = new System.Drawing.Point(4, 25);
            this.出勤工时日报表.Name = "出勤工时日报表";
            this.出勤工时日报表.Padding = new System.Windows.Forms.Padding(3);
            this.出勤工时日报表.Size = new System.Drawing.Size(1037, 509);
            this.出勤工时日报表.TabIndex = 1;
            this.出勤工时日报表.Tag = "DesktopApp.frmAttendanceWorkDay";
            this.出勤工时日报表.Text = "出勤工时日报表";
            this.出勤工时日报表.UseVisualStyleBackColor = true;
            // 
            // 考勤统计表
            // 
            this.考勤统计表.Location = new System.Drawing.Point(4, 25);
            this.考勤统计表.Name = "考勤统计表";
            this.考勤统计表.Padding = new System.Windows.Forms.Padding(3);
            this.考勤统计表.Size = new System.Drawing.Size(1037, 509);
            this.考勤统计表.TabIndex = 2;
            this.考勤统计表.Tag = "DesktopApp.frmAttendanceStatistics";
            this.考勤统计表.Text = "考勤统计表";
            this.考勤统计表.UseVisualStyleBackColor = true;
            // 
            // 出勤工时月报表
            // 
            this.出勤工时月报表.Location = new System.Drawing.Point(4, 25);
            this.出勤工时月报表.Name = "出勤工时月报表";
            this.出勤工时月报表.Padding = new System.Windows.Forms.Padding(3);
            this.出勤工时月报表.Size = new System.Drawing.Size(1037, 509);
            this.出勤工时月报表.TabIndex = 3;
            this.出勤工时月报表.Tag = "DesktopApp.frmAttendanceWorkMonthly";
            this.出勤工时月报表.Text = "出勤工时月报表";
            this.出勤工时月报表.UseVisualStyleBackColor = true;
            // 
            // frmAttendanceManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 538);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmAttendanceManagement";
            this.Text = "考勤管理";
            this.Load += new System.EventHandler(this.frmAttendanceManagement_Load);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage 出勤工时日报表;
        private System.Windows.Forms.TabPage 考勤统计表;
        private System.Windows.Forms.TabPage 出勤工时月报表;
        private System.Windows.Forms.TabPage 签到明细表;

    }
}