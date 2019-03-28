namespace DesktopApp
{
    partial class frmFaceRecognitionRegistration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFaceRecognitionRegistration));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmb_Department = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtImageFile = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cmb_Image = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnRegisterImage = new System.Windows.Forms.Button();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(515, 184);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 29);
            this.btnCancel.TabIndex = 38;
            this.btnCancel.Text = "取消";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::DesktopApp.Properties.Resources.ok;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(451, 135);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 29);
            this.btnSave.TabIndex = 37;
            this.btnSave.Text = "确定";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmb_Department);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 180);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "人脸识别注册信息";
            // 
            // cmb_Department
            // 
            this.cmb_Department.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Department.FormattingEnabled = true;
            this.cmb_Department.Location = new System.Drawing.Point(204, 104);
            this.cmb_Department.Name = "cmb_Department";
            this.cmb_Department.Size = new System.Drawing.Size(165, 23);
            this.cmb_Department.TabIndex = 49;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 48;
            this.label2.Text = "部门：";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(204, 56);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ReadOnly = true;
            this.txtUserName.Size = new System.Drawing.Size(165, 25);
            this.txtUserName.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(75, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 46;
            this.label1.Text = "用户姓名：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtImageFile);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.cmb_Image);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.btnRegisterImage);
            this.groupBox2.Controls.Add(this.btnUploadImage);
            this.groupBox2.Location = new System.Drawing.Point(12, 216);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(560, 319);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "照片注册信息";
            // 
            // txtImageFile
            // 
            this.txtImageFile.Location = new System.Drawing.Point(222, 52);
            this.txtImageFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtImageFile.Name = "txtImageFile";
            this.txtImageFile.ReadOnly = true;
            this.txtImageFile.Size = new System.Drawing.Size(212, 25);
            this.txtImageFile.TabIndex = 63;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(92, 54);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(82, 15);
            this.label26.TabIndex = 62;
            this.label26.Text = "照片路径：";
            // 
            // cmb_Image
            // 
            this.cmb_Image.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Image.FormattingEnabled = true;
            this.cmb_Image.Items.AddRange(new object[] {
            "照片1",
            "照片2",
            "照片3"});
            this.cmb_Image.Location = new System.Drawing.Point(223, 21);
            this.cmb_Image.Margin = new System.Windows.Forms.Padding(4);
            this.cmb_Image.Name = "cmb_Image";
            this.cmb_Image.Size = new System.Drawing.Size(73, 23);
            this.cmb_Image.TabIndex = 61;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(92, 21);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(82, 15);
            this.label25.TabIndex = 60;
            this.label25.Text = "用户照片：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(222, 85);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(213, 171);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 59;
            this.pictureBox1.TabStop = false;
            // 
            // btnRegisterImage
            // 
            this.btnRegisterImage.Image = global::DesktopApp.Properties.Resources.up;
            this.btnRegisterImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegisterImage.Location = new System.Drawing.Point(451, 237);
            this.btnRegisterImage.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegisterImage.Name = "btnRegisterImage";
            this.btnRegisterImage.Size = new System.Drawing.Size(102, 29);
            this.btnRegisterImage.TabIndex = 58;
            this.btnRegisterImage.Text = "注册照片";
            this.btnRegisterImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRegisterImage.UseVisualStyleBackColor = true;
            this.btnRegisterImage.Click += new System.EventHandler(this.btnRegisterImage_Click);
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.Image = global::DesktopApp.Properties.Resources.up;
            this.btnUploadImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUploadImage.Location = new System.Drawing.Point(451, 283);
            this.btnUploadImage.Margin = new System.Windows.Forms.Padding(4);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(102, 29);
            this.btnUploadImage.TabIndex = 57;
            this.btnUploadImage.Text = "上传照片";
            this.btnUploadImage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUploadImage.UseVisualStyleBackColor = true;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(384, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 50;
            this.label3.Text = "未注册";
            // 
            // frmFaceRecognitionRegistration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 547);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmFaceRecognitionRegistration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "人脸识别人员注册";
            this.Load += new System.EventHandler(this.frmFaceRecognitionRegistration_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_Department;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtImageFile;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cmb_Image;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnRegisterImage;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.Label label3;
    }
}