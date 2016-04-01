using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_Liste.Model
{
    public class User
    {
        #region Private Member
        private int _id;
        private string __vorname;
        private string _nachname;
        private string _mail;
        private string _initialien;
        private string _password;
        #endregion

        #region Public Member
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Vorname
        {
            get { return __vorname; }
            set { __vorname = value; }
        }

        public string Nachname
        {
            get { return _nachname; }
            set { _nachname = value; }
        }

        public string Mail
        {
            get { return _mail; }
            set { _mail = value; }
        }

        public string Initialien
        {
            get { return _initialien; }
            set { _initialien = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        #endregion
    }
}
