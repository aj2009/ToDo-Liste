using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDo_Liste.ViewModel
{
    public class AddNewUserViewModel : ViewModelBase
    {
        private DataRow _newUser;
        private DataAccess.DatabaseFactory _dbWork;
        private string _vorname;
        private string _nachname;
        private string _mail;
        private string _initialien;
        private string _password;

        public DataRow NewUser
        {
            get { return _newUser; }
            set
            {
                _newUser = value;
                OnPropertyChanged();
            }
        }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }

        public string Vorname
        {
            get { return _vorname; }
            set
            {
                _vorname = value;
                OnPropertyChanged();
            }
        }

        public string Nachname
        {
            get { return _nachname; }
            set
            {
                _nachname = value;
                OnPropertyChanged();
            }
        }

        public string Mail
        {
            get { return _mail; }
            set
            {
                _mail = value;
                OnPropertyChanged();
            }
        }

        public string Initialien
        {
            get { return _initialien; }
            set
            {
                _initialien = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public AddNewUserViewModel(DataAccess.DatabaseFactory DBWork)
        {
            SaveCommand = new DelegateCommand(SaveCommandHandler);
            CancelCommand = new DelegateCommand(CancelCommandHandler);

            _dbWork = DBWork;
            InitializeView();
        }

        private void CancelCommandHandler(object sender)
        {
            if (MessageBox.Show("Möchten Sie die Daten verwerfen?", "Abbrechen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                CloseCurrentWindow();
            }
        }

        private void SaveCommandHandler(object sender)
        {
            NewUser = _dbWork.ToDo_Dataset.Tables["Users"].NewRow();
            NewUser["Vorname"] = Vorname;
            NewUser["Nachname"] = Nachname;
            NewUser["Mail"] = Mail;
            NewUser["Initialien"] = Initialien;
            NewUser["Password"] = Password;
            _dbWork.ToDo_Dataset.Tables["Users"].Rows.Add(NewUser);
            _dbWork.SaveUsers();

            MessageBox.Show("Erfolgreich gespeichert", "Erfolgreich", MessageBoxButton.OK, MessageBoxImage.Information);
            CloseCurrentWindow();
        }

        private void InitializeView()
        {
            View.AddNewUserView addNewUser = new View.AddNewUserView();
            addNewUser.DataContext = this;
            addNewUser.ShowDialog();
        }

        private void CloseCurrentWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name.Equals("NewUser"))
                {
                    window.Close();
                }
            }
        }
    }
}
