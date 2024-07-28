using System;

using Cache_VergleichProj.DataStructs;
using Cache_VergleichProj.CacheTests;

// Imports
// dotnet add package StackExchange.Redis
// dotnet add package EnyimMemcached
// dotnet add package Hazelcast.Net


// prepare Date

int datacapSafe = 10000,
    datacapGet = 10000;

Times
    Redis = new Times(),
    Memcache = new Times(),
    Hazelcast = new Times();




List<User> _users = new List<User>();
int[] getList = new int[datacapGet];





Console.WriteLine("Start Preparing Date");

for (int i = 0; i < datacapSafe; i++)
{
    User thmpUser = new(i, "User" + i);
    _users.Add(thmpUser);
}
Random random = new Random();

for (int j = 0; j < datacapGet; j++)
{
    getList[j] = random.Next(0, datacapSafe);
}

Console.WriteLine("End prepare data");


// Test Redis

Console.WriteLine("----------------------------");
Console.WriteLine("Start Redis DB Test");
RedisTest Redis_Test = new RedisTest();

Redis = Redis_Test.RunTest(_users, getList, Redis);

Console.WriteLine("Ende Redis DB Test ");
Console.WriteLine("----------------------------");

// Test Memcache

Console.WriteLine("----------------------------");
Console.WriteLine("Start Memcache DB Test");
MemcacheTest Memcache_Test = new MemcacheTest();

Memcache = Memcache_Test.RunTest(_users, getList, Memcache);

Console.WriteLine("Ende Memcache DB Test ");
Console.WriteLine("----------------------------");


// Test Hazelcast

Console.WriteLine("----------------------------");
Console.WriteLine("Start Hazelcast DB Test");
HazelcastTest Hazelcast_Test = new HazelcastTest();

Hazelcast = Hazelcast_Test.RunTest(_users, getList, Hazelcast).Result;

Console.WriteLine("Ende Hazelcast DB Test ");
Console.WriteLine("----------------------------");



// Show Stats 


Console.WriteLine("Redis        Safe    Time: " + Redis.diffWrite.TotalSeconds);
Console.WriteLine("Memcache     Safe    Time: " + Memcache.diffWrite.TotalSeconds);
Console.WriteLine("Hazelcast    Safe    Time: " + Hazelcast.diffWrite.TotalSeconds);

Console.WriteLine();
Console.WriteLine("-----------------------------------------------------");
Console.WriteLine();

Console.WriteLine("Redis        Get     Time: " + Redis.diffRead.TotalSeconds);
Console.WriteLine("Memcache     Get     Time: " + Memcache.diffRead.TotalSeconds);
Console.WriteLine("Hazelcast    Get     Time: " + Hazelcast.diffRead.TotalSeconds);

Console.ReadLine();