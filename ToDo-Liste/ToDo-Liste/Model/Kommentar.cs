using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_Liste.Model
{
    public class Kommentar
    {
        private string _datum;
        private string _verfasser;
        private string _text;

        public string Datum
        {
            get { return _datum; }
            set { _datum = value; }
        }

        public string Verfasser
        {
            get { return _verfasser; }
            set { _verfasser = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
    }
}
