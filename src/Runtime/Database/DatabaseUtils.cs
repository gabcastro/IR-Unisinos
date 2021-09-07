using System.Data.SQLite;
using System.Collections.Generic;
using System.IO;
using InformationRetrieval.Runtime.RankManager;
using System;

namespace InformationRetrieval.Runtime.Database
{
    internal class DatabaseUtils
    {
        private static SQLiteConnection sqliteConnection;

        public DatabaseUtils(string path, string name)
        {
            var fullPath = string.Concat(path, name);
            CreateDatabase(fullPath);
            ConnectDatabase(fullPath);
            CreateTable();
        }

        public static void ConnectDatabase(string fullPath)
        {
            var dir = Directory.GetCurrentDirectory();
            dir = string.Concat("Data Source= ", dir, "\\", fullPath, ";");
            sqliteConnection = new SQLiteConnection(dir);
            sqliteConnection.Open();
        }

        public static void CreateDatabase(string name)
        {
            SQLiteConnection.CreateFile(name);
        }

        public static void CreateTable()
        {
            using var cmd = sqliteConnection.CreateCommand();
            cmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS IR_HISTORY (
                        ID INTEGER NOT NULL, 
                        QUERYSTRING VARCHAR(255) NOT NULL, 
                        DOCNAME VARCHAR(255) NOT NULL,
                        SIMILARITY INTEGER NOT NULL, 
                        RANKING INTEGER NOT NULL,
                        DTIME DATETIME NOT NULL, 
                        PRIMARY KEY(ID AUTOINCREMENT)
                    )";
            cmd.ExecuteNonQuery();
        }

        public void InsertInDatabase(string queryString, List<RankRetrieval> rr)
        { 
            foreach (var item in rr)
            {
                using var cmd = sqliteConnection.CreateCommand();
                cmd.CommandText = @"INSERT INTO IR_HISTORY (
                    QUERYSTRING, 
                    DOCNAME,
                    SIMILARITY, 
                    RANKING,
                    DTIME
                ) VALUES (
                    @QUERYSTRING, 
                    @DOCNAME,
                    @SIMILARITY, 
                    @RANKING,
                    @DTIME
                )";

                cmd.Parameters.Add(new SQLiteParameter("@QUERYSTRING", queryString));
                cmd.Parameters.Add(new SQLiteParameter("@DOCNAME", item.DocName));
                cmd.Parameters.Add(new SQLiteParameter("@SIMILARITY", item.Similarity));
                cmd.Parameters.Add(new SQLiteParameter("@RANKING", item.Ranking));
                cmd.Parameters.Add(new SQLiteParameter("DTIME", DateTime.Now.ToString()));
                cmd.ExecuteNonQuery();
            }
        }
    }
}