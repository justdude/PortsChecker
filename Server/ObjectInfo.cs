﻿/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 27.10.2014
 */
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Data
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class ObjectInfo
	{
		//public uint Size{ get; set;}
		public object TargetObject {get; set;}
		
		public bool IsNeedAnswer {get; set;}
		public DateTime SendTime {get; set;}
		public string ID {get; set;}
		
		public ObjectInfo()
		{
			TargetObject = null;
			Init();
		}
		
		public ObjectInfo(object target)//, uint size)
		{
			TargetObject = target;
			Init();
		}
		
		private void Init()
		{
			IsNeedAnswer = false;
			SendTime = DateTime.Now;
			ID = GetFormattedDate() + GenerateID();
		}
		
		public void ToObjectInfo(byte[] target)
		{
			
	      MemoryStream memStream = new MemoryStream();
	      BinaryFormatter binForm = new BinaryFormatter();
	      
	      memStream.Write(target, 0, target.Length);
	      memStream.Seek(0, SeekOrigin.Begin);
	      
	      ID = (string) binForm.Deserialize(memStream);
	      TargetObject = (Object) binForm.Deserialize(memStream);
	      IsNeedAnswer = (bool) binForm.Deserialize(memStream);
		  SendTime = (DateTime) binForm.Deserialize(memStream);
		  
		}
		
		public byte[] ToByte()
		{
			BinaryFormatter binFormatter = new BinaryFormatter();
			MemoryStream stream = new MemoryStream();
			
			binFormatter.Serialize(stream, ID);
			binFormatter.Serialize(stream, TargetObject);
			binFormatter.Serialize(stream, IsNeedAnswer);
			binFormatter.Serialize(stream, SendTime);
			
			return stream.ToArray();
		}
		
		public static string GetFormattedDate()
		{
			return string.Format("{0:00}{1:00}{2:00}{3:00}{4:00}{5:00}{6:000}",
			              DateTime.Now.Year,
			              DateTime.Now.Month,
			              DateTime.Now.Day,
			              DateTime.Now.Hour,
			              DateTime.Now.Minute,
			              DateTime.Now.Second,
			              DateTime.Now.Millisecond);
		
		}
		
		public static string GenerateID()
		{
			return "ID" + new Random().Next(0, 9999).ToString();
		}
	}
}
