﻿namespace DesktopApp
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
            this.txtWorkShopName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtStockName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtRecordName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.生产订单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.车间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.线边仓 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工艺代码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料编码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料批次 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料价格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.二维码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbStock
            // 
            this.cmbStock.FormattingEnabled = true;
            this.cmbStock.Location = new System.Drawing.Point(106, 59);
            this.cmbStock.Name = "cmbStock";
            this.cmbStock.Size = new System.Drawing.Size(121, 20);
            this.cmbStock.TabIndex = 17;
            this.cmbStock.SelectedIndexChanged += new System.EventHandler(this.cmbStock_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(50, 62);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "线边仓：";
            // 
            // comOrderNo
            // 
            this.comOrderNo.FormattingEnabled = true;
            this.comOrderNo.Location = new System.Drawing.Point(106, 7);
            this.comOrderNo.Name = "comOrderNo";
            this.comOrderNo.Size = new System.Drawing.Size(121, 20);
            this.comOrderNo.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "生产订单号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 141);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "物料编码：";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(545, 138);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbWorkShop
            // 
            this.cmbWorkShop.FormattingEnabled = true;
            this.cmbWorkShop.Location = new System.Drawing.Point(106, 33);
            this.cmbWorkShop.Name = "cmbWorkShop";
            this.cmbWorkShop.Size = new System.Drawing.Size(121, 20);
            this.cmbWorkShop.TabIndex = 20;
            this.cmbWorkShop.SelectedIndexChanged += new System.EventHandler(this.cmbWorkShop_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "车间：";
            // 
            // cmbRecord
            // 
            this.cmbRecord.FormattingEnabled = true;
            this.cmbRecord.Location = new System.Drawing.Point(106, 83);
            this.cmbRecord.Name = "cmbRecord";
            this.cmbRecord.Size = new System.Drawing.Size(121, 20);
            this.cmbRecord.TabIndex = 23;
            this.cmbRecord.SelectedIndexChanged += new System.EventHandler(this.cmbRecord_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "工艺代码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(62, 184);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 24;
            this.label4.Text = "价格：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(318, 183);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 25;
            this.label5.Text = "数量：";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(106, 180);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(121, 21);
            this.txtPrice.TabIndex = 26;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(388, 180);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(121, 21);
            this.txtQty.TabIndex = 27;
            // 
            // txtWorkShopName
            // 
            this.txtWorkShopName.Location = new System.Drawing.Point(388, 28);
            this.txtWorkShopName.Name = "txtWorkShopName";
            this.txtWorkShopName.Size = new System.Drawing.Size(121, 21);
            this.txtWorkShopName.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(318, 31);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "车间名称：";
            // 
            // txtStockName
            // 
            this.txtStockName.Location = new System.Drawing.Point(388, 55);
            this.txtStockName.Name = "txtStockName";
            this.txtStockName.Size = new System.Drawing.Size(121, 21);
            this.txtStockName.TabIndex = 31;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(318, 58);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 30;
            this.label9.Text = "仓库名称：";
            // 
            // txtRecordName
            // 
            this.txtRecordName.Location = new System.Drawing.Point(388, 82);
            this.txtRecordName.Name = "txtRecordName";
            this.txtRecordName.Size = new System.Drawing.Size(121, 21);
            this.txtRecordName.TabIndex = 33;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(318, 85);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 32;
            this.label10.Text = "工艺名称：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(388, 160);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(121, 21);
            this.txtName.TabIndex = 35;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(318, 163);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 34;
            this.label11.Text = "物料名称：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.生产订单号,
            this.车间,
            this.线边仓,
            this.工艺代码,
            this.物料编码,
            this.物料批次,
            this.物料价格,
            this.物料数量,
            this.二维码});
            this.dataGridView1.Location = new System.Drawing.Point(31, 213);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(759, 211);
            this.dataGridView1.TabIndex = 36;
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
            // 
            // 线边仓
            // 
            this.线边仓.DataPropertyName = "I_StockCode";
            this.线边仓.HeaderText = "线边仓";
            this.线边仓.Name = "线边仓";
            // 
            // 工艺代码
            // 
            this.工艺代码.DataPropertyName = "I_Record";
            this.工艺代码.HeaderText = "工艺代码";
            this.工艺代码.Name = "工艺代码";
            // 
            // 物料编码
            // 
            this.物料编码.DataPropertyName = "I_GoodsCode";
            this.物料编码.HeaderText = "物料编码";
            this.物料编码.Name = "物料编码";
            // 
            // 物料批次
            // 
            this.物料批次.DataPropertyName = "I_Batch";
            this.物料批次.HeaderText = "物料批次";
            this.物料批次.Name = "物料批次";
            // 
            // 物料价格
            // 
            this.物料价格.DataPropertyName = "I_Price";
            this.物料价格.HeaderText = "物料价格";
            this.物料价格.Name = "物料价格";
            // 
            // 物料数量
            // 
            this.物料数量.DataPropertyName = "I_Qty";
            this.物料数量.HeaderText = "物料数量";
            this.物料数量.Name = "物料数量";
            // 
            // 二维码
            // 
            this.二维码.DataPropertyName = "I_Barcode";
            this.二维码.HeaderText = "二维码";
            this.二维码.Name = "二维码";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(545, 176);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 39;
            this.btnUpload.Text = "完工";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(545, 97);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 40;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(60, 163);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 41;
            this.label13.Text = "批次：";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(388, 138);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(121, 21);
            this.txtCode.TabIndex = 43;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(106, 138);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(121, 21);
            this.txtBarcode.TabIndex = 45;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(39, 141);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 44;
            this.label14.Text = "二维码：";
            // 
            // txtPc
            // 
            this.txtPc.Location = new System.Drawing.Point(106, 160);
            this.txtPc.Name = "txtPc";
            this.txtPc.Size = new System.Drawing.Size(121, 21);
            this.txtPc.TabIndex = 46;
            // 
            // cmbProce
            // 
            this.cmbProce.FormattingEnabled = true;
            this.cmbProce.Location = new System.Drawing.Point(106, 109);
            this.cmbProce.Name = "cmbProce";
            this.cmbProce.Size = new System.Drawing.Size(121, 20);
            this.cmbProce.TabIndex = 48;
            this.cmbProce.SelectedIndexChanged += new System.EventHandler(this.cmbProce_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(48, 112);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 47;
            this.label12.Text = "工序号：";
            // 
            // txtProceName
            // 
            this.txtProceName.Location = new System.Drawing.Point(388, 112);
            this.txtProceName.Name = "txtProceName";
            this.txtProceName.Size = new System.Drawing.Size(121, 21);
            this.txtProceName.TabIndex = 50;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(318, 115);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 49;
            this.label15.Text = "工序名称：";
            // 
            // frmInWorkShopList
            // 
            this.ClientSize = new System.Drawing.Size(848, 472);
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
        private System.Windows.Forms.TextBox txtWorkShopName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtStockName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtRecordName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtPc;
        private System.Windows.Forms.DataGridViewTextBoxColumn 生产订单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 车间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 线边仓;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工艺代码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料编码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料批次;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料价格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 二维码;
        private System.Windows.Forms.ComboBox cmbProce;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtProceName;
        private System.Windows.Forms.Label label15;
    }
}