using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVM
{
    public class Node : ViewModelBase
    {
        public Node()
        {
            this.id = Guid.NewGuid().ToString();
        }

        private ObservableCollection<Node> children = new ObservableCollection<Node>();
        private ObservableCollection<Node> parent = new ObservableCollection<Node>();
        private string text;
        private string id;
        private bool isSelected = false;
        private bool isExpanded;

        public ObservableCollection<Node> Children
        {
            get { return this.children; }
        }

        public ObservableCollection<Node> Parent
        {
            get { return this.parent; }
        }

        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                this.isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public string Text
        {
            get { return this.text; }
            set
            {
                this.text = value;
                OnPropertyChanged("Text");
            }
        }

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                OnPropertyChanged("IsExpanded");
            }
        }

        public string Id
        {
            get { return this.id; }
            set
            {
                this.id = value;
            }
        }

        public override string ToString()
        {
            return Text;
        }

        /*private static Node GetSelectedItem(IEnumerable<Node> items)
        {
            //top-level items:
            Node item = items.FirstOrDefault(i => i.IsSelected);
            if (item == null)
            {
                //sub-level items:
                IEnumerable<Node> subItems = items.OfType<Node>().SelectMany(d => d.Children);
                if (items.Any())
                    item = GetSelectedItem(subItems);
            }
            return item;
        }*/

        public static Node GetSelectedItem(ObservableCollection<Node> nodes)
        {
            Node selectedNode = null;
            foreach (var node in nodes)
            {
                selectedNode = GetSelected(node);
                if (selectedNode != null)
                    return selectedNode;
            }
            return selectedNode;
        }

        private static Node GetSelected(Node baseNode)
        {
            if (baseNode == null) return null;
            if (baseNode.isSelected )
                return baseNode;

            if (baseNode.Children!=null)
                foreach (Node node in baseNode.Children)
                {
                    if (node != null && node.IsSelected)
                        return node;
                    if (node.Children != null)
                    {
                        var temp = Node.GetSelected(node);
                        if (temp != null) return temp;
                    }
                }
            return null;
        }

        public static void RemoveRecursive(ObservableCollection<Node> nodes, Node target)
        {
            bool result = nodes.Remove(target);
            if (!result)
            {
                foreach(var node in nodes)
                    RemoveSelected(node,target);
            }
        }

        private static Node RemoveSelected(Node baseNode,Node target)
        {
            if (baseNode == null) return null;

            if (baseNode.Children != null)
            {
                if (baseNode.Children.Remove(target))
                    return null;

                foreach (Node node in baseNode.Children)
                {
                        
                    if (node.Children != null)
                        return Node.RemoveSelected(node, target);
                }
            }
            return null;
        }

        /*public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }*/

    }//class Node

}
