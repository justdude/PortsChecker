/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 19.10.2014
 */
using System;
using System.Windows.Input;

using MVVM;

using Data;
using Client;

namespace PortsChecker.ModelView
{
	/// <summary>
	/// Description of AddComputerModelView.
	/// </summary>
	public class NewComputerModelView : ViewModelBase
	{
		private CCLient modTargetClient = null;
		
		private string mvHost = "";
		private bool mvIsWaitingConnection = false;
		
		public ICommand Save { get; set;}
		public IComputer Comp {get; protected set;}
		public Action Close { get; set;}
		
		public string Host
		{ 
			get
			{
				return mvHost;
			}
			set
			{
				if (value ==mvHost)
					return;
				mvHost = value;
				OnPropertyChanged("Host");
			}
		}
		
		public bool IsWaitingConnection 
		{ 
			get
			{
				return mvIsWaitingConnection;
			}
			set
			{
				if (value == mvIsWaitingConnection)
					return;
				mvIsWaitingConnection = value;
				OnPropertyChanged("IsWaitingConnection");
			}
		}
		
		public const string defaultHost ="127.0.0.1";
		
		
		public NewComputerModelView()
		{
			Save = new DelegateCommand(OnSave);
			Host = defaultHost;
		}
		
		public void OnSave()
		{
			IsWaitingConnection = true;
			
			modTargetClient = Engines.ClientsConnections.Instance.Connect( Host ,
			                     	Engines.ClientsConnections.Instance.CurrentPort);

			IsWaitingConnection = false;
			
			if (modTargetClient.State == ConnectionState.Connected)
			{
				Comp  = new ComputerModelView(modTargetClient);
			}
			
			
			if (Close != null)
				Close();
		}
	}
}
