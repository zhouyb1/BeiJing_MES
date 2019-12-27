namespace DesktopApp
{
    partial class frmBackStock
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.选择 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.物料 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.批次 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.实用数量 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.价格 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.车间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.仓库编码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.仓库名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBack = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWorkShop = new System.Windows.Forms.TextBox();
            this.txtWorkShopName = new System.Windows.Forms.TextBox();
            this.btn_Search = new System.Windows.Forms.Button();
            this.lblTS = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.选择,
            this.物料,
            this.物料名称,
            this.批次,
            this.数量,
            this.实用数量,
            this.价格,
            this.车间,
            this.状态,
            this.单位,
            this.ID,
            this.备注,
            this.单号,
            this.仓库编码,
            this.仓库名称});
            this.dataGridView1.Location = new System.Drawing.Point(12, 45);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(893, 334);
            this.dataGridView1.TabIndex = 98;
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
            // 物料名称
            // 
            this.物料名称.DataPropertyName = "W_GoodsName";
            this.物料名称.HeaderText = "物料名称";
            this.物料名称.Name = "物料名称";
            this.物料名称.ReadOnly = true;
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
            // 仓库编码
            // 
            this.仓库编码.DataPropertyName = "W_StockCode";
            this.仓库编码.HeaderText = "仓库编码";
            this.仓库编码.Name = "仓库编码";
            // 
            // 仓库名称
            // 
            this.仓库名称.DataPropertyName = "W_StockName";
            this.仓库名称.HeaderText = "仓库名称";
            this.仓库名称.Name = "仓库名称";
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("宋体", 11F);
            this.btnBack.Image = global::DesktopApp.Properties.Resources.prev;
            this.btnBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBack.Location = new System.Drawing.Point(731, 7);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(80, 30);
            this.btnBack.TabIndex = 128;
            this.btnBack.Text = "退回";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 11F);
            this.label7.Location = new System.Drawing.Point(297, 15);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 130;
            this.label7.Text = "车间编码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 11F);
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 129;
            this.label1.Text = "车间：";
            // 
            // txtWorkShop
            // 
            this.txtWorkShop.Location = new System.Drawing.Point(68, 15);
            this.txtWorkShop.Name = "txtWorkShop";
            this.txtWorkShop.ReadOnly = true;
            this.txtWorkShop.Size = new System.Drawing.Size(173, 21);
            this.txtWorkShop.TabIndex = 131;
            // 
            // txtWorkShopName
            // 
            this.txtWorkShopName.Location = new System.Drawing.Point(384, 12);
            this.txtWorkShopName.Name = "txtWorkShopName";
            this.txtWorkShopName.ReadOnly = true;
            this.txtWorkShopName.Size = new System.Drawing.Size(186, 21);
            this.txtWorkShopName.TabIndex = 132;
            // 
            // btn_Search
            // 
            this.btn_Search.Font = new System.Drawing.Font("宋体", 11F);
            this.btn_Search.Image = global::DesktopApp.Properties.Resources.search1;
            this.btn_Search.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Search.Location = new System.Drawing.Point(619, 7);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(80, 30);
            this.btn_Search.TabIndex = 133;
            this.btn_Search.Text = "查询";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // lblTS
            // 
            this.lblTS.AutoSize = true;
            this.lblTS.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTS.ForeColor = System.Drawing.Color.Red;
            this.lblTS.Location = new System.Drawing.Point(17, 388);
            this.lblTS.Name = "lblTS";
            this.lblTS.Size = new System.Drawing.Size(0, 16);
            this.lblTS.TabIndex = 149;
            // 
            // frmBackStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 422);
            this.Controls.Add(this.lblTS);
            this.Controls.Add(this.btn_Search);
            this.Controls.Add(this.txtWorkShopName);
            this.Controls.Add(this.txtWorkShop);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmBackStock";
            this.Text = "退回日耗库";
            this.Load += new System.EventHandler(this.frmBackStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn 选择;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 批次;
        private System.Windows.Forms.DataGridViewTextBoxColumn 数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 实用数量;
        private System.Windows.Forms.DataGridViewTextBoxColumn 价格;
        private System.Windows.Forms.DataGridViewTextBoxColumn 车间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 状态;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单位;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注;
        private System.Windows.Forms.DataGridViewTextBoxColumn 单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 仓库编码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 仓库名称;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWorkShop;
        private System.Windows.Forms.TextBox txtWorkShopName;
        private global::System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Label lblTS;
    }
}