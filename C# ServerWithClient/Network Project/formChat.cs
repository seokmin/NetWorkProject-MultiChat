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
using System.Collections;

namespace Network_Project
{
	public partial class FormChat : Form
	{
		public FormChat(Form callingForm)
		{
			startForm = callingForm as FormStart;
			InitializeComponent();
		}
		public FormChat()
		{
			InitializeComponent();
		}

		struct structQuestion
		{
			public NetworkStream Stream;
			public StreamReader Reader;
			public StreamWriter Writer;
			public Thread Thread;
			public Thread ReceiveThread;
			public TcpListener Server;
			public TcpClient Client;
			/*public bool Connected;*/
		}
		struct structClientMode
		{
			public TcpListener server;
			public TcpClient client;
			public string name;
			public IPAddress destIp;
			public NetworkStream stream;
			public StreamReader reader;
			public StreamWriter writer;
			public Thread receiveThread;
			public Thread questionThread;
			public Thread thread;
			public string names;
		}
		structQuestion stQuestion;
		structClientMode stClient;

		Dictionary<int, ClientSet> clientDict;


		bool hostMode;

		int lastID = 0;
		int defaultPort = 9282;
		string myIp;

		private FormStart startForm = null;

		private delegate void AddTextDelegate(string text);
		private delegate void AddLogDelegate(string text);
		private delegate void ConnectDelegate(IPAddress ip, int port);
		private delegate void VoidDelegate();

		public void PrintLog(string text)
		{
			serverLog.Items.Add(text);
			serverLog.TopIndex = serverLog.Items.Count - 1;
		}

		private void PrintToChatBox(string text)
		{
			chatBox.Items.Add(text + "\r\n");
			chatBox.TopIndex = chatBox.Items.Count - 1;

		}

		private void StartClient()
		{
			try
			{/*
				AddTextDelegate AddTextToChatBox = new AddTextDelegate(PrintToChatBox);
				ConnectDelegate connect = new ConnectDelegate(ConnectForDelegate);
				VoidDelegate setStream = new VoidDelegate(SetStreamForDelegate);
*/

				stClient.client = new TcpClient();

				//question server 접속 시도
				PrintToChatBox(stClient.destIp + " Question Server 접속 시도..");
				stClient.client.Connect(stClient.destIp, defaultPort);
				/*stClient.stream = stClient.client.GetStream();*/
				stClient.stream = stClient.client.GetStream();
				PrintToChatBox("Question Server 접속 완료..");

				stClient.reader = new StreamReader(stClient.stream);
				stClient.writer = new StreamWriter(stClient.stream);

				stClient.thread = new Thread(new ThreadStart(ReceiveForClient));
				stClient.thread.Start();

			}
			catch (Exception e)
			{
				MessageBox.Show("서버 연결 실패");
				this.Close();
			}
		}

		private void SendQuestion()
		{
			while (true)
			{
				Thread.Sleep(1);
				stClient.writer.WriteLine("|||" + myIp);
				stClient.writer.Flush();
			}
		}
		private void RunServer()
		{
			stQuestion.Thread = new Thread(new ThreadStart(QuestionServer));
			stQuestion.Thread.Start();
		}

		private void FormChat_Load(object sender, EventArgs e)
		{
			clientDict = new Dictionary<int, ClientSet>();
			myIp = startForm.txtIp.Text;
			hostMode = startForm.radioHost.Checked;
			if (hostMode)
				stClient.destIp = IPAddress.Parse("127.0.0.1");
			else
				stClient.destIp = startForm.ipBox.GetIPAddress;
			//이벤트 등록
			this.FormClosing += FormChat_Closing;
			this.inputBox.KeyDown += inputBox_KeyDown;

			stClient.name = startForm.txtName.Text;



			startForm.btnStart.Enabled = false;
			startForm.txtName.Enabled = false;


			inputBox.Select();

			//스레드 생성후 실행

			if (hostMode == true)
				RunServer();
			StartClient();
		}

		private void FormChat_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				startForm.btnStart.Enabled = true;
				startForm.txtName.Enabled = true;

				if (stClient.reader != null)
					stClient.reader.Close();
				if (stClient.writer != null)
					stClient.writer.Close();
				if (stClient.server != null)
					stClient.server.Stop();
				if (stClient.client != null)
					stClient.client.Close();
				if (stClient.receiveThread != null)
					stClient.receiveThread.Abort();
				if (stClient.questionThread != null)
					stClient.questionThread.Abort();
				if (stClient.thread != null)
					stClient.thread.Abort();
				if (stQuestion.Reader != null)
					stQuestion.Reader.Close();
				if (stQuestion.Writer != null)
					stQuestion.Writer.Close();
				if (stQuestion.Server != null)
					stQuestion.Server.Stop();
				if (stQuestion.Client != null)
					stQuestion.Client.Close();
				if (stQuestion.ReceiveThread != null)
					stQuestion.ReceiveThread.Abort();
				if (stQuestion.Thread != null)
					stQuestion.Thread.Abort();

				foreach (KeyValuePair<int, ClientSet> i in clientDict)
				{
					i.Value.CloseAll();
				}
			}
			catch
			{ }
		}


		//클라에서 서버로 메시지 전송
		private void SendMsgToServer()
		{
			if (stClient.client.Connected)
			{
				stClient.writer.WriteLine(stClient.name + ">>" + inputBox.Text);
				stClient.writer.Flush();
			}
			inputBox.Clear();
		}

		//모든 클라에 메시지 전송
		public void BroadCastMsg(string message)
		{
			string names = "";
			ArrayList removeList = new ArrayList();
			foreach (KeyValuePair<int, ClientSet> i in clientDict)
			{
				if (i.Value.connected)
					names += i.Value.name + "ㅫ";
				else
					removeList.Add(i.Key);
			}
			foreach (int i in removeList)
				clientDict.Remove(i);
			foreach (KeyValuePair<int, ClientSet> i in clientDict)
			{
				i.Value.SendMsg(names + "ㅩ" + message);
			}
		}

		private void inputBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				SendMsgToServer();
			}
		}
		private void btnSend_Click(object sender, EventArgs e)
		{
			SendMsgToServer();
		}

		private delegate void delAddNewClient(int id, NetworkStream stream);
		private void QuestionServer()
		{
			try
			{
				AddTextDelegate AddText = new AddTextDelegate(PrintToChatBox);
				AddLogDelegate AddLog = new AddLogDelegate(PrintLog);
				delAddNewClient addNewClient = new delAddNewClient(AddNewClient);

				IPAddress ip = new IPAddress(0);
				int port = defaultPort;

				stQuestion.Server = new TcpListener(ip, port);

				stQuestion.Server.Start();

				Invoke(AddText, "서버가 켜졌어용!");
				Invoke(AddLog, "server on");

				while (true)
				{
					stQuestion.Client = stQuestion.Server.AcceptTcpClient();
					Invoke(AddLog, "new client");
					Invoke(addNewClient, lastID++, stQuestion.Client.GetStream());
				}

			}
			catch (Exception e)
			{
			}
		}

		private void AddNewClient(int clientID, NetworkStream stream)
		{
			clientDict.Add(clientID, new ClientSet(this, stream));
			clientDict[clientID].StartReceive();
		}

		private void RefreshNames(string names)
		{
			usersBox.Items.Clear();
			foreach (string i in names.Split('ㅫ'))
				if(i.Length>0)
					usersBox.Items.Add(i);
		}
		private delegate void delRefresh(string names);
		private void ReceiveForClient()
		{
			try
			{
				delRefresh refreshNames = new delRefresh(RefreshNames);
				AddTextDelegate AddText = new AddTextDelegate(PrintToChatBox);

				while (stClient.client.Connected)
				{
					Thread.Sleep(1);

					if (stClient.stream.CanRead == true)
					{
						string[] tmpStr = new string[3];
						tmpStr[0] = stClient.reader.ReadLine();
						tmpStr = tmpStr[0].Split('ㅩ');
						Invoke(refreshNames, tmpStr[0]);
						if (tmpStr[1].Contains('ㅴ'))
						{
							tmpStr = tmpStr[1].Split('ㅴ');
							Invoke(AddText, tmpStr[0] + "님께서 입장하셨습니다.");
							//tmpStr[0] = tmpStr[1];
						}
						if (tmpStr[1].Length > 0)
						{
							Invoke(AddText, tmpStr[1]);
						}
					}
				}
			}
			catch (Exception e)
			{
			}

		}

	}
}
