using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_Liste.DataAccess
{
    public class DatabaseFactory
    {
        private string _path;
        private OleDbConnection _con;
        private DataSet _dataSet;
        private OleDbDataAdapter _userAdapter = null;
        private OleDbDataAdapter _taskAdapter = null;

        public DatabaseFactory(string DBPfad)
        {
            _path = DBPfad;
            _con = new OleDbConnection(@"Provider=Microsoft.JET.OLEDB.4.0; Data Source=" + _path);
            InitializeAdapter();
            Load();
        }

        #region Public Methoden
        public List<Model.User> GetUserList()
        {
            List<Model.User> _result = new List<Model.User>();
            DataTableReader tableReader = new DataTableReader(_dataSet.Tables["Users"]);
            if (tableReader.HasRows)
            {
                while (tableReader.Read())
                {
                    _result.Add(new Model.User()
                    {
                        Id = (int)tableReader["BenutzerID"],
                        Vorname = tableReader["Vorname"] as string,
                        Nachname = tableReader["Nachname"] as string,
                        Mail = tableReader["Mail"] as string,
                        Initialien = tableReader["Initialien"] as string,
                        Password = tableReader["Password"] as string
                    });
                }
            }
            return _result;
        }
        public List<Model.Task> GetTaskList()
        {
            List<Model.Task> _result = new List<Model.Task>();
            DataTableReader tableReader = new DataTableReader(_dataSet.Tables["Tasks"]);
            if (tableReader.HasRows)
            {
                while (tableReader.Read())
                {
                    _result.Add(new Model.Task()
                    {
                        JiraTicketNr = tableReader["Jira-Ticket-Nr"] as string,
                        Typ = tableReader["Typ"] as string,
                        Überschrift = tableReader["Überschrift"] as string,
                        Beschreibung = tableReader["Beschreibung"] as string,
                        Prio = tableReader["Priorität"] as string,
                        Status = tableReader["Status"] as string,
                        Anforderer = tableReader["Anforderer"] as Model.User,
                        //Datum= tableReader["Datum"] as DateTime,
                        Verantwortlicher = tableReader["Verantwortlicher"] as Model.User,
                        //Zieltermin = tableReader["Zieltermin"] as string,
                    });
                }
            }
            return _result;
        }
        public ObservableCollection<Model.User> GetUserCollection()
        { return ConvertToCollection(GetUserList()); }
        public ObservableCollection<Model.Task> GetTaskCollection()
        { return ConvertToCollection(GetTaskList()); }
        public DataSet ToDo_Dataset
        {
            get
            {
                return _dataSet;
            }
            set { _dataSet = value; }
        }

        public void SaveUsers()
        {
            try
            {
                _con.Open();
                _userAdapter.Update(_dataSet, "Users");
            }
            catch (OleDbException e) { throw new Exception(e.Message); }
            finally { _con.Close(); }
        }
        public void SaveTasks()
        {
            try
            {
                _con.Open();
                _taskAdapter.Update(_dataSet, "Tasks");
            }
            catch (OleDbException e) { throw new Exception(e.Message); }
            catch (DBConcurrencyException e) { throw new Exception(e.Message); }
            finally { _con.Close(); }
        }

        public void ReloadUsers()
        {
            _dataSet.Tables["Users"].Clear();
            _userAdapter.Fill(_dataSet, "Users");
        }
        #endregion

        #region Private Methoden
        private void InitializeAdapter()
        {
            InitializeUserAdapter();
            InitializeTaskAdapter();
        }
        private void InitializeUserAdapter()
        {
            _userAdapter = new OleDbDataAdapter();

            // SELECT Command
            OleDbCommand command = new OleDbCommand("SELECT * FROM Users", _con);
            _userAdapter.SelectCommand = command;

            //INSERT Command
            command = new OleDbCommand("INSERT INTO Users (Vorname, Nachname, Mail, Initialien) VALUES (@Vorname, @Nachname, @Mail, @Initialien)", _con);
            command.Parameters.Add("@Vorname", OleDbType.VarWChar, 255, "Vorname");
            command.Parameters.Add("@Nachname", OleDbType.VarWChar, 255, "Nachname");
            command.Parameters.Add("@Mail", OleDbType.VarWChar, 255, "Mail");
            command.Parameters.Add("@Initialien", OleDbType.VarWChar, 255, "Initialien");
            _userAdapter.InsertCommand = command;

            //Update Command
            command = new OleDbCommand("UPDATE Users SET Vorname = @Vorname, Nachname = @Nachname, Mail = @Mail, Initialien = @Initialien WHERE BenutzerID = @BenutzerID", _con);
            command.Parameters.Add("@Vorname", OleDbType.VarWChar, 255, "Vorname");
            command.Parameters.Add("@Nachname", OleDbType.VarWChar, 255, "Nachname");
            command.Parameters.Add("@Mail", OleDbType.VarWChar, 255, "Mail");
            command.Parameters.Add("@Initialien", OleDbType.VarWChar, 255, "Initialien");
            command.Parameters.Add("@BenutzerID", OleDbType.UnsignedInt, 4, "BenutzerID");
            _userAdapter.UpdateCommand = command;
        }
        private void InitializeTaskAdapter()
        {
            _taskAdapter = new OleDbDataAdapter();

            // SELECT Command
            OleDbCommand command = new OleDbCommand("SELECT * FROM Tasks;", _con);
            _taskAdapter.SelectCommand = command;

            // INSERT Command
            command = new OleDbCommand("INSERT INTO Tasks (Typ, JiraTicketNr, Überschrift, Beschreibung, Priorität, Status, Anforderer, Erstellungsdatum, Verantwortlicher, Zieltermin) VALUES (@Typ, @JiraTicketNr, @Überschrift, @Beschreibung, @Priorität, @Status, @Anforderer, @Erstellungsdatum, @Verantwortlicher, @Zieltermin);", _con);
            command.Parameters.Add("@Typ", OleDbType.VarWChar, 255, "Typ");
            command.Parameters.Add("@JiraTicketNr", OleDbType.VarWChar, 255, "JiraTicketNr");
            command.Parameters.Add("@Überschrift", OleDbType.VarWChar, 255, "Überschrift");
            command.Parameters.Add("@Beschreibung", OleDbType.VarChar, -1, "Beschreibung");
            command.Parameters.Add("@Priorität", OleDbType.VarWChar, 255, "Priorität");
            command.Parameters.Add("@Status", OleDbType.VarWChar, 255, "Status");
            command.Parameters.Add("@Anforderer", OleDbType.VarWChar, 255, "Anforderer");
            command.Parameters.Add("@Erstellungsdatum", OleDbType.Date, 255, "Erstellungsdatum");
            command.Parameters.Add("@Verantwortlicher", OleDbType.VarWChar, 255, "Verantwortlicher");
            command.Parameters.Add("@Zieltermin", OleDbType.Date, 255, "Zieltermin");
            _taskAdapter.InsertCommand = command;

            // Update Command
            command = new OleDbCommand("UPDATE Tasks SET Typ = @Typ, JiraTicketNr = @JiraTicketNr, Überschrift = @Überschrift, Beschreibung = @Beschreibung, Priorität = @Priorität, Status = @Status, Anforderer = @Anforderer, Erstellungsdatum = @Erstellungsdatum, Verantwortlicher = @Verantwortlicher, Zieltermin = @Zieltermin WHERE [ID] = @ID;", _con);
            command.Parameters.Add("@Typ", OleDbType.VarWChar, 255, "Typ");
            command.Parameters.Add("@JiraTicketNr", OleDbType.VarWChar, 255, "JiraTicketNr");
            command.Parameters.Add("@Überschrift", OleDbType.VarWChar, 255, "Überschrift");
            command.Parameters.Add("@Beschreibung", OleDbType.VarChar, -1, "Beschreibung");
            command.Parameters.Add("@Priorität", OleDbType.VarWChar, 255, "Priorität");
            command.Parameters.Add("@Status", OleDbType.VarWChar, 255, "Status");
            command.Parameters.Add("@Anforderer", OleDbType.VarWChar, 255, "Anforderer");
            command.Parameters.Add("@Erstellungsdatum", OleDbType.Date, 255, "Erstellungsdatum");
            command.Parameters.Add("@Verantwortlicher", OleDbType.VarWChar, 255, "Verantwortlicher");
            command.Parameters.Add("@Zieltermin", OleDbType.Date, 255, "Zieltermin");
            command.Parameters.Add("@ID", OleDbType.UnsignedInt, 255, "ID");
            _taskAdapter.UpdateCommand = command;

            // Delete Command
            command = new OleDbCommand("DELETE FROM Tasks WHERE ID = @ID;", _con);
            command.Parameters.Add("@ID", OleDbType.UnsignedInt, -1, "ID");
            _taskAdapter.DeleteCommand = command;
        }

        private void Load()
        {
            _dataSet = new DataSet();
            try
            {
                _con.Open();
                _userAdapter.Fill(_dataSet, "Users");
                _taskAdapter.Fill(_dataSet, "Tasks");
            }
            catch (OleDbException e) { throw new Exception(e.Message); }
            finally { _con.Close(); }
        }

        private ObservableCollection<T> ConvertToCollection<T>(List<T> original)
        { return new ObservableCollection<T>(original); }
        #endregion
    }
}
