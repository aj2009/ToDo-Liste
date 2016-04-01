using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToDo_Liste.ViewModel
{
    public class OptionenViewModel : ViewModelBase
    {
        #region Commands
        public DelegateCommand ChangePathDatabase { get; set; }
        public DelegateCommand ChangePathUsersFile { get; set; }
        public DelegateCommand ChangePathTasksFile { get; set; }
        public DelegateCommand Speichern { get; set; }
        public DelegateCommand Abbrechen { get; set; }

        #endregion

        #region Private Member
        private Model.Optionen _optionen;
        private string _pathToDatabase;
        private string _pathToUsersFile;
        private string _pathToTasksFile;
        #endregion

        #region Public Member
        public Model.Optionen Optionen
        {
            get { return _optionen; }
            set
            {
                _optionen = value;
                OnPropertyChanged();
            }
        }
        public string PathToDatabase
        {
            get { return _pathToDatabase; }
            set
            {
                _pathToDatabase = value;
                OnPropertyChanged();
            }
        }
        public string PathToUsersFile
        {
            get { return _pathToUsersFile; }
            set
            {
                _pathToUsersFile = value;
                OnPropertyChanged();
            }
        }
        public string PathToTasksFile
        {
            get { return _pathToTasksFile; }
            set
            {
                _pathToTasksFile = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public OptionenViewModel(Model.Optionen Optionen)
        {
            ChangePathDatabase = new DelegateCommand(ChangePathDatabaseHandler);
            ChangePathUsersFile = new DelegateCommand(ChangePathUsersFileHandler);
            ChangePathTasksFile = new DelegateCommand(ChangePathTasksFileHandler);
            Speichern = new DelegateCommand(SpeichernHandler);
            Abbrechen = new DelegateCommand(AbbrechenHandler);

            this.Optionen = Optionen;
            InitializeView();
        }

        #region Private Methoden
        private void InitializeView()
        {
            View.OptionenView optionenView = new View.OptionenView();

            PathToDatabase = Optionen.PfadDatenbank;
            PathToTasksFile = Optionen.TaskListXML;
            PathToUsersFile = Optionen.UserListXML;

            optionenView.DataContext = this;
            optionenView.ShowDialog();
        }

        private string FileOpenDialog(string extension)
        {
            string _result = null;
            string _filter = null;
            if (extension.Equals("mdb"))
            {
                _filter = "Datenbankdatei|*.mdb|All files|*.*";
            }
            else if (extension.Equals("xml"))
            {
                _filter = "XML-Datei|*.xml|All files|*.*";
            }
            else _filter = "All files|*.*";

            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Title = "Wählen Sie bitte die Datei aus.";
            openFileDialog.Filter = _filter;
            if (openFileDialog.ShowDialog() == true)
            {
                _result = openFileDialog.FileName;
            }
            return _result;
        }

        #endregion

        #region Command Handler
        private void AbbrechenHandler(object sender)
        {
            ((System.Windows.Window)sender).Close();
        }

        private void SpeichernHandler(object sender)
        {
            DataAccess.OptionenFactory optionen = new DataAccess.OptionenFactory();
            Optionen.PfadDatenbank = PathToDatabase;

            optionen.SaveOptionen(Optionen);

            MessageBox.Show("Speichern erfolgreich", "Erfolgreich", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ChangePathTasksFileHandler(object sender)
        {
            string path = FileOpenDialog("xml");
            if (path != null)
            {
                Optionen.TaskListXML = path;
            }
        }

        private void ChangePathUsersFileHandler(object sender)
        {
            string path = FileOpenDialog("xml");
            if (path != null)
            {
                Optionen.UserListXML = path;
            }
        }

        private void ChangePathDatabaseHandler(object sender)
        {
            string path = FileOpenDialog("mdb");
            if (path != null)
            {
                PathToDatabase = path;
            }
        }
        #endregion
    }
}
