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
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_Search = new System.Windows.Forms.Button();
            this.btn_upload = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.生产订单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.车间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.线边仓 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工艺代码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.批次 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.条码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtRecordName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtStockName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtWorkShopName = new System.Windows.Forms.TextBox();
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
            this.txtPc = new System.Windows.Forms.TextBox();
            this.cmbProce = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProceName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(106, 134);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(403, 21);
            this.txtBarcode.TabIndex = 77;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox8_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(60, 137);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 76;
            this.label14.Text = "条码：";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(107, 157);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(121, 21);
            this.txtCode.TabIndex = 75;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(60, 187);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 73;
            this.label13.Text = "批次：";
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(548, 22);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(75, 23);
            this.btn_Search.TabIndex = 72;
            this.btn_Search.Text = "查询";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_upload
            // 
            this.btn_upload.Location = new System.Drawing.Point(548, 102);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.Size = new System.Drawing.Size(75, 23);
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
            this.线边仓,
            this.工艺代码,
            this.物料,
            this.批次,
            this.数量,
            this.条码});
            this.dataGridView1.Location = new System.Drawing.Point(31, 260);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(737, 184);
            this.dataGridView1.TabIndex = 68;
            // 
            // 生产订单号
            // 
            this.生产订单号.HeaderText = "生产订单号";
            this.生产订单号.Name = "生产订单号";
            // 
            // 车间
            // 
            this.车间.HeaderText = "车间";
            this.车间.Name = "车间";
            // 
            // 线边仓
            // 
            this.线边仓.HeaderText = "线边仓";
            this.线边仓.Name = "线边仓";
            // 
            // 工艺代码
            // 
            this.工艺代码.HeaderText = "工艺代码";
            this.工艺代码.Name = "工艺代码";
            // 
            // 物料
            // 
            this.物料.HeaderText = "物料";
            this.物料.Name = "物料";
            // 
            // 批次
            // 
            this.批次.HeaderText = "批次";
            this.批次.Name = "批次";
            // 
            // 数量
            // 
            this.数量.HeaderText = "数量";
            this.数量.Name = "数量";
            // 
            // 条码
            // 
            this.条码.HeaderText = "条码";
            this.条码.Name = "条码";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(388, 160);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(121, 21);
            this.txtName.TabIndex = 67;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(318, 163);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 66;
            this.label11.Text = "物料名称：";
            // 
            // txtRecordName
            // 
            this.txtRecordName.Location = new System.Drawing.Point(388, 84);
            this.txtRecordName.Name = "txtRecordName";
            this.txtRecordName.Size = new System.Drawing.Size(121, 21);
            this.txtRecordName.TabIndex = 65;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(318, 87);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 64;
            this.label10.Text = "工艺名称：";
            // 
            // txtStockName
            // 
            this.txtStockName.Location = new System.Drawing.Point(388, 57);
            this.txtStockName.Name = "txtStockName";
            this.txtStockName.Size = new System.Drawing.Size(121, 21);
            this.txtStockName.TabIndex = 63;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(318, 60);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 62;
            this.label9.Text = "仓库名称：";
            // 
            // txtWorkShopName
            // 
            this.txtWorkShopName.Location = new System.Drawing.Point(388, 30);
            this.txtWorkShopName.Name = "txtWorkShopName";
            this.txtWorkShopName.Size = new System.Drawing.Size(121, 21);
            this.txtWorkShopName.TabIndex = 61;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(318, 33);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 60;
            this.label7.Text = "车间名称：";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(388, 187);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(121, 21);
            this.txtQty.TabIndex = 59;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(318, 190);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 57;
            this.label5.Text = "数量：";
            // 
            // cmbRecord
            // 
            this.cmbRecord.FormattingEnabled = true;
            this.cmbRecord.Location = new System.Drawing.Point(106, 85);
            this.cmbRecord.Name = "cmbRecord";
            this.cmbRecord.Size = new System.Drawing.Size(121, 20);
            this.cmbRecord.TabIndex = 55;
            this.cmbRecord.SelectedIndexChanged += new System.EventHandler(this.cmbRecord_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 88);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 54;
            this.label3.Text = "工艺代码：";
            // 
            // cmbWorkShop
            // 
            this.cmbWorkShop.FormattingEnabled = true;
            this.cmbWorkShop.Location = new System.Drawing.Point(106, 35);
            this.cmbWorkShop.Name = "cmbWorkShop";
            this.cmbWorkShop.Size = new System.Drawing.Size(121, 20);
            this.cmbWorkShop.TabIndex = 53;
            this.cmbWorkShop.SelectedIndexChanged += new System.EventHandler(this.cmbWorkShop_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 52;
            this.label1.Text = "车间：";
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(548, 64);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 51;
            this.btn_save.Text = "保存";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // cmbStock
            // 
            this.cmbStock.FormattingEnabled = true;
            this.cmbStock.Location = new System.Drawing.Point(106, 61);
            this.cmbStock.Name = "cmbStock";
            this.cmbStock.Size = new System.Drawing.Size(121, 20);
            this.cmbStock.TabIndex = 50;
            this.cmbStock.SelectedIndexChanged += new System.EventHandler(this.cmbStock_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(50, 64);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 49;
            this.label8.Text = "线边仓：";
            // 
            // comOrderNo
            // 
            this.comOrderNo.FormattingEnabled = true;
            this.comOrderNo.Location = new System.Drawing.Point(106, 9);
            this.comOrderNo.Name = "comOrderNo";
            this.comOrderNo.Size = new System.Drawing.Size(121, 20);
            this.comOrderNo.TabIndex = 48;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 12);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 46;
            this.label6.Text = "生产订单号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 160);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 47;
            this.label2.Text = "物料编码：";
            // 
            // txtPc
            // 
            this.txtPc.Location = new System.Drawing.Point(106, 184);
            this.txtPc.Name = "txtPc";
            this.txtPc.Size = new System.Drawing.Size(121, 21);
            this.txtPc.TabIndex = 78;
            // 
            // cmbProce
            // 
            this.cmbProce.FormattingEnabled = true;
            this.cmbProce.Location = new System.Drawing.Point(106, 108);
            this.cmbProce.Name = "cmbProce";
            this.cmbProce.Size = new System.Drawing.Size(121, 20);
            this.cmbProce.TabIndex = 80;
            this.cmbProce.SelectedIndexChanged += new System.EventHandler(this.cmbProce_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 111);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 79;
            this.label4.Text = "最后工序：";
            // 
            // txtProceName
            // 
            this.txtProceName.Location = new System.Drawing.Point(388, 111);
            this.txtProceName.Name = "txtProceName";
            this.txtProceName.Size = new System.Drawing.Size(121, 21);
            this.txtProceName.TabIndex = 82;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(321, 114);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 81;
            this.label12.Text = "工序名称：";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(106, 211);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(121, 21);
            this.txtPrice.TabIndex = 84;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(60, 214);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 83;
            this.label15.Text = "价格：";
            // 
            // frmOutWorkShopList
            // 
            this.ClientSize = new System.Drawing.Size(826, 456);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtProceName);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmbProce);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPc);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.btn_upload);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtRecordName);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtStockName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtWorkShopName);
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
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Button btn_upload;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtRecordName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtStockName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWorkShopName;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn 生产订单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 车间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 线边仓;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工艺代码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料;
        private System.Windows.Forms.DataGridViewTextBoxColumn 批次;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 条码;
        private System.Windows.Forms.TextBox txtPc;
        private System.Windows.Forms.ComboBox cmbProce;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProceName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label label15;
    }
}