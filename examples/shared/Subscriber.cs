using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using ServiceStack.Redis;

namespace Shared
{
    public class Subscriber : IDisposable
    {
        private readonly IRedisClientsManager _redisManager;
        private readonly IRedisClient _client;
        private readonly IRedisSubscription _subscription;
        private Action<EmailMessage> _handler;
        private Task listenerLoop;

        public Subscriber(IRedisClientsManager redisManager)
        {
            _redisManager = redisManager;
            _client = _redisManager.GetClient();
            _subscription = _client.CreateSubscription();
        }

        public void Listen(CancellationToken token, Action<EmailMessage> handler) 
        {
            _handler = handler;
            _subscription.OnMessage += (from, body) =>
            {
                var message = JsonSerializer.Deserialize<EmailMessage>(body);
                try
                {
                    handler(message);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message} \n {ex.StackTrace}");
                }
            };
            _subscription.SubscribeToChannels(Constants.CHANNEL);
        }

        public void Dispose()
        {
            Console.WriteLine($"Disposing");

            _subscription.Dispose();
            _client.Dispose();
        }
    }
}