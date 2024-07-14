using System;
using NRedisStack;
using NRedisStack.DataTypes;
using NRedisStack.RedisStackCommands;
using StackExchange.Redis;


using System.Diagnostics.Metrics;
using DB_Vergleich.DataStructs;
using DB_Vergleich.DB_Tests;

// prepare Date

int datacapSafe = 10,
    datacapGet  = 10;

Times
    Cassandra   = new Times(),
    mongo       = new Times(),
    MySql       = new Times(),
    postgress   = new Times(),
    sqlite      = new Times();




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

CassandraTest CassTest = new CassandraTest();

Cassandra = CassTest.RunTest(_users, getList, Cassandra);

// Test Mongo

MongoTest _MongoTest = new MongoTest();

mongo = _MongoTest.RunTest(_users, getList,mongo);


// MySql
MySqlTest _MySqlTest = new MySqlTest();

MySql = _MySqlTest.RunTest(_users, getList, MySql);

// Postgress

// Sqlite







// Show Stats 


Console.ReadLine();

Console.Clear();
Console.WriteLine("Cassandra    Safe    Time: " + Cassandra.diffWrite.TotalSeconds);
Console.WriteLine("MongoDB      Safe    Time: " + mongo.diffWrite.TotalSeconds);
Console.WriteLine("MySql        Safe    Time: " + MySql.diffWrite.TotalSeconds);
Console.WriteLine("PostGress    Safe    Time: " + postgress.diffWrite.TotalSeconds);
Console.WriteLine("SqLite       Safe    Time: " + sqlite.diffWrite.TotalSeconds);

Console.WriteLine("Cassandra    Get     Time: " + Cassandra.diffRead.TotalSeconds);
Console.WriteLine("MongoDB      Get     Time: " + mongo.diffRead.TotalSeconds);
Console.WriteLine("MySql        Get     Time: " + MySql.diffRead.TotalSeconds);
Console.WriteLine("PostGress    Get     Time: " + postgress.diffRead.TotalSeconds);
Console.WriteLine("SqLite       Get     Time: " + sqlite.diffRead.TotalSeconds);


