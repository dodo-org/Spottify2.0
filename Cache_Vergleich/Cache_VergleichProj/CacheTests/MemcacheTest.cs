using Cache_VergleichProj.DataStructs;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Diagnostics;


namespace Cache_VergleichProj.CacheTests
{
    public class MemcacheTest
    {
        private List<User> users = new();
        private Times times = new Times();
        private int[] getList = new int[0];
        public Times RunTest(List<User> _users, int[] _getList, Times _times)
        {
            users = _users;
            times = _times;
            getList = _getList;

            // Memcached Setup
            var memcachedOptions = Options.Create(new MemcachedClientOptions
            {
                Servers = new System.Collections.Generic.List<Server>
                {
                    new Server { Address = "localhost", Port = 11211 }
                },
                Protocol = MemcachedProtocol.Binary
            });

            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

            var configuration = new ConfigurationBuilder().Build();

            var memcachedClientConfig = new MemcachedClientConfiguration(
                loggerFactory,
                memcachedOptions,
                configuration,
                new DefaultTranscoder(),
                new DefaultKeyTransformer());

            var memcachedClient = new MemcachedClient(loggerFactory, memcachedClientConfig);


            times.StartWrite = DateTime.Now;
            foreach (var user in _users)
            {
                memcachedClient.Store(StoreMode.Set, user.Id.ToString(), user.Name);
                Console.WriteLine(user.Id + "Written");
            }
            times.EndWrite = DateTime.Now;

            times.StartRead = DateTime.Now;
            for (int i = 0; i < _getList.Length; i++)
            {
                var memcachedValue = memcachedClient.Get<string>(_getList[i].ToString());
                Console.WriteLine($"{_getList[i]}: {memcachedValue.ToString()}");
            }
            times.EndRead = DateTime.Now;

            times.diffRead = times.EndRead - times.StartRead;
            times.diffWrite = times.EndWrite - times.StartWrite;

            return times;
        }
    }
}
