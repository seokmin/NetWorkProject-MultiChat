using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Network_Project
{
	public partial class FormChat : Form
	{
		string name;

		TcpListener server;
		TcpClient client;

		StreamReader reader;
		StreamWriter writer;

		NetworkStream stream;

		Thread receiveThread;

		bool hostMode;
		bool connected;

		int startPort;
		int nextPort = 9282;

		IPAddress destIp;

		private delegate void AddTextDelegate(string strText);

		private void PrintMsg(string text)
		{
			chatBox.Items.Add(text + "\r\n");
			chatBox.TopIndex = chatBox.Items.Count - 1;
		}

		private void RunServer()
		{
			Thread ListenThread = new Thread(new ThreadStart(Listen));
			ListenThread.Start();
		}

		private void InitializeClient()
		{
			try
			{
				destIp = startForm.ipBox.GetIPAddress;
				client = new TcpClient();

				client.Connect(destIp, nextPort);

				stream = client.GetStream();
				connected = true;
				PrintMsg(destIp + " 연결 완료!");

				reader = new StreamReader(stream);
				writer = new StreamWriter(stream);

				receiveThread = new Thread(new ThreadStart(Receive));
				receiveThread.Start();
			}
			catch (Exception e)
			{
				MessageBox.Show("서버 연결 실패");
				this.Close();
			}
		}

		private FormStart startForm = null;
		public FormChat(Form callingForm)
		{
			startForm = callingForm as FormStart;
			InitializeComponent();
		}

		public FormChat()
		{
			InitializeComponent();
		}

		private void FormChat_Load(object sender, EventArgs e)
		{
			hostMode = startForm.radioHost.Checked;
			//이벤트 등록
			this.FormClosing += FormChat_Closing;
			this.inputBox.KeyDown += inputBox_KeyDown;

			name = startForm.txtName.Text;

			//소켓 생성
			Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
			sck.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);




			startForm.btnStart.Enabled = false;
			startForm.txtName.Enabled = false;


			inputBox.Select();

			//스레드 생성후 실행

			if (hostMode == true)
				RunServer();
			else
				InitializeClient();
		}

		private void FormChat_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			startForm.btnStart.Enabled = true;
			startForm.txtName.Enabled = true;

			connected = false;

			if (reader != null)
				reader.Close();
			if (writer != null)
				writer.Close();
			if (server != null)
				server.Stop();
			if (client != null)
				client.Close();
			if (receiveThread != null)
				receiveThread.Abort();
		}


		//send 훼미리
		private void SendMsg()
		{
			PrintMsg(name + ">>" + inputBox.Text);
			if (connected)
			{
				writer.WriteLine(inputBox.Text);
				writer.Flush();
			}
			inputBox.Clear();
		}
		private void inputBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SendMsg();
			}
		}
		private void btnSend_Click(object sender, EventArgs e)
		{
			SendMsg();
		}

		private void Listen()
		{
			try
			{
				AddTextDelegate AddText = new AddTextDelegate(PrintMsg);

				IPAddress ip = new IPAddress(0);
				int port = nextPort;

				server = new TcpListener(ip, port);

				server.Start();

				Invoke(AddText, "서버가 켜졌어용!");

				client = server.AcceptTcpClient();
				connected = true;
				Invoke(AddText, "클라와 연결됐당!...");

				//클라 연결 성공시 셋팅 부분
				stream = client.GetStream();
				reader = new StreamReader(stream);
				writer = new StreamWriter(stream);

				//값 받아오기
				receiveThread = new Thread(new ThreadStart(Receive));
				receiveThread.Start();
			}
			catch (Exception e)
			{ }
		}

		private void Receive()
		{
			try
			{
				AddTextDelegate AddText = new AddTextDelegate(PrintMsg);

				while (connected == true)
				{
					Thread.Sleep(1);

					if (stream.CanRead == true)
					{
						string tmpStr = reader.ReadLine();

						if (tmpStr.Length > 0)
						{
							Invoke(AddText, "점마>>" + tmpStr + "");
						}
					}
				}
			}
			catch (Exception e)
			{ }

		}
	}
}
