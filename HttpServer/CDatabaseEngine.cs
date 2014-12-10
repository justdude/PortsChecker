using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpServer
{

	public class CCommand 
	{

		public string Name {get; set;}
		public string QueryType {get; set;}
		public string Param {get; set;}
		public bool IsProcessCollection
		{
			get
			{
				return string.IsNullOrEmpty(Param);
			}
		}

		public CCommand(string name, string queryType, string param)
		{
			Name = name;
			QueryType = queryType;
			Param = param;
		}
	}

	public class CDatabaseEngine
	{

		public CColCClient Clients { get; private set; }
		public Stack<CCommand> commands = new Stack<CCommand>();

		CDatabaseEngine()
		{
			Clients = new CColCClient();
		}


		public void AddCommand(string name, string queryType, string param)
		{
			var command = new CCommand(name, queryType, param);
			commands.Push(command);
		}
		public void Clear()
		{
			commands.Clear();
		}

		public string ProccessCommand()
		{
			var first = commands.FirstOrDefault();

			CColCClient clients = null;

			//switch (first.Name)
			//{
			//	case (Const.Clients):
			//		if (first.IsProcessCollection)
			//			switch (first.QueryType)
			//			{
			//				case (Const.GET):
			//						return Clients.Serialize();
			//				case (Const.DELETE):
			//					return "";
			//				case (Const.PUT):
			//					return "";
			//				case (Const.POST):
			//					return "";
			//			}
				
			//		switch (first.QueryType)
			//			{
			//				case (Const.GET):
			//						return Clients.Serialize();
			//				case (Const.DELETE):
			//					return "";
			//				case (Const.PUT):
			//					return "";
			//				case (Const.POST):
			//					return "";
			//			}


			//				clients = Clients.Select(first.Param);


			//		}
			//	case (Const.Threads):
			//		break;
			//}
			return "";
		}

		public object SelectClient(string id)
		{
			var client = Clients.FirstOrDefault(p => p.ClienId == id);
			if (client == null)
				return string.Empty;

			return JsonConvert.SerializeObject(client, Formatting.None);
		}

		public string Serialize()
		{
			//var str = JsonConvert.SerializeObject(item, Formatting.None);
			//var answer = JsonConvert.DeserializeObject<RestItem<string>>(str);
			return "";
		}

	}
}
