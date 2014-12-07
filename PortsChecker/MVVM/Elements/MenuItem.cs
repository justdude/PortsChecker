using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVM
{
    public class MenuItem
    {
        public string Text { get; set; }
        public List<MenuItem> Children { get; private set; }
        public ICommand Command { get; set; }

        //public Type type;

        public MenuItem(string item)
        {
            Text = item;
            Children = new List<MenuItem>();
        }
    }
}
