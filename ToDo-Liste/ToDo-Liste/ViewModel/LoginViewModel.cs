using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDo_Liste.ViewModel
{
    public class LoginViewModel
    {

        #region Commands
        public DelegateCommand Login { get; set; }
        public DelegateCommand DBChange { get; set; }

        //public DelegateCommand Registrieren { get; set; }
        #endregion

        public DataTable DTUsers { get; set; }
        public ObservableCollection<Model.User> Users { get; set; }
        public DataRowView SelectedUser { get; set; }

        private Model.Optionen _optionen;
        private DataAccess.DatabaseFactory _dbWork = null;

        public LoginViewModel(Model.Optionen optionen)
        {
            _optionen = optionen;

            Login = new DelegateCommand(LoginHandler);
            //Registrieren = new DelegateCommand(RegistrierenHandler);
            DBChange = new DelegateCommand(DBChangeHandler);
        }

        public void LoadUsers()
        {
            try
            {
                _dbWork = new DataAccess.DatabaseFactory(_optionen.PfadDatenbank);
                //Users = _dbWork.GetUserCollection();
                DTUsers = _dbWork.ToDo_Dataset.Tables["Users"];
            }
            catch (Exception e)
            {
                MessageBox.Show("Es ist ein Fehler aufgetretten!\n" + e.Message);
            }
        }

        #region Command Methoden
        private void DBChangeHandler(object sender)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Datenbank|*.mdb";
            if (openFileDialog.ShowDialog() == true)
            {
                _optionen.PfadDatenbank = openFileDialog.FileName;
            }
        }

        //private void RegistrierenHandler(object sender)
        //{
        //    View.AddUserView addUser = new View.AddUserView();
        //    foreach (Window window in Application.Current.Windows)
        //    {
        //        if (window.Name.Equals("NewUser"))
        //        {
        //            ((ViewModel.AddUserViewModel)window.DataContext).dbWork = _dbWork;
        //        }
        //    }
        //    addUser.ShowDialog();
        //    //LoadUsers();
        //}

        private void LoginHandler(object sender)
        {
            if (SelectedUser != null)
            {
                MainViewModel mainView = new MainViewModel(SelectedUser, _optionen, _dbWork);
                ((Window)sender).Close();
            }
            else
            {
                MessageBox.Show("Bitte einen Benutzer auswählen");
            }
        }
        #endregion
    }
}
