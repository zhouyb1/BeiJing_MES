namespace DesktopApp
{
    partial class frmResolve
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

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.TextBox txtUnit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtBatch;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtResolveQty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnResolve;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.txtUnit = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtBatch = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtResolveQty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnResolve = new System.Windows.Forms.Button();
            this.lblTS = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(68, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "标签:";
            // 
            // txtBarcode
            // 
            this.txtBarcode.Font = new System.Drawing.Font("宋体", 11F);
            this.txtBarcode.Location = new System.Drawing.Point(119, 40);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.ReadOnly = true;
            this.txtBarcode.Size = new System.Drawing.Size(244, 24);
            this.txtBarcode.TabIndex = 1;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBarcode_KeyDown);
            // 
            // txtUnit
            // 
            this.txtUnit.Font = new System.Drawing.Font("宋体", 11F);
            this.txtUnit.Location = new System.Drawing.Point(119, 161);
            this.txtUnit.Name = "txtUnit";
            this.txtUnit.ReadOnly = true;
            this.txtUnit.Size = new System.Drawing.Size(244, 24);
            this.txtUnit.TabIndex = 136;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(43, 166);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 135;
            this.label2.Text = "物料单位：";
            // 
            // txtQty
            // 
            this.txtQty.Font = new System.Drawing.Font("宋体", 11F);
            this.txtQty.Location = new System.Drawing.Point(119, 187);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(244, 24);
            this.txtQty.TabIndex = 134;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 11F);
            this.label15.Location = new System.Drawing.Point(43, 193);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 15);
            this.label15.TabIndex = 133;
            this.label15.Text = "物料数量：";
            // 
            // txtBatch
            // 
            this.txtBatch.Font = new System.Drawing.Font("宋体", 11F);
            this.txtBatch.Location = new System.Drawing.Point(119, 109);
            this.txtBatch.Name = "txtBatch";
            this.txtBatch.ReadOnly = true;
            this.txtBatch.Size = new System.Drawing.Size(244, 24);
            this.txtBatch.TabIndex = 132;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 11F);
            this.label12.Location = new System.Drawing.Point(43, 112);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 15);
            this.label12.TabIndex = 131;
            this.label12.Text = "物料批次：";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("宋体", 11F);
            this.txtName.Location = new System.Drawing.Point(119, 135);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(244, 24);
            this.txtName.TabIndex = 130;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 11F);
            this.label9.Location = new System.Drawing.Point(43, 139);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 15);
            this.label9.TabIndex = 129;
            this.label9.Text = "物料名称：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 11F);
            this.label8.Location = new System.Drawing.Point(43, 85);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 15);
            this.label8.TabIndex = 128;
            this.label8.Text = "物料编码：";
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("宋体", 11F);
            this.txtCode.Location = new System.Drawing.Point(119, 82);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(244, 24);
            this.txtCode.TabIndex = 137;
            // 
            // txtResolveQty
            // 
            this.txtResolveQty.Font = new System.Drawing.Font("宋体", 11F);
            this.txtResolveQty.Location = new System.Drawing.Point(119, 217);
            this.txtResolveQty.Name = "txtResolveQty";
            this.txtResolveQty.Size = new System.Drawing.Size(244, 24);
            this.txtResolveQty.TabIndex = 139;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 11F);
            this.label3.Location = new System.Drawing.Point(43, 223);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 138;
            this.label3.Text = "分写数量：";
            // 
            // btnResolve
            // 
            this.btnResolve.Font = new System.Drawing.Font("宋体", 11F);
            this.btnResolve.Image = global::DesktopApp.Properties.Resources.discuss;
            this.btnResolve.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResolve.Location = new System.Drawing.Point(273, 257);
            this.btnResolve.Name = "btnResolve";
            this.btnResolve.Size = new System.Drawing.Size(90, 30);
            this.btnResolve.TabIndex = 140;
            this.btnResolve.Text = "分写";
            this.btnResolve.UseVisualStyleBackColor = true;
            this.btnResolve.Click += new System.EventHandler(this.btnResolve_Click);
            // 
            // lblTS
            // 
            this.lblTS.AutoSize = true;
            this.lblTS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTS.ForeColor = System.Drawing.Color.Red;
            this.lblTS.Location = new System.Drawing.Point(58, 305);
            this.lblTS.Name = "lblTS";
            this.lblTS.Size = new System.Drawing.Size(0, 16);
            this.lblTS.TabIndex = 142;
            // 
            // frmResolve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 342);
            this.Controls.Add(this.lblTS);
            this.Controls.Add(this.btnResolve);
            this.Controls.Add(this.txtResolveQty);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtUnit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtBatch);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtBarcode);
            this.Controls.Add(this.label1);
            this.Name = "frmResolve";
            this.Text = "ESL标签拆分";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Label lblTS;

    }
}