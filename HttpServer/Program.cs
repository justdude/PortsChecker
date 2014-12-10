using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
namespace HttpServer
{
	class Program
	{
		static void Main(string[] args)
		{
			//CHttpServer serv = new CHttpServer();

			//serv.Bind("127.0.0.1", 8080);
			//serv.Listen(100);
			//serv.Close();
			TestingJson();
			//WebServer ws = new WebServer(SendResponse, "http://localhost:8080/test/");
			//ws.Run();

			Console.ReadKey(true);
			Console.Write("Press any key to continue . . . ");

		}

		private static void TestingJson()
		{
			RestItem<string> item = new RestItem<string>();
			item.ItemValue = "sadsad";
			item.ChildItem = new RestItem<string>();
			var str = JsonConvert.SerializeObject(item, Formatting.None);
			var answer = JsonConvert.DeserializeObject<RestItem<string>>(str);
		}


		public static string SendResponse(HttpListenerRequest request)
		{
			return string.Format("<HTML><BODY>My web page.<br>{0}</BODY></HTML>", DateTime.Now);
		}
	}
}
