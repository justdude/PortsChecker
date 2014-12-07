/*
 * Created by SharpDevelop.
 * User: Ivan
 * Date: 04.11.2014
 * Time: 22:41
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Client;

namespace Data
{
	/// <summary>
	/// Description of AbstractComputer.
	/// </summary>
	public abstract class AbstractComputer: IComputer
	{
		protected int mvPortNumber = 0;
		protected string mvHost = "127.0.0.1";
		protected ConnectionState mvState = ConnectionState.Disconnected;
		
		public AbstractComputer()
		{
			mvPortNumber = 0;
			mvHost = "127.0.0.1";
			mvState = ConnectionState.Disconnected;
		}

        public AbstractComputer(CCLient socket)
        {
            mvPortNumber = socket.Port;
            mvHost = socket.Host;
            mvState = socket.State;
        }
		
		public AbstractComputer(string host, int port, ConnectionState state) 
		{
			mvPortNumber = port;
			mvHost = host;
			mvState = state;
		}

        public CCLient ClientSocket { get; protected set; }

		
		public int PortNumber 
		{ 
			get {return mvPortNumber; } 
			set { mvPortNumber = value; } 
		}
		
		public string Host 
		{ 
			get {return mvHost; } 
			set { mvHost = value; } 
		}
		
		public ConnectionState State
		{ 
			get {return mvState; } 
			set { mvState = value; } 
		}
	}

}
