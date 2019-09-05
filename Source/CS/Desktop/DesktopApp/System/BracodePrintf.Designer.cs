namespace DesktopApp
{
    partial class BracodePrintf
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comGoods = new System.Windows.Forms.ComboBox();
            this.btn_Weight = new System.Windows.Forms.Button();
            this.comBasketType = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnWeigh = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtBatch = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtKind = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.cmbSupply = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBasketQty = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(541, 171);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(108, 16);
            this.checkBox1.TabIndex = 1042;
            this.checkBox1.Text = "是否减容器重量";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comGoods
            // 
            this.comGoods.FormattingEnabled = true;
            this.comGoods.Location = new System.Drawing.Point(105, 50);
            this.comGoods.Name = "comGoods";
            this.comGoods.Size = new System.Drawing.Size(285, 20);
            this.comGoods.TabIndex = 1041;
            this.comGoods.SelectedIndexChanged += new System.EventHandler(this.comGoods_SelectedIndexChanged);
            // 
            // btn_Weight
            // 
            this.btn_Weight.Image = global::DesktopApp.Properties.Resources.ok;
            this.btn_Weight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Weight.Location = new System.Drawing.Point(123, 132);
            this.btn_Weight.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Weight.Name = "btn_Weight";
            this.btn_Weight.Size = new System.Drawing.Size(61, 26);
            this.btn_Weight.TabIndex = 1040;
            this.btn_Weight.Text = "获重";
            this.btn_Weight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_Weight.UseVisualStyleBackColor = true;
            this.btn_Weight.Click += new System.EventHandler(this.btn_Weight_Click);
            // 
            // comBasketType
            // 
            this.comBasketType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comBasketType.FormattingEnabled = true;
            this.comBasketType.Location = new System.Drawing.Point(596, 132);
            this.comBasketType.Margin = new System.Windows.Forms.Padding(2);
            this.comBasketType.Name = "comBasketType";
            this.comBasketType.Size = new System.Drawing.Size(62, 20);
            this.comBasketType.TabIndex = 1039;
            this.comBasketType.SelectedIndexChanged += new System.EventHandler(this.comBasketType_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(530, 133);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 1038;
            this.label9.Text = "容器类型：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1026;
            this.label2.Text = "物料编码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(405, 94);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 1031;
            this.label4.Text = "数量：";
            // 
            // btnWeigh
            // 
            this.btnWeigh.Image = global::DesktopApp.Properties.Resources.ok;
            this.btnWeigh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnWeigh.Location = new System.Drawing.Point(40, 132);
            this.btnWeigh.Margin = new System.Windows.Forms.Padding(2);
            this.btnWeigh.Name = "btnWeigh";
            this.btnWeigh.Size = new System.Drawing.Size(61, 26);
            this.btnWeigh.TabIndex = 1037;
            this.btnWeigh.Text = "打印";
            this.btnWeigh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnWeigh.UseVisualStyleBackColor = true;
            this.btnWeigh.Click += new System.EventHandler(this.btnWeigh_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(228, 90);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 1029;
            this.label6.Text = "入库单价：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(530, 56);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 1030;
            this.label8.Text = "物料类型：";
            // 
            // txtUnit
            // 
            this.txtUnit.Location = new System.Drawing.Point(448, 50);
            this.txtUnit.Margin = new System.Windows.Forms.Padding(2);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.ReadOnly = true;
            this.txtUnit.Size = new System.Drawing.Size(70, 21);
            this.txtUnit.TabIndex = 1032;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(57, 94);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 1028;
            this.label7.Text = "批次：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(405, 54);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 1027;
            this.label5.Text = "单位：";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(448, 87);
            this.txtQty.Margin = new System.Windows.Forms.Padding(2);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(70, 21);
            this.txtQty.TabIndex = 1033;
            // 
            // txtBatch
            // 
            this.txtBatch.Location = new System.Drawing.Point(103, 87);
            this.txtBatch.Margin = new System.Windows.Forms.Padding(2);
            this.txtBatch.Name = "txtBatch";
            this.txtBatch.Size = new System.Drawing.Size(91, 21);
            this.txtBatch.TabIndex = 1034;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(294, 88);
            this.txtPrice.Margin = new System.Windows.Forms.Padding(2);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(96, 21);
            this.txtPrice.TabIndex = 1035;
            // 
            // txtKind
            // 
            this.txtKind.Location = new System.Drawing.Point(596, 53);
            this.txtKind.Margin = new System.Windows.Forms.Padding(2);
            this.txtKind.Name = "txtKind";
            this.txtKind.ReadOnly = true;
            this.txtKind.Size = new System.Drawing.Size(62, 21);
            this.txtKind.TabIndex = 1036;
            // 
            // cmbSupply
            // 
            this.cmbSupply.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupply.FormattingEnabled = true;
            this.cmbSupply.Location = new System.Drawing.Point(448, 131);
            this.cmbSupply.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSupply.Name = "cmbSupply";
            this.cmbSupply.Size = new System.Drawing.Size(70, 20);
            this.cmbSupply.TabIndex = 1046;
            this.cmbSupply.SelectedIndexChanged += new System.EventHandler(this.cmbSupply_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(398, 134);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 1045;
            this.label10.Text = "供应商：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(530, 93);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1043;
            this.label3.Text = "容器重量：";
            // 
            // txtBasketQty
            // 
            this.txtBasketQty.Location = new System.Drawing.Point(596, 90);
            this.txtBasketQty.Margin = new System.Windows.Forms.Padding(2);
            this.txtBasketQty.Name = "txtBasketQty";
            this.txtBasketQty.Size = new System.Drawing.Size(62, 21);
            this.txtBasketQty.TabIndex = 1044;
            // 
            // BracodePrintf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 242);
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
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnWeigh);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.txtBatch);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtKind);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "BracodePrintf";
            this.Text = "条码补打";
            this.Load += new System.EventHandler(this.BracodePrintf_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comGoods;
        private System.Windows.Forms.Button btn_Weight;
        private System.Windows.Forms.ComboBox comBasketType;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnWeigh;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtBatch;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtKind;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox cmbSupply;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBasketQty;
    }
}