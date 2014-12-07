/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 19.10.2014
 */
using System;
using Client;

namespace Data
{
	public interface IComputer
	{
		int PortNumber { get; }
		string Host { get;  }
		ConnectionState State { get; }
	}
}
