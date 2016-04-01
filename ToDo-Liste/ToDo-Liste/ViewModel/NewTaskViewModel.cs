using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDo_Liste.ViewModel
{
    public class NewTaskViewModel : ViewModelBase
    {
        #region Private Parameter
        private DataAccess.DatabaseFactory _dbWork;
        private DataRow _task;
        private string _jiraTicketNr;
        private string _typ;
        private DataRowView _anforderer;
        private DataRowView _verantwortlicher;
        private string _überschrift;
        private string _priorität;
        private string _status;
        private DateTime _erstellungsdatum;
        private DateTime _zieldatum;
        private string _beschreibung;

        private DataRowView _currentUser;
        #endregion

        #region Public Parameter
        public DataRow Task
        {
            get { return _task; }
            private set
            {
                _task = value;
                OnPropertyChanged();
            }
        }
        public string JiraTicketNr
        {
            get { return _jiraTicketNr; }
            set
            {
                _jiraTicketNr = value;
                OnPropertyChanged();
            }
        }
        public string Typ
        {
            get { return _typ; }
            set
            {
                _typ = value;
                OnPropertyChanged();
            }
        }
        public DataRowView Anforderer
        {
            get { return _anforderer; }
            set
            {
                _anforderer = value;
                OnPropertyChanged();
            }
        }
        public DataRowView Verantwortlicher
        {
            get { return _verantwortlicher; }
            set
            {
                _verantwortlicher = value;
                OnPropertyChanged();
            }
        }
        public string Überschrift
        {
            get { return _überschrift; }
            set
            {
                _überschrift = value;
                OnPropertyChanged();
            }
        }
        public string Priorität
        {
            get { return _priorität; }
            set
            {
                _priorität = value;
                OnPropertyChanged();
            }
        }
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }
        public DateTime Erstellungsdatum
        {
            get { return _erstellungsdatum; }
            set
            {
                _erstellungsdatum = value;
                OnPropertyChanged();
            }
        }
        public DateTime Zieldatum
        {
            get { return _zieldatum; }
            set
            {
                _zieldatum = value;
                OnPropertyChanged();
            }
        }
        public string Beschreibung
        {
            get { return _beschreibung; }
            set
            {
                _beschreibung = value;
                OnPropertyChanged();
            }
        }

        public List<string> TypenList { get; private set; }
        public List<string> PrioritätenList { get; private set; }
        public List<string> StatusList { get; private set; }
        public DataTable Users { get; set; }
        #endregion

        #region Commands
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        #endregion

        public NewTaskViewModel(DataRowView CurrentUser, DataTable Users, DataAccess.DatabaseFactory DBWork)
        {
            SaveCommand = new DelegateCommand(SaveCommandHandler);
            CancelCommand = new DelegateCommand(CancelCommandHandler);

            _currentUser = CurrentUser;
            this.Users = Users;
            _dbWork = DBWork;
            InitializeView();
        }

        #region Command Handler
        private void SaveCommandHandler(object sender)
        {
            Task = _dbWork.ToDo_Dataset.Tables["Tasks"].NewRow();
            Task["Typ"] = Typ;
            Task["JiraTicketNr"] = JiraTicketNr;
            Task["Überschrift"] = Überschrift;
            Task["Beschreibung"] = Beschreibung;
            Task["Priorität"] = Priorität;
            Task["Status"] = Status;
            Task["Anforderer"] = string.Format("{0} {1}", Anforderer.Row["Nachname"].ToString(), Anforderer.Row["Vorname"].ToString());
            Task["Erstellungsdatum"] = Erstellungsdatum;
            Task["Verantwortlicher"] = string.Format("{0} {1}", Verantwortlicher.Row["Nachname"].ToString(), Verantwortlicher.Row["Vorname"].ToString());
            Task["Zieltermin"] = Zieldatum;

            _dbWork.ToDo_Dataset.Tables["Tasks"].Rows.Add(Task);
            _dbWork.SaveTasks();

            MessageBox.Show("Aufgabe wurde gespeichert");
            CloseCurrentWindow();
        }

        private void CancelCommandHandler(object sender)
        {
            if (MessageBox.Show("Daten werden nicht gespeichert. \nWirklich Abbrechen?", "Abbrechen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //Task = null;
                CloseCurrentWindow();
            }
        }
        #endregion

        #region Private Methoden
        private void InitializeView()
        {
            InitializeLists();
            Zieldatum = DateTime.Today;
            Erstellungsdatum = DateTime.Today;
            Anforderer = _currentUser;
            View.NewTaskView newTaskView = new View.NewTaskView();

            newTaskView.DataContext = this;
            newTaskView.ShowDialog();
        }

        private void InitializeLists()
        {
            TypenList = new List<string>() { "Aufgabe", "Information" };
            PrioritätenList = new List<string>() { "Hoch", "Mittel", "Niedrieg" };
            StatusList = new List<string>() { "Neu", "In Bearbeitung", "Zur Kontrolle", "Genehmigt", "Abgehlent", "Geschlossen" };
        }

        private void CloseCurrentWindow()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.Name.Equals("NewTask"))
                {
                    window.Close();
                }
            }
        }
        #endregion
    }
}
