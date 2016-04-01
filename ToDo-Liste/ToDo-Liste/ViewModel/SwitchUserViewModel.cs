using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDo_Liste.ViewModel
{
    public class SwitchUserViewModel : ViewModelBase
    {
        private DataTable _users;
        private DataRowView _currentUser;

        public DataTable Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        public DataRowView CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand AendernCommand { get; set; }

        public SwitchUserViewModel(DataTable Users, DataRowView CurrentUser)
        {
            AendernCommand = new DelegateCommand(AendernCommandHandler);
            this.Users = Users;
            this.CurrentUser = CurrentUser;

            InitializeView();
        }

        private void AendernCommandHandler(object sender)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name.Equals("MainWindow"))
                {
                    ((MainViewModel)window.DataContext).CurrentUser = CurrentUser;
                }
            }
            CloseCurrentWindow();
        }

        private void CloseCurrentWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name.Equals("SwitchUser"))
                {
                    window.Close();
                }
            }
        }

        private void InitializeView()
        {
            View.SwitchUserView switchUserView = new View.SwitchUserView();
            switchUserView.DataContext = this;
            switchUserView.ShowDialog();
        }
    }
}
