/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 21.10.2014
 */
using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
	/// <summary>
	/// Description of SingletonClass1.
	/// </summary>
	public sealed class CCLient: IDisposable
	{
		private NetworkStream mvNetworkStream = null;
		public NetworkStream NetworkStream 
		{
			get
			{
				return mvNetworkStream;
			}
			private set
			{
				mvNetworkStream = value;
			}
		}
		
		private TcpClient mvClient = null;
		public TcpClient TcpClient 
		{
			get
			{
				return mvClient;
			}
			private set
			{
				mvClient = value;
			}
		}
		
		
		public CCLient()
		{
			mvClient = new TcpClient();
		}
		
		public void Connect(string host, int port)
		{
			mvClient.Connect(host,port);
			this.NetworkStream = mvClient.GetStream();
		}
		
		public void SendMessage(string str)
		{
			byte[] sendBytes = Encoding.ASCII.GetBytes(str);
			NetworkStream.Write(sendBytes, 0, sendBytes.Length);
		}
		
		public void Send(byte[] arr)
		{
			NetworkStream.Write(arr, 0, arr.Length);
		}
		
		
		public string ReceiveMessage()
		{
			byte[] bytes = new byte[mvClient.ReceiveBufferSize];
			NetworkStream.Read(bytes, 0, bytes.Length);
			return Encoding.UTF8.GetString(bytes);
		}
		
		public void Close()
		{
			mvClient.Close();
		}
		
		public static IPEndPoint ComputePoint(string host, int port)
		{
			IPAddress adress = null;
			IPAddress.TryParse(host, out adress);
			if (adress == null) 
				return null;
			return new IPEndPoint(adress.Address,port);
		}
		
		
		public void Dispose()
		{
			mvClient.Close();
			mvClient = null;
		}
	}
}
