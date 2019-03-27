namespace DesktopApp
{
    partial class pd
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.barcodeControl1 = new Cobainsoft.Windows.Forms.BarcodeControl();
            this.SuspendLayout();
            // 
            // barcodeControl1
            // 
            this.barcodeControl1.AddOnCaption = null;
            this.barcodeControl1.AddOnData = null;
            this.barcodeControl1.BackColor = System.Drawing.Color.White;
            this.barcodeControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.barcodeControl1.BarcodeType = Cobainsoft.Windows.Forms.BarcodeType.CODE128B;
            this.barcodeControl1.Caption = "123456789fdfd";
            this.barcodeControl1.CopyRight = "";
            this.barcodeControl1.Data = "123456789";
            this.barcodeControl1.Font = new System.Drawing.Font("Arial", 9F);
            this.barcodeControl1.ForeColor = System.Drawing.Color.Black;
            this.barcodeControl1.InvalidDataAction = Cobainsoft.Windows.Forms.InvalidDataAction.DisplayInvalid;
            this.barcodeControl1.Location = new System.Drawing.Point(48, 100);
            this.barcodeControl1.LowerTopTextBy = 0F;
            this.barcodeControl1.Margin = new System.Windows.Forms.Padding(4);
            this.barcodeControl1.Name = "barcodeControl1";
            this.barcodeControl1.RaiseBottomTextBy = 0F;
            this.barcodeControl1.Size = new System.Drawing.Size(199, 76);
            this.barcodeControl1.TabIndex = 6;
            // 
            // pd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barcodeControl1);
            this.Name = "pd";
            this.Size = new System.Drawing.Size(340, 282);
            this.ResumeLayout(false);

        }

        #endregion

        public Cobainsoft.Windows.Forms.BarcodeControl barcodeControl1;
    }
}
