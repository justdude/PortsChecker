/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 21.10.2014
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
	
	public enum ConnectionState
	{
		Connected,
		Disconnected,
		Send,
		Receive
	}
	
	/// <summary>
	/// Description of SingletonClass1.
	/// </summary>
	public sealed class CCLient: IDisposable
	{
		
		public string Host { get; private set;}
		public int Port { get; private set;}
		
		public ConnectionState State { get; private set;}
		
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
			Host = host;
			Port = port;
			try
			{
				mvClient.Connect(host,port);
				this.NetworkStream = mvClient.GetStream();
				
				State = ConnectionState.Connected;
			}
			catch(Exception ex)
			{
				State = ConnectionState.Disconnected;
			}
		}
		
		public void Send(byte[] arr)
		{
			State = ConnectionState.Send;
			NetworkStream.Write(arr, 0, arr.Length);
		}
		
		//byte[] sendBytes = Encoding.ASCII.GetBytes(str);	
		public byte[] ReceiveMessage()
		{
			State = ConnectionState.Receive;
			byte[] bytes = new byte[mvClient.ReceiveBufferSize];
			NetworkStream.Read(bytes, 0, bytes.Length);
			return bytes;
		}
		
		public void Close()
		{
			State = ConnectionState.Disconnected;
			mvClient.Close();
		}
		
		public static IPEndPoint ComputePoint(string host, int port)
		{
			IPAddress adress = null;
			IPAddress.TryParse(host, out adress);
			if (adress == null) 
				return null;
			return new IPEndPoint( new IPAddress(adress.Address), port);
		}
		
		
		public void Dispose()
		{
			mvClient.Close();
			mvClient = null;
		}
	}
}
