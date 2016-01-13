using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IpBox;
using System.Net;
using System.Net.Sockets;

namespace Network_Project
{
	public partial class FormStart : Form
	{
		public static string My_IP
		{
			get
			{
				IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
				string ClientIP = string.Empty;
				for (int i = 0; i < host.AddressList.Length; i++)
				{
					if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
					{
						ClientIP = host.AddressList[i].ToString();
					}
				}
				return ClientIP;
			}
		}
		internal IpBox.IpBox ipBox = new IpBox.IpBox();
		public FormStart()
		{
			InitializeComponent();
		}
		private void radioClient_CheckedChanged(object sender, EventArgs e)
		{
			ipBox.Enabled = radioClient.Checked;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//ip입력 박스 초기화
			ipBox.Location = txtIp.Location;
			ipBox.Enabled = false;
			this.Controls.Add(ipBox);

			//자신ip 출력
			lbIpAdress.Text = My_IP;
		}

		private void btnCopyIP_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(My_IP);
			MessageBox.Show("IP주소 " + My_IP + "를 클립보드에 저장했습니다.");
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			if (txtName.Text == "")
				MessageBox.Show("이름을 입력하세요");
			else
			{
				FormChat frmChat = new FormChat(this);
				frmChat.Show();

			}
		}
	}
}
