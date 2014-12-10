using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpServer
{

	public class CColCClient : List<CClient>
	{
		public CColCClient()
			: base()
		{

		}

		public CColCClient(IEnumerable<CClient> items)
			: base(items)
		{
		}

		public bool Insert(CClient thread)
		{
			if (this.Contains(thread))
				return false;

			this.Add(thread);
			return true;
		}

		public CColCClient Select(string id)
		{
			return new CColCClient(this.Where(p => p.ClienId == id));
		}

		public bool Delete(string id)
		{
			var item = this.FirstOrDefault(p => p.ClienId == id);
			if (item == null)
				return false;

			Remove(item);
			return true;
		}

		public string Serialize()
		{
			return JsonConvert.SerializeObject(this, Formatting.None);
		}

		public List<CClient> DeSerialise(string str)
		{
			return JsonConvert.DeserializeObject<List<CClient>>(str);
		}

	}



	public class CClient
	{
		public enum ClientState
		{
			Initiate,
			Process
		}

		public string ClienId { get; set; }
		public string ClientName { get; set; }
		public ClientState ClientStaus { get; set; }

		public CColPersonThread Threads { get; private set; }

		public CClient()
		{
			ClientName = "Nonamed";
			ClienId = "1";
			Threads = new CColPersonThread();
		}

		public CClient(string id, string name, ClientState status)
		{
			ClienId = id;
			ClientName = name;
			ClientStaus = status;
			Threads = new CColPersonThread();
		}

	}

}
