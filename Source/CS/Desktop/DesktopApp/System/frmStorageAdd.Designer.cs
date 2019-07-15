namespace DesktopApp
{
    partial class frmStorageAdd
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
            this.btnSave = new System.Windows.Forms.Button();
            this.txtCreateBy = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMaterInNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comStock = new System.Windows.Forms.ComboBox();
            this.comProductNo = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(193, 257);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 26);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtCreateBy
            // 
            this.txtCreateBy.Location = new System.Drawing.Point(115, 185);
            this.txtCreateBy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtCreateBy.Name = "txtCreateBy";
            this.txtCreateBy.ReadOnly = true;
            this.txtCreateBy.Size = new System.Drawing.Size(116, 21);
            this.txtCreateBy.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 187);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "添加人：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(38, 138);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "生产订单号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 87);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "仓库编码：";
            // 
            // txtMaterInNo
            // 
            this.txtMaterInNo.Location = new System.Drawing.Point(116, 26);
            this.txtMaterInNo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtMaterInNo.Name = "txtMaterInNo";
            this.txtMaterInNo.ReadOnly = true;
            this.txtMaterInNo.Size = new System.Drawing.Size(174, 21);
            this.txtMaterInNo.TabIndex = 1002;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1001;
            this.label1.Text = "入库单号：";
            // 
            // comStock
            // 
            this.comStock.FormattingEnabled = true;
            this.comStock.Location = new System.Drawing.Point(113, 84);
            this.comStock.Name = "comStock";
            this.comStock.Size = new System.Drawing.Size(177, 20);
            this.comStock.TabIndex = 1004;
            // 
            // comProductNo
            // 
            this.comProductNo.FormattingEnabled = true;
            this.comProductNo.Location = new System.Drawing.Point(113, 136);
            this.comProductNo.Name = "comProductNo";
            this.comProductNo.Size = new System.Drawing.Size(177, 20);
            this.comProductNo.TabIndex = 1005;
            // 
            // frmStorageAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 327);
            this.Controls.Add(this.comProductNo);
            this.Controls.Add(this.comStock);
            this.Controls.Add(this.txtMaterInNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtCreateBy);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmStorageAdd";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物料入库新增";
            this.Load += new System.EventHandler(this.frmStorageAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtCreateBy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMaterInNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comStock;
        private System.Windows.Forms.ComboBox comProductNo;
    }
}