using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Data.SqlLite.Schema
{
    //Keyvalues in Databasetable ergeben sich aus 'MajorVersion' & 'MinorVersion'
    public sealed class DatabaseVersionTableVersionEntity : AbstractEntity
    {

        //Tableversion, welche bei 10 vollen Minorversions inkrementiert
        public int DatabaseMajorVersion { get; set; }
        //Tableminorversion, welche beim 10ten inkrement die Majorversion inkrementiert und auf 0 gesetzt wird
        public int DatabaseMinorVersion { get; set; }

        //Tablename as LowerString
        public string TableName { get; set; }
        //TableDefinition als JSON serialisiert
        public string TableDefinition { get; set; }
    }
}
