namespace DesktopApp
{
    partial class frmEquipmentList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEquipmentList));
            this.panTop = new System.Windows.Forms.Panel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.btnRfidSet = new System.Windows.Forms.Button();
            this.cbCity = new System.Windows.Forms.ComboBox();
            this.txtKey = new System.Windows.Forms.TextBox();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panMain = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.E_BoxCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_Village = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_IP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_MonitorNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_CameraType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_CameraQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_Direction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_Range = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_InstallType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_Height = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_Width = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_Longitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_Latitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_ElectricityType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_EquipmentBoxQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_OpticalFiberQty1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_OpticalFiberQty2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_CreateBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_UpdateBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_UpdateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E_Active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.cmsDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsWrite = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsRfidSet = new System.Windows.Forms.ToolStripMenuItem();
            this.pagerControl = new UserDefineControl.PagerControl();
            this.panTop.SuspendLayout();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.btnExport);
            this.panTop.Controls.Add(this.btnWrite);
            this.panTop.Controls.Add(this.btnRfidSet);
            this.panTop.Controls.Add(this.cbCity);
            this.panTop.Controls.Add(this.txtKey);
            this.panTop.Controls.Add(this.btnDetail);
            this.panTop.Controls.Add(this.btnClose);
            this.panTop.Controls.Add(this.btnFind);
            this.panTop.Controls.Add(this.btnDelete);
            this.panTop.Controls.Add(this.btnEdit);
            this.panTop.Controls.Add(this.btnAdd);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(0, 0);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(935, 50);
            this.panTop.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.Image = global::DesktopApp.Properties.Resources.Excel;
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(567, 14);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(56, 23);
            this.btnExport.TabIndex = 20;
            this.btnExport.Text = "导出";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Image = global::DesktopApp.Properties.Resources.pencil;
            this.btnWrite.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWrite.Location = new System.Drawing.Point(638, 14);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(92, 23);
            this.btnWrite.TabIndex = 17;
            this.btnWrite.Text = "写入RFID卡";
            this.btnWrite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWriteRfid_Click);
            // 
            // btnRfidSet
            // 
            this.btnRfidSet.Image = global::DesktopApp.Properties.Resources.cog;
            this.btnRfidSet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRfidSet.Location = new System.Drawing.Point(736, 14);
            this.btnRfidSet.Name = "btnRfidSet";
            this.btnRfidSet.Size = new System.Drawing.Size(90, 23);
            this.btnRfidSet.TabIndex = 16;
            this.btnRfidSet.Text = "写卡器设置";
            this.btnRfidSet.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRfidSet.UseVisualStyleBackColor = true;
            this.btnRfidSet.Click += new System.EventHandler(this.btnRfidSet_Click);
            // 
            // cbCity
            // 
            this.cbCity.FormattingEnabled = true;
            this.cbCity.Location = new System.Drawing.Point(12, 15);
            this.cbCity.Name = "cbCity";
            this.cbCity.Size = new System.Drawing.Size(130, 20);
            this.cbCity.TabIndex = 15;
            // 
            // txtKey
            // 
            this.txtKey.Location = new System.Drawing.Point(148, 15);
            this.txtKey.Name = "txtKey";
            this.txtKey.Size = new System.Drawing.Size(90, 21);
            this.txtKey.TabIndex = 13;
            // 
            // btnDetail
            // 
            this.btnDetail.Image = global::DesktopApp.Properties.Resources.open;
            this.btnDetail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetail.Location = new System.Drawing.Point(443, 14);
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
            this.btnClose.Location = new System.Drawing.Point(843, 14);
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
            this.btnFind.Location = new System.Drawing.Point(243, 14);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(56, 23);
            this.btnFind.TabIndex = 9;
            this.btnFind.Text = "查询";
            this.btnFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::DesktopApp.Properties.Resources.edit_remove;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(505, 14);
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
            this.btnEdit.Location = new System.Drawing.Point(381, 14);
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
            this.btnAdd.Location = new System.Drawing.Point(319, 14);
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
            this.panMain.Controls.Add(this.pagerControl);
            this.panMain.Controls.Add(this.dataGridView);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(0, 50);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(935, 431);
            this.panMain.TabIndex = 2;
            // 
            // dataGridView
            // 
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.E_BoxCode,
            this.E_Code,
            this.E_City,
            this.E_Village,
            this.E_Address,
            this.E_IP,
            this.E_MonitorNumber,
            this.E_CameraType,
            this.E_CameraQty,
            this.E_Direction,
            this.E_Range,
            this.E_InstallType,
            this.E_Height,
            this.E_Width,
            this.E_Longitude,
            this.E_Latitude,
            this.E_ElectricityType,
            this.E_EquipmentBoxQty,
            this.E_OpticalFiberQty1,
            this.E_OpticalFiberQty2,
            this.E_CreateBy,
            this.E_CreateDate,
            this.E_UpdateBy,
            this.E_UpdateDate,
            this.E_Active});
            this.dataGridView.ContextMenuStrip = this.contextMenuStrip;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(935, 402);
            this.dataGridView.TabIndex = 0;
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
            // E_CameraQty
            // 
            this.E_CameraQty.DataPropertyName = "E_CameraQty";
            this.E_CameraQty.HeaderText = "摄像机数量";
            this.E_CameraQty.Name = "E_CameraQty";
            this.E_CameraQty.Width = 120;
            // 
            // E_Direction
            // 
            this.E_Direction.DataPropertyName = "E_Direction";
            this.E_Direction.HeaderText = "横杆朝向";
            this.E_Direction.Name = "E_Direction";
            this.E_Direction.Width = 120;
            // 
            // E_Range
            // 
            this.E_Range.DataPropertyName = "E_Range";
            this.E_Range.HeaderText = "监控范围";
            this.E_Range.Name = "E_Range";
            this.E_Range.Width = 240;
            // 
            // E_InstallType
            // 
            this.E_InstallType.DataPropertyName = "E_InstallType";
            this.E_InstallType.HeaderText = "安装方式";
            this.E_InstallType.Name = "E_InstallType";
            this.E_InstallType.Width = 120;
            // 
            // E_Height
            // 
            this.E_Height.DataPropertyName = "E_Height";
            this.E_Height.HeaderText = "高度(米)";
            this.E_Height.Name = "E_Height";
            this.E_Height.Width = 120;
            // 
            // E_Width
            // 
            this.E_Width.DataPropertyName = "E_Width";
            this.E_Width.HeaderText = "臂长(米)";
            this.E_Width.Name = "E_Width";
            this.E_Width.Width = 120;
            // 
            // E_Longitude
            // 
            this.E_Longitude.DataPropertyName = "E_Longitude";
            this.E_Longitude.HeaderText = "经度";
            this.E_Longitude.Name = "E_Longitude";
            this.E_Longitude.Width = 120;
            // 
            // E_Latitude
            // 
            this.E_Latitude.DataPropertyName = "E_Latitude";
            this.E_Latitude.HeaderText = "纬度";
            this.E_Latitude.Name = "E_Latitude";
            this.E_Latitude.Width = 120;
            // 
            // E_ElectricityType
            // 
            this.E_ElectricityType.DataPropertyName = "E_ElectricityType";
            this.E_ElectricityType.HeaderText = "取电方式";
            this.E_ElectricityType.Name = "E_ElectricityType";
            this.E_ElectricityType.Width = 120;
            // 
            // E_EquipmentBoxQty
            // 
            this.E_EquipmentBoxQty.DataPropertyName = "E_EquipmentBoxQty";
            this.E_EquipmentBoxQty.HeaderText = "新装设备箱数量";
            this.E_EquipmentBoxQty.Name = "E_EquipmentBoxQty";
            this.E_EquipmentBoxQty.Width = 150;
            // 
            // E_OpticalFiberQty1
            // 
            this.E_OpticalFiberQty1.DataPropertyName = "E_OpticalFiberQty1";
            this.E_OpticalFiberQty1.HeaderText = "光纤收发器(一光一电)";
            this.E_OpticalFiberQty1.Name = "E_OpticalFiberQty1";
            this.E_OpticalFiberQty1.Width = 150;
            // 
            // E_OpticalFiberQty2
            // 
            this.E_OpticalFiberQty2.DataPropertyName = "E_OpticalFiberQty2";
            this.E_OpticalFiberQty2.HeaderText = "光纤收发器(一光二电)";
            this.E_OpticalFiberQty2.Name = "E_OpticalFiberQty2";
            this.E_OpticalFiberQty2.Width = 150;
            // 
            // E_CreateBy
            // 
            this.E_CreateBy.DataPropertyName = "E_CreateBy";
            this.E_CreateBy.HeaderText = "创建人";
            this.E_CreateBy.Name = "E_CreateBy";
            this.E_CreateBy.Width = 120;
            // 
            // E_CreateDate
            // 
            this.E_CreateDate.DataPropertyName = "E_CreateDate";
            this.E_CreateDate.HeaderText = "创建日期";
            this.E_CreateDate.Name = "E_CreateDate";
            this.E_CreateDate.Width = 150;
            // 
            // E_UpdateBy
            // 
            this.E_UpdateBy.DataPropertyName = "E_UpdateBy";
            this.E_UpdateBy.HeaderText = "更新人";
            this.E_UpdateBy.Name = "E_UpdateBy";
            this.E_UpdateBy.Width = 120;
            // 
            // E_UpdateDate
            // 
            this.E_UpdateDate.DataPropertyName = "E_UpdateDate";
            this.E_UpdateDate.HeaderText = "更新日期";
            this.E_UpdateDate.Name = "E_UpdateDate";
            this.E_UpdateDate.Width = 150;
            // 
            // E_Active
            // 
            this.E_Active.DataPropertyName = "E_Active";
            this.E_Active.HeaderText = "是否启用";
            this.E_Active.Name = "E_Active";
            this.E_Active.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.E_Active.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsAdd,
            this.cmsEdit,
            this.cmsDetail,
            this.toolStripMenuItem,
            this.cmsDelete,
            this.toolStripMenuItem1,
            this.cmsWrite,
            this.cmsRfidSet});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(140, 148);
            // 
            // cmsAdd
            // 
            this.cmsAdd.Image = global::DesktopApp.Properties.Resources.edit_add;
            this.cmsAdd.Name = "cmsAdd";
            this.cmsAdd.Size = new System.Drawing.Size(139, 22);
            this.cmsAdd.Text = "添加";
            this.cmsAdd.Click += new System.EventHandler(this.cmsAdd_Click);
            // 
            // cmsEdit
            // 
            this.cmsEdit.Image = global::DesktopApp.Properties.Resources.edit1;
            this.cmsEdit.Name = "cmsEdit";
            this.cmsEdit.Size = new System.Drawing.Size(139, 22);
            this.cmsEdit.Text = "修改";
            this.cmsEdit.Click += new System.EventHandler(this.cmsEdit_Click);
            // 
            // cmsDetail
            // 
            this.cmsDetail.Image = global::DesktopApp.Properties.Resources.open;
            this.cmsDetail.Name = "cmsDetail";
            this.cmsDetail.Size = new System.Drawing.Size(139, 22);
            this.cmsDetail.Text = "明细";
            this.cmsDetail.Click += new System.EventHandler(this.cmsDetail_Click);
            // 
            // toolStripMenuItem
            // 
            this.toolStripMenuItem.Name = "toolStripMenuItem";
            this.toolStripMenuItem.Size = new System.Drawing.Size(136, 6);
            // 
            // cmsDelete
            // 
            this.cmsDelete.Image = global::DesktopApp.Properties.Resources.edit_remove;
            this.cmsDelete.Name = "cmsDelete";
            this.cmsDelete.Size = new System.Drawing.Size(139, 22);
            this.cmsDelete.Text = "删除";
            this.cmsDelete.Click += new System.EventHandler(this.cmsDelete_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(136, 6);
            // 
            // cmsWrite
            // 
            this.cmsWrite.Image = global::DesktopApp.Properties.Resources.pencil;
            this.cmsWrite.Name = "cmsWrite";
            this.cmsWrite.Size = new System.Drawing.Size(139, 22);
            this.cmsWrite.Text = "写入RFID卡";
            this.cmsWrite.Click += new System.EventHandler(this.btnWriteRfid_Click);
            // 
            // cmsRfidSet
            // 
            this.cmsRfidSet.Image = global::DesktopApp.Properties.Resources.cog;
            this.cmsRfidSet.Name = "cmsRfidSet";
            this.cmsRfidSet.Size = new System.Drawing.Size(139, 22);
            this.cmsRfidSet.Text = "写卡器设置";
            this.cmsRfidSet.Click += new System.EventHandler(this.btnRfidSet_Click);
            // 
            // pagerControl
            // 
            this.pagerControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagerControl.Location = new System.Drawing.Point(0, 408);
            this.pagerControl.Name = "pagerControl";
            this.pagerControl.PageIndex = 1;
            this.pagerControl.PageSize = 100;
            this.pagerControl.RecordCount = 0;
            this.pagerControl.Size = new System.Drawing.Size(935, 23);
            this.pagerControl.TabIndex = 2;
            // 
            // frmEquipmentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(935, 481);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.panTop);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEquipmentList";
            this.Text = "设备箱登记";
            this.Load += new System.EventHandler(this.frmEquipmentList_Load);
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
        private System.Windows.Forms.ComboBox cbCity;
        private System.Windows.Forms.TextBox txtKey;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Button btnRfidSet;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem cmsWrite;
        private System.Windows.Forms.ToolStripMenuItem cmsRfidSet;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_BoxCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_City;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_Village;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_IP;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_MonitorNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_CameraType;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_CameraQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_Direction;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_Range;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_InstallType;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_Height;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_Width;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_Longitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_Latitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_ElectricityType;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_EquipmentBoxQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_OpticalFiberQty1;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_OpticalFiberQty2;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_CreateBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_UpdateBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn E_UpdateDate;
        private System.Windows.Forms.DataGridViewCheckBoxColumn E_Active;
        private System.Windows.Forms.Button btnExport;
        private UserDefineControl.PagerControl pagerControl;
    }
}