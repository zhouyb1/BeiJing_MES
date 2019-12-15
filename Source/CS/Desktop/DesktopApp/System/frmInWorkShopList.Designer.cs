namespace DesktopApp
{
    partial class frmInWorkShopList
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


        private void InitializeComponent()
        {
            this.cmbStock = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comOrderNo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbWorkShop = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbRecord = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtRecordName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtPc = new System.Windows.Forms.TextBox();
            this.cmbProce = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtProceName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.生产订单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.车间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkShopName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.线边仓 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoodsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.批次 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工艺代码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.条码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTS = new System.Windows.Forms.Label();
            this.txtOrderDate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbWorkshopName = new System.Windows.Forms.ComboBox();
            this.cmbStockName = new System.Windows.Forms.ComboBox();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbStock
            // 
            this.cmbStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStock.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbStock.FormattingEnabled = true;
            this.cmbStock.Location = new System.Drawing.Point(467, 58);
            this.cmbStock.Name = "cmbStock";
            this.cmbStock.Size = new System.Drawing.Size(232, 23);
            this.cmbStock.TabIndex = 17;
            this.cmbStock.SelectedIndexChanged += new System.EventHandler(this.cmbStock_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 11F);
            this.label8.Location = new System.Drawing.Point(36, 62);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 16;
            this.label8.Text = "日耗库：";
            // 
            // comOrderNo
            // 
            this.comOrderNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comOrderNo.Font = new System.Drawing.Font("宋体", 11F);
            this.comOrderNo.FormattingEnabled = true;
            this.comOrderNo.Location = new System.Drawing.Point(106, 7);
            this.comOrderNo.Name = "comOrderNo";
            this.comOrderNo.Size = new System.Drawing.Size(232, 23);
            this.comOrderNo.TabIndex = 15;
            this.comOrderNo.SelectedIndexChanged += new System.EventHandler(this.comOrderNo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 11F);
            this.label6.Location = new System.Drawing.Point(6, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "生产订单号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(21, 131);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "物料编码：";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("宋体", 12F);
            this.btnSave.Image = global::DesktopApp.Properties.Resources.save_disabled;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(762, 58);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbWorkShop
            // 
            this.cmbWorkShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkShop.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbWorkShop.FormattingEnabled = true;
            this.cmbWorkShop.Location = new System.Drawing.Point(467, 32);
            this.cmbWorkShop.Name = "cmbWorkShop";
            this.cmbWorkShop.Size = new System.Drawing.Size(232, 23);
            this.cmbWorkShop.TabIndex = 20;
            this.cmbWorkShop.SelectedIndexChanged += new System.EventHandler(this.cmbWorkShop_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(51, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 19;
            this.label1.Text = "车间：";
            // 
            // cmbRecord
            // 
            this.cmbRecord.FormattingEnabled = true;
            this.cmbRecord.Location = new System.Drawing.Point(378, 172);
            this.cmbRecord.Name = "cmbRecord";
            this.cmbRecord.Size = new System.Drawing.Size(121, 20);
            this.cmbRecord.TabIndex = 23;
            this.cmbRecord.Visible = false;
            this.cmbRecord.SelectedIndexChanged += new System.EventHandler(this.cmbRecord_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(311, 175);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "工艺代码：";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 11F);
            this.label4.Location = new System.Drawing.Point(51, 184);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 24;
            this.label4.Text = "价格：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F);
            this.label5.Location = new System.Drawing.Point(415, 158);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 25;
            this.label5.Text = "数量：";
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("宋体", 11F);
            this.txtPrice.Location = new System.Drawing.Point(106, 180);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.ReadOnly = true;
            this.txtPrice.Size = new System.Drawing.Size(246, 24);
            this.txtPrice.TabIndex = 26;
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("宋体", 11F);
            this.txtQty.Location = new System.Drawing.Point(467, 155);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(232, 24);
            this.txtQty.TabIndex = 27;
            // 
            // txtRecordName
            // 
            this.txtRecordName.Location = new System.Drawing.Point(660, 152);
            this.txtRecordName.Name = "txtRecordName";
            this.txtRecordName.Size = new System.Drawing.Size(121, 21);
            this.txtRecordName.TabIndex = 33;
            this.txtRecordName.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(590, 174);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 32;
            this.label10.Text = "工艺名称：";
            this.label10.Visible = false;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 11F);
            this.txtName.Location = new System.Drawing.Point(467, 128);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(232, 24);
            this.txtName.TabIndex = 35;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 11F);
            this.label11.Location = new System.Drawing.Point(385, 131);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 15);
            this.label11.TabIndex = 34;
            this.label11.Text = "物料名称：";
            // 
            // btnUpload
            // 
            this.btnUpload.Font = new System.Drawing.Font("宋体", 12F);
            this.btnUpload.Image = global::DesktopApp.Properties.Resources.ok;
            this.btnUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUpload.Location = new System.Drawing.Point(762, 134);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(90, 30);
            this.btnUpload.TabIndex = 39;
            this.btnUpload.Text = "完工";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("宋体", 12F);
            this.btnSearch.Image = global::DesktopApp.Properties.Resources.search1;
            this.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.Location = new System.Drawing.Point(762, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(90, 30);
            this.btnSearch.TabIndex = 40;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 11F);
            this.label13.Location = new System.Drawing.Point(51, 156);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 15);
            this.label13.TabIndex = 41;
            this.label13.Text = "批次：";
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("宋体", 11F);
            this.txtCode.Location = new System.Drawing.Point(107, 127);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(246, 24);
            this.txtCode.TabIndex = 43;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("宋体", 11F);
            this.txtBarcode.Location = new System.Drawing.Point(106, 101);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(593, 24);
            this.txtBarcode.TabIndex = 45;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 11F);
            this.label14.Location = new System.Drawing.Point(36, 103);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 15);
            this.label14.TabIndex = 44;
            this.label14.Text = "二维码：";
            // 
            // txtPc
            // 
            this.txtPc.Font = new System.Drawing.Font("宋体", 11F);
            this.txtPc.Location = new System.Drawing.Point(106, 153);
            this.txtPc.Name = "txtPc";
            this.txtPc.Size = new System.Drawing.Size(246, 24);
            this.txtPc.TabIndex = 46;
            // 
            // cmbProce
            // 
            this.cmbProce.FormattingEnabled = true;
            this.cmbProce.Location = new System.Drawing.Point(378, 198);
            this.cmbProce.Name = "cmbProce";
            this.cmbProce.Size = new System.Drawing.Size(121, 20);
            this.cmbProce.TabIndex = 48;
            this.cmbProce.Visible = false;
            this.cmbProce.SelectedIndexChanged += new System.EventHandler(this.cmbProce_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(320, 201);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 47;
            this.label12.Text = "工序号：";
            this.label12.Visible = false;
            // 
            // txtProceName
            // 
            this.txtProceName.Location = new System.Drawing.Point(660, 179);
            this.txtProceName.Name = "txtProceName";
            this.txtProceName.Size = new System.Drawing.Size(121, 21);
            this.txtProceName.TabIndex = 50;
            this.txtProceName.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(590, 204);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 49;
            this.label15.Text = "工序名称：";
            this.label15.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 11F);
            this.label16.Location = new System.Drawing.Point(385, 9);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 15);
            this.label16.TabIndex = 51;
            this.label16.Text = "订单时间：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.生产订单号,
            this.车间,
            this.WorkShopName,
            this.线边仓,
            this.StockName,
            this.物料,
            this.GoodsName,
            this.批次,
            this.Price,
            this.unit,
            this.status,
            this.CreateBy,
            this.CreateDate,
            this.工艺代码,
            this.数量,
            this.条码,
            this.ID,
            this.remark});
            this.dataGridView1.Location = new System.Drawing.Point(31, 209);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(821, 231);
            this.dataGridView1.TabIndex = 69;
            // 
            // 生产订单号
            // 
            this.生产订单号.DataPropertyName = "I_OrderNo";
            this.生产订单号.HeaderText = "生产订单号";
            this.生产订单号.Name = "生产订单号";
            // 
            // 车间
            // 
            this.车间.DataPropertyName = "I_WorkShop";
            this.车间.HeaderText = "车间";
            this.车间.Name = "车间";
            this.车间.Width = 60;
            // 
            // WorkShopName
            // 
            this.WorkShopName.DataPropertyName = "I_WorkShopName";
            this.WorkShopName.HeaderText = "车间名称";
            this.WorkShopName.Name = "WorkShopName";
            // 
            // 线边仓
            // 
            this.线边仓.DataPropertyName = "I_StockCode";
            this.线边仓.HeaderText = "线边仓";
            this.线边仓.Name = "线边仓";
            // 
            // StockName
            // 
            this.StockName.DataPropertyName = "I_StockName";
            this.StockName.HeaderText = "线边仓名称";
            this.StockName.Name = "StockName";
            // 
            // 物料
            // 
            this.物料.DataPropertyName = "I_GoodsCode";
            this.物料.HeaderText = "物料";
            this.物料.Name = "物料";
            // 
            // GoodsName
            // 
            this.GoodsName.DataPropertyName = "I_GoodsName";
            this.GoodsName.HeaderText = "物料名";
            this.GoodsName.Name = "GoodsName";
            // 
            // 批次
            // 
            this.批次.DataPropertyName = "I_Batch";
            this.批次.HeaderText = "批次";
            this.批次.Name = "批次";
            // 
            // Price
            // 
            this.Price.DataPropertyName = "I_Price";
            this.Price.HeaderText = "价格";
            this.Price.Name = "Price";
            // 
            // unit
            // 
            this.unit.DataPropertyName = "I_Unit";
            this.unit.HeaderText = "单位";
            this.unit.Name = "unit";
            // 
            // status
            // 
            this.status.DataPropertyName = "I_Status";
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            // 
            // CreateBy
            // 
            this.CreateBy.DataPropertyName = "I_CreateBy";
            this.CreateBy.HeaderText = "添加人";
            this.CreateBy.Name = "CreateBy";
            // 
            // CreateDate
            // 
            this.CreateDate.DataPropertyName = "I_CreateDate";
            this.CreateDate.HeaderText = "添加时间";
            this.CreateDate.Name = "CreateDate";
            // 
            // 工艺代码
            // 
            this.工艺代码.DataPropertyName = "I_Record";
            this.工艺代码.HeaderText = "工艺代码";
            this.工艺代码.Name = "工艺代码";
            // 
            // 数量
            // 
            this.数量.DataPropertyName = "I_Qty";
            this.数量.HeaderText = "数量";
            this.数量.Name = "数量";
            // 
            // 条码
            // 
            this.条码.DataPropertyName = "I_Barcode";
            this.条码.HeaderText = "条码";
            this.条码.Name = "条码";
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            this.ID.Width = 5;
            // 
            // remark
            // 
            this.remark.DataPropertyName = "I_Remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            // 
            // lblTS
            // 
            this.lblTS.AutoSize = true;
            this.lblTS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTS.ForeColor = System.Drawing.Color.Red;
            this.lblTS.Location = new System.Drawing.Point(38, 446);
            this.lblTS.Name = "lblTS";
            this.lblTS.Size = new System.Drawing.Size(0, 16);
            this.lblTS.TabIndex = 70;
            // 
            // txtOrderDate
            // 
            this.txtOrderDate.Font = new System.Drawing.Font("宋体", 11F);
            this.txtOrderDate.Location = new System.Drawing.Point(467, 6);
            this.txtOrderDate.Name = "txtOrderDate";
            this.txtOrderDate.Size = new System.Drawing.Size(232, 24);
            this.txtOrderDate.TabIndex = 52;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 11F);
            this.label7.Location = new System.Drawing.Point(385, 31);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 28;
            this.label7.Text = "车间编码：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 11F);
            this.label9.Location = new System.Drawing.Point(385, 58);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 30;
            this.label9.Text = "仓库编码：";
            // 
            // cmbWorkshopName
            // 
            this.cmbWorkshopName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkshopName.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbWorkshopName.FormattingEnabled = true;
            this.cmbWorkshopName.Location = new System.Drawing.Point(106, 32);
            this.cmbWorkshopName.Name = "cmbWorkshopName";
            this.cmbWorkshopName.Size = new System.Drawing.Size(232, 23);
            this.cmbWorkshopName.TabIndex = 72;
            this.cmbWorkshopName.SelectedIndexChanged += new System.EventHandler(this.cmbWorkshopName_SelectedIndexChanged);
            // 
            // cmbStockName
            // 
            this.cmbStockName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStockName.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbStockName.FormattingEnabled = true;
            this.cmbStockName.Location = new System.Drawing.Point(106, 58);
            this.cmbStockName.Name = "cmbStockName";
            this.cmbStockName.Size = new System.Drawing.Size(232, 23);
            this.cmbStockName.TabIndex = 71;
            this.cmbStockName.SelectedIndexChanged += new System.EventHandler(this.cmbStockName_SelectedIndexChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("宋体", 12F);
            this.btnDelete.Image = global::DesktopApp.Properties.Resources.delete;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(762, 95);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.TabIndex = 73;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmInWorkShopList
            // 
            this.ClientSize = new System.Drawing.Size(907, 498);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.cmbWorkshopName);
            this.Controls.Add(this.cmbStockName);
            this.Controls.Add(this.lblTS);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtOrderDate);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtProceName);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cmbProce);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtPc);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtRecordName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbRecord);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbWorkShop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbStock);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comOrderNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmInWorkShopList";
            this.Text = "车间入库到线边仓";
            this.Load += new System.EventHandler(this.frmInWorkShop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbStock;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comOrderNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbWorkShop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbRecord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtRecordName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPc;
        private System.Windows.Forms.ComboBox cmbProce;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtProceName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 生产订单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 车间;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkShopName;
        private System.Windows.Forms.DataGridViewTextBoxColumn 线边仓;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockName;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoodsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn 批次;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工艺代码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 条码;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn remark;
        private System.Windows.Forms.Label lblTS;
        private System.Windows.Forms.TextBox txtOrderDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbWorkshopName;
        private System.Windows.Forms.ComboBox cmbStockName;
        private System.Windows.Forms.Button btnDelete;
    }
}