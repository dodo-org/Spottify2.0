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
                string createTableQuery = "CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY, name TEXT)";
                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Insert data into the table

                times.StartWrite = DateTime.Now;
                foreach (User user in users)
                {
                    string insertDataQuery = $"INSERT INTO users (id, name) VALUES ({user.Id}, {user.Name})";
                    using (SQLiteCommand command = new SQLiteCommand(insertDataQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine(user.Id + " eingefügt.");
                    }
                }
                times.EndWrite = DateTime.Now;


                // Retrieve data from the table

                times.StartRead = DateTime.Now;
                for (int counter1 = 0; counter1 < getList.Count(); counter1++)
                {
                    string selectDataQuery = $"SELECT * FROM users WHERE Id = {getList[counter1]}";
                    using (SQLiteCommand command = new SQLiteCommand(selectDataQuery, connection))
                    {
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string name = reader.GetString(1);

                                Console.WriteLine($"Nr:{counter1}, ID: {id}");
                            }
                        }
                    }
                }
                times.EndRead = DateTime.Now;


                connection.Close();
            }

            times.diffRead = times.EndRead - times.StartRead;
            times.diffWrite = times.EndWrite - times.StartWrite;
            return times;
        }
    }
}
