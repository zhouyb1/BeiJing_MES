namespace DesktopApp
{
    partial class frmStorageList
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMaterInNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.入库单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.仓库编码 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.仓库名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.物料类型 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.生产订单号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.订单时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.状态 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.添加人 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.添加时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.修改人 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.修改时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.删除人 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.删除时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.提交人 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.提交时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.备注 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.供应商 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.供应商名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtMaterInNo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnFind);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(840, 67);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // txtMaterInNo
            // 
            this.txtMaterInNo.Font = new System.Drawing.Font("宋体", 11F);
            this.txtMaterInNo.Location = new System.Drawing.Point(132, 21);
            this.txtMaterInNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaterInNo.Name = "txtMaterInNo";
            this.txtMaterInNo.Size = new System.Drawing.Size(172, 24);
            this.txtMaterInNo.TabIndex = 14;
            this.txtMaterInNo.TextChanged += new System.EventHandler(this.txtMaterInNo_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 11F);
            this.label2.Location = new System.Drawing.Point(46, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "入库单号：";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnFind
            // 
            this.btnFind.Font = new System.Drawing.Font("宋体", 11F);
            this.btnFind.Image = global::DesktopApp.Properties.Resources.search1;
            this.btnFind.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFind.Location = new System.Drawing.Point(341, 18);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(70, 30);
            this.btnFind.TabIndex = 11;
            this.btnFind.Text = "查询";
            this.btnFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("宋体", 11F);
            this.btnAdd.Image = global::DesktopApp.Properties.Resources.edit_add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(434, 18);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(70, 30);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "添加";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.入库单号,
            this.仓库编码,
            this.仓库名称,
            this.物料类型,
            this.生产订单号,
            this.订单时间,
            this.状态,
            this.添加人,
            this.添加时间,
            this.修改人,
            this.修改时间,
            this.删除人,
            this.删除时间,
            this.提交人,
            this.提交时间,
            this.备注,
            this.供应商,
            this.供应商名称});
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 67);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 27;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(840, 416);
            this.dataGridView.TabIndex = 17;
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);
            this.dataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellDoubleClick);
            this.dataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_CellFormatting);
            // 
            // 入库单号
            // 
            this.入库单号.DataPropertyName = "M_MaterInNo";
            this.入库单号.HeaderText = "入库单号";
            this.入库单号.Name = "入库单号";
            this.入库单号.ReadOnly = true;
            this.入库单号.Width = 140;
            // 
            // 仓库编码
            // 
            this.仓库编码.DataPropertyName = "M_StockCode";
            this.仓库编码.HeaderText = "仓库编码";
            this.仓库编码.Name = "仓库编码";
            this.仓库编码.ReadOnly = true;
            this.仓库编码.Width = 80;
            // 
            // 仓库名称
            // 
            this.仓库名称.DataPropertyName = "M_StockName";
            this.仓库名称.HeaderText = "仓库名称";
            this.仓库名称.Name = "仓库名称";
            this.仓库名称.ReadOnly = true;
            // 
            // 物料类型
            // 
            this.物料类型.DataPropertyName = "M_Kind";
            this.物料类型.HeaderText = "物料类型";
            this.物料类型.Name = "物料类型";
            // 
            // 生产订单号
            // 
            this.生产订单号.DataPropertyName = "M_OrderNo";
            this.生产订单号.HeaderText = "生产订单号";
            this.生产订单号.Name = "生产订单号";
            this.生产订单号.ReadOnly = true;
            this.生产订单号.Width = 160;
            // 
            // 订单时间
            // 
            this.订单时间.DataPropertyName = "M_OrderDate";
            this.订单时间.HeaderText = "订单时间";
            this.订单时间.Name = "订单时间";
            this.订单时间.ReadOnly = true;
            // 
            // 状态
            // 
            this.状态.DataPropertyName = "M_Status";
            this.状态.HeaderText = "状态";
            this.状态.Name = "状态";
            this.状态.ReadOnly = true;
            // 
            // 添加人
            // 
            this.添加人.DataPropertyName = "M_CreateBy";
            this.添加人.HeaderText = "添加人";
            this.添加人.Name = "添加人";
            this.添加人.ReadOnly = true;
            // 
            // 添加时间
            // 
            this.添加时间.DataPropertyName = "M_CreateDate";
            this.添加时间.HeaderText = "添加时间";
            this.添加时间.Name = "添加时间";
            this.添加时间.ReadOnly = true;
            this.添加时间.Width = 160;
            // 
            // 修改人
            // 
            this.修改人.DataPropertyName = "M_UpdateBy";
            this.修改人.HeaderText = "修改人";
            this.修改人.Name = "修改人";
            this.修改人.ReadOnly = true;
            // 
            // 修改时间
            // 
            this.修改时间.DataPropertyName = "M_UpdateDate";
            this.修改时间.HeaderText = "修改时间";
            this.修改时间.Name = "修改时间";
            this.修改时间.ReadOnly = true;
            this.修改时间.Width = 160;
            // 
            // 删除人
            // 
            this.删除人.DataPropertyName = "M_DeleteBy";
            this.删除人.HeaderText = "删除人";
            this.删除人.Name = "删除人";
            this.删除人.ReadOnly = true;
            // 
            // 删除时间
            // 
            this.删除时间.DataPropertyName = "M_DeleteDate";
            this.删除时间.HeaderText = "删除时间";
            this.删除时间.Name = "删除时间";
            this.删除时间.ReadOnly = true;
            // 
            // 提交人
            // 
            this.提交人.DataPropertyName = "M_UploadBy";
            this.提交人.HeaderText = "提交人";
            this.提交人.Name = "提交人";
            this.提交人.ReadOnly = true;
            // 
            // 提交时间
            // 
            this.提交时间.DataPropertyName = "M_UploadDate";
            this.提交时间.HeaderText = "提交时间";
            this.提交时间.Name = "提交时间";
            this.提交时间.ReadOnly = true;
            this.提交时间.Width = 160;
            // 
            // 备注
            // 
            this.备注.DataPropertyName = "M_Remark";
            this.备注.HeaderText = "备注";
            this.备注.Name = "备注";
            this.备注.ReadOnly = true;
            // 
            // 供应商
            // 
            this.供应商.DataPropertyName = "M_SupplyCode";
            this.供应商.HeaderText = "供应商";
            this.供应商.Name = "供应商";
            // 
            // 供应商名称
            // 
            this.供应商名称.DataPropertyName = "M_SupplyName";
            this.供应商名称.HeaderText = "供应商名称";
            this.供应商名称.Name = "供应商名称";
            // 
            // frmStorageList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(840, 483);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmStorageList";
            this.Text = "物料入库";
            this.Load += new System.EventHandler(this.frmStorageMake_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtMaterInNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn 入库单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 仓库编码;
        private System.Windows.Forms.DataGridViewTextBoxColumn 仓库名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 物料类型;
        private System.Windows.Forms.DataGridViewTextBoxColumn 生产订单号;
        private System.Windows.Forms.DataGridViewTextBoxColumn 订单时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 状态;
        private System.Windows.Forms.DataGridViewTextBoxColumn 添加人;
        private System.Windows.Forms.DataGridViewTextBoxColumn 添加时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 修改人;
        private System.Windows.Forms.DataGridViewTextBoxColumn 修改时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 删除人;
        private System.Windows.Forms.DataGridViewTextBoxColumn 删除时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 提交人;
        private System.Windows.Forms.DataGridViewTextBoxColumn 提交时间;
        private System.Windows.Forms.DataGridViewTextBoxColumn 备注;
        private System.Windows.Forms.DataGridViewTextBoxColumn 供应商;
        private System.Windows.Forms.DataGridViewTextBoxColumn 供应商名称;

    }
}