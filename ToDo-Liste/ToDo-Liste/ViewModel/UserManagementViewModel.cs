using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDo_Liste.ViewModel
{
    public class UserManagementViewModel : ViewModelBase
    {
        #region Private Member
        private DataRowView _selectedUserDetail;
        private DataTable _users;
        private DataAccess.DatabaseFactory _dbWork;
        #endregion

        #region Public Member
        public DataRowView SelectedUserDetail
        {
            get { return _selectedUserDetail; }
            set
            {
                _selectedUserDetail = value;
                OnPropertyChanged();
            }
        }
        public DataTable Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand NewUserCommand { get; set; }
        #endregion

        public UserManagementViewModel(DataTable Users, DataAccess.DatabaseFactory DBWork)
        {
            CancelCommand = new DelegateCommand(CancelCommandHandler);
            SaveCommand = new DelegateCommand(SaveCommandHandler);
            NewUserCommand = new DelegateCommand(NewUserCommandHandler);
            _dbWork = DBWork;
            this.Users = Users;

            InitializeView();
        }


        #region Private Methoden
        private void InitializeView()
        {
            View.UserManagementView userManagementView = new View.UserManagementView();
            userManagementView.DataContext = this;
            userManagementView.Show();
        }

        private void CloseCurrentWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name.Equals("UserManagement"))
                {
                    window.Close();
                }
            }
        }
        #endregion

        #region Command Handler
        private void CancelCommandHandler(object sender)
        {
            if (MessageBox.Show("Ungespeicherte Änderungen gehen verloren. \nWirklich beenden", "Abbrechen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Users.RejectChanges();
                CloseCurrentWindow();
            }
        }

        private void SaveCommandHandler(object sender)
        {
            _dbWork.SaveUsers();
            MessageBox.Show("Erfolgreich gespeichert");
        }

        private void NewUserCommandHandler(object sender)
        {
            AddNewUserViewModel addNewUserViewModel = new AddNewUserViewModel(_dbWork);
        }
        #endregion
    }
}
