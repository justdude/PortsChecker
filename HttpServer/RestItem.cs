using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpServer
{
	public class RestItem<T>
	{
		public T ItemValue { get; set; }
		public RestItem<T> ChildItem { get; set; }

		public object GetChild()
		{
			if (ChildItem == null)
				return null;

			return ChildItem;
		}
	}
}
