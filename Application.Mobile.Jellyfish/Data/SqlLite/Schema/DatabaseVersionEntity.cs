using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Data.SqlLite.Schema
{
    //Keyvalues in Databasetable ergeben sich aus 'MajorVersion' & 'MinorVersion'
    public sealed class DatabaseVersionEntity : AbstractEntity
    {

        //Tableversion, welche bei 10 vollen Minorversions inkrementiert
        public int DatabaseMajorVersion { get; set; }
        //Tableminorversion, welche beim 10ten inkrement die Majorversion inkrementiert und auf 0 gesetzt wird
        public int DatabaseMinorVersion { get; set; }



        //Applicationmajorversion zum Zeitpunkt der Databaseversionierung
        public int ApplicationMajorVersion { get; set; }
        //Applicationminorversion zum Zeitpunkt der Databaseversionierung
        public int ApplicationMinorVersion { get; set; }
        //Allgemeiner Counter wie oft Migrationen des Schemas angestoßen werden
        public int MigrationCounter { get; set; }   
        //Migrationszeitpunkt
        public DateTime LastMigration { get; set; } 
    }
}
