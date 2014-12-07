using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVM
{
    public class TabItemViewModelBase: ViewModelBase
    {

        private string header;
        public string Header 
        { 
            get
            { return header; }
            set
            {
                header = value;
                OnPropertyChanged("Header");
            }
        }



    }
}
