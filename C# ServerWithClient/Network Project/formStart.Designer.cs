namespace Network_Project
{
    partial class FormStart
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
			this.con1 = new System.Windows.Forms.GroupBox();
			this.radioClient = new System.Windows.Forms.RadioButton();
			this.radioHost = new System.Windows.Forms.RadioButton();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnCopyIP = new System.Windows.Forms.Button();
			this.lbNothing1 = new System.Windows.Forms.Label();
			this.txtIp = new System.Windows.Forms.TextBox();
			this.lbIpAdress = new System.Windows.Forms.Label();
			this.lbNothing2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.con1.SuspendLayout();
			this.SuspendLayout();
			// 
			// con1
			// 
			this.con1.Controls.Add(this.radioClient);
			this.con1.Controls.Add(this.radioHost);
			this.con1.Location = new System.Drawing.Point(12, 12);
			this.con1.Name = "con1";
			this.con1.Size = new System.Drawing.Size(111, 61);
			this.con1.TabIndex = 8;
			this.con1.TabStop = false;
			this.con1.Text = "선택";
			// 
			// radioClient
			// 
			this.radioClient.AutoSize = true;
			this.radioClient.Location = new System.Drawing.Point(6, 35);
			this.radioClient.Name = "radioClient";
			this.radioClient.Size = new System.Drawing.Size(91, 16);
			this.radioClient.TabIndex = 2;
			this.radioClient.Text = "다른 방 참가";
			this.radioClient.UseVisualStyleBackColor = true;
			this.radioClient.CheckedChanged += new System.EventHandler(this.radioClient_CheckedChanged);
			// 
			// radioHost
			// 
			this.radioHost.AutoSize = true;
			this.radioHost.Checked = true;
			this.radioHost.Location = new System.Drawing.Point(6, 13);
			this.radioHost.Name = "radioHost";
			this.radioHost.Size = new System.Drawing.Size(75, 16);
			this.radioHost.TabIndex = 1;
			this.radioHost.TabStop = true;
			this.radioHost.Text = "방 만들기";
			this.radioHost.UseVisualStyleBackColor = true;
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(173, 81);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(58, 23);
			this.btnStart.TabIndex = 7;
			this.btnStart.Text = "시작";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnCopyIP
			// 
			this.btnCopyIP.Location = new System.Drawing.Point(240, 81);
			this.btnCopyIP.Name = "btnCopyIP";
			this.btnCopyIP.Size = new System.Drawing.Size(56, 23);
			this.btnCopyIP.TabIndex = 11;
			this.btnCopyIP.Text = "IP 복사";
			this.btnCopyIP.UseVisualStyleBackColor = true;
			this.btnCopyIP.Click += new System.EventHandler(this.btnCopyIP_Click);
			// 
			// lbNothing1
			// 
			this.lbNothing1.AutoSize = true;
			this.lbNothing1.Location = new System.Drawing.Point(129, 54);
			this.lbNothing1.Name = "lbNothing1";
			this.lbNothing1.Size = new System.Drawing.Size(58, 12);
			this.lbNothing1.TabIndex = 10;
			this.lbNothing1.Text = "Your IP : ";
			// 
			// txtIp
			// 
			this.txtIp.Location = new System.Drawing.Point(190, 20);
			this.txtIp.Name = "txtIp";
			this.txtIp.Size = new System.Drawing.Size(106, 21);
			this.txtIp.TabIndex = 9;
			this.txtIp.Text = "IP컨트롤 위치";
			this.txtIp.Visible = false;
			// 
			// lbIpAdress
			// 
			this.lbIpAdress.AutoSize = true;
			this.lbIpAdress.Location = new System.Drawing.Point(188, 54);
			this.lbIpAdress.Name = "lbIpAdress";
			this.lbIpAdress.Size = new System.Drawing.Size(87, 12);
			this.lbIpAdress.TabIndex = 12;
			this.lbIpAdress.Text = "ip adress here";
			// 
			// lbNothing2
			// 
			this.lbNothing2.AutoSize = true;
			this.lbNothing2.Location = new System.Drawing.Point(129, 23);
			this.lbNothing2.Name = "lbNothing2";
			this.lbNothing2.Size = new System.Drawing.Size(57, 12);
			this.lbNothing2.TabIndex = 13;
			this.lbNothing2.Text = "Host IP : ";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 86);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 12);
			this.label1.TabIndex = 14;
			this.label1.Text = "이름";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(51, 83);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(72, 21);
			this.txtName.TabIndex = 15;
			// 
			// FormStart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(303, 110);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lbNothing2);
			this.Controls.Add(this.lbIpAdress);
			this.Controls.Add(this.con1);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.btnCopyIP);
			this.Controls.Add(this.lbNothing1);
			this.Controls.Add(this.txtIp);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormStart";
			this.Text = "채팅 후로그램";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.con1.ResumeLayout(false);
			this.con1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.GroupBox con1;
        internal System.Windows.Forms.RadioButton radioClient;
        internal System.Windows.Forms.RadioButton radioHost;
        internal System.Windows.Forms.Button btnStart;
        internal System.Windows.Forms.Button btnCopyIP;
        internal System.Windows.Forms.Label lbNothing1;
        internal System.Windows.Forms.TextBox txtIp;
		private System.Windows.Forms.Label lbIpAdress;
		internal System.Windows.Forms.Label lbNothing2;
		private System.Windows.Forms.Label label1;
		internal System.Windows.Forms.TextBox txtName;

    }
}

