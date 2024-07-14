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
            // Verbindungseinstellungen
            string host = "localhost";
            string port = "5432";
            string username = "postgres";
            string password = "your_password";
            string database = "your_database";

            // Verbindungszeichenfolge
            string connectionString = $"Host={host};Port={port};Username={username};Password={password};Database={database}";

            // Verbindung zur Datenbank herstellen
            using (var conn = new NpgsqlConnection(connectionString))
            {
                try
                {
                    // Öffnen der Verbindung
                    conn.Open();
                    Console.WriteLine("Verbindung erfolgreich hergestellt.");

                    // speichern
                    times.StartWrite = DateTime.Now;
                    foreach (User user in users)
                    {
                        string insertQuery = "INSERT INTO users (id, name) VALUES (@id, @name) RETURNING id";
                        using (var cmd = new NpgsqlCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("id", user.Id );
                            cmd.Parameters.AddWithValue("name", user.Name);

                            int newUserId = (int)cmd.ExecuteScalar();
                            Console.WriteLine($"Neuer User gespeichert mit ID: {newUserId}");
                        }
                    }
                    times.EndWrite = DateTime.Now;
                    Console.WriteLine("Daten Geschrieben");

                    // User Auslesen
                    times.StartRead = DateTime.Now;
                    for (int counter1 = 0; counter1 < getList.Count(); counter1++)
                    {
                        string selectQuery = "SELECT id, username, email FROM users WHERE username = @username";
                        using (var cmd = new NpgsqlCommand(selectQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("id", getList[counter1]);

                            using (var reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    int id = reader.GetInt32(0);
                                    string name = reader.GetString(1);

                                    Console.WriteLine($"ID: {id}, Username: {name}");
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
                    Console.WriteLine("Verbindung geschlossen.");
                }
            }
        

            return times;
        }




    }
}
