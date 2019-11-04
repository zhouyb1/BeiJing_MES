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
            this.选择 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.物料 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.批次 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.实用数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.价格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.车间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbRecord = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbWorkShop = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comOrderNo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.btn_Weight = new System.Windows.Forms.Button();
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
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTeam = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOrderDate = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTS = new System.Windows.Forms.Label();
            this.cmbTeamName = new System.Windows.Forms.ComboBox();
            this.cmbWorkShopName = new System.Windows.Forms.ComboBox();
            this.cmbProceName = new System.Windows.Forms.ComboBox();
            this.cmbRecordName = new System.Windows.Forms.ComboBox();
            this.btnPrintf = new System.Windows.Forms.Button();
            this.btnResolve = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbProce
            // 
            this.cmbProce.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProce.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbProce.FormattingEnabled = true;
            this.cmbProce.Location = new System.Drawing.Point(444, 76);
            this.cmbProce.Name = "cmbProce";
            this.cmbProce.Size = new System.Drawing.Size(200, 23);
            this.cmbProce.TabIndex = 106;
            this.cmbProce.SelectedIndexChanged += new System.EventHandler(this.cmbProce_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11F);
            this.label4.Location = new System.Drawing.Point(70, 81);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 105;
            this.label4.Text = "工序：";
            // 
            // btn_Search
            // 
            this.btn_Search.Font = new System.Drawing.Font("宋体", 11F);
            this.btn_Search.Image = global::DesktopApp.Properties.Resources.search1;
            this.btn_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Search.Location = new System.Drawing.Point(664, 9);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(90, 30);
            this.btn_Search.TabIndex = 99;
            this.btn_Search.Text = "查询";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_upload
            // 
            this.btn_upload.Font = new System.Drawing.Font("宋体", 11F);
            this.btn_upload.Image = global::DesktopApp.Properties.Resources.ok;
            this.btn_upload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_upload.Location = new System.Drawing.Point(764, 9);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(90, 30);
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
            this.车间,
            this.状态,
            this.物料名称,
            this.单位,
            this.ID,
            this.备注,
            this.单号});
            this.dataGridView1.Location = new System.Drawing.Point(12, 129);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(914, 158);
            this.dataGridView1.TabIndex = 97;
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
            // 车间
            // 
            this.车间.DataPropertyName = "W_WorkShop";
            this.车间.HeaderText = "车间";
            this.车间.Name = "车间";
            this.车间.ReadOnly = true;
            // 
            // 状态
            // 
            this.状态.DataPropertyName = "W_Status";
            this.状态.HeaderText = "状态";
            this.状态.Name = "状态";
            this.状态.ReadOnly = true;
            this.状态.Visible = false;
            this.状态.Width = 60;
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
            this.单位.Width = 60;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // 备注
            // 
            this.备注.DataPropertyName = "W_Remark";
            this.备注.HeaderText = "备注";
            this.备注.Name = "备注";
            this.备注.ReadOnly = true;
            // 
            // 单号
            // 
            this.单号.DataPropertyName = "W_RecordCode";
            this.单号.HeaderText = "单号";
            this.单号.Name = "单号";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 11F);
            this.label10.Location = new System.Drawing.Point(360, 51);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 15);
            this.label10.TabIndex = 93;
            this.label10.Text = "工艺编码：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 11F);
            this.label7.Location = new System.Drawing.Point(360, 26);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 91;
            this.label7.Text = "车间编码：";
            // 
            // cmbRecord
            // 
            this.cmbRecord.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecord.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbRecord.FormattingEnabled = true;
            this.cmbRecord.Location = new System.Drawing.Point(444, 51);
            this.cmbRecord.Name = "cmbRecord";
            this.cmbRecord.Size = new System.Drawing.Size(200, 23);
            this.cmbRecord.TabIndex = 88;
            this.cmbRecord.SelectedIndexChanged += new System.EventHandler(this.cmbRecord_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F);
            this.label3.Location = new System.Drawing.Point(40, 56);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 87;
            this.label3.Text = "工艺代码：";
            // 
            // cmbWorkShop
            // 
            this.cmbWorkShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkShop.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbWorkShop.FormattingEnabled = true;
            this.cmbWorkShop.Location = new System.Drawing.Point(444, 26);
            this.cmbWorkShop.Name = "cmbWorkShop";
            this.cmbWorkShop.Size = new System.Drawing.Size(200, 23);
            this.cmbWorkShop.TabIndex = 86;
            this.cmbWorkShop.SelectedIndexChanged += new System.EventHandler(this.cmbWorkShop_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(70, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 85;
            this.label1.Text = "车间：";
            // 
            // comOrderNo
            // 
            this.comOrderNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comOrderNo.Font = new System.Drawing.Font("宋体", 11F);
            this.comOrderNo.FormattingEnabled = true;
            this.comOrderNo.Location = new System.Drawing.Point(126, 3);
            this.comOrderNo.Name = "comOrderNo";
            this.comOrderNo.Size = new System.Drawing.Size(222, 23);
            this.comOrderNo.TabIndex = 83;
            this.comOrderNo.SelectedIndexChanged += new System.EventHandler(this.comOrderNo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 11F);
            this.label6.Location = new System.Drawing.Point(25, 6);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 81;
            this.label6.Text = "生产订单号：";
            // 
            // btnScan
            // 
            this.btnScan.Font = new System.Drawing.Font("宋体", 11F);
            this.btnScan.Image = global::DesktopApp.Properties.Resources.home;
            this.btnScan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnScan.Location = new System.Drawing.Point(860, 31);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(90, 30);
            this.btnScan.TabIndex = 116;
            this.btnScan.Text = "扫描";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Visible = false;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btn_Weight
            // 
            this.btn_Weight.Font = new System.Drawing.Font("宋体", 11F);
            this.btn_Weight.Image = global::DesktopApp.Properties.Resources.communication;
            this.btn_Weight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Weight.Location = new System.Drawing.Point(764, 49);
            this.btn_Weight.Name = "btn_Weight";
            this.btn_Weight.Size = new System.Drawing.Size(90, 30);
            this.btn_Weight.TabIndex = 117;
            this.btn_Weight.Text = "称重";
            this.btn_Weight.UseVisualStyleBackColor = true;
            this.btn_Weight.Click += new System.EventHandler(this.btn_Weight_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(360, 79);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 118;
            this.label2.Text = "工序编码：";
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
            this.dataGridView2.Location = new System.Drawing.Point(12, 287);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(914, 161);
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
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F);
            this.label5.Location = new System.Drawing.Point(360, 105);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 15);
            this.label5.TabIndex = 123;
            this.label5.Text = "班组编码：";
            // 
            // cmbTeam
            // 
            this.cmbTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeam.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbTeam.FormattingEnabled = true;
            this.cmbTeam.Location = new System.Drawing.Point(444, 102);
            this.cmbTeam.Name = "cmbTeam";
            this.cmbTeam.Size = new System.Drawing.Size(200, 23);
            this.cmbTeam.TabIndex = 122;
            this.cmbTeam.SelectedIndexChanged += new System.EventHandler(this.cmbTeam_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 11F);
            this.label8.Location = new System.Drawing.Point(70, 107);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 15);
            this.label8.TabIndex = 121;
            this.label8.Text = "班组：";
            // 
            // txtOrderDate
            // 
            this.txtOrderDate.Location = new System.Drawing.Point(444, 3);
            this.txtOrderDate.Name = "txtOrderDate";
            this.txtOrderDate.Size = new System.Drawing.Size(200, 21);
            this.txtOrderDate.TabIndex = 126;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 11F);
            this.label9.Location = new System.Drawing.Point(360, 6);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 125;
            this.label9.Text = "订单时间：";
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("宋体", 11F);
            this.btnBack.Image = global::DesktopApp.Properties.Resources.prev;
            this.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.Location = new System.Drawing.Point(664, 49);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(90, 30);
            this.btnBack.TabIndex = 127;
            this.btnBack.Text = "退回";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblTS
            // 
            this.lblTS.AutoSize = true;
            this.lblTS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTS.ForeColor = System.Drawing.Color.Red;
            this.lblTS.Location = new System.Drawing.Point(34, 453);
            this.lblTS.Name = "lblTS";
            this.lblTS.Size = new System.Drawing.Size(0, 16);
            this.lblTS.TabIndex = 128;
            // 
            // cmbTeamName
            // 
            this.cmbTeamName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTeamName.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbTeamName.FormattingEnabled = true;
            this.cmbTeamName.Location = new System.Drawing.Point(126, 104);
            this.cmbTeamName.Name = "cmbTeamName";
            this.cmbTeamName.Size = new System.Drawing.Size(222, 23);
            this.cmbTeamName.TabIndex = 130;
            this.cmbTeamName.SelectedIndexChanged += new System.EventHandler(this.cmbTeamName_SelectedIndexChanged);
            // 
            // cmbWorkShopName
            // 
            this.cmbWorkShopName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkShopName.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbWorkShopName.FormattingEnabled = true;
            this.cmbWorkShopName.Location = new System.Drawing.Point(126, 28);
            this.cmbWorkShopName.Name = "cmbWorkShopName";
            this.cmbWorkShopName.Size = new System.Drawing.Size(222, 23);
            this.cmbWorkShopName.TabIndex = 129;
            this.cmbWorkShopName.SelectedIndexChanged += new System.EventHandler(this.cmbWorkShopName_SelectedIndexChanged);
            // 
            // cmbProceName
            // 
            this.cmbProceName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProceName.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbProceName.FormattingEnabled = true;
            this.cmbProceName.Location = new System.Drawing.Point(126, 78);
            this.cmbProceName.Name = "cmbProceName";
            this.cmbProceName.Size = new System.Drawing.Size(222, 23);
            this.cmbProceName.TabIndex = 132;
            this.cmbProceName.SelectedIndexChanged += new System.EventHandler(this.cmbProceName_SelectedIndexChanged);
            // 
            // cmbRecordName
            // 
            this.cmbRecordName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRecordName.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbRecordName.FormattingEnabled = true;
            this.cmbRecordName.Location = new System.Drawing.Point(126, 53);
            this.cmbRecordName.Name = "cmbRecordName";
            this.cmbRecordName.Size = new System.Drawing.Size(222, 23);
            this.cmbRecordName.TabIndex = 131;
            this.cmbRecordName.SelectedIndexChanged += new System.EventHandler(this.cmbRecordName_SelectedIndexChanged);
            // 
            // btnPrintf
            // 
            this.btnPrintf.Font = new System.Drawing.Font("宋体", 11F);
            this.btnPrintf.Image = global::DesktopApp.Properties.Resources.camera_lens;
            this.btnPrintf.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPrintf.Location = new System.Drawing.Point(664, 90);
            this.btnPrintf.Name = "btnPrintf";
            this.btnPrintf.Size = new System.Drawing.Size(90, 30);
            this.btnPrintf.TabIndex = 133;
            this.btnPrintf.Text = "补写";
            this.btnPrintf.UseVisualStyleBackColor = true;
            this.btnPrintf.Click += new System.EventHandler(this.btnPrintf_Click);
            // 
            // btnResolve
            // 
            this.btnResolve.Font = new System.Drawing.Font("宋体", 11F);
            this.btnResolve.Image = global::DesktopApp.Properties.Resources.discuss;
            this.btnResolve.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResolve.Location = new System.Drawing.Point(764, 90);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(90, 30);
            this.btnResolve.TabIndex = 134;
            this.btnResolve.Text = "分写";
            this.btnResolve.UseVisualStyleBackColor = true;
            this.btnResolve.Click += new System.EventHandler(this.btnResolve_Click);
            // 
            // frmOrgres
            // 
            this.ClientSize = new System.Drawing.Size(950, 541);
            this.Controls.Add(this.btnResolve);
            this.Controls.Add(this.btnPrintf);
            this.Controls.Add(this.cmbProceName);
            this.Controls.Add(this.cmbRecordName);
            this.Controls.Add(this.cmbTeamName);
            this.Controls.Add(this.cmbWorkShopName);
            this.Controls.Add(this.lblTS);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.txtOrderDate);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbTeam);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Weight);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.cmbProce);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label10);
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
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbRecord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbWorkShop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comOrderNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btn_Weight;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView2;
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
        private System.Windows.Forms.Label lblTS;
        private System.Windows.Forms.ComboBox cmbTeamName;
        private System.Windows.Forms.ComboBox cmbWorkShopName;
        private System.Windows.Forms.ComboBox cmbProceName;
        private System.Windows.Forms.ComboBox cmbRecordName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 选择;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料;
        private System.Windows.Forms.DataGridViewTextBoxColumn 批次;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 实用数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 价格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 车间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 状态;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单位;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单号;
        private System.Windows.Forms.Button btnPrintf;
        private System.Windows.Forms.Button btnResolve;
    }
}