using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace HttpServer
{
	public static class Const
	{
		public const string GET = "GET";
		public const string PUT = "PUT";
		public const string POST = "POST";
		public const string DELETE = "DELETE";

		public const string Clients = "Clients";
		public const string Threads = "Threads";

	}

	class RequestHandler
	{
		// Отправка страницы с ошибкой
		private static byte[] SendError(int Code)
		{
			// Получаем строку вида "200 OK"
			// HttpStatusCode хранит в себе все статус-коды HTTP/1.1
			string CodeStr = Code.ToString() + " " + ((HttpStatusCode)Code).ToString();
			// Код простой HTML-странички
			string Html = "<html><body><h1>" + CodeStr + "</h1></body></html>";
			// Необходимые заголовки: ответ сервера, тип и длина содержимого. После двух пустых строк - само содержимое
			string Str = "HTTP/1.1 " + CodeStr + "\nContent-type: text/html\nContent-Length:" + Html.Length.ToString() + "\n\n" + Html;
			// Приведем строку к виду массива байт
			byte[] Buffer = Encoding.ASCII.GetBytes(Str);
			// Отправим его клиенту
			return Buffer;
		}

		private static string streamReadLine(byte[] data)
		{
			int next_char;
			string res = "";
			foreach(var d in data)
			{
				next_char = d;
				if (next_char == '\n') { break; }
				if (next_char == '\r') { continue; }
				if (next_char == -1) { continue; };
				res += Convert.ToChar(next_char);
			}
			return res;
		}

		
		public static byte[] HandleReq(byte[] data)
		{
			// Объявим строку, в которой будет хранится запрос клиента
			string Request = "";

			Request = Encoding.ASCII.GetString(data);

			
			// Парсим строку запроса с использованием регулярных выражений
			// При этом отсекаем все переменные GET-запроса
			Match ReqMatch = Regex.Match(Request, @"^\w+\s+([^\s\?]+)[^\s]*\s+HTTP/.*|");
		
			if (ReqMatch == Match.Empty)
			{
				// Передаем клиенту ошибку 400 - неверный запрос
				return SendError(400);
			}
			var pairs = Request.Split(':');
			var lines = Request.Split((Environment.NewLine).ToCharArray());
			var dict = new Dictionary<string, string>();
			foreach(var line in lines)
			{
				if (string.IsNullOrEmpty(line))
					continue;
				int ind = line.IndexOf(':');
				if (ind == -1)
				{
					dict.Add(line, line);
					continue;
				}

				var p1 = line.Substring(0, ind);
				var p2 = line.Substring(ind);
				dict.Add(p1, p2);
				
			}

			string MethodName = ReqMatch.Groups[0].Value;
			
			if (string.IsNullOrEmpty(MethodName))
				return SendError(404);

			MethodName = MethodName.Split(' ')[0];

			// Получаем строку запроса
			string RequestUri = ReqMatch.Groups[1].Value;
			// Приводим ее к изначальному виду, преобразуя экранированные символы
			// Например, "%20" -> " "
			RequestUri = Uri.UnescapeDataString(RequestUri);

			// Если в строке содержится двоеточие, передадим ошибку 400
			// Это нужно для защиты от URL типа http://example.com/../../file.txt
			if (RequestUri.IndexOf("..") >= 0)
			{
				return SendError(400);
			}

			// Если строка запроса оканчивается на "/", то добавим к ней index.html
			if (RequestUri.EndsWith("/"))
			{
				RequestUri += "index.html";
			}
			string FilePath = "www/" + RequestUri;
			
			// Получаем расширение файла из строки запроса
			//string Extension = RequestUri.Substring(RequestUri.LastIndexOf('.'));
			
			var Headers = writeSuccess();
			var res = string.Empty;
			switch (MethodName)
			{
				case (Const.GET):
					return handleGETRequest(Headers);
				case (Const.POST):
					MemoryStream stream = new MemoryStream();
					var last = dict.Last();
					var bytes = Encoding.ASCII.GetBytes(last.Value);
					stream.Write(bytes, 0, bytes.Length);
					stream.Seek(0, SeekOrigin.Begin);
				return handlePOSTRequest(new StreamReader(stream));
			}
			return new byte[] { };
		}

		private static string SelectContentType(string Extension)
		{
			string ContentType = "";
			// Пытаемся определить тип содержимого по расширению файла
			switch (Extension)
			{
				case ".htm":
				case ".html":
					ContentType = "text/html";
					break;
				case ".css":
					ContentType = "text/stylesheet";
					break;
				case ".js":
					ContentType = "text/javascript";
					break;
				case ".jpg":
					ContentType = "image/jpeg";
					break;
				case ".jpeg":
				case ".png":
				case ".gif":
					ContentType = "image/" + Extension.Substring(1);
					break;
				default:
					if (Extension.Length > 1)
					{
						ContentType = "application/" + Extension.Substring(1);
					}
					else
					{
						ContentType = "application/unknown";
					}
					break;
			}
			return ContentType;
		}


		public static byte[] handleGETRequest(string header)
		{
			string str = "";

			str += ("<html><body><h1>test server</h1>");
			str += ("Current Time: " + DateTime.Now.ToString());
			str += ("url : " + header);

			str += ("<form method=post action=/form>");
			str += ("<input type=text name=foo value=foovalue>");
			str += ("<input type=submit name=bar value=barvalue>");
			str += ("</form>");

			return Encoding.ASCII.GetBytes(str);
		}

		public static byte[] handlePOSTRequest(StreamReader inputData)
		{
			string str = String.Empty;

			string data = inputData.ReadToEnd();

			str += ("<html><body><h1>test server</h1>");
			str += ("<a href=/test>return</a><p>");
			str += ("postbody: <pre>{0}</pre>");
			str += data;

			return Encoding.ASCII.GetBytes(str);
		}

		public static string writeSuccess()
		{
			string str = "";

			str += "HTTP/1.0 200 OK\n";
			str += "Content-Type: text/html\n";
			str += "Connection: close\n";
			str += "\n";

			return str;
		}

		public static string writeFailure()
		{
			string str = "";

			str += "HTTP/1.0 404 File not found\n";
			str += "Connection: close\n";
			str += "\n";

			return str;
		}

	}
}
