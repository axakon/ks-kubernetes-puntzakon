using System.Text.Json;
using ServiceStack.Redis;

namespace Shared
{
    public class Publisher
    {
        private readonly IRedisClientsManager _redisManager;

        public Publisher(IRedisClientsManager redisManager)
        {
            _redisManager = redisManager;
        }

        public void Publish(EmailMessage msg)
        {
            using var _client = _redisManager.GetClient();
            _client.PublishMessage(Constants.CHANNEL, JsonSerializer.Serialize(msg));
        }
    }
}