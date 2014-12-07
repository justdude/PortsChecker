/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 09.11.2014
 */
using System;
using System.Collections;
using System.Collections.Generic;
using Data;

namespace Server
{
	/// <summary>
	/// Description of CommandExecutor.
	/// </summary>
	public class CommandExecutor
	{
		
		public delegate object ExecuteCommand(object param);
		
		private Dictionary<string, ExecuteCommand> modActions;
		
		
		public CommandExecutor()
		{
			modActions = new Dictionary<string, CommandExecutor.ExecuteCommand>();
		}
		
		public object Execute(string command,  object param)
		{
			if (modActions.ContainsKey(command))
			{
				return modActions[command].Invoke(param);
			}
			return null;
		}
		
		public void Add(string command,  ExecuteCommand del)
		{
			if (modActions.ContainsKey(command))
			{
				return;
			}
			else
			{
				modActions.Add(command, del);
			}
		}
		

		
	}
}
