/*
* Created by SharpDevelop.
* User: Albantov
* Date: 19.10.2014
*/
using System;
using System.Text;
using System.Windows.Input;

using Client;
using Data;
using MVVM;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace PortsChecker.ModelView
{

	public class ComputersListSelectModelView : ViewModelBase
	{

		private CCLient mvSelectedClient = null;
		private List<Computer> mvServerClients = null;


		public CCLient SelectedClient
		{
			get
			{
				return mvSelectedClient;
			}
			set
			{
				if (value == mvSelectedClient)
					return;

				mvSelectedClient = value;
				OnPropertyChanged("SelectedClient");
			}
		}

		public ObservableCollection<Computer> ServerClients
		{
			get
			{
				return new ObservableCollection<Computer>(mvServerClients);
			}
		}

	}//ComputersListSelectModelView

}//namespace