﻿namespace DesktopApp
{
    partial class frmRoleEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRoleEdit));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.R_UpdateDate = new System.Windows.Forms.DateTimePicker();
            this.R_CreateDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.R_UpdateBy = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.R_CreateBy = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.R_Code = new System.Windows.Forms.TextBox();
            this.R_Remark = new System.Windows.Forms.TextBox();
            this.R_Name = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.R_UpdateDate);
            this.groupBox2.Controls.Add(this.R_CreateDate);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.R_UpdateBy);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.R_CreateBy);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(472, 94);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "人员信息";
            // 
            // R_UpdateDate
            // 
            this.R_UpdateDate.Enabled = false;
            this.R_UpdateDate.Location = new System.Drawing.Point(296, 56);
            this.R_UpdateDate.Name = "R_UpdateDate";
            this.R_UpdateDate.Size = new System.Drawing.Size(160, 21);
            this.R_UpdateDate.TabIndex = 9;
            // 
            // R_CreateDate
            // 
            this.R_CreateDate.Enabled = false;
            this.R_CreateDate.Location = new System.Drawing.Point(296, 26);
            this.R_CreateDate.Name = "R_CreateDate";
            this.R_CreateDate.Size = new System.Drawing.Size(160, 21);
            this.R_CreateDate.TabIndex = 8;
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
            // R_UpdateBy
            // 
            this.R_UpdateBy.Enabled = false;
            this.R_UpdateBy.Location = new System.Drawing.Point(72, 56);
            this.R_UpdateBy.Name = "R_UpdateBy";
            this.R_UpdateBy.Size = new System.Drawing.Size(160, 21);
            this.R_UpdateBy.TabIndex = 4;
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
            // R_CreateBy
            // 
            this.R_CreateBy.Enabled = false;
            this.R_CreateBy.Location = new System.Drawing.Point(72, 26);
            this.R_CreateBy.Name = "R_CreateBy";
            this.R_CreateBy.Size = new System.Drawing.Size(160, 21);
            this.R_CreateBy.TabIndex = 2;
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
            this.groupBox1.Controls.Add(this.R_Code);
            this.groupBox1.Controls.Add(this.R_Remark);
            this.groupBox1.Controls.Add(this.R_Name);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(472, 91);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "角色信息";
            // 
            // R_Code
            // 
            this.R_Code.Location = new System.Drawing.Point(72, 25);
            this.R_Code.Name = "R_Code";
            this.R_Code.Size = new System.Drawing.Size(160, 21);
            this.R_Code.TabIndex = 1;
            // 
            // R_Remark
            // 
            this.R_Remark.Location = new System.Drawing.Point(72, 55);
            this.R_Remark.Name = "R_Remark";
            this.R_Remark.Size = new System.Drawing.Size(384, 21);
            this.R_Remark.TabIndex = 7;
            // 
            // R_Name
            // 
            this.R_Name.Location = new System.Drawing.Point(296, 25);
            this.R_Name.Name = "R_Name";
            this.R_Name.Size = new System.Drawing.Size(160, 21);
            this.R_Name.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 58);
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
            this.label1.Text = "角色编号：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(238, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "角色名称：";
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(428, 211);
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
            this.btnSave.Location = new System.Drawing.Point(366, 211);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 23);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "确定";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmRoleEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 254);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmRoleEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "角色信息编辑";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox R_CreateBy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox R_Code;
        private System.Windows.Forms.TextBox R_Remark;
        private System.Windows.Forms.TextBox R_Name;
        private System.Windows.Forms.TextBox R_UpdateBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker R_UpdateDate;
        private System.Windows.Forms.DateTimePicker R_CreateDate;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}