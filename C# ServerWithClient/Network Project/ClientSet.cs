using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Network_Project
{
	class ClientSet
	{
		private delegate void AddTextDelegate(string text);
		private bool connected = false;
		private int port;
		private TcpListener server;
		private TcpClient client;
		private NetworkStream stream;
		private StreamReader reader;
		private StreamWriter writer;
		private FormChat parent;
		private Thread listenThread;
		private Thread receiveThread;

		public ClientSet(FormChat callingForm, int portNum)
		{
			this.parent = callingForm;
			this.port = portNum;

			listenThread = new Thread(new ThreadStart(Listen));
		}

		public void CloseAll()
		{
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

		public void SendMsg(string msg)
		{
			writer.Write(msg);
		}

		public void StartListen()
		{
			listenThread.Start();
		}

		private void Listen()
		{
			try
			{
				IPAddress ip = new IPAddress(0);

				server = new TcpListener(ip, port);

				server.Start();


				client = server.AcceptTcpClient();
				connected = true;

				//클라 연결 성공시 셋팅 부분
				stream = client.GetStream();
				reader = new StreamReader(stream);
				writer = new StreamWriter(stream);

				//값 받아오기
				receiveThread = new Thread(new ThreadStart(Receive));
				receiveThread.Start();
			}
			catch (Exception e)
			{ 
			
			}
		}

		private delegate void delBroadCastMsg(string str);
		private void Receive()
		{
			try
			{
				delBroadCastMsg broadCast = new delBroadCastMsg(parent.BroadCastMsg);
				while (connected == true)
				{
					Thread.Sleep(1);

					if (stream.CanRead == true)
					{
						string tmpStr = reader.ReadLine();

						if (tmpStr.Length > 0)
						{
							//parent.BroadCastMsg(tmpStr);
							broadCast.BeginInvoke(tmpStr,null,null);
							//Invoke(broadCast,tmpStr);
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
