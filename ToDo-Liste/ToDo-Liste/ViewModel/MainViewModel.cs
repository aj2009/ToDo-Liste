using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDo_Liste.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Private Parameter
        private DataTable _users;
        private DataTable _tasks;
        private DataAccess.DatabaseFactory _dbWork;
        private Model.Optionen _optionen;
        private DataRowView _currentUser;
        private DataRowView _taskDetail;
        #endregion

        #region Public Parameter
        public DataTable Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        public DataTable Tasks
        {
            get { return _tasks; }
            set
            {
                _tasks = value;
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
        public DataRowView TaskDetail
        {
            get { return _taskDetail; }
            set
            {
                _taskDetail = value;
                OnPropertyChanged();
            }
        }
        public List<string> TypenList { get; set; }
        public List<string> PrioritätenList { get; set; }
        public List<string> StatusList { get; set; }
        #endregion

        #region Commands
        public DelegateCommand ShowInfos { get; set; }
        public DelegateCommand AddNewTask { get; set; }
        public DelegateCommand DeleteTask { get; set; }
        public DelegateCommand SaveTaskDetails { get; set; }
        public DelegateCommand ShowOptionen { get; set; }
        public DelegateCommand ShowLizenz { get; set; }
        public DelegateCommand UserManagement { get; set; }
        public DelegateCommand ExcelImport { get; set; }
        public DelegateCommand SwitchUser { get; set; }
        public DelegateCommand CloseCommand { get; set; }
        public DelegateCommand ShowHelp { get; set; }
        #endregion

        public MainViewModel(DataRowView CurrentUser, Model.Optionen Optionen, DataAccess.DatabaseFactory DBWork)
        {
            ShowInfos = new DelegateCommand(ShowInfosHandler);
            AddNewTask = new DelegateCommand(AddNewTaskHandler);
            DeleteTask = new DelegateCommand(DeleteTaskHandler);
            SaveTaskDetails = new DelegateCommand(SaveTaskDetailsHandler);
            ShowOptionen = new DelegateCommand(ShowOptionenHandler);
            ShowLizenz = new DelegateCommand(ShowLizenzHandler);
            UserManagement = new DelegateCommand(UserManagementHandler);
            ExcelImport = new DelegateCommand(ExcelImportHandler);
            SwitchUser = new DelegateCommand(SwitchUserHandler);
            CloseCommand = new DelegateCommand(CloseCommandHandler);
            ShowHelp = new DelegateCommand(ShowHelpHandler);

            _dbWork = DBWork;
            _optionen = Optionen;
            this.CurrentUser = CurrentUser;

            InitializeView();
        }

        #region Private Methoden
        private void InitializeView()
        {
            Tasks = _dbWork.ToDo_Dataset.Tables["Tasks"];
            Users = _dbWork.ToDo_Dataset.Tables["Users"];
            InitializeLists();

            View.MainView mainView = new View.MainView();
            mainView.DataContext = this;
            mainView.btnNewTicket.DataContext = this;
            mainView.btnDeleteTask.DataContext = this;
            mainView.Show();
        }

        private void InitializeLists()
        {
            TypenList = new List<string>() { "Aufgabe", "Information" };
            PrioritätenList = new List<string>() { "Hoch", "Mittel", "Niedrieg" };
            StatusList = new List<string>() { "Neu", "In Bearbeitung", "Zur Kontrolle", "Genehmigt", "Abgehlent", "Geschlossen" };
        }

        private void ReloadOptions()
        {
            _optionen = null;
            DataAccess.OptionenFactory optionenWork = new DataAccess.OptionenFactory();
            _optionen = optionenWork.GetOptionen();
        }
        #endregion

        #region Command Handler
        private void SaveTaskDetailsHandler(object sender)
        {
            _dbWork.SaveTasks();
        }

        private void ShowInfosHandler(object sender)
        {
            MessageBox.Show("Softwareversion: " + Assembly.GetExecutingAssembly().GetName().Version.ToString());
        }

        private void DeleteTaskHandler(object sender)
        {
            if (MessageBox.Show("Möchten Sie das Ticket wirklich löschen?", "Bestätigung", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                _taskDetail.Delete();
                _dbWork.SaveTasks();
            }
        }

        private void AddNewTaskHandler(object sender)
        {
            NewTaskViewModel newTaskViewModel = new NewTaskViewModel(CurrentUser, Users, _dbWork);
        }

        private void ShowOptionenHandler(object sender)
        {
            OptionenViewModel optionenViewModel = new OptionenViewModel(_optionen);
            _optionen = optionenViewModel.Optionen;
        }

        private void UserManagementHandler(object sender)
        {
            UserManagementViewModel userManagementViewModel = new UserManagementViewModel(Users, _dbWork);
        }

        private void ShowLizenzHandler(object sender)
        {
            View.LizenzView lizenzView = new View.LizenzView();
            lizenzView.ShowDialog();
        }

        private void ExcelImportHandler(object sender)
        {
            MessageBox.Show("Diese Funktion ist in der aktuellen Version nicht verfügbar");
        }

        private void SwitchUserHandler(object sender)
        {
            ViewModel.SwitchUserViewModel switchUserViewModel = new SwitchUserViewModel(Users, CurrentUser);
        }

        private void CloseCommandHandler(object sender)
        {
            Application.Current.Windows[0].Close();
        }

        private void ShowHelpHandler(object sender)
        {
            MessageBox.Show("Hier entsteht die Hilfe, leider ist diese noch nicht fertig.");
        }
        #endregion
    }
}
