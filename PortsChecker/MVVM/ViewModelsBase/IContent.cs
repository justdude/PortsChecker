using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVM
{
    interface IContent
    {
        string Title { get; set; }
        void GetContent();
    }

    public class Category
    {
        public string Title { get; set; }
        public bool IsSelected { get; set; }
    }
}
