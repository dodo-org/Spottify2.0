using DB_Vergleich.DataStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;


namespace DB_Vergleich.DB_Tests
{
    public class MySqlTest
    {
        private List<User> users = new();
        private Times times = new Times();
        private int[] getList = new int[0];
        public Times RunTest(List<User> _users, int[] _getList, Times _times)
        {
            users = _users;
            getList = _getList;
            times = _times;
            string connectionString = "server=localhost;user=user;database=mydb;port=3306;password=password";

            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Tabelle löschen, falls sie existiert
                    string dropTableSql = "DROP TABLE IF EXISTS users";
                    MySqlCommand dropTableCmd = new MySqlCommand(dropTableSql, connection);
                    dropTableCmd.ExecuteNonQuery();
                    Console.WriteLine("Tabelle 'users' gelöscht (falls vorhanden).");

                    // Neue Tabelle erstellen
                    string createTableSql = @"
                    CREATE TABLE users (
                        Id INT ,
                        Name VARCHAR(255) NOT NULL
                    )";
                    MySqlCommand createTableCmd = new MySqlCommand(createTableSql, connection);
                    createTableCmd.ExecuteNonQuery();
                    Console.WriteLine("Tabelle 'users' erfolgreich erstellt.");



                    times.StartWrite = DateTime.Now;
                    foreach (var user in _users)
                    {
                        string sql = "INSERT INTO users (Id, Name) VALUES (@id, @name)";
                        using (var cmd = new MySqlCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", user.Id);
                            cmd.Parameters.AddWithValue("@name", user.Name);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine(user.Id);
                        }
                    }
                    times.EndWrite = DateTime.Now;
                    Console.WriteLine("Daten Geschrieben");

                    // read in redis

                    times.StartRead = DateTime.Now;
                    for (int counter1 = 0; counter1 < getList.Count(); counter1++)
                    {
                        string sql = "SELECT * FROM users WHERE id = @id";
                        using (var cmd = new MySqlCommand(sql, connection))
                        {
                            cmd.Parameters.AddWithValue("@id", getList[counter1]);
                            using (MySqlDataReader xamppDataReader = cmd.ExecuteReader())
                            {
                                while (xamppDataReader.Read())
                                {
                                    Console.WriteLine("id= " + getList[counter1] + " " + xamppDataReader.GetString("Name"));
                                }
                            }
                        }

                    }
                    times.EndRead = DateTime.Now;
                    Console.WriteLine("Press any key to exit MySql...");
                    Console.ReadKey();

                }
                catch (Exception ex)
                {
                       Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
                    return new Times();
                }
            }


            times.diffRead = times.EndRead - times.StartRead;
            times.diffWrite = times.EndWrite - times.StartWrite;

            return times;
        }

    }
}
