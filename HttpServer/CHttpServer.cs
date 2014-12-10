/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 20.10.2014
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Data;

namespace Server
{
	/// <summary>
	/// Description of Server.
	/// </summary>
	public class CHttpServer
	{
		#region Fields
		private Socket modServerSocket = null;
		private Socket modClientSocket = null;
		//private byte[] modBuffer = null;
		private string mvMessage = "";

		private IPEndPoint mvEndPoint = null;
		private IPAddress mvIpAdress = null;

		private static ManualResetEvent resetEvent = null;

		//private static CServer instance = null;
		#endregion

		#region Properties
		public Dictionary<Socket, byte[]> Clients
		{
			get;
			private set;
		}

		public string Message
		{
			get
			{
				return mvMessage;
			}
		}
		public IPEndPoint EndPoint
		{
			get
			{
				return mvEndPoint;
			}
			set
			{
				mvEndPoint = value;
			}
		}
		public IPAddress IpAdress
		{
			get
			{
				return mvIpAdress;
			}
			set
			{
				mvIpAdress = value;
			}
		}

		//public static CServer Instance
		//{
		//	get
		//	{
		//		if (instance == null)
		//			instance = new CServer();
		//		return instance;
		//	}
		//}
		#endregion

		#region Ctr
		public CHttpServer()
		{
			Clients = new Dictionary<Socket, byte[]>();
		}
		static CHttpServer()
		{
			resetEvent = new ManualResetEvent(false);
		}
		#endregion

		public void Bind(string host, int port)
		{
			var res = IPAddress.TryParse(host, out mvIpAdress);
			mvEndPoint = new IPEndPoint(IPAddress.Any, port);

			modServerSocket = new Socket(IpAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			modServerSocket.Bind(mvEndPoint);
		}

		public void Listen(int maxConnections)
		{
			modServerSocket.Listen(maxConnections);

			while (true) //|| modServerSocket != null)
			{
				resetEvent.Reset();

				Console.WriteLine("Listen..");
				//ThreadPool.QueueUserWorkItem(
				modServerSocket.BeginAccept(new AsyncCallback(AcceptCallBack), modServerSocket);

				resetEvent.WaitOne();
			}
		}

		protected void AcceptCallBack(IAsyncResult res)
		{
			try
			{
				var clientSocket = modServerSocket.EndAccept(res);
				Console.WriteLine("OnAccepted " + res.ToString());
				resetEvent.Set();

				if (Clients.ContainsKey(clientSocket))
					Clients.Add(clientSocket, null);

				Receive(clientSocket);
			}
			catch (Exception ex)
			{
				ProcessError(res, ex);
			}
		}

		private void Receive(Socket clientSocket)
		{
			byte[] buffer = new byte[clientSocket.ReceiveBufferSize];

			Clients[clientSocket] = buffer;
			try
			{
				clientSocket.BeginReceive(buffer, 0, buffer.Count(),
																			SocketFlags.None, new AsyncCallback(ReceiveCallBack),
																			clientSocket);
			}
			catch(Exception ex)
			{
				ProcessError(clientSocket, ex);
			}
		}


		protected virtual void ReceiveCallBack(IAsyncResult res)
		{
			try
			{
				var clientSocket = res.AsyncState as Socket;
				int bytesRead = clientSocket.EndReceive(res); //new AsyncCallback(this.OnSended)

				var arr = Clients[clientSocket];
				Array.Resize(ref arr, bytesRead);

				Clients[clientSocket] = ProccessAndAnswer(clientSocket, arr);
				Send(clientSocket);

			}
			catch (Exception ex)
			{
				var clientSocket = res.AsyncState as Socket;
				ProcessError(res, ex);
			}
		}
		private object lockObj = new object();
		protected virtual byte[] ProccessAndAnswer(Socket clientSocket, byte[] data)
		{
			lock (lockObj)
			{
				var request = Encoding.ASCII.GetString(data);
				var answer = HttpServer.RequestHandler.HandleReq(data);
				return answer;
			}
			//return new byte[0]{};
		}

		private void ProcessError(IAsyncResult res, Exception ex)
		{
			System.Console.WriteLine(ex.Message);

			var clientSocket = res.AsyncState as Socket;
			ProcessError(clientSocket, ex);
		}

		private void ProcessError(Socket clientSocket, Exception ex)
		{
			//if (clientSocket == null || clientSocket.Connected == false)
			CloseConnection(clientSocket);
			Clients.Remove(clientSocket);

			resetEvent.Set();
		}

		private void CloseConnection(Socket clientSocket)
		{
			if (clientSocket == null)
				return;

			if (clientSocket.Connected)
				return;

			clientSocket.Close();

			System.Console.WriteLine("Closed " + clientSocket.Connected);
		}

		public virtual void Send(Socket client)
		{
			byte[] data = Clients[client];
			client.BeginSend(data, 0, data.Length, SocketFlags.None,
								  new AsyncCallback(SendCallBack), client);
		}


		protected void SendCallBack(IAsyncResult res)
		{
			try
			{
				var clientSocket = res.AsyncState as Socket;
				clientSocket.EndSend(res);
				Receive(clientSocket);
			}
			catch (Exception ex)
			{
				ProcessError(res, ex);
			}
			//sendDone.Set();
		}

		public void Close()
		{
			modClientSocket.Close();
			modServerSocket.Close();
		}

	}
}
