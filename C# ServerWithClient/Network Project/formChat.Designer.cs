namespace Network_Project
{
	partial class FormChat
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
			this.chatBox = new System.Windows.Forms.ListBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.usersBox = new System.Windows.Forms.ListBox();
			this.inputBox = new System.Windows.Forms.TextBox();
			this.serverLog = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// chatBox
			// 
			this.chatBox.FormattingEnabled = true;
			this.chatBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.chatBox.ItemHeight = 12;
			this.chatBox.Location = new System.Drawing.Point(12, 12);
			this.chatBox.Name = "chatBox";
			this.chatBox.Size = new System.Drawing.Size(349, 424);
			this.chatBox.TabIndex = 0;
			// 
			// btnSend
			// 
			this.btnSend.Location = new System.Drawing.Point(280, 442);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(81, 21);
			this.btnSend.TabIndex = 2;
			this.btnSend.Text = "전송(Enter)";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(367, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "접속자 명단";
			// 
			// usersBox
			// 
			this.usersBox.FormattingEnabled = true;
			this.usersBox.ItemHeight = 12;
			this.usersBox.Location = new System.Drawing.Point(369, 27);
			this.usersBox.Name = "usersBox";
			this.usersBox.Size = new System.Drawing.Size(96, 220);
			this.usersBox.TabIndex = 4;
			// 
			// inputBox
			// 
			this.inputBox.Location = new System.Drawing.Point(12, 442);
			this.inputBox.Name = "inputBox";
			this.inputBox.Size = new System.Drawing.Size(262, 21);
			this.inputBox.TabIndex = 1;
			// 
			// serverLog
			// 
			this.serverLog.FormattingEnabled = true;
			this.serverLog.ItemHeight = 12;
			this.serverLog.Location = new System.Drawing.Point(369, 278);
			this.serverLog.Name = "serverLog";
			this.serverLog.Size = new System.Drawing.Size(96, 184);
			this.serverLog.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(367, 263);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 12);
			this.label2.TabIndex = 6;
			this.label2.Text = "Server Log";
			// 
			// FormChat
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(477, 474);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.serverLog);
			this.Controls.Add(this.usersBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnSend);
			this.Controls.Add(this.inputBox);
			this.Controls.Add(this.chatBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormChat";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "채팅 후로그램";
			this.Load += new System.EventHandler(this.FormChat_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox chatBox;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox usersBox;
		private System.Windows.Forms.TextBox inputBox;
		private System.Windows.Forms.ListBox serverLog;
		private System.Windows.Forms.Label label2;
	}
}