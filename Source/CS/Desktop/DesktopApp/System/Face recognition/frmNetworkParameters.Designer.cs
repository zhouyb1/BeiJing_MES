namespace DesktopApp
{
    partial class frmNetworkParameters
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_SettingPassword = new System.Windows.Forms.Button();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.txt_OldPassword = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmb_IP = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_DNS = new System.Windows.Forms.TextBox();
            this.txt_Gateway = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_SettingInternet = new System.Windows.Forms.Button();
            this.txt_SubnetMask = new System.Windows.Forms.TextBox();
            this.txt_deviceIP = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmb_WifiIP = new System.Windows.Forms.ComboBox();
            this.txt_WifideviceIP = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_WifiDNS = new System.Windows.Forms.TextBox();
            this.txt_WifiGateway = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btn_SettingWifi = new System.Windows.Forms.Button();
            this.txt_WifiPassword = new System.Windows.Forms.TextBox();
            this.txt_WifiName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_SettingPassword);
            this.groupBox1.Controls.Add(this.txt_Password);
            this.groupBox1.Controls.Add(this.txt_OldPassword);
            this.groupBox1.Location = new System.Drawing.Point(42, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(723, 103);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "通讯参数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "新密码：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "原密码：";
            // 
            // btn_SettingPassword
            // 
            this.btn_SettingPassword.Location = new System.Drawing.Point(302, 40);
            this.btn_SettingPassword.Name = "btn_SettingPassword";
            this.btn_SettingPassword.Size = new System.Drawing.Size(88, 33);
            this.btn_SettingPassword.TabIndex = 1;
            this.btn_SettingPassword.Text = "设置密码";
            this.btn_SettingPassword.UseVisualStyleBackColor = true;
            this.btn_SettingPassword.Click += new System.EventHandler(this.btn_SettingPassword_Click);
            // 
            // txt_Password
            // 
            this.txt_Password.Location = new System.Drawing.Point(132, 55);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.Size = new System.Drawing.Size(140, 25);
            this.txt_Password.TabIndex = 0;
            this.txt_Password.Text = "12345678";
            // 
            // txt_OldPassword
            // 
            this.txt_OldPassword.Location = new System.Drawing.Point(132, 24);
            this.txt_OldPassword.Name = "txt_OldPassword";
            this.txt_OldPassword.Size = new System.Drawing.Size(140, 25);
            this.txt_OldPassword.TabIndex = 0;
            this.txt_OldPassword.Text = "12345678";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmb_IP);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txt_DNS);
            this.groupBox2.Controls.Add(this.txt_Gateway);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btn_SettingInternet);
            this.groupBox2.Controls.Add(this.txt_SubnetMask);
            this.groupBox2.Controls.Add(this.txt_deviceIP);
            this.groupBox2.Location = new System.Drawing.Point(42, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(723, 200);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "网络参数";
            // 
            // cmb_IP
            // 
            this.cmb_IP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_IP.FormattingEnabled = true;
            this.cmb_IP.Items.AddRange(new object[] {
            "DHCP 模式",
            "手动配置模式"});
            this.cmb_IP.Location = new System.Drawing.Point(132, 153);
            this.cmb_IP.Name = "cmb_IP";
            this.cmb_IP.Size = new System.Drawing.Size(139, 23);
            this.cmb_IP.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(98, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "IP获取模式：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(75, 125);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "DNS：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(69, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 6;
            this.label6.Text = "网关：";
            // 
            // txt_DNS
            // 
            this.txt_DNS.Location = new System.Drawing.Point(132, 122);
            this.txt_DNS.Name = "txt_DNS";
            this.txt_DNS.Size = new System.Drawing.Size(140, 25);
            this.txt_DNS.TabIndex = 3;
            this.txt_DNS.Text = "8.8.8.8";
            // 
            // txt_Gateway
            // 
            this.txt_Gateway.Location = new System.Drawing.Point(132, 91);
            this.txt_Gateway.Name = "txt_Gateway";
            this.txt_Gateway.Size = new System.Drawing.Size(140, 25);
            this.txt_Gateway.TabIndex = 4;
            this.txt_Gateway.Text = "192.168.1.1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "子网掩码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(53, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "设备IP：";
            // 
            // btn_SettingInternet
            // 
            this.btn_SettingInternet.Location = new System.Drawing.Point(302, 32);
            this.btn_SettingInternet.Name = "btn_SettingInternet";
            this.btn_SettingInternet.Size = new System.Drawing.Size(88, 33);
            this.btn_SettingInternet.TabIndex = 1;
            this.btn_SettingInternet.Text = "设置";
            this.btn_SettingInternet.UseVisualStyleBackColor = true;
            this.btn_SettingInternet.Click += new System.EventHandler(this.btn_SettingInternet_Click);
            // 
            // txt_SubnetMask
            // 
            this.txt_SubnetMask.Location = new System.Drawing.Point(132, 60);
            this.txt_SubnetMask.Name = "txt_SubnetMask";
            this.txt_SubnetMask.Size = new System.Drawing.Size(140, 25);
            this.txt_SubnetMask.TabIndex = 0;
            this.txt_SubnetMask.Text = "255.255.255.0";
            // 
            // txt_deviceIP
            // 
            this.txt_deviceIP.Location = new System.Drawing.Point(132, 29);
            this.txt_deviceIP.Name = "txt_deviceIP";
            this.txt_deviceIP.Size = new System.Drawing.Size(140, 25);
            this.txt_deviceIP.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.cmb_WifiIP);
            this.groupBox3.Controls.Add(this.txt_WifideviceIP);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txt_WifiDNS);
            this.groupBox3.Controls.Add(this.txt_WifiGateway);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.btn_SettingWifi);
            this.groupBox3.Controls.Add(this.txt_WifiPassword);
            this.groupBox3.Controls.Add(this.txt_WifiName);
            this.groupBox3.Location = new System.Drawing.Point(42, 343);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(723, 231);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "wifi参数";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(54, 89);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 15);
            this.label13.TabIndex = 23;
            this.label13.Text = "设备IP：";
            // 
            // cmb_WifiIP
            // 
            this.cmb_WifiIP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_WifiIP.FormattingEnabled = true;
            this.cmb_WifiIP.Items.AddRange(new object[] {
            "DHCP 模式",
            "手动配置模式"});
            this.cmb_WifiIP.Location = new System.Drawing.Point(132, 179);
            this.cmb_WifiIP.Name = "cmb_WifiIP";
            this.cmb_WifiIP.Size = new System.Drawing.Size(139, 23);
            this.cmb_WifiIP.TabIndex = 22;
            // 
            // txt_WifideviceIP
            // 
            this.txt_WifideviceIP.Location = new System.Drawing.Point(132, 86);
            this.txt_WifideviceIP.Name = "txt_WifideviceIP";
            this.txt_WifideviceIP.Size = new System.Drawing.Size(140, 25);
            this.txt_WifideviceIP.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 182);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 15);
            this.label8.TabIndex = 19;
            this.label8.Text = "IP获取模式：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(74, 151);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 15);
            this.label9.TabIndex = 16;
            this.label9.Text = "DNS：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(68, 120);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 15);
            this.label10.TabIndex = 17;
            this.label10.Text = "网关：";
            // 
            // txt_WifiDNS
            // 
            this.txt_WifiDNS.Location = new System.Drawing.Point(132, 148);
            this.txt_WifiDNS.Name = "txt_WifiDNS";
            this.txt_WifiDNS.Size = new System.Drawing.Size(140, 25);
            this.txt_WifiDNS.TabIndex = 14;
            this.txt_WifiDNS.Text = "8.8.8.8";
            // 
            // txt_WifiGateway
            // 
            this.txt_WifiGateway.Location = new System.Drawing.Point(132, 117);
            this.txt_WifiGateway.Name = "txt_WifiGateway";
            this.txt_WifiGateway.Size = new System.Drawing.Size(140, 25);
            this.txt_WifiGateway.TabIndex = 15;
            this.txt_WifiGateway.Text = "192.168.1.1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(39, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 15);
            this.label11.TabIndex = 12;
            this.label11.Text = "wifi密码：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(39, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 15);
            this.label12.TabIndex = 13;
            this.label12.Text = "wifi名称：";
            // 
            // btn_SettingWifi
            // 
            this.btn_SettingWifi.Location = new System.Drawing.Point(302, 27);
            this.btn_SettingWifi.Name = "btn_SettingWifi";
            this.btn_SettingWifi.Size = new System.Drawing.Size(88, 33);
            this.btn_SettingWifi.TabIndex = 11;
            this.btn_SettingWifi.Text = "设置";
            this.btn_SettingWifi.UseVisualStyleBackColor = true;
            this.btn_SettingWifi.Click += new System.EventHandler(this.btn_SettingWifi_Click);
            // 
            // txt_WifiPassword
            // 
            this.txt_WifiPassword.Location = new System.Drawing.Point(132, 55);
            this.txt_WifiPassword.Name = "txt_WifiPassword";
            this.txt_WifiPassword.Size = new System.Drawing.Size(140, 25);
            this.txt_WifiPassword.TabIndex = 9;
            // 
            // txt_WifiName
            // 
            this.txt_WifiName.Location = new System.Drawing.Point(132, 24);
            this.txt_WifiName.Name = "txt_WifiName";
            this.txt_WifiName.Size = new System.Drawing.Size(140, 25);
            this.txt_WifiName.TabIndex = 10;
            // 
            // frmNetworkParameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 614);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "frmNetworkParameters";
            this.Text = "网络参数";
            this.Load += new System.EventHandler(this.frmNetworkParameters_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_SettingPassword;
        private System.Windows.Forms.TextBox txt_Password;
        private System.Windows.Forms.TextBox txt_OldPassword;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmb_IP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_DNS;
        private System.Windows.Forms.TextBox txt_Gateway;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_SettingInternet;
        private System.Windows.Forms.TextBox txt_SubnetMask;
        private System.Windows.Forms.TextBox txt_deviceIP;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmb_WifiIP;
        private System.Windows.Forms.TextBox txt_WifideviceIP;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_WifiDNS;
        private System.Windows.Forms.TextBox txt_WifiGateway;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btn_SettingWifi;
        private System.Windows.Forms.TextBox txt_WifiPassword;
        private System.Windows.Forms.TextBox txt_WifiName;
        private System.Windows.Forms.Label label13;
    }
}