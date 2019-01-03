namespace DesktopApp
{
    partial class frmInspectList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInspectList));
            this.panTop = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.cbCity = new System.Windows.Forms.ComboBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panMain = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pagerControl = new UserDefineControl.PagerControl();
            this.E_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_BoxCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I_Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I_Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I_User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I_Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_Village = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_MonitorNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_CameraType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.I_Photo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panTop.SuspendLayout();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.btnDelete);
            this.panTop.Controls.Add(this.btnExport);
            this.panTop.Controls.Add(this.dateTimePicker2);
            this.panTop.Controls.Add(this.dateTimePicker1);
            this.panTop.Controls.Add(this.cbCity);
            this.panTop.Controls.Add(this.txtKey);
            this.panTop.Controls.Add(this.btnClose);
            this.panTop.Controls.Add(this.btnFind);
            this.panTop.Controls.Add(this.label1);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(895, 50);
            this.panTop.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.Image = global::DesktopApp.Properties.Resources.Excel;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(598, 15);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(56, 23);
            this.btnExport.TabIndex = 19;
            this.btnExport.Text = "导出";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(267, 15);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(105, 21);
            this.dateTimePicker2.TabIndex = 3;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(149, 15);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(105, 21);
            this.dateTimePicker1.TabIndex = 2;
            // 
            // cbCity
            // 
            this.cbCity.FormattingEnabled = true;
            this.cbCity.Location = new System.Drawing.Point(12, 15);
            this.cbCity.Name = "cbCity";
            this.cbCity.Size = new System.Drawing.Size(130, 20);
            this.cbCity.TabIndex = 1;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(380, 15);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(90, 21);
            this.txtKey.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.Image = global::DesktopApp.Properties.Resources.clear;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(670, 15);
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
            this.btnFind.Location = new System.Drawing.Point(474, 15);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(56, 23);
            this.btnFind.TabIndex = 9;
            this.btnFind.Text = "查询";
            this.btnFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(253, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "至";
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.pagerControl);
            this.panMain.Controls.Add(this.dataGridView);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 50);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(895, 431);
            this.panMain.TabIndex = 2;
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.E_ID,
            this.E_BoxCode,
            this.E_Code,
            this.I_Type,
            this.I_Result,
            this.I_User,
            this.I_Date,
            this.E_City,
            this.E_Village,
            this.E_Address,
            this.E_IP,
            this.E_MonitorNumber,
            this.E_CameraType,
            this.I_Photo});
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(895, 402);
            this.dataGridView.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::DesktopApp.Properties.Resources.edit_remove;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(536, 15);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(56, 23);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.Text = "删除";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pagerControl
            // 
            this.pagerControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagerControl.Location = new System.Drawing.Point(0, 408);
            this.pagerControl.Name = "pagerControl";
            this.pagerControl.PageIndex = 1;
            this.pagerControl.PageSize = 100;
            this.pagerControl.RecordCount = 0;
            this.pagerControl.Size = new System.Drawing.Size(895, 23);
            this.pagerControl.TabIndex = 1;
            // 
            // E_ID
            // 
            this.E_ID.DataPropertyName = "E_ID";
            this.E_ID.HeaderText = "E_ID";
            this.E_ID.Name = "E_ID";
            this.E_ID.Visible = false;
            // 
            // E_BoxCode
            // 
            this.E_BoxCode.DataPropertyName = "E_BoxCode";
            this.E_BoxCode.HeaderText = "设备箱码";
            this.E_BoxCode.Name = "E_BoxCode";
            this.E_BoxCode.Width = 150;
            // 
            // E_Code
            // 
            this.E_Code.DataPropertyName = "E_Code";
            this.E_Code.HeaderText = "设备编码";
            this.E_Code.Name = "E_Code";
            this.E_Code.Width = 150;
            // 
            // I_Type
            // 
            this.I_Type.DataPropertyName = "I_Type";
            this.I_Type.HeaderText = "巡检类型";
            this.I_Type.Name = "I_Type";
            this.I_Type.Width = 120;
            // 
            // I_Result
            // 
            this.I_Result.DataPropertyName = "I_Result";
            this.I_Result.HeaderText = "巡检反馈";
            this.I_Result.Name = "I_Result";
            this.I_Result.Width = 240;
            // 
            // I_User
            // 
            this.I_User.DataPropertyName = "I_User";
            this.I_User.HeaderText = "巡检人";
            this.I_User.Name = "I_User";
            this.I_User.Width = 120;
            // 
            // I_Date
            // 
            this.I_Date.DataPropertyName = "I_Date";
            this.I_Date.HeaderText = "巡检时间";
            this.I_Date.Name = "I_Date";
            this.I_Date.Width = 150;
            // 
            // E_City
            // 
            this.E_City.DataPropertyName = "E_City";
            this.E_City.HeaderText = "街道办";
            this.E_City.Name = "E_City";
            this.E_City.Width = 180;
            // 
            // E_Village
            // 
            this.E_Village.DataPropertyName = "E_Village";
            this.E_Village.HeaderText = "改制村(社区)";
            this.E_Village.Name = "E_Village";
            this.E_Village.Width = 180;
            // 
            // E_Address
            // 
            this.E_Address.DataPropertyName = "E_Address";
            this.E_Address.HeaderText = "摄像点(机)详细安装地点";
            this.E_Address.Name = "E_Address";
            this.E_Address.Width = 240;
            // 
            // E_IP
            // 
            this.E_IP.DataPropertyName = "E_IP";
            this.E_IP.HeaderText = "IP地址";
            this.E_IP.Name = "E_IP";
            this.E_IP.Width = 120;
            // 
            // E_MonitorNumber
            // 
            this.E_MonitorNumber.DataPropertyName = "E_MonitorNumber";
            this.E_MonitorNumber.HeaderText = "监控点编号";
            this.E_MonitorNumber.Name = "E_MonitorNumber";
            this.E_MonitorNumber.Width = 120;
            // 
            // E_CameraType
            // 
            this.E_CameraType.DataPropertyName = "E_CameraType";
            this.E_CameraType.HeaderText = "摄像机类型";
            this.E_CameraType.Name = "E_CameraType";
            this.E_CameraType.Width = 120;
            // 
            // I_Photo
            // 
            this.I_Photo.DataPropertyName = "I_Photo";
            this.I_Photo.HeaderText = "现场照片";
            this.I_Photo.Name = "I_Photo";
            this.I_Photo.Visible = false;
            // 
            // frmInspectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(895, 481);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.panTop);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmInspectList";
            this.Text = "巡检记录";
            this.Load += new System.EventHandler(this.frmInspectList_Load);
            this.panTop.ResumeLayout(false);
            this.panTop.PerformLayout();
            this.panMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Panel panMain;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbCity;
        private System.Windows.Forms.TextBox txtKey;
        private UserDefineControl.PagerControl pagerControl;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_BoxCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn I_Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn I_Result;
        private System.Windows.Forms.DataGridViewTextBoxColumn I_User;
        private System.Windows.Forms.DataGridViewTextBoxColumn I_Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_City;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_Village;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_MonitorNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_CameraType;
        private System.Windows.Forms.DataGridViewTextBoxColumn I_Photo;
    }
}