using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.Data.SqlLite.Schema
{
    [SQLite.Table("message_location")]
    public class MessageLocationEntity : AbstractEntity
    {
        private int _msgId;
        private int _msgLocationId;

        [PrimaryKey, AutoIncrement]
        public int MessageLocationId
        {
            get { return _msgLocationId; }
            set { _msgLocationId = value; }
        }
        [ForeignKey(typeof(MessageEntity))]
        public int MessageId
        {
            get { return _msgId; }
            set { _msgId = value; }
        }
        public double Course { get; set; }
        public double Speed { get; set; }
        //Reduzierte Genauigkeit
        public bool ReducedAccuracy { get; set; }
        //Vertikale Genauigkeit
        public double VerticalAccuracy { get; set; }
        //Genauigkeit
        public double Accuracy { get; set; }
        public double Altitude { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public int AltitudeReferenceSystem { get; set; }

        [OneToOne]
        public MessageEntity Message { get; set; }
        /*
         Herleitung aus 'AltitudeReferenceSystem' aus dem Namespace 'Microsoft.Maui.Devices.Sensors':
        
            //
            // Zusammenfassung:
            //     Indicates the altitude reference system to be used in defining a location.
            //
            // Hinweise:
            //     This enum is a copy of Windows.Devices.Geolocation.AltitudeReferenceSystem.
            public enum AltitudeReferenceSystem
            {
                //
                // Zusammenfassung:
                //     The altitude reference system was not specified.
                Unspecified = 0,
                //
                // Zusammenfassung:
                //     The altitude reference system is based on distance above terrain or ground level.
                Terrain = 1,
                //
                // Zusammenfassung:
                //     The altitude reference system is based on an ellipsoid (usually WGS84), which
                //     is a mathematical approximation of the shape of the Earth.
                Ellipsoid = 2,
                //
                // Zusammenfassung:
                //     The altitude reference system is based on the distance above sea level (parametrized
                //     by a so-called Geoid).
                Geoid = 3,
                //
                // Zusammenfassung:
                //     The altitude reference system is based on the distance above the tallest surface
                //     structures, such as buildings, trees, roads, etc., above terrain or ground level.
                Surface = 4
            }
         */
    }
}
