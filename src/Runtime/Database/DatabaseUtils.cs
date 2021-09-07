using System.Data.SQLite;
using System.Collections.Generic;

namespace InformationRetrieval.Runtime.Database
{
    internal class DatabaseUtils
    {
        private static SQLiteConnection sqliteConnection;

        public DatabaseUtils(string path, string name)
        {
            ConnectDatabase(path);
            CreateDatabase(name);
        }

        public static void ConnectDatabase(string path)
        {
            sqliteConnection = new SQLiteConnection(path);
            sqliteConnection.Open();
        }

        public static void CreateDatabase(string name)
        {
            SQLiteConnection.CreateFile(name);
        }

        public static void CreateTable()
        {
            using (var cmd = sqliteConnection.CreateCommand())
            {
                // TODO: COLOQUEI O NOME DTIME POR CAUSA DO TIPO.
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS DATAFILE(ID INTEGER NOT NULL, SEARCHSTRING VARCHAR NOT NULL, RANKING INTEGER,  SIMILARITY INTEGER NOT NULL, DTIME DATETIME NOT NULL, PRIMARY KEY(ID AUTOINCREMENT)";
                cmd.ExecuteNonQuery();
            }
        }

        // TODO: Change name of table row. This table row will be orderedabstract by ranking. Group by searchString + ranking.
        public static void InsertInDatabase(List<List<string>> tableRow)
        {
            foreach (var item in tableRow)
            {
            }
            

            using (var cmd = sqliteConnection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO DATAFILE(SIMILARITY, SEARCHSTRING, DTIME) VALUES (@SIMILARITY, @SEARCHSTRING, @DTIME)";

                //TODO: VERIFY PARAMETERS TO DO THE INSERT
                // cmd.Parameters.Add(new SQLiteParameter("@SEARCHSTRING",));
                // cmd.Parameters.Add(new SQLiteParameter("@SIMILARITY",));
                // cmd.Parameters.Add(new SQLiteParameter("@DTIME",));
                cmd.ExecuteNonQuery();
            
            }
        }
    }
}