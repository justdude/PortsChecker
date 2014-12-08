using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Server
{
	public class CClientBase: Socket
	{

		public byte[] ReceiveBuffer { get; set; }
		public int ReceiveBufferLength
		{
			get
			{
				if (ReceiveBuffer == null)
					return 0;

				return ReceiveBuffer.Length;
			}
		}


		public CClientBase(SocketInformation inf): base(inf)
		{
			
		}

		public CClientBase(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType):base(addressFamily, socketType, protocolType)
		{
		}


	}
}
