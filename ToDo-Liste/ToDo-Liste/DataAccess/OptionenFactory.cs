using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ToDo_Liste.DataAccess
{
    public class OptionenFactory
    {
        private Model.Optionen _optionen;

        public OptionenFactory()
        {
            Load();
        }

        public Model.Optionen GetOptionen()
        { return _optionen; }

        public void Reload()
        { Load(); }

        public void SaveOptionen(Model.Optionen optionen)
        {
            DataSet dsoptionen = new DataSet();
            DataTable dtOptionen = new DataTable("Optionen");

            DataColumn _pfadDatenbank = new DataColumn("PfadDatenbank");
            DataColumn _pfadToUserFile = new DataColumn("PfadToUserFile");
            DataColumn _pfadToTaskFile = new DataColumn("PfadToTaskFile");
            dtOptionen.Columns.Add(_pfadDatenbank);
            dtOptionen.Columns.Add(_pfadToUserFile);
            dtOptionen.Columns.Add(_pfadToTaskFile);
            dsoptionen.Tables.Add(dtOptionen);

            DataRow row = dtOptionen.NewRow();
            row["PfadDatenbank"] = optionen.PfadDatenbank;
            dtOptionen.Rows.Add(row);

            dsoptionen.WriteXml((Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) + "\\Data\\Optionen.xml");
        }

        private void Load()
        {
            _optionen = new Model.Optionen();
            DataSet result = new DataSet();
            result.ReadXml((Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)) + "\\Data\\Optionen.xml");
            _optionen.PfadDatenbank = result.Tables["Optionen"].Rows[0]["PfadDatenbank"].ToString();
        }
    }
}
