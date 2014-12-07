/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 20.10.2014
 */
using System;
using System.Threading;
using System.Diagnostics;

namespace Server
{
	class Program
	{

		public static void Main(string[] args)
		{
		
			CServer.Instance.Bind("127.0.0.1",11102);
			CServer.Instance.Listen(100);
			CServer.Instance.Close();
			Console.ReadKey(true);
			Console.Write("Press any key to continue . . . ");
		}
	}
}