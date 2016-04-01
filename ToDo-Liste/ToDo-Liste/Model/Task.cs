using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_Liste.Model
{
    public class Task
    {
        private string _taskID;
        private string _typ;
        private string _jiraTicketNr;
        private string _überschrift;
        private string _beschreibung;
        private string _prio;
        private string _status;
        private Model.User _anforderer;
        private DateTime _datum;
        private Model.User _verantwortlicher;
        private DateTime _zieltermin;
        private List<Kommentar> _kommentare;

        public Task()
        {
            Kommentare = new List<Kommentar>();
        }

        public string TaskID
        {
            get { return _taskID; }
            set { _taskID = value; }
        }

        public string Typ
        {
            get { return _typ; }
            set { _typ = value; }
        }

        public string JiraTicketNr
        {
            get { return _jiraTicketNr; }
            set { _jiraTicketNr = value; }
        }

        public string Überschrift
        {
            get { return _überschrift; }
            set { _überschrift = value; }
        }

        public string Beschreibung
        {
            get { return _beschreibung; }
            set { _beschreibung = value; }
        }

        public string Prio
        {
            get { return _prio; }
            set { _prio = value; }
        }

        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        public Model.User Anforderer
        {
            get { return _anforderer; }
            set { _anforderer = value; }
        }

        public DateTime Datum
        {
            get { return _datum; }
            set { _datum = value; }
        }

        public Model.User Verantwortlicher
        {
            get { return _verantwortlicher; }
            set { _verantwortlicher = value; }
        }

        public DateTime Zieltermin
        {
            get { return _zieltermin; }
            set { _zieltermin = value; }
        }

        public List<Kommentar> Kommentare
        {
            get { return _kommentare; }
            set { _kommentare = value; }
        }
    }
}
