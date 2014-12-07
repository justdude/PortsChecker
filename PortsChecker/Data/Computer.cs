/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 19.10.2014
 */
using System;
using Client;

namespace Data
{
	/// <summary>
	/// Description of Computer.
	/// </summary>
	public class Computer : AbstractComputer
	{

		public Computer(int port, string ip, ConnectionState state): 
			base(ip, port, state)
		{

		}
	}
}
