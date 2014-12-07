/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 09.11.2014
 */
using System;
using System.Collections.Generic;
using PortsChecker.Handlers;
using Data;
using System.Net;

namespace Server
{
	/// <summary>
	/// Description of AnswerCommands.
	/// </summary>
	public class CommandsAnswers
	{
		public CommandExecutor  executor {get; private set;}
		
		public CommandsAnswers()
		{
			executor = new CommandExecutor();
			
			executor.Add(Commands.AnalyzeAndSendUrl, OnAnalyzeAndSendUrl);
			executor.Add(Commands.GetPorts, OnGetPortsInfo);
			executor.Add(Commands.SendMessage, OnSendMessage);
			executor.Add(Commands.SendFile, OnSendFile);
			executor.Add(Commands.GetServerClients, OnGetServerClients);
			
		}
		
		private object OnAnalyzeAndSendUrl(object param)
		{
			System.Console.WriteLine("OnAnalyzeAndSendUrl: " + param as string);
			return null;
		}
		
		private object OnGetPortsInfo(object param)
		{
			System.Console.WriteLine("OnGetPortsInfo ");
			return ConnectionsChecker.GetOpenPort();
		}
		
		private object OnSendMessage(object param)
		{
			System.Console.WriteLine("OnSendMessage: " + param as string);
			return null;
		}
		
		private object OnSendFile(object param)
		{
            var bytes = param as byte[];
            System.Console.WriteLine("OnSendFile: " + bytes.Length);
			return null;
		}
		
		private object OnGetServerClients(object param)
		{
			System.Console.WriteLine("OnGetServerClients ");
            List<Computer> computers = new List<Computer>();
            foreach (var client in CServer.Instance.Clients)
            {
                IPEndPoint remotePoint = client.RemoteEndPoint as IPEndPoint;
                var computer = new Computer(remotePoint.Port,  remotePoint.Address.ToString() , 
                    client.Connected? 
                        Client.ConnectionState.Connected: 
                        Client.ConnectionState.Disconnected );
                computers.Add (computer);
            }

			return computers;
		}
		
	}
}
