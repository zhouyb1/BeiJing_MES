namespace DesktopApp
{
    partial class frmOrgres
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
            this.cmbProce = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Search = new System.Windows.Forms.Button();
            this.btn_upload = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtRecordName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtWorkShopName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbRecord = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbWorkShop = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comOrderNo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.btn_Weight = new System.Windows.Forms.Button();
            this.txtProceName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.选择2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.物料2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.批次2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数量2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.生产订单2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.车间2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工艺代码2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工艺名称2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工序2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工序名称2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.车间名称2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.状态2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.添加人2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.添加时间2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料名称2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单位2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTeamName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTeam = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOrderDate = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.选择 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.物料 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.批次 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.实用数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.价格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.生产订单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.车间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工艺代码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工序 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工艺代码名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工序名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.车间名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.添加人 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.添加时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbProce
            // 
            this.cmbProce.FormattingEnabled = true;
            this.cmbProce.Location = new System.Drawing.Point(126, 90);
            this.cmbProce.Name = "cmbProce";
            this.cmbProce.Size = new System.Drawing.Size(121, 20);
            this.cmbProce.TabIndex = 106;
            this.cmbProce.SelectedIndexChanged += new System.EventHandler(this.cmbProce_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(80, 93);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 105;
            this.label4.Text = "工序：";
            // 
            // btn_Search
            // 
            this.btn_Search.Image = global::DesktopApp.Properties.Resources.search1;
            this.btn_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Search.Location = new System.Drawing.Point(499, 21);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(75, 23);
            this.btn_Search.TabIndex = 99;
            this.btn_Search.Text = "查询";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_upload
            // 
            this.btn_upload.Image = global::DesktopApp.Properties.Resources.ok;
            this.btn_upload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_upload.Location = new System.Drawing.Point(580, 21);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(75, 23);
            this.btn_upload.TabIndex = 98;
            this.btn_upload.Text = "提交";
            this.btn_upload.UseVisualStyleBackColor = true;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.选择,
            this.物料,
            this.批次,
            this.数量,
            this.实用数量,
            this.价格,
            this.生产订单号,
            this.车间,
            this.工艺代码,
            this.工序,
            this.ID,
            this.工艺代码名称,
            this.工序名称,
            this.车间名称,
            this.状态,
            this.添加人,
            this.添加时间,
            this.物料名称,
            this.单位,
            this.备注});
            this.dataGridView1.Location = new System.Drawing.Point(12, 143);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(683, 184);
            this.dataGridView1.TabIndex = 97;
            // 
            // txtRecordName
            // 
            this.txtRecordName.Location = new System.Drawing.Point(345, 62);
            this.txtRecordName.Name = "txtRecordName";
            this.txtRecordName.Size = new System.Drawing.Size(121, 21);
            this.txtRecordName.TabIndex = 94;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(275, 62);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 93;
            this.label10.Text = "工艺名称：";
            // 
            // txtWorkShopName
            // 
            this.txtWorkShopName.Location = new System.Drawing.Point(345, 37);
            this.txtWorkShopName.Name = "txtWorkShopName";
            this.txtWorkShopName.Size = new System.Drawing.Size(121, 21);
            this.txtWorkShopName.TabIndex = 92;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(275, 36);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 91;
            this.label7.Text = "车间名称：";
            // 
            // cmbRecord
            // 
            this.cmbRecord.FormattingEnabled = true;
            this.cmbRecord.Location = new System.Drawing.Point(126, 64);
            this.cmbRecord.Name = "cmbRecord";
            this.cmbRecord.Size = new System.Drawing.Size(121, 20);
            this.cmbRecord.TabIndex = 88;
            this.cmbRecord.SelectedIndexChanged += new System.EventHandler(this.cmbRecord_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 87;
            this.label3.Text = "工艺代码：";
            // 
            // cmbWorkShop
            // 
            this.cmbWorkShop.FormattingEnabled = true;
            this.cmbWorkShop.Location = new System.Drawing.Point(126, 38);
            this.cmbWorkShop.Name = "cmbWorkShop";
            this.cmbWorkShop.Size = new System.Drawing.Size(121, 20);
            this.cmbWorkShop.TabIndex = 86;
            this.cmbWorkShop.SelectedIndexChanged += new System.EventHandler(this.cmbWorkShop_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 85;
            this.label1.Text = "车间：";
            // 
            // comOrderNo
            // 
            this.comOrderNo.FormattingEnabled = true;
            this.comOrderNo.Location = new System.Drawing.Point(126, 12);
            this.comOrderNo.Name = "comOrderNo";
            this.comOrderNo.Size = new System.Drawing.Size(121, 20);
            this.comOrderNo.TabIndex = 83;
            this.comOrderNo.SelectedIndexChanged += new System.EventHandler(this.comOrderNo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 15);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 81;
            this.label6.Text = "生产订单号：";
            // 
            // btnScan
            // 
            this.btnScan.Image = global::DesktopApp.Properties.Resources.home;
            this.btnScan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScan.Location = new System.Drawing.Point(499, 61);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(75, 23);
            this.btnScan.TabIndex = 116;
            this.btnScan.Text = "扫描";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btn_Weight
            // 
            this.btn_Weight.Image = global::DesktopApp.Properties.Resources.communication;
            this.btn_Weight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Weight.Location = new System.Drawing.Point(580, 61);
            this.btn_Weight.Name = "btn_Weight";
            this.btn_Weight.Size = new System.Drawing.Size(75, 23);
            this.btn_Weight.TabIndex = 117;
            this.btn_Weight.Text = "称重";
            this.btn_Weight.UseVisualStyleBackColor = true;
            this.btn_Weight.Click += new System.EventHandler(this.btn_Weight_Click);
            // 
            // txtProceName
            // 
            this.txtProceName.Location = new System.Drawing.Point(345, 88);
            this.txtProceName.Name = "txtProceName";
            this.txtProceName.Size = new System.Drawing.Size(121, 21);
            this.txtProceName.TabIndex = 119;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(275, 91);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 118;
            this.label2.Text = "工序名称：";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.选择2,
            this.物料2,
            this.批次2,
            this.数量2,
            this.生产订单2,
            this.车间2,
            this.工艺代码2,
            this.工艺名称2,
            this.工序2,
            this.工序名称2,
            this.ID2,
            this.车间名称2,
            this.状态2,
            this.添加人2,
            this.添加时间2,
            this.物料名称2,
            this.单位2,
            this.备注2});
            this.dataGridView2.Location = new System.Drawing.Point(12, 333);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(683, 161);
            this.dataGridView2.TabIndex = 120;
            // 
            // 选择2
            // 
            this.选择2.HeaderText = "选择2";
            this.选择2.Name = "选择2";
            this.选择2.Width = 40;
            // 
            // 物料2
            // 
            this.物料2.DataPropertyName = "W_SecGoodsCode";
            this.物料2.HeaderText = "物料";
            this.物料2.Name = "物料2";
            // 
            // 批次2
            // 
            this.批次2.DataPropertyName = "W_SecBatch";
            this.批次2.HeaderText = "批次";
            this.批次2.Name = "批次2";
            // 
            // 数量2
            // 
            this.数量2.DataPropertyName = "W_SecQty";
            this.数量2.HeaderText = "数量";
            this.数量2.Name = "数量2";
            // 
            // 生产订单2
            // 
            this.生产订单2.DataPropertyName = "W_OrderNo";
            this.生产订单2.HeaderText = "生产订单号";
            this.生产订单2.Name = "生产订单2";
            // 
            // 车间2
            // 
            this.车间2.DataPropertyName = "W_WorkShopCode";
            this.车间2.HeaderText = "车间";
            this.车间2.Name = "车间2";
            // 
            // 工艺代码2
            // 
            this.工艺代码2.DataPropertyName = "W_RecordCode";
            this.工艺代码2.HeaderText = "工艺代码";
            this.工艺代码2.Name = "工艺代码2";
            // 
            // 工艺名称2
            // 
            this.工艺名称2.DataPropertyName = "W_RecordName";
            this.工艺名称2.HeaderText = "工艺名称";
            this.工艺名称2.Name = "工艺名称2";
            // 
            // 工序2
            // 
            this.工序2.DataPropertyName = "W_ProceCode";
            this.工序2.HeaderText = "工序";
            this.工序2.Name = "工序2";
            // 
            // 工序名称2
            // 
            this.工序名称2.DataPropertyName = "W_ProceName";
            this.工序名称2.HeaderText = "工序名称";
            this.工序名称2.Name = "工序名称2";
            // 
            // ID2
            // 
            this.ID2.DataPropertyName = "ID";
            this.ID2.HeaderText = "ID";
            this.ID2.Name = "ID2";
            this.ID2.Visible = false;
            // 
            // 车间名称2
            // 
            this.车间名称2.DataPropertyName = "W_WorkShopName";
            this.车间名称2.HeaderText = "车间名称";
            this.车间名称2.Name = "车间名称2";
            // 
            // 状态2
            // 
            this.状态2.DataPropertyName = "W_Status";
            this.状态2.HeaderText = "状态";
            this.状态2.Name = "状态2";
            // 
            // 添加人2
            // 
            this.添加人2.DataPropertyName = "W_CreateBy";
            this.添加人2.HeaderText = "添加人";
            this.添加人2.Name = "添加人2";
            // 
            // 添加时间2
            // 
            this.添加时间2.DataPropertyName = "(W_CreateDate";
            this.添加时间2.HeaderText = "添加时间";
            this.添加时间2.Name = "添加时间2";
            // 
            // 物料名称2
            // 
            this.物料名称2.DataPropertyName = "W_SecGoodsName";
            this.物料名称2.HeaderText = "物料名称";
            this.物料名称2.Name = "物料名称2";
            // 
            // 单位2
            // 
            this.单位2.DataPropertyName = "W_SecUnit";
            this.单位2.HeaderText = "单位";
            this.单位2.Name = "单位2";
            // 
            // 备注2
            // 
            this.备注2.DataPropertyName = "W_Remark";
            this.备注2.HeaderText = "备注";
            this.备注2.Name = "备注2";
            // 
            // txtTeamName
            // 
            this.txtTeamName.Location = new System.Drawing.Point(345, 114);
            this.txtTeamName.Name = "txtTeamName";
            this.txtTeamName.Size = new System.Drawing.Size(121, 21);
            this.txtTeamName.TabIndex = 124;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(275, 118);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 123;
            this.label5.Text = "班组名称：";
            // 
            // cmbTeam
            // 
            this.cmbTeam.FormattingEnabled = true;
            this.cmbTeam.Location = new System.Drawing.Point(126, 117);
            this.cmbTeam.Name = "cmbTeam";
            this.cmbTeam.Size = new System.Drawing.Size(121, 20);
            this.cmbTeam.TabIndex = 122;
            this.cmbTeam.SelectedIndexChanged += new System.EventHandler(this.cmbTeam_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(80, 120);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 121;
            this.label8.Text = "班组：";
            // 
            // txtOrderDate
            // 
            this.txtOrderDate.Location = new System.Drawing.Point(345, 12);
            this.txtOrderDate.Name = "txtOrderDate";
            this.txtOrderDate.Size = new System.Drawing.Size(121, 21);
            this.txtOrderDate.TabIndex = 126;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(275, 15);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 125;
            this.label9.Text = "订单时间：";
            // 
            // btnBack
            // 
            this.btnBack.Image = global::DesktopApp.Properties.Resources.cancel;
            this.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.Location = new System.Drawing.Point(499, 107);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 127;
            this.btnBack.Text = "    退回";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // 选择
            // 
            this.选择.HeaderText = "选择";
            this.选择.Name = "选择";
            this.选择.Width = 40;
            // 
            // 物料
            // 
            this.物料.DataPropertyName = "W_GoodsCode";
            this.物料.HeaderText = "物料";
            this.物料.Name = "物料";
            this.物料.ReadOnly = true;
            // 
            // 批次
            // 
            this.批次.DataPropertyName = "W_Batch";
            this.批次.HeaderText = "批次";
            this.批次.Name = "批次";
            this.批次.ReadOnly = true;
            // 
            // 数量
            // 
            this.数量.DataPropertyName = "W_Qty";
            this.数量.HeaderText = "数量";
            this.数量.Name = "数量";
            this.数量.ReadOnly = true;
            // 
            // 实用数量
            // 
            this.实用数量.HeaderText = "实用数量";
            this.实用数量.Name = "实用数量";
            // 
            // 价格
            // 
            this.价格.DataPropertyName = "W_Price";
            this.价格.HeaderText = "价格";
            this.价格.Name = "价格";
            this.价格.ReadOnly = true;
            // 
            // 生产订单号
            // 
            this.生产订单号.DataPropertyName = "W_OrderNo";
            this.生产订单号.HeaderText = "生产订单号";
            this.生产订单号.Name = "生产订单号";
            this.生产订单号.ReadOnly = true;
            // 
            // 车间
            // 
            this.车间.DataPropertyName = "W_WorkShop";
            this.车间.HeaderText = "车间";
            this.车间.Name = "车间";
            this.车间.ReadOnly = true;
            // 
            // 工艺代码
            // 
            this.工艺代码.DataPropertyName = "W_RecordCode";
            this.工艺代码.HeaderText = "工艺代码";
            this.工艺代码.Name = "工艺代码";
            this.工艺代码.ReadOnly = true;
            // 
            // 工序
            // 
            this.工序.DataPropertyName = "W_ProceCode";
            this.工序.HeaderText = "工序";
            this.工序.Name = "工序";
            this.工序.ReadOnly = true;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // 工艺代码名称
            // 
            this.工艺代码名称.DataPropertyName = "W_RecordName";
            this.工艺代码名称.HeaderText = "工艺代码名称";
            this.工艺代码名称.Name = "工艺代码名称";
            this.工艺代码名称.ReadOnly = true;
            this.工艺代码名称.Visible = false;
            // 
            // 工序名称
            // 
            this.工序名称.DataPropertyName = "W_ProceName";
            this.工序名称.HeaderText = "工序名称";
            this.工序名称.Name = "工序名称";
            this.工序名称.ReadOnly = true;
            this.工序名称.Visible = false;
            // 
            // 车间名称
            // 
            this.车间名称.DataPropertyName = "W_WorkShopName";
            this.车间名称.HeaderText = "车间名称";
            this.车间名称.Name = "车间名称";
            this.车间名称.ReadOnly = true;
            this.车间名称.Visible = false;
            // 
            // 状态
            // 
            this.状态.DataPropertyName = "W_Status";
            this.状态.HeaderText = "状态";
            this.状态.Name = "状态";
            this.状态.ReadOnly = true;
            this.状态.Visible = false;
            // 
            // 添加人
            // 
            this.添加人.DataPropertyName = "W_CreateBy";
            this.添加人.HeaderText = "添加人";
            this.添加人.Name = "添加人";
            this.添加人.ReadOnly = true;
            this.添加人.Visible = false;
            // 
            // 添加时间
            // 
            this.添加时间.DataPropertyName = "W_CreateDate";
            this.添加时间.HeaderText = "添加时间";
            this.添加时间.Name = "添加时间";
            this.添加时间.ReadOnly = true;
            this.添加时间.Visible = false;
            // 
            // 物料名称
            // 
            this.物料名称.DataPropertyName = "W_GoodsName";
            this.物料名称.HeaderText = "物料名称";
            this.物料名称.Name = "物料名称";
            this.物料名称.ReadOnly = true;
            // 
            // 单位
            // 
            this.单位.DataPropertyName = "W_Unit";
            this.单位.HeaderText = "单位";
            this.单位.Name = "单位";
            this.单位.ReadOnly = true;
            // 
            // 备注
            // 
            this.备注.DataPropertyName = "W_Remark";
            this.备注.HeaderText = "备注";
            this.备注.Name = "备注";
            this.备注.ReadOnly = true;
            // 
            // frmOrgres
            // 
            this.ClientSize = new System.Drawing.Size(775, 504);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.txtOrderDate);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtTeamName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbTeam);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.txtProceName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Weight);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.cmbProce);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtRecordName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtWorkShopName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cmbRecord);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbWorkShop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comOrderNo);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmOrgres";
            this.Text = "车间扫描";
            this.Load += new System.EventHandler(this.frmOrgres_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbProce;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtRecordName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtWorkShopName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbRecord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbWorkShop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comOrderNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btn_Weight;
        private System.Windows.Forms.TextBox txtProceName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TextBox txtTeamName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTeam;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOrderDate;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 选择2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 批次2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 生产订单2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 车间2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工艺代码2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工艺名称2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工序2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工序名称2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 车间名称2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 状态2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 添加人2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 添加时间2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料名称2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单位2;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 选择;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料;
        private System.Windows.Forms.DataGridViewTextBoxColumn 批次;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 实用数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 价格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 生产订单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 车间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工艺代码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工序;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工艺代码名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工序名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 车间名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 状态;
        private System.Windows.Forms.DataGridViewTextBoxColumn 添加人;
        private System.Windows.Forms.DataGridViewTextBoxColumn 添加时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注;
    }
}