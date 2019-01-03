namespace DesktopApp
{
    partial class frmCompanyEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompanyEdit));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.C_UpdateDate = new System.Windows.Forms.DateTimePicker();
            this.C_CreateDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.C_UpdateBy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.C_CreateBy = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.C_Code = new System.Windows.Forms.TextBox();
            this.C_Email = new System.Windows.Forms.TextBox();
            this.C_Phone = new System.Windows.Forms.TextBox();
            this.C_Remark = new System.Windows.Forms.TextBox();
            this.C_Address = new System.Windows.Forms.TextBox();
            this.C_Fax = new System.Windows.Forms.TextBox();
            this.C_Name = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.C_UpdateDate);
            this.groupBox2.Controls.Add(this.C_CreateDate);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.C_UpdateBy);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.C_CreateBy);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 94);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "人员信息";
            // 
            // C_UpdateDate
            // 
            this.C_UpdateDate.Enabled = false;
            this.C_UpdateDate.Location = new System.Drawing.Point(296, 56);
            this.C_UpdateDate.Name = "C_UpdateDate";
            this.C_UpdateDate.Size = new System.Drawing.Size(160, 21);
            this.C_UpdateDate.TabIndex = 9;
            // 
            // C_CreateDate
            // 
            this.C_CreateDate.Enabled = false;
            this.C_CreateDate.Location = new System.Drawing.Point(296, 26);
            this.C_CreateDate.Name = "C_CreateDate";
            this.C_CreateDate.Size = new System.Drawing.Size(160, 21);
            this.C_CreateDate.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(238, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "更新日期：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(238, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "创建日期：";
            // 
            // C_UpdateBy
            // 
            this.C_UpdateBy.Enabled = false;
            this.C_UpdateBy.Location = new System.Drawing.Point(72, 56);
            this.C_UpdateBy.Name = "C_UpdateBy";
            this.C_UpdateBy.Size = new System.Drawing.Size(160, 21);
            this.C_UpdateBy.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "更新人：";
            // 
            // C_CreateBy
            // 
            this.C_CreateBy.Enabled = false;
            this.C_CreateBy.Location = new System.Drawing.Point(72, 26);
            this.C_CreateBy.Name = "C_CreateBy";
            this.C_CreateBy.Size = new System.Drawing.Size(160, 21);
            this.C_CreateBy.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 3;
            this.label6.Text = "创建人：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.C_Code);
            this.groupBox1.Controls.Add(this.C_Email);
            this.groupBox1.Controls.Add(this.C_Phone);
            this.groupBox1.Controls.Add(this.C_Remark);
            this.groupBox1.Controls.Add(this.C_Address);
            this.groupBox1.Controls.Add(this.C_Fax);
            this.groupBox1.Controls.Add(this.C_Name);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 150);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "公司信息";
            // 
            // C_Code
            // 
            this.C_Code.Location = new System.Drawing.Point(72, 25);
            this.C_Code.Name = "C_Code";
            this.C_Code.Size = new System.Drawing.Size(160, 21);
            this.C_Code.TabIndex = 1;
            // 
            // C_Email
            // 
            this.C_Email.Location = new System.Drawing.Point(72, 80);
            this.C_Email.Name = "C_Email";
            this.C_Email.Size = new System.Drawing.Size(160, 21);
            this.C_Email.TabIndex = 5;
            // 
            // C_Phone
            // 
            this.C_Phone.Location = new System.Drawing.Point(72, 53);
            this.C_Phone.Name = "C_Phone";
            this.C_Phone.Size = new System.Drawing.Size(160, 21);
            this.C_Phone.TabIndex = 3;
            // 
            // C_Remark
            // 
            this.C_Remark.Location = new System.Drawing.Point(72, 110);
            this.C_Remark.Name = "C_Remark";
            this.C_Remark.Size = new System.Drawing.Size(384, 21);
            this.C_Remark.TabIndex = 7;
            // 
            // C_Address
            // 
            this.C_Address.Location = new System.Drawing.Point(296, 80);
            this.C_Address.Name = "C_Address";
            this.C_Address.Size = new System.Drawing.Size(160, 21);
            this.C_Address.TabIndex = 6;
            // 
            // C_Fax
            // 
            this.C_Fax.Location = new System.Drawing.Point(296, 53);
            this.C_Fax.Name = "C_Fax";
            this.C_Fax.Size = new System.Drawing.Size(160, 21);
            this.C_Fax.TabIndex = 4;
            // 
            // C_Name
            // 
            this.C_Name.Location = new System.Drawing.Point(296, 25);
            this.C_Name.Name = "C_Name";
            this.C_Name.Size = new System.Drawing.Size(160, 21);
            this.C_Name.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 83);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 3;
            this.label12.Text = "电子邮箱：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 56);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "联系方式：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "备注说明：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "公司编号：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(240, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "通讯地址：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(239, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 3;
            this.label11.Text = "公司传真：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(238, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "公司名称：";
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(428, 275);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 23);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "取消";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::DesktopApp.Properties.Resources.ok;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(366, 275);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "确定";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmCompanyEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 313);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCompanyEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "公司信息编辑";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox C_CreateBy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox C_Code;
        private System.Windows.Forms.TextBox C_Email;
        private System.Windows.Forms.TextBox C_Phone;
        private System.Windows.Forms.TextBox C_Remark;
        private System.Windows.Forms.TextBox C_Address;
        private System.Windows.Forms.TextBox C_Fax;
        private System.Windows.Forms.TextBox C_Name;
        private System.Windows.Forms.TextBox C_UpdateBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker C_UpdateDate;
        private System.Windows.Forms.DateTimePicker C_CreateDate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}