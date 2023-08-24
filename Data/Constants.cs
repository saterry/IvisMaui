using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IvisMaui.Data
{
    public static class Constants
    {
        public const string DatabaseFilename = "IvisMaui.ivis.db";

        public const SQLite.SQLiteOpenFlags Flags =
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        // Use http cleartext for local deployment. Change to https for production

        //public static string LocalhostUrl = "localhost";
        public static string LocalhostUrl = "192.168.1.217";

        public static string Scheme = "https"; // or http
        public static string Port = "5445";
        public static string RestUrl = $"{Scheme}://{LocalhostUrl}:{Port}/api/History/Upload";

    }
}
