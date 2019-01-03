namespace DesktopApp
{
    partial class frmSerialPortConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSerialPortConfig));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.imgTip = new System.Windows.Forms.PictureBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.cbbStopBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbDataBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbParity = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbBaudRate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbComList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClearRev = new System.Windows.Forms.LinkLabel();
            this.lblRev = new System.Windows.Forms.Label();
            this.lblRevCount = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClearSend = new System.Windows.Forms.LinkLabel();
            this.lblSend = new System.Windows.Forms.Label();
            this.lblSendCount = new System.Windows.Forms.Label();
            this.txtShowData = new System.Windows.Forms.TextBox();
            this.txtSendData = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnReceive = new System.Windows.Forms.Button();
            this.rbtnUnicode = new System.Windows.Forms.RadioButton();
            this.rbtnUTF8 = new System.Windows.Forms.RadioButton();
            this.rbtnASCII = new System.Windows.Forms.RadioButton();
            this.rbtnHex = new System.Windows.Forms.RadioButton();
            this.chkAutoLine = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbtnSendUnicode = new System.Windows.Forms.RadioButton();
            this.rbtnSendHex = new System.Windows.Forms.RadioButton();
            this.rbtnSendASCII = new System.Windows.Forms.RadioButton();
            this.rbtnSendUTF8 = new System.Windows.Forms.RadioButton();
            this.btnSend = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgTip)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.imgTip);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.cbbStopBits);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbbDataBits);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbbParity);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbbBaudRate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbbComList);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 194);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通讯设置";
            // 
            // imgTip
            // 
            this.imgTip.Location = new System.Drawing.Point(13, 153);
            this.imgTip.Name = "imgTip";
            this.imgTip.Size = new System.Drawing.Size(32, 32);
            this.imgTip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.imgTip.TabIndex = 11;
            this.imgTip.TabStop = false;
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(51, 153);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(121, 32);
            this.btnOpen.TabIndex = 10;
            this.btnOpen.Text = "打开串口";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // cbbStopBits
            // 
            this.cbbStopBits.FormattingEnabled = true;
            this.cbbStopBits.Items.AddRange(new object[] {
            "1",
            "2",
            "3"});
            this.cbbStopBits.Location = new System.Drawing.Point(54, 122);
            this.cbbStopBits.Name = "cbbStopBits";
            this.cbbStopBits.Size = new System.Drawing.Size(121, 20);
            this.cbbStopBits.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "停止位：";
            // 
            // cbbDataBits
            // 
            this.cbbDataBits.FormattingEnabled = true;
            this.cbbDataBits.Items.AddRange(new object[] {
            "8",
            "7",
            "6"});
            this.cbbDataBits.Location = new System.Drawing.Point(54, 96);
            this.cbbDataBits.Name = "cbbDataBits";
            this.cbbDataBits.Size = new System.Drawing.Size(121, 20);
            this.cbbDataBits.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "数据位：";
            // 
            // cbbParity
            // 
            this.cbbParity.FormattingEnabled = true;
            this.cbbParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.cbbParity.Location = new System.Drawing.Point(54, 70);
            this.cbbParity.Name = "cbbParity";
            this.cbbParity.Size = new System.Drawing.Size(121, 20);
            this.cbbParity.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "校验位：";
            // 
            // cbbBaudRate
            // 
            this.cbbBaudRate.FormattingEnabled = true;
            this.cbbBaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cbbBaudRate.Location = new System.Drawing.Point(54, 44);
            this.cbbBaudRate.Name = "cbbBaudRate";
            this.cbbBaudRate.Size = new System.Drawing.Size(121, 20);
            this.cbbBaudRate.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率：";
            // 
            // cbbComList
            // 
            this.cbbComList.FormattingEnabled = true;
            this.cbbComList.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9"});
            this.cbbComList.Location = new System.Drawing.Point(54, 18);
            this.cbbComList.Name = "cbbComList";
            this.cbbComList.Size = new System.Drawing.Size(121, 20);
            this.cbbComList.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口号：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClearRev);
            this.groupBox2.Controls.Add(this.lblRev);
            this.groupBox2.Controls.Add(this.lblRevCount);
            this.groupBox2.Location = new System.Drawing.Point(12, 216);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(186, 45);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "接收";
            // 
            // btnClearRev
            // 
            this.btnClearRev.AutoSize = true;
            this.btnClearRev.Location = new System.Drawing.Point(122, 23);
            this.btnClearRev.Name = "btnClearRev";
            this.btnClearRev.Size = new System.Drawing.Size(53, 12);
            this.btnClearRev.TabIndex = 1;
            this.btnClearRev.TabStop = true;
            this.btnClearRev.Text = "清除接收";
            this.btnClearRev.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClearRev_LinkClicked);
            // 
            // lblRev
            // 
            this.lblRev.AutoSize = true;
            this.lblRev.Location = new System.Drawing.Point(7, 22);
            this.lblRev.Name = "lblRev";
            this.lblRev.Size = new System.Drawing.Size(71, 12);
            this.lblRev.TabIndex = 16;
            this.lblRev.Text = "接收字节数:";
            // 
            // lblRevCount
            // 
            this.lblRevCount.AutoSize = true;
            this.lblRevCount.Location = new System.Drawing.Point(84, 23);
            this.lblRevCount.Name = "lblRevCount";
            this.lblRevCount.Size = new System.Drawing.Size(11, 12);
            this.lblRevCount.TabIndex = 17;
            this.lblRevCount.Text = "0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClearSend);
            this.groupBox3.Controls.Add(this.lblSend);
            this.groupBox3.Controls.Add(this.lblSendCount);
            this.groupBox3.Location = new System.Drawing.Point(12, 283);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(186, 45);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "发送";
            // 
            // btnClearSend
            // 
            this.btnClearSend.AutoSize = true;
            this.btnClearSend.Location = new System.Drawing.Point(122, 21);
            this.btnClearSend.Name = "btnClearSend";
            this.btnClearSend.Size = new System.Drawing.Size(53, 12);
            this.btnClearSend.TabIndex = 2;
            this.btnClearSend.TabStop = true;
            this.btnClearSend.Text = "清除发送";
            this.btnClearSend.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClearSend_LinkClicked);
            // 
            // lblSend
            // 
            this.lblSend.AutoSize = true;
            this.lblSend.Location = new System.Drawing.Point(7, 21);
            this.lblSend.Name = "lblSend";
            this.lblSend.Size = new System.Drawing.Size(71, 12);
            this.lblSend.TabIndex = 18;
            this.lblSend.Text = "发送字节数:";
            // 
            // lblSendCount
            // 
            this.lblSendCount.AutoSize = true;
            this.lblSendCount.Location = new System.Drawing.Point(84, 21);
            this.lblSendCount.Name = "lblSendCount";
            this.lblSendCount.Size = new System.Drawing.Size(11, 12);
            this.lblSendCount.TabIndex = 19;
            this.lblSendCount.Text = "0";
            // 
            // txtShowData
            // 
            this.txtShowData.Location = new System.Drawing.Point(6, 40);
            this.txtShowData.Multiline = true;
            this.txtShowData.Name = "txtShowData";
            this.txtShowData.Size = new System.Drawing.Size(274, 148);
            this.txtShowData.TabIndex = 3;
            // 
            // txtSendData
            // 
            this.txtSendData.Location = new System.Drawing.Point(6, 20);
            this.txtSendData.Multiline = true;
            this.txtSendData.Name = "txtSendData";
            this.txtSendData.Size = new System.Drawing.Size(274, 61);
            this.txtSendData.TabIndex = 4;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnReceive);
            this.groupBox4.Controls.Add(this.rbtnUnicode);
            this.groupBox4.Controls.Add(this.rbtnUTF8);
            this.groupBox4.Controls.Add(this.rbtnASCII);
            this.groupBox4.Controls.Add(this.rbtnHex);
            this.groupBox4.Controls.Add(this.chkAutoLine);
            this.groupBox4.Controls.Add(this.txtShowData);
            this.groupBox4.Location = new System.Drawing.Point(205, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(354, 194);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "数据接收区";
            // 
            // btnReceive
            // 
            this.btnReceive.Location = new System.Drawing.Point(283, 93);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(62, 44);
            this.btnReceive.TabIndex = 12;
            this.btnReceive.Text = "读取数据";
            this.btnReceive.UseVisualStyleBackColor = true;
            this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // rbtnUnicode
            // 
            this.rbtnUnicode.AutoSize = true;
            this.rbtnUnicode.Location = new System.Drawing.Point(263, 18);
            this.rbtnUnicode.Name = "rbtnUnicode";
            this.rbtnUnicode.Size = new System.Drawing.Size(65, 16);
            this.rbtnUnicode.TabIndex = 10;
            this.rbtnUnicode.Text = "Unicode";
            this.rbtnUnicode.UseVisualStyleBackColor = true;
            // 
            // rbtnUTF8
            // 
            this.rbtnUTF8.AutoSize = true;
            this.rbtnUTF8.Location = new System.Drawing.Point(204, 18);
            this.rbtnUTF8.Name = "rbtnUTF8";
            this.rbtnUTF8.Size = new System.Drawing.Size(53, 16);
            this.rbtnUTF8.TabIndex = 9;
            this.rbtnUTF8.Text = "UTF-8";
            this.rbtnUTF8.UseVisualStyleBackColor = true;
            // 
            // rbtnASCII
            // 
            this.rbtnASCII.AutoSize = true;
            this.rbtnASCII.Location = new System.Drawing.Point(145, 18);
            this.rbtnASCII.Name = "rbtnASCII";
            this.rbtnASCII.Size = new System.Drawing.Size(53, 16);
            this.rbtnASCII.TabIndex = 8;
            this.rbtnASCII.Text = "ASCII";
            this.rbtnASCII.UseVisualStyleBackColor = true;
            // 
            // rbtnHex
            // 
            this.rbtnHex.AutoSize = true;
            this.rbtnHex.Checked = true;
            this.rbtnHex.Location = new System.Drawing.Point(82, 18);
            this.rbtnHex.Name = "rbtnHex";
            this.rbtnHex.Size = new System.Drawing.Size(59, 16);
            this.rbtnHex.TabIndex = 7;
            this.rbtnHex.TabStop = true;
            this.rbtnHex.Text = "16进制";
            this.rbtnHex.UseVisualStyleBackColor = true;
            // 
            // chkAutoLine
            // 
            this.chkAutoLine.AutoSize = true;
            this.chkAutoLine.Location = new System.Drawing.Point(7, 18);
            this.chkAutoLine.Name = "chkAutoLine";
            this.chkAutoLine.Size = new System.Drawing.Size(72, 16);
            this.chkAutoLine.TabIndex = 6;
            this.chkAutoLine.Text = "自动换行";
            this.chkAutoLine.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rbtnSendUnicode);
            this.groupBox5.Controls.Add(this.rbtnSendHex);
            this.groupBox5.Controls.Add(this.rbtnSendASCII);
            this.groupBox5.Controls.Add(this.rbtnSendUTF8);
            this.groupBox5.Controls.Add(this.txtSendData);
            this.groupBox5.Controls.Add(this.btnSend);
            this.groupBox5.Location = new System.Drawing.Point(205, 216);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(354, 112);
            this.groupBox5.TabIndex = 13;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "数据发送区";
            // 
            // rbtnSendUnicode
            // 
            this.rbtnSendUnicode.AutoSize = true;
            this.rbtnSendUnicode.Location = new System.Drawing.Point(187, 87);
            this.rbtnSendUnicode.Name = "rbtnSendUnicode";
            this.rbtnSendUnicode.Size = new System.Drawing.Size(65, 16);
            this.rbtnSendUnicode.TabIndex = 15;
            this.rbtnSendUnicode.Text = "Unicode";
            this.rbtnSendUnicode.UseVisualStyleBackColor = true;
            // 
            // rbtnSendHex
            // 
            this.rbtnSendHex.AutoSize = true;
            this.rbtnSendHex.Checked = true;
            this.rbtnSendHex.Location = new System.Drawing.Point(8, 87);
            this.rbtnSendHex.Name = "rbtnSendHex";
            this.rbtnSendHex.Size = new System.Drawing.Size(59, 16);
            this.rbtnSendHex.TabIndex = 12;
            this.rbtnSendHex.TabStop = true;
            this.rbtnSendHex.Text = "16进制";
            this.rbtnSendHex.UseVisualStyleBackColor = true;
            // 
            // rbtnSendASCII
            // 
            this.rbtnSendASCII.AutoSize = true;
            this.rbtnSendASCII.Location = new System.Drawing.Point(69, 87);
            this.rbtnSendASCII.Name = "rbtnSendASCII";
            this.rbtnSendASCII.Size = new System.Drawing.Size(53, 16);
            this.rbtnSendASCII.TabIndex = 13;
            this.rbtnSendASCII.Text = "ASCII";
            this.rbtnSendASCII.UseVisualStyleBackColor = true;
            // 
            // rbtnSendUTF8
            // 
            this.rbtnSendUTF8.AutoSize = true;
            this.rbtnSendUTF8.Location = new System.Drawing.Point(128, 87);
            this.rbtnSendUTF8.Name = "rbtnSendUTF8";
            this.rbtnSendUTF8.Size = new System.Drawing.Size(53, 16);
            this.rbtnSendUTF8.TabIndex = 14;
            this.rbtnSendUTF8.Text = "UTF-8";
            this.rbtnSendUTF8.UseVisualStyleBackColor = true;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(286, 30);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(62, 44);
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "写入数据";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(10, 343);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(315, 10);
            this.label6.TabIndex = 16;
            this.label6.Text = "*注：为了使配置正确，请连接COM通讯设备，系统会自动检测串口号！";
            // 
            // btnCancel
            // 
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(501, 336);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 23);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "取消";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::DesktopApp.Properties.Resources.ok;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(439, 336);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(56, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "确定";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmSerialPortConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 371);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmSerialPortConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "COM通讯配置";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSerialPortConfig_FormClosed);
            this.Load += new System.EventHandler(this.FrmSerialPortConfig_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgTip)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbbStopBits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbDataBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbParity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbBaudRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbComList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtShowData;
        private System.Windows.Forms.TextBox txtSendData;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.LinkLabel btnClearRev;
        private System.Windows.Forms.LinkLabel btnClearSend;
        private System.Windows.Forms.PictureBox imgTip;
        private System.Windows.Forms.Label lblRevCount;
        private System.Windows.Forms.Label lblRev;
        private System.Windows.Forms.Label lblSendCount;
        private System.Windows.Forms.Label lblSend;
        private System.Windows.Forms.RadioButton rbtnUnicode;
        private System.Windows.Forms.RadioButton rbtnUTF8;
        private System.Windows.Forms.RadioButton rbtnASCII;
        private System.Windows.Forms.RadioButton rbtnHex;
        private System.Windows.Forms.CheckBox chkAutoLine;
        private System.Windows.Forms.RadioButton rbtnSendUnicode;
        private System.Windows.Forms.RadioButton rbtnSendHex;
        private System.Windows.Forms.RadioButton rbtnSendASCII;
        private System.Windows.Forms.RadioButton rbtnSendUTF8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnReceive;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}