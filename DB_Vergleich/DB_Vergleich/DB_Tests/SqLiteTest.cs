using DB_Vergleich.DataStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SQLite;

namespace DB_Vergleich.DB_Tests
{
    public class SqLiteTest
    {
        private List<User> users = new();
        private Times times = new Times();
        private int[] getList = new int[0];
        public Times RunTest(List<User> _users, int[] _getList, Times _times)
        {
            string connectionString = "Data Source=\\C:\\Custom\\Projekte\\SystemArchitecture\\Spottify2.0\\DB_Vergleich\\DBData/example.db;Version=3;";

            // Create a new database connection
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Create a table
                string createTableQuery = "CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY, name TEXT, age INTEGER)";
                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Insert data into the table
                string insertDataQuery = "INSERT INTO users (name, age) VALUES ('Alice', 30), ('Bob', 25)";
                using (SQLiteCommand command = new SQLiteCommand(insertDataQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Retrieve data from the table
                string selectDataQuery = "SELECT * FROM users";
                using (SQLiteCommand command = new SQLiteCommand(selectDataQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            int age = reader.GetInt32(2);

                            Console.WriteLine($"ID: {id}, Name: {name}, Age: {age}");
                        }
                    }
                }

                connection.Close();
            }
        

            return times;
        }
    }
}
