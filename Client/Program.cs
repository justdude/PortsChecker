/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 22.10.2014
 */
using System;
using Data;

namespace Client
{
	class Program
	{
		public static void Main(string[] args)
		{
			try
			{
				using(CCLient client = new CCLient())
				{
					client.Connect("127.0.0.1",1100);
					//client.SendMessage("!!dsada");
					
					ObjectInfo obj = new ObjectInfo("Hello world");
					client.Send( obj.ToByte());
					//string str = client.ReceiveMessage();
					//System.Console.WriteLine( "received: "+ str);
					client.Close();
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
	}
}