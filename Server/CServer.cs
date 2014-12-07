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
	public sealed class CServer
	{
		
		private Socket modServerSocket = null;
		private Socket modClientSocket = null;
		private byte[] modBuffer = null;
		private string mvMessage = "";
		
		private IPEndPoint mvEndPoint = null;
		private IPAddress mvIpAdress = null;
		
		private static CommandsAnswers modCommandsAnswers = null;
		
		private static ManualResetEvent resetEvent = null;
		private static ManualResetEvent sendDone = new ManualResetEvent(false);
		
		private static CServer instance = null;
		
		static CServer()
		{
			modCommandsAnswers = new CommandsAnswers();
			resetEvent = new ManualResetEvent(false);
		}


        public List<Socket> Clients
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
		
		public static CServer Instance {
			get {
				if (instance == null)
					instance = new CServer();
				return instance;
			}
		}
		
		private CServer()
		{
            Clients = new List<Socket>();
		}
		
		public void Bind(string host, int port)
		{
			var res = IPAddress.TryParse(host, out mvIpAdress);
			mvEndPoint = new IPEndPoint( mvIpAdress.Address, port);
			
			modServerSocket = new Socket( IpAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			modServerSocket.Bind( mvEndPoint );
		}
		
		public void Listen(int maxConnections)
		{
			modServerSocket.Listen(maxConnections);
			
			while(true || modServerSocket != null)
			{
				resetEvent.Reset();
				
				Console.WriteLine("Listen..");
				modServerSocket.BeginAccept( new AsyncCallback(this.AcceptCallBack), modServerSocket);
				
				resetEvent.WaitOne();
			}
		}
		
		protected void AcceptCallBack(IAsyncResult res)
		{
			resetEvent.Set();
			
			Console.WriteLine("OnAccepted " +res.ToString());
			
			try
			{
				modClientSocket = modServerSocket.EndAccept(res);

                if (!Clients.Exists(p => p == modClientSocket))
                    Clients.Add(modClientSocket);

                Receive(modClientSocket, ref modBuffer);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

        private void Receive(Socket clientSocket, ref byte[] buffer)
        {
            buffer = new byte[clientSocket.ReceiveBufferSize];
            clientSocket.BeginReceive(buffer, 0, buffer.Count(),
                                         SocketFlags.None, new AsyncCallback(ReceiveCallBack),
                                         clientSocket);
        }
		

		protected void ReceiveCallBack(IAsyncResult res)
		{
			try
			{
                var clientSocket = res.AsyncState as Socket;
                int bytesRead = clientSocket.EndReceive(res); //new AsyncCallback(this.OnSended)
				
				Array.Resize(ref modBuffer, bytesRead);
				
				var objInfo = new ObjectInfo();
				objInfo.ToObjectInfo(modBuffer);
				
				object targetAnswer = modCommandsAnswers.executor.Execute(objInfo.Command, objInfo.TargetObject);
				
				if (targetAnswer != null)
                //if (objInfo.IsNeedAnswer)
				{
					var answerObjInfo = new ObjectInfo(Commands.VOID, targetAnswer);
					modBuffer = answerObjInfo.ToByte();
				}

                Send(clientSocket, ref modBuffer);
				//sendDone.WaitOne();
				//modClientSocket.BeginSend(

			}
			catch(Exception ex)
			{
				
			}
		}
		
		public void Send(Socket client, ref  byte[] data)
		{
			client.BeginSend(data, 0, data.Length, SocketFlags.None, 
                          new AsyncCallback( SendCallBack), client);
		}
		
		
		protected void SendCallBack(IAsyncResult res)
		{
			try
			{
                var clientSocket = res.AsyncState as Socket;
                clientSocket.EndSend(res);
                Receive(clientSocket, ref modBuffer);
			}
			catch(Exception ex)
			{
				
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
