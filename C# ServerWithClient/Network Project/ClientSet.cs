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
		public bool connected = false;
		private int port;
		private NetworkStream stream;
		private StreamReader reader;
		private StreamWriter writer;
		private FormChat parent;
		private Thread receiveThread;
		public string name = "";
		bool first = true;

		public ClientSet(FormChat parent, NetworkStream stream)
		{
			this.parent = parent;
			this.stream = stream;
			writer = new StreamWriter(stream);
			reader = new StreamReader(stream);
			receiveThread = new Thread(new ThreadStart(Receive));
			connected = true;
		}

		public void CloseAll()
		{
			connected = false;
			if (reader != null)
			{
				reader.Close();
				reader = null;
			}
			if (writer != null)
			{
				writer.Close();
				writer = null;
			}
			if (stream != null)
			{
				stream.Close();
				stream = null;
			}
			if (receiveThread != null)
				receiveThread.Abort();
		}

		public void SendMsg(string msg)
		{
			writer.WriteLine(msg.Crypt());
			writer.Flush();
		}

		public void StartReceive()
		{
			receiveThread.Start();
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
						string tmpStr = reader.ReadLine().Decrypt();

						if (first)
						{
							name = tmpStr.Split('>')[0];
							first = false;
							tmpStr = name + "ㅴ" + tmpStr;
						}
						if (tmpStr.Length > 0)
						{
							broadCast.BeginInvoke(tmpStr,null,null);
						}
					}
				}
			}
			catch (Exception e)
			{
				CloseAll();
			}
		}
	}
}
