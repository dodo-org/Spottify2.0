using DB_Vergleich.DataStructs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;

namespace DB_Vergleich.DB_Tests
{
    public class PostgressTest
    {
        private List<User> users = new();
        private Times times = new Times();
        private int[] getList = new int[0];
        public Times RunTest(List<User> _users, int[] _getList, Times _times)
        {
            users = _users;
            getList = _getList;
            times = _times;

            // Verbindungseinstellungen
            string host = "localhost";
            string port = "5432";
            string username = "user";
            string password = "password";
            string database = "mydb";

            // Verbindungszeichenfolge
            //string connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={database}";
            string connectionString = "Host=localhost;Database=mydb;Username=user;Password=password;Port=5432";
            // Verbindung zur Datenbank herstellen
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    // Öffnen der Verbindung
                    conn.Open();
                    Console.WriteLine("Verbindung erfolgreich hergestellt.");


                    // SQL-Befehl zum Löschen der Tabelle, falls sie existiert
                    string dropTableQuery = "DROP TABLE IF EXISTS users;";

                    using (var dropCmd = new NpgsqlCommand(dropTableQuery, conn))
                    {
                        dropCmd.ExecuteNonQuery();
                        Console.WriteLine("Tabelle gelöscht, falls sie existierte.");
                    }

                    // SQL-Befehl zum Erstellen einer neuen Tabelle
                    string createTableQuery = @"
                        CREATE TABLE users (
                            id INT PRIMARY KEY,
                            name VARCHAR(100)
                        );";

                    using (var createCmd = new NpgsqlCommand(createTableQuery, conn))
                    {
                        createCmd.ExecuteNonQuery();
                        Console.WriteLine("Neue Tabelle erfolgreich erstellt.");
                    }
                    // speichern
                    times.StartWrite = DateTime.Now;
                    Console.WriteLine("test");
                    foreach (User user in users)
                    {
                        string insertQuery = "INSERT INTO users (id, name) VALUES (@id, @name) RETURNING id";
                        using (var cmd = new NpgsqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("id", user.Id );
                            cmd.Parameters.AddWithValue("name", user.Name);

                            int newUserId = (int)cmd.ExecuteScalar();
                            Console.WriteLine(newUserId + " eingefügt.");
                        }
                    }
                    
                    times.EndWrite = DateTime.Now;
                    
                    // User Auslesen
                    times.StartRead = DateTime.Now;
                    for (int counter1 = 0; counter1 < getList.Count(); counter1++)
                    {
                        string selectQuery = "SELECT id, name FROM users WHERE id = @id";
                        using (var cmd = new NpgsqlCommand(selectQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("id", getList[counter1]);

                            using (var reader = cmd.ExecuteReader())
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

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fehler bei der Verbindung: {ex.Message}");
                }
                finally
                {
                    // Schließen der Verbindung
                    conn.Close();
                }
            }

            times.diffRead = times.EndRead - times.StartRead;
            times.diffWrite = times.EndWrite - times.StartWrite;

            return times;
        }




    }
}
