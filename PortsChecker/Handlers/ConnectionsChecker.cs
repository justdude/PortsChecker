/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 19.10.2014
 */
using System;
using System.Linq;
using System.Collections.Generic;

using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

using Data;

namespace PortsChecker.Handlers
{
	/// <summary>
	/// Description of ConnectionsChecker.
	/// </summary>
	public class ConnectionsChecker
	{
		
		
		public ConnectionsChecker()
		{
		}
		
		public static IPAddress GetOwnIPAdress()
		{
			return Dns.GetHostEntry(Dns.GetHostName()).AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
		}
		
		
	 	public static List<PortInfo> GetOpenPort()
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpEndPoints = properties.GetActiveTcpListeners();
            TcpConnectionInformation[] tcpConnections = properties.GetActiveTcpConnections();

            return tcpConnections.Select(p =>
                {
                    return new PortInfo(
                        i: p.LocalEndPoint.Port,
                        local: String.Format("{0}:{1}", p.LocalEndPoint.Address, p.LocalEndPoint.Port),
                        remote: String.Format("{0}:{1}", p.RemoteEndPoint.Address, p.RemoteEndPoint.Port),
                        state: p.State.ToString());
                }).ToList();
        }
	}
}
