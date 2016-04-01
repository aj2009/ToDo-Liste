using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_Liste.Model
{
    public class Optionen : INotifyPropertyChanged
    {
        private string _pfadDatenbank;
        private string _userListXML;
        private string _taskListXML;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string caller = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        public string PfadDatenbank
        {
            get { return _pfadDatenbank; }
            set { _pfadDatenbank = value; OnPropertyChanged(); }
        }

        public string UserListXML
        {
            get { return _userListXML; }
            set { _userListXML = value; OnPropertyChanged(); }
        }

        public string TaskListXML
        {
            get { return _taskListXML; }
            set { _taskListXML = value; OnPropertyChanged(); }
        }
    }
}
