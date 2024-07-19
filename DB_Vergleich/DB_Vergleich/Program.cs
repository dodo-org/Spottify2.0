using System;

using DB_Vergleich.DataStructs;
using DB_Vergleich.DB_Tests;

// prepare Date

int datacapSafe = 1000,
    datacapGet  = 1000;

Times
    Cassandra = new Times(),
    mongo = new Times(),
    MySql = new Times(),
    postgress = new Times();




List<User> _users   = new List<User>();
int[] getList       = new int[datacapGet];





Console.WriteLine("Start Preparing Date");

for (int i = 0; i < datacapSafe; i++)
{
    User thmpUser = new(i, "User" + i);
    _users.Add(thmpUser);
}
Random random = new Random();

for (int j = 0; j < datacapGet;  j++)
{
    getList[j] = random.Next(0,datacapSafe);
}

Console.WriteLine("End prepare data");


// Test Cassandra

Console.WriteLine("----------------------------");
Console.WriteLine("Start Cassandra DB Test");
CassandraTest CassTest = new CassandraTest();

Cassandra = CassTest.RunTest(_users, getList, Cassandra);

Console.WriteLine("Ende Cassandra DB Test ");
Console.WriteLine("----------------------------");
// Test Mongo

Console.WriteLine("----------------------------");
Console.WriteLine("Start Mongo DB Test");
MongoTest _MongoTest = new MongoTest();

mongo = _MongoTest.RunTest(_users, getList,mongo);
Console.WriteLine("Ende Mongo DB Test ");
Console.WriteLine("----------------------------");
// MySql

Console.WriteLine("----------------------------");
Console.WriteLine("Start MySql DB Test");

MySqlTest _MySqlTest = new MySqlTest();

MySql = _MySqlTest.RunTest(_users, getList, MySql);
Console.WriteLine("Ende MySql DB Test ");
Console.WriteLine("----------------------------");

// Postgress

Console.WriteLine("----------------------------");
Console.WriteLine("Start Postgress DB Test");

PostgressTest _PostgressTest = new PostgressTest();

postgress = _PostgressTest.RunTest(_users, getList, postgress);
Console.WriteLine("Ende Postgress DB Test ");
Console.WriteLine("----------------------------");







// Show Stats 


Console.WriteLine("Cassandra    Safe    Time: " + Cassandra.diffWrite.TotalSeconds);
Console.WriteLine("MongoDB      Safe    Time: " + mongo.diffWrite.TotalSeconds);
Console.WriteLine("MySql        Safe    Time: " + MySql.diffWrite.TotalSeconds);
Console.WriteLine("PostGress    Safe    Time: " + postgress.diffWrite.TotalSeconds);

Console.WriteLine();
Console.WriteLine("-----------------------------------------------------");
Console.WriteLine();

Console.WriteLine("Cassandra    Get     Time: " + Cassandra.diffRead.TotalSeconds);
Console.WriteLine("MongoDB      Get     Time: " + mongo.diffRead.TotalSeconds);
Console.WriteLine("MySql        Get     Time: " + MySql.diffRead.TotalSeconds);
Console.WriteLine("PostGress    Get     Time: " + postgress.diffRead.TotalSeconds);


