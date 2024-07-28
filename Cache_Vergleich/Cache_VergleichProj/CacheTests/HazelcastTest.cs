using Cache_VergleichProj.DataStructs;
using Enyim.Caching.Memcached;
using Enyim.Caching;
using Hazelcast;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

using Hazelcast.DistributedObjects;

namespace Cache_VergleichProj.CacheTests
{
    public class HazelcastTest
    {
        private List<User> users = new();
        private Times times = new Times();
        private int[] getList = new int[0];
        public async Task<Times> RunTest(List<User> _users, int[] _getList, Times _times)
        {
            users = _users;
            times = _times;
            getList = _getList;

            // Hazelcast Setup
            //var hazelcastOptions = new HazelcastOptionsBuilder().Build();
            //var hazelcastClient =  HazelcastClientFactory.GetNewStartingClient(hazelcastOptions);
            //var hazelcastMap = hazelcastClient.Client.GetMapAsync<string, string>("my-distributed-map");

            // Hazelcast Setup
            var hazelcastOptions = new HazelcastOptionsBuilder().Build();
            var hazelcastClient = await HazelcastClientFactory.StartNewClientAsync(hazelcastOptions);
            var hazelcastMap = await hazelcastClient.GetMapAsync<string, string>("my-distributed-map");


            times.StartWrite = DateTime.Now;
            foreach (var user in _users)
            {
                await hazelcastMap.PutAsync(user.Id.ToString(), user.Name);
                Console.WriteLine(user.Id + "Written");
            }
            times.EndWrite = DateTime.Now;

            times.StartRead = DateTime.Now;
            for (int i = 0; i < _getList.Length; i++)
            {
                var hazelcastValue = hazelcastMap.GetAsync(_getList[i].ToString());
                Console.WriteLine($"{_getList[i]}: {hazelcastValue.Result}");
            }
            times.EndRead = DateTime.Now;


            times.diffRead = times.EndRead - times.StartRead;
            times.diffWrite = times.EndWrite - times.StartWrite;

            return times;
        }
    }
}
