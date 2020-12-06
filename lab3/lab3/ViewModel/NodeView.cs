using AssemblyInformation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace lab3.ViewModel
{
    class NodeView : INotifyPropertyChanged
    {

        private string inf;
        public string Inf
        {
            get { return inf; }
            set
            {
                inf = value;
                OnPropertyChanged("Inf");
            }
        }

        private ObservableCollection<NodeView> children;
        public ObservableCollection<NodeView> Children
        {
            get { return children; }
            set
            {
                children = value;
                OnPropertyChanged("Children");
            }
        }


        public NodeView(Node node)
        {
            ObservableCollection<NodeView> temp = new ObservableCollection<NodeView>();
            Inf = node.inf;
            if (node.children != null) { 
                foreach (Node n in node.children)
                {
                    temp.Add(new NodeView(n));
                }
            }
            Children = temp;
        }
       
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
