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
		private NetworkStream stream;
		private StreamReader reader;
		private StreamWriter writer;
		private FormChat parent;
		private Thread receiveThread;

		public ClientSet(FormChat parent, NetworkStream stream)
		{
			this.parent = parent;
			this.stream = stream;
			writer = new StreamWriter(stream);
			reader = new StreamReader(stream);
			receiveThread = new Thread(new ThreadStart(Receive));
		}

		public void CloseAll()
		{
			if (reader != null)
				reader.Close();
			if (writer != null)
				writer.Close();
			
			if (receiveThread != null)
				receiveThread.Abort();
		}

		public void SendMsg(string msg)
		{
			writer.WriteLine(msg);
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
				while (/*connected == */true)
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
