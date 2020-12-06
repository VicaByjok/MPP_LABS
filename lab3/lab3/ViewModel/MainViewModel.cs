using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace lab3.ViewModel 
{ 
    class MainViewModel : INotifyPropertyChanged
    {
        private NodeView root;
        public NodeView Root { 
            get 
            {
                return root;
            }
            set 
            {
                root = value;
                OnPropertyChanged("Root");
            } 
        }

        private ObservableCollection<NodeView> namespaces;
        public ObservableCollection<NodeView> Namespaces
        {
            get { return namespaces; }
            set
            {
                namespaces = value;
                OnPropertyChanged("Namespaces");
            }
        }

        private OpenFileService dialogService = new OpenFileService();

        private RelayCommand openCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }


        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                (openCommand = new RelayCommand(obj =>
                {
                    try
                    {
                        if (dialogService.OpenFileDialog() == true)
                        {
                            AssemblyInformation.AssemblyService assInf = 
                                new AssemblyInformation.AssemblyService(dialogService.FilePath);
                            Root = new NodeView(assInf.ProcessAssembly());
                            Namespaces = root.Children;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Не удалось открыть файл");
                    }
                }));
            }
        }

    
    }
}
