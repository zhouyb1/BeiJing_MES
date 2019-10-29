namespace DesktopApp
{
    partial class frmStorageEdit
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.入库单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.生产订单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料编码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.批次 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.价格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtMaterInNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnPeeling = new System.Windows.Forms.Button();
            this.btnWeigh = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtBatch = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtKind = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comBasketType = new System.Windows.Forms.ComboBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.btn_Weight = new System.Windows.Forms.Button();
            this.comGoods = new System.Windows.Forms.ComboBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBasketQty = new System.Windows.Forms.TextBox();
            this.cmbSupply = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.入库单号,
            this.生产订单号,
            this.物料编码,
            this.物料名称,
            this.物料类型,
            this.单位,
            this.数量,
            this.批次,
            this.备注,
            this.价格});
            this.dataGridView.Location = new System.Drawing.Point(9, 49);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowTemplate.Height = 27;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(916, 228);
            this.dataGridView.TabIndex = 60;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            // 
            // 入库单号
            // 
            this.入库单号.DataPropertyName = "M_MaterInNo";
            this.入库单号.HeaderText = "入库单号";
            this.入库单号.Name = "入库单号";
            this.入库单号.ReadOnly = true;
            // 
            // 生产订单号
            // 
            this.生产订单号.DataPropertyName = "M_OrderNo";
            this.生产订单号.HeaderText = "生产订单号";
            this.生产订单号.Name = "生产订单号";
            this.生产订单号.ReadOnly = true;
            // 
            // 物料编码
            // 
            this.物料编码.DataPropertyName = "M_GoodsCode";
            this.物料编码.HeaderText = "物料编码";
            this.物料编码.Name = "物料编码";
            this.物料编码.ReadOnly = true;
            // 
            // 物料名称
            // 
            this.物料名称.DataPropertyName = "M_GoodsName";
            this.物料名称.HeaderText = "物料名称";
            this.物料名称.Name = "物料名称";
            this.物料名称.ReadOnly = true;
            // 
            // 物料类型
            // 
            this.物料类型.DataPropertyName = "M_Kind";
            this.物料类型.HeaderText = "物料类型";
            this.物料类型.Name = "物料类型";
            this.物料类型.ReadOnly = true;
            // 
            // 单位
            // 
            this.单位.DataPropertyName = "M_Unit";
            this.单位.HeaderText = "单位";
            this.单位.Name = "单位";
            this.单位.ReadOnly = true;
            // 
            // 数量
            // 
            this.数量.DataPropertyName = "M_Qty";
            this.数量.HeaderText = "数量";
            this.数量.Name = "数量";
            this.数量.ReadOnly = true;
            // 
            // 批次
            // 
            this.批次.DataPropertyName = "M_Batch";
            this.批次.HeaderText = "批次";
            this.批次.Name = "批次";
            this.批次.ReadOnly = true;
            // 
            // 备注
            // 
            this.备注.DataPropertyName = "M_Remark";
            this.备注.HeaderText = "备注";
            this.备注.Name = "备注";
            this.备注.ReadOnly = true;
            // 
            // 价格
            // 
            this.价格.DataPropertyName = "M_Price";
            this.价格.HeaderText = "价格";
            this.价格.Name = "价格";
            this.价格.ReadOnly = true;
            // 
            // txtMaterInNo
            // 
            this.txtMaterInNo.Location = new System.Drawing.Point(83, 21);
            this.txtMaterInNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaterInNo.Name = "txtMaterInNo";
            this.txtMaterInNo.ReadOnly = true;
            this.txtMaterInNo.Size = new System.Drawing.Size(131, 21);
            this.txtMaterInNo.TabIndex = 1002;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1001;
            this.label1.Text = "入库单号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(15, 295);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 1003;
            this.label2.Text = "物料编码：";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("宋体", 12F);
            this.btnSave.Location = new System.Drawing.Point(395, 412);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 26);
            this.btnSave.TabIndex = 1018;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Visible = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F);
            this.label4.Location = new System.Drawing.Point(410, 336);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 1009;
            this.label4.Text = "数量：";
            // 
            // btnPeeling
            // 
            this.btnPeeling.Font = new System.Drawing.Font("宋体", 12F);
            this.btnPeeling.Location = new System.Drawing.Point(477, 412);
            this.btnPeeling.Margin = new System.Windows.Forms.Padding(2);
            this.btnPeeling.Name = "btnPeeling";
            this.btnPeeling.Size = new System.Drawing.Size(62, 26);
            this.btnPeeling.TabIndex = 1019;
            this.btnPeeling.Text = "去框称重";
            this.btnPeeling.UseVisualStyleBackColor = true;
            this.btnPeeling.Visible = false;
            this.btnPeeling.Click += new System.EventHandler(this.btnPeeling_Click);
            // 
            // btnWeigh
            // 
            this.btnWeigh.Font = new System.Drawing.Font("宋体", 12F);
            this.btnWeigh.Image = global::DesktopApp.Properties.Resources.ok;
            this.btnWeigh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWeigh.Location = new System.Drawing.Point(33, 395);
            this.btnWeigh.Margin = new System.Windows.Forms.Padding(2);
            this.btnWeigh.Name = "btnWeigh";
            this.btnWeigh.Size = new System.Drawing.Size(100, 40);
            this.btnWeigh.TabIndex = 1017;
            this.btnWeigh.Text = "称重保存";
            this.btnWeigh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnWeigh.UseVisualStyleBackColor = true;
            this.btnWeigh.Click += new System.EventHandler(this.btnWeigh_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F);
            this.label6.Location = new System.Drawing.Point(211, 331);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 1006;
            this.label6.Text = "入库单价：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F);
            this.label8.Location = new System.Drawing.Point(557, 298);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 16);
            this.label8.TabIndex = 1008;
            this.label8.Text = "物料类型：";
            // 
            // txtUnit
            // 
            this.txtUnit.Font = new System.Drawing.Font("宋体", 12F);
            this.txtUnit.Location = new System.Drawing.Point(470, 292);
            this.txtUnit.Margin = new System.Windows.Forms.Padding(2);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.ReadOnly = true;
            this.txtUnit.Size = new System.Drawing.Size(78, 26);
            this.txtUnit.TabIndex = 1011;
            this.txtUnit.TextChanged += new System.EventHandler(this.txtUnit_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F);
            this.label5.Location = new System.Drawing.Point(410, 296);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 1004;
            this.label5.Text = "单位：";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("宋体", 12F);
            this.txtQty.Location = new System.Drawing.Point(470, 329);
            this.txtQty.Margin = new System.Windows.Forms.Padding(2);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(78, 26);
            this.txtQty.TabIndex = 1013;
            // 
            // txtBatch
            // 
            this.txtBatch.Font = new System.Drawing.Font("宋体", 12F);
            this.txtBatch.Location = new System.Drawing.Point(108, 329);
            this.txtBatch.Margin = new System.Windows.Forms.Padding(2);
            this.txtBatch.Name = "txtBatch";
            this.txtBatch.Size = new System.Drawing.Size(91, 26);
            this.txtBatch.TabIndex = 1014;
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("宋体", 12F);
            this.txtPrice.Location = new System.Drawing.Point(305, 329);
            this.txtPrice.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.ReadOnly = true;
            this.txtPrice.Size = new System.Drawing.Size(76, 26);
            this.txtPrice.TabIndex = 1015;
            this.txtPrice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrice_KeyDown);
            // 
            // txtKind
            // 
            this.txtKind.Font = new System.Drawing.Font("宋体", 12F);
            this.txtKind.Location = new System.Drawing.Point(649, 295);
            this.txtKind.Margin = new System.Windows.Forms.Padding(2);
            this.txtKind.Name = "txtKind";
            this.txtKind.ReadOnly = true;
            this.txtKind.Size = new System.Drawing.Size(101, 26);
            this.txtKind.TabIndex = 1016;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F);
            this.label9.Location = new System.Drawing.Point(754, 298);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 16);
            this.label9.TabIndex = 1021;
            this.label9.Text = "容器类型：";
            // 
            // comBasketType
            // 
            this.comBasketType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comBasketType.Font = new System.Drawing.Font("宋体", 12F);
            this.comBasketType.FormattingEnabled = true;
            this.comBasketType.Location = new System.Drawing.Point(845, 297);
            this.comBasketType.Margin = new System.Windows.Forms.Padding(2);
            this.comBasketType.Name = "comBasketType";
            this.comBasketType.Size = new System.Drawing.Size(78, 24);
            this.comBasketType.TabIndex = 1022;
            this.comBasketType.SelectedIndexChanged += new System.EventHandler(this.comBasketType_SelectedIndexChanged);
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM4";
            // 
            // btn_Weight
            // 
            this.btn_Weight.Font = new System.Drawing.Font("宋体", 12F);
            this.btn_Weight.Image = global::DesktopApp.Properties.Resources.ok;
            this.btn_Weight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Weight.Location = new System.Drawing.Point(154, 395);
            this.btn_Weight.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Weight.Name = "btn_Weight";
            this.btn_Weight.Size = new System.Drawing.Size(100, 40);
            this.btn_Weight.TabIndex = 1023;
            this.btn_Weight.Text = "获重";
            this.btn_Weight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Weight.UseVisualStyleBackColor = true;
            this.btn_Weight.Click += new System.EventHandler(this.btn_Weight_Click);
            // 
            // comGoods
            // 
            this.comGoods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comGoods.Font = new System.Drawing.Font("宋体", 12F);
            this.comGoods.FormattingEnabled = true;
            this.comGoods.Location = new System.Drawing.Point(108, 292);
            this.comGoods.Name = "comGoods";
            this.comGoods.Size = new System.Drawing.Size(273, 24);
            this.comGoods.TabIndex = 1024;
            this.comGoods.SelectedIndexChanged += new System.EventHandler(this.comGoods_SelectedIndexChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.checkBox1.Location = new System.Drawing.Point(767, 378);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(139, 20);
            this.checkBox1.TabIndex = 1025;
            this.checkBox1.Text = "是否减容器重量";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(754, 336);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 1026;
            this.label3.Text = "容器重量：";
            // 
            // txtBasketQty
            // 
            this.txtBasketQty.Font = new System.Drawing.Font("宋体", 12F);
            this.txtBasketQty.Location = new System.Drawing.Point(845, 333);
            this.txtBasketQty.Margin = new System.Windows.Forms.Padding(2);
            this.txtBasketQty.Name = "txtBasketQty";
            this.txtBasketQty.Size = new System.Drawing.Size(78, 26);
            this.txtBasketQty.TabIndex = 1027;
            // 
            // cmbSupply
            // 
            this.cmbSupply.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupply.Font = new System.Drawing.Font("宋体", 12F);
            this.cmbSupply.FormattingEnabled = true;
            this.cmbSupply.Location = new System.Drawing.Point(649, 332);
            this.cmbSupply.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSupply.Name = "cmbSupply";
            this.cmbSupply.Size = new System.Drawing.Size(101, 24);
            this.cmbSupply.TabIndex = 1029;
            this.cmbSupply.SelectedIndexChanged += new System.EventHandler(this.cmbSupply_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F);
            this.label10.Location = new System.Drawing.Point(573, 335);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 16);
            this.label10.TabIndex = 1028;
            this.label10.Text = "供应商：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F);
            this.label11.Location = new System.Drawing.Point(15, 333);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 16);
            this.label11.TabIndex = 1030;
            this.label11.Text = "生产批次:";
            // 
            // frmStorageEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(936, 490);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmbSupply);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtBasketQty);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.comGoods);
            this.Controls.Add(this.btn_Weight);
            this.Controls.Add(this.comBasketType);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnPeeling);
            this.Controls.Add(this.btnWeigh);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtBatch);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtKind);
            this.Controls.Add(this.txtMaterInNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmStorageEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物料入库编辑";
            this.Load += new System.EventHandler(this.frmStorageEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox txtMaterInNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnPeeling;
        private System.Windows.Forms.Button btnWeigh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtBatch;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtKind;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comBasketType;
        private System.Windows.Forms.DataGridViewTextBoxColumn 入库单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 生产订单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料编码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单位;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 批次;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注;
        private System.Windows.Forms.DataGridViewTextBoxColumn 价格;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button btn_Weight;
        private System.Windows.Forms.ComboBox comGoods;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBasketQty;
        private System.Windows.Forms.ComboBox cmbSupply;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}