using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpServer
{

	public class CColPersonThread : List<CPersonThread>
	{
		public CColPersonThread():base()
		{

		}

		public CColPersonThread(IEnumerable<CPersonThread> items):base(items)
		{
		}

		public bool Insert(CPersonThread thread)
		{
			if (this.Contains(thread))
				return false;

			this.Add(thread);
			return true;
		}

		public CColPersonThread Select(string id)
		{
			return new CColPersonThread(this.Where(p => p.threadId == id));
		}

		public bool Delete(string id)
		{
			var item = this.FirstOrDefault(p=>p.threadId == id);
			if (item == null)
				return false;

			Remove(item);
			return true;
		}

		public string Serialize()
		{
			return JsonConvert.SerializeObject(this, Formatting.None);
		}

		public CColPersonThread DeSerialise(string str)
		{
			return JsonConvert.DeserializeObject<CColPersonThread>(str);
		}

	}

	public class CPersonThread
	{
		public string threadId { get; set; }
		public int threadPriority { get; set; }
		public string threadTask { get; set; }
		public int Memory { get; set; }
		public long DurationTimeSeconds { get; set; }

		private Action Del = null;

		public void SetAction(Action del)
		{
			Del = del;
		}

		public void Execute()
		{
			if (Del == null)
				return;
			Del();
		}

	}

}
