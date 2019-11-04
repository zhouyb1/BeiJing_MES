namespace DesktopApp
{
    partial class frmOutWorkShopList
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
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_Search = new System.Windows.Forms.Button();
            this.btn_upload = new System.Windows.Forms.Button();
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRecordName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbRecord = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbWorkShop = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_save = new System.Windows.Forms.Button();
            this.cmbStock = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comOrderNo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbProce = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProceName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.txtOrderDate = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cmbGoodsCode = new System.Windows.Forms.ComboBox();
            this.cmbPc = new System.Windows.Forms.ComboBox();
            this.cmbSupplyName = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.lblTS = new System.Windows.Forms.Label();
            this.cmbWorkshopName = new System.Windows.Forms.ComboBox();
            this.cmbStockName = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("宋体", 11F);
            this.txtBarcode.Location = new System.Drawing.Point(106, 107);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(534, 24);
            this.txtBarcode.TabIndex = 77;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox8_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 11F);
            this.label14.Location = new System.Drawing.Point(50, 110);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 15);
            this.label14.TabIndex = 76;
            this.label14.Text = "条码：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 11F);
            this.label13.Location = new System.Drawing.Point(50, 160);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 15);
            this.label13.TabIndex = 73;
            this.label13.Text = "批次：";
            // 
            // btn_Search
            // 
            this.btn_Search.Font = new System.Drawing.Font("宋体", 11F);
            this.btn_Search.Image = global::DesktopApp.Properties.Resources.search1;
            this.btn_Search.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_Search.Location = new System.Drawing.Point(713, 12);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(90, 30);
            this.btn_Search.TabIndex = 72;
            this.btn_Search.Text = "查询";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_upload
            // 
            this.btn_upload.Font = new System.Drawing.Font("宋体", 11F);
            this.btn_upload.Image = global::DesktopApp.Properties.Resources.ok;
            this.btn_upload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_upload.Location = new System.Drawing.Point(713, 87);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(90, 30);
            this.btn_upload.TabIndex = 71;
            this.btn_upload.Text = "完工";
            this.btn_upload.UseVisualStyleBackColor = true;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
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
            this.dataGridView1.Location = new System.Drawing.Point(23, 215);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(780, 223);
            this.dataGridView1.TabIndex = 68;
            // 
            // 生产订单号
            // 
            this.生产订单号.DataPropertyName = "O_OrderNo";
            this.生产订单号.HeaderText = "生产订单号";
            this.生产订单号.Name = "生产订单号";
            // 
            // 车间
            // 
            this.车间.DataPropertyName = "O_WorkShop";
            this.车间.HeaderText = "车间";
            this.车间.Name = "车间";
            this.车间.Width = 60;
            // 
            // WorkShopName
            // 
            this.WorkShopName.DataPropertyName = "O_WorkShopName";
            this.WorkShopName.HeaderText = "车间名称";
            this.WorkShopName.Name = "WorkShopName";
            // 
            // 线边仓
            // 
            this.线边仓.DataPropertyName = "O_StockCode";
            this.线边仓.HeaderText = "线边仓";
            this.线边仓.Name = "线边仓";
            // 
            // StockName
            // 
            this.StockName.DataPropertyName = "O_StockName";
            this.StockName.HeaderText = "线边仓名称";
            this.StockName.Name = "StockName";
            // 
            // 物料
            // 
            this.物料.DataPropertyName = "O_GoodsCode";
            this.物料.HeaderText = "物料";
            this.物料.Name = "物料";
            // 
            // GoodsName
            // 
            this.GoodsName.DataPropertyName = "O_GoodsName";
            this.GoodsName.HeaderText = "物料名";
            this.GoodsName.Name = "GoodsName";
            // 
            // 批次
            // 
            this.批次.DataPropertyName = "O_Batch";
            this.批次.HeaderText = "批次";
            this.批次.Name = "批次";
            // 
            // Price
            // 
            this.Price.DataPropertyName = "O_Price";
            this.Price.HeaderText = "价格";
            this.Price.Name = "Price";
            // 
            // unit
            // 
            this.unit.DataPropertyName = "O_Unit";
            this.unit.HeaderText = "单位";
            this.unit.Name = "unit";
            // 
            // status
            // 
            this.status.DataPropertyName = "O_Status";
            this.status.HeaderText = "状态";
            this.status.Name = "status";
            // 
            // CreateBy
            // 
            this.CreateBy.DataPropertyName = "O_CreateBy";
            this.CreateBy.HeaderText = "添加人";
            this.CreateBy.Name = "CreateBy";
            // 
            // CreateDate
            // 
            this.CreateDate.DataPropertyName = "O_CreateDate";
            this.CreateDate.HeaderText = "添加时间";
            this.CreateDate.Name = "CreateDate";
            // 
            // 工艺代码
            // 
            this.工艺代码.DataPropertyName = "O_Record";
            this.工艺代码.HeaderText = "工艺代码";
            this.工艺代码.Name = "工艺代码";
            // 
            // 数量
            // 
            this.数量.DataPropertyName = "O_Qty";
            this.数量.HeaderText = "数量";
            this.数量.Name = "数量";
            // 
            // 条码
            // 
            this.条码.DataPropertyName = "O_Barcode";
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
            this.remark.DataPropertyName = "O_Remark";
            this.remark.HeaderText = "备注";
            this.remark.Name = "remark";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 11F);
            this.txtName.Location = new System.Drawing.Point(404, 158);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(236, 24);
            this.txtName.TabIndex = 67;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 11F);
            this.label11.Location = new System.Drawing.Point(317, 161);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 15);
            this.label11.TabIndex = 66;
            this.label11.Text = "物料名称：";
            // 
            // txtRecordName
            // 
            this.txtRecordName.Location = new System.Drawing.Point(692, 113);
            this.txtRecordName.Name = "txtRecordName";
            this.txtRecordName.Size = new System.Drawing.Size(121, 21);
            this.txtRecordName.TabIndex = 65;
            this.txtRecordName.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(622, 115);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 64;
            this.label10.Text = "工艺名称：";
            this.label10.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 11F);
            this.label9.Location = new System.Drawing.Point(317, 60);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 62;
            this.label9.Text = "仓库编码：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 11F);
            this.label7.Location = new System.Drawing.Point(317, 33);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 60;
            this.label7.Text = "车间编码：";
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("宋体", 11F);
            this.txtQty.Location = new System.Drawing.Point(404, 185);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(236, 24);
            this.txtQty.TabIndex = 59;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 11F);
            this.label5.Location = new System.Drawing.Point(347, 188);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 57;
            this.label5.Text = "数量：";
            // 
            // cmbRecord
            // 
            this.cmbRecord.FormattingEnabled = true;
            this.cmbRecord.Location = new System.Drawing.Point(692, 184);
            this.cmbRecord.Name = "cmbRecord";
            this.cmbRecord.Size = new System.Drawing.Size(121, 20);
            this.cmbRecord.TabIndex = 55;
            this.cmbRecord.Visible = false;
            this.cmbRecord.SelectedIndexChanged += new System.EventHandler(this.cmbRecord_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(625, 187);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 54;
            this.label3.Text = "工艺代码：";
            this.label3.Visible = false;
            // 
            // cmbWorkShop
            // 
            this.cmbWorkShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkShop.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbWorkShop.FormattingEnabled = true;
            this.cmbWorkShop.Location = new System.Drawing.Point(404, 33);
            this.cmbWorkShop.Name = "cmbWorkShop";
            this.cmbWorkShop.Size = new System.Drawing.Size(236, 23);
            this.cmbWorkShop.TabIndex = 53;
            this.cmbWorkShop.SelectedIndexChanged += new System.EventHandler(this.cmbWorkShop_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(50, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 52;
            this.label1.Text = "车间：";
            // 
            // btn_save
            // 
            this.btn_save.Font = new System.Drawing.Font("宋体", 11F);
            this.btn_save.Image = global::DesktopApp.Properties.Resources.save_disabled;
            this.btn_save.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btn_save.Location = new System.Drawing.Point(713, 49);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(90, 30);
            this.btn_save.TabIndex = 51;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // cmbStock
            // 
            this.cmbStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStock.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbStock.FormattingEnabled = true;
            this.cmbStock.Location = new System.Drawing.Point(404, 59);
            this.cmbStock.Name = "cmbStock";
            this.cmbStock.Size = new System.Drawing.Size(236, 23);
            this.cmbStock.TabIndex = 50;
            this.cmbStock.SelectedIndexChanged += new System.EventHandler(this.cmbStock_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 11F);
            this.label8.Location = new System.Drawing.Point(35, 64);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 49;
            this.label8.Text = "日耗库：";
            // 
            // comOrderNo
            // 
            this.comOrderNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comOrderNo.Font = new System.Drawing.Font("宋体", 11F);
            this.comOrderNo.FormattingEnabled = true;
            this.comOrderNo.Location = new System.Drawing.Point(106, 9);
            this.comOrderNo.Name = "comOrderNo";
            this.comOrderNo.Size = new System.Drawing.Size(194, 23);
            this.comOrderNo.TabIndex = 48;
            this.comOrderNo.SelectedIndexChanged += new System.EventHandler(this.comOrderNo_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 11F);
            this.label6.Location = new System.Drawing.Point(5, 12);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(97, 15);
            this.label6.TabIndex = 46;
            this.label6.Text = "生产订单号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(20, 133);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 47;
            this.label2.Text = "物料编码：";
            // 
            // cmbProce
            // 
            this.cmbProce.FormattingEnabled = true;
            this.cmbProce.Location = new System.Drawing.Point(692, 207);
            this.cmbProce.Name = "cmbProce";
            this.cmbProce.Size = new System.Drawing.Size(121, 20);
            this.cmbProce.TabIndex = 80;
            this.cmbProce.Visible = false;
            this.cmbProce.SelectedIndexChanged += new System.EventHandler(this.cmbProce_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(625, 210);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 79;
            this.label4.Text = "最后工序：";
            this.label4.Visible = false;
            // 
            // txtProceName
            // 
            this.txtProceName.Location = new System.Drawing.Point(692, 139);
            this.txtProceName.Name = "txtProceName";
            this.txtProceName.Size = new System.Drawing.Size(121, 21);
            this.txtProceName.TabIndex = 82;
            this.txtProceName.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(625, 142);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 81;
            this.label12.Text = "工序名称：";
            this.label12.Visible = false;
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("宋体", 11F);
            this.txtPrice.Location = new System.Drawing.Point(106, 184);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.ReadOnly = true;
            this.txtPrice.Size = new System.Drawing.Size(194, 24);
            this.txtPrice.TabIndex = 84;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 11F);
            this.label15.Location = new System.Drawing.Point(50, 187);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 15);
            this.label15.TabIndex = 83;
            this.label15.Text = "价格：";
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM4";
            // 
            // txtOrderDate
            // 
            this.txtOrderDate.Font = new System.Drawing.Font("宋体", 11F);
            this.txtOrderDate.Location = new System.Drawing.Point(404, 8);
            this.txtOrderDate.Name = "txtOrderDate";
            this.txtOrderDate.Size = new System.Drawing.Size(236, 24);
            this.txtOrderDate.TabIndex = 86;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 11F);
            this.label16.Location = new System.Drawing.Point(317, 11);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 15);
            this.label16.TabIndex = 85;
            this.label16.Text = "订单时间：";
            // 
            // cmbGoodsCode
            // 
            this.cmbGoodsCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGoodsCode.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbGoodsCode.FormattingEnabled = true;
            this.cmbGoodsCode.Location = new System.Drawing.Point(106, 130);
            this.cmbGoodsCode.Name = "cmbGoodsCode";
            this.cmbGoodsCode.Size = new System.Drawing.Size(194, 23);
            this.cmbGoodsCode.TabIndex = 87;
            this.cmbGoodsCode.SelectedIndexChanged += new System.EventHandler(this.cmbGoodsCode_SelectedIndexChanged);
            // 
            // cmbPc
            // 
            this.cmbPc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPc.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbPc.FormattingEnabled = true;
            this.cmbPc.Location = new System.Drawing.Point(106, 158);
            this.cmbPc.Name = "cmbPc";
            this.cmbPc.Size = new System.Drawing.Size(194, 23);
            this.cmbPc.TabIndex = 88;
            // 
            // cmbSupplyName
            // 
            this.cmbSupplyName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplyName.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbSupplyName.FormattingEnabled = true;
            this.cmbSupplyName.Location = new System.Drawing.Point(404, 133);
            this.cmbSupplyName.Name = "cmbSupplyName";
            this.cmbSupplyName.Size = new System.Drawing.Size(236, 23);
            this.cmbSupplyName.TabIndex = 90;
            this.cmbSupplyName.Visible = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 11F);
            this.label17.Location = new System.Drawing.Point(302, 138);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(97, 15);
            this.label17.TabIndex = 89;
            this.label17.Text = "供应商名称：";
            this.label17.Visible = false;
            // 
            // lblTS
            // 
            this.lblTS.AutoSize = true;
            this.lblTS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTS.ForeColor = System.Drawing.Color.Red;
            this.lblTS.Location = new System.Drawing.Point(21, 450);
            this.lblTS.Name = "lblTS";
            this.lblTS.Size = new System.Drawing.Size(0, 16);
            this.lblTS.TabIndex = 91;
            // 
            // cmbWorkshopName
            // 
            this.cmbWorkshopName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkshopName.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbWorkshopName.FormattingEnabled = true;
            this.cmbWorkshopName.Location = new System.Drawing.Point(106, 33);
            this.cmbWorkshopName.Name = "cmbWorkshopName";
            this.cmbWorkshopName.Size = new System.Drawing.Size(194, 23);
            this.cmbWorkshopName.TabIndex = 93;
            this.cmbWorkshopName.SelectedIndexChanged += new System.EventHandler(this.cmbWorkshopName_SelectedIndexChanged);
            // 
            // cmbStockName
            // 
            this.cmbStockName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStockName.Font = new System.Drawing.Font("宋体", 11F);
            this.cmbStockName.FormattingEnabled = true;
            this.cmbStockName.Location = new System.Drawing.Point(106, 59);
            this.cmbStockName.Name = "cmbStockName";
            this.cmbStockName.Size = new System.Drawing.Size(194, 23);
            this.cmbStockName.TabIndex = 92;
            this.cmbStockName.SelectedIndexChanged += new System.EventHandler(this.cmbStockName_SelectedIndexChanged);
            // 
            // frmOutWorkShopList
            // 
            this.ClientSize = new System.Drawing.Size(888, 474);
            this.Controls.Add(this.cmbWorkshopName);
            this.Controls.Add(this.cmbStockName);
            this.Controls.Add(this.lblTS);
            this.Controls.Add(this.cmbSupplyName);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cmbPc);
            this.Controls.Add(this.cmbGoodsCode);
            this.Controls.Add(this.txtOrderDate);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtProceName);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbProce);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtRecordName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbRecord);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbWorkShop);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.cmbStock);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comOrderNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmOutWorkShopList";
            this.Text = "出库到车间";
            this.Load += new System.EventHandler(this.frmOutWorkShopList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRecordName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbRecord;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbWorkShop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.ComboBox cmbStock;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comOrderNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbProce;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProceName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label15;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox txtOrderDate;
        private System.Windows.Forms.Label label16;
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
        private System.Windows.Forms.ComboBox cmbGoodsCode;
        private System.Windows.Forms.ComboBox cmbPc;
        private System.Windows.Forms.ComboBox cmbSupplyName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblTS;
        private System.Windows.Forms.ComboBox cmbWorkshopName;
        private System.Windows.Forms.ComboBox cmbStockName;
    }
}