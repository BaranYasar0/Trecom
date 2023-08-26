using StackExchange.Redis;

namespace Trecom.Api.Services.BasketService.Services
{
    public class RedisService
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IConfiguration _configuration;

        public RedisService(IConfiguration configuration)
        {
            _configuration = configuration;
            _redis= ConnectionMultiplexer.Connect($"{_configuration["RedisSettings:Host"]}:{_configuration["RedisSettings:Port"]}");
        }

        public IDatabase GetDb(int db = 0)
        {
            return _redis.GetDatabase(db);
        }

    }
}
