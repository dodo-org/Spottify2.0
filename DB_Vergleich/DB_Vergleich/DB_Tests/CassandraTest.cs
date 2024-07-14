using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Cassandra;
using DB_Vergleich.DataStructs;
using MySqlX.XDevAPI.Common;

namespace DB_Vergleich.DB_Tests
{
    public class CassandraTest
    {
        private static Cluster cluster;
        private static ISession session;
        private List<User> users = new();
        private Times times = new Times();
        private int[] getList = new int[0];
        public Times RunTest(List<User> _users,int[] _getList, Times _times)
        {
            users = _users;
            getList = _getList;
            times = _times;
            try
            {
                // Verbindungsaufbau
                Connect();

                // Beispiel-Abfragen
                CreateSchema();
                InsertData();
                ReadData();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ein Fehler ist aufgetreten: {ex.Message}");
                return new Times();
            }
            finally
            {
                // Ressourcen freigeben
                Disconnect();
            }
            times.diffRead  = times.EndRead - times.StartRead;
            times.diffWrite = times.EndWrite - times.StartWrite;
            return times;
        }

        // Verbindungsaufbau zur Cassandra-Datenbank
        public static void Connect()
        {
            Console.WriteLine("Verbindungs aufbau.");
            cluster = Cluster.Builder()
                .AddContactPoint("127.0.0.1")
                .Build();
            session = cluster.Connect();
            Console.WriteLine("Verbindung zu Cassandra hergestellt.");
        }

        // Ressourcen freigeben
        public static void Disconnect()
        {
            session.Dispose();
            cluster.Dispose();
            Console.WriteLine("Verbindung zu Cassandra geschlossen.");
        }

        // Schema (Keyspace und Tabelle) erstellen
        public static void CreateSchema()
        {
            session.Execute("CREATE KEYSPACE IF NOT EXISTS testks WITH replication = {'class': 'SimpleStrategy', 'replication_factor': 1};");
            session.Execute("CREATE TABLE IF NOT EXISTS testks.users (Id INT PRIMARY KEY , Name TEXT);");
            Console.WriteLine("Schema erstellt.");
        }

        // Daten einfügen
        public void InsertData()
        {
            times.StartWrite = DateTime.Now;
            foreach (User user in users)
            {
                var insertQuery = "INSERT INTO testks.users (Id, Name) VALUES (?, ?);";
                var preparedStatement = session.Prepare(insertQuery);
                var boundStatement = preparedStatement.Bind(user.Id, user.Name);
                session.Execute(boundStatement);
                Console.WriteLine(user.Id + " eingefügt.");
            }
            times.EndWrite = DateTime.Now;
            Console.WriteLine("Daten Geschrieben");
        }

        // Daten lesen
        public void ReadData()
        {
            times.StartRead = DateTime.Now;
            for (int counter1 = 0; counter1 < getList.Count(); counter1++)
            {
               
                var selectQuery = "SELECT Id, Name FROM testks.users WHERE Id = ?;";
                var preparedStatement = session.Prepare(selectQuery);
                var boundStatement = preparedStatement.Bind(getList[counter1]);
                var result = session.Execute(boundStatement);

                foreach (var row in result)
                {
                    //Console.Write($", Name: {row.GetValue<string>("Name")}");
                    Console.Write($"ID: {row.GetValue<int>("id")}" );
                    Console.WriteLine();
                }

            }
            times.EndRead = DateTime.Now;
            
        }


    }
}
