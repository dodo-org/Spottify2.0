using Cache_VergleichProj.DataStructs;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache_VergleichProj.CacheTests
{
    public class RedisTest
    {
        private List<User> users = new();
        private Times times = new Times();
        private int[] getList = new int[0];
        public Times RunTest(List<User> _users, int[] _getList, Times _times)
        {
            users = _users;
            times = _times;
            getList = _getList;

            // Redis Setup
            string redisConnectionString = "localhost:6379";
            var redis = ConnectionMultiplexer.Connect(redisConnectionString);
            IDatabase redisDb = redis.GetDatabase();

            times.StartWrite = DateTime.Now;
            foreach (var user in _users)
            {
                redisDb.StringSet(user.Id.ToString(), user.Name);
                Console.WriteLine(user.Id + "Written");
            }
            times.EndWrite = DateTime.Now;

            times.StartRead = DateTime.Now;
            for (int i = 0; i< _getList.Length; i++)
            {
                var redisValue = redisDb.StringGet(_getList[i].ToString());
                Console.WriteLine($"{_getList[i]}: {redisValue.ToString()}");
            }
            times.EndRead = DateTime.Now;

            times.diffRead = times.EndRead - times.StartRead;
            times.diffWrite = times.EndWrite - times.StartWrite;

            return times;
        }
    }
}
