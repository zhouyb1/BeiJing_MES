namespace DesktopApp
{
    partial class frmWorkShopWeightList
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
            this.cmbGoodsCode = new System.Windows.Forms.ComboBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtBatch = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.生产订单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.车间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.工艺代码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.批次 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.条码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbGoodsCode
            // 
            this.cmbGoodsCode.FormattingEnabled = true;
            this.cmbGoodsCode.Location = new System.Drawing.Point(153, 16);
            this.cmbGoodsCode.Name = "cmbGoodsCode";
            this.cmbGoodsCode.Size = new System.Drawing.Size(121, 20);
            this.cmbGoodsCode.TabIndex = 123;
            this.cmbGoodsCode.SelectedIndexChanged += new System.EventHandler(this.cmbGoodsCode_SelectedIndexChanged);
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(153, 122);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(121, 21);
            this.txtQty.TabIndex = 122;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(47, 125);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(101, 12);
            this.label15.TabIndex = 121;
            this.label15.Text = "转换后物料数量：";
            // 
            // txtBatch
            // 
            this.txtBatch.Location = new System.Drawing.Point(153, 40);
            this.txtBatch.Name = "txtBatch";
            this.txtBatch.Size = new System.Drawing.Size(121, 21);
            this.txtBatch.TabIndex = 120;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(47, 44);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 12);
            this.label12.TabIndex = 119;
            this.label12.Text = "转换后物料批次：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(153, 70);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(121, 21);
            this.txtName.TabIndex = 118;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(47, 78);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 12);
            this.label9.TabIndex = 117;
            this.label9.Text = "转换后物料名称：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(47, 17);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 12);
            this.label8.TabIndex = 116;
            this.label8.Text = "转换后物料编码：";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(328, 122);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 124;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.生产订单号,
            this.车间,
            this.工艺代码,
            this.物料,
            this.批次,
            this.数量,
            this.条码});
            this.dataGridView1.Location = new System.Drawing.Point(12, 156);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(508, 175);
            this.dataGridView1.TabIndex = 125;
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
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(153, 95);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.Size = new System.Drawing.Size(121, 21);
            this.txtUnit.TabIndex = 127;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(47, 103);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 126;
            this.label1.Text = "转换后物料单位：";
            // 
            // frmWorkShopWeightList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 343);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbGoodsCode);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtBatch);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmWorkShopWeightList";
            this.Text = "车间称重";
            this.Load += new System.EventHandler(this.frmWorkShopWeightList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbGoodsCode;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtBatch;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 生产订单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 车间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 工艺代码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料;
        private System.Windows.Forms.DataGridViewTextBoxColumn 批次;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 条码;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label1;
    }
}