using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp.ApplicationSpecific
{
    public static class Global
    {
        public const string DatabaseFilename = "jellydb.db3";
        public const string ApplicationConfigFilename = "jellyconf.json";
        public const string LoggingFolder = "log";
        public const string MediaFolder = "media";
        public const string MediaPhotosFolder = "pictures";
        public const string MediaVideosFolder = "videos";

        public const SQLite.SQLiteOpenFlags DatabaseFlags =
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache; // Multithread DB Access kompatibel,

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        //FileSystem.AppDataDirectory: Für DeviceUser via Default-Filesystemexplorer nicht erreichbar
        public static string ApplicationConfigPath =>
            Path.Combine(FileSystem.AppDataDirectory, ApplicationConfigFilename);

        public static string CacheDirectory =>
            Path.Combine(FileSystem.CacheDirectory);

        public static string MediaDirectory =>
            Path.Combine(FileSystem.AppDataDirectory, MediaFolder);
        public static string MediaPhotosDirectory =>
            Path.Combine(FileSystem.AppDataDirectory, MediaFolder, MediaPhotosFolder);
        public static string MediaVideosDirectory =>
            Path.Combine(FileSystem.AppDataDirectory, MediaFolder, MediaVideosFolder);

        public static string LoggingDirectory =>
            Path.Combine(FileSystem.CacheDirectory, LoggingFolder);
    }
}
