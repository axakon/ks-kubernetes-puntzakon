using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using puntman.Models;
using ServiceStack.Redis;
using Shared;

namespace puntman.Data
{
    public class PunService
    {
        const string LIST_ID = "list123";
        private readonly IRedisClientsManager _redisManager;
        private readonly Publisher _pub;

        public PunService(IRedisClientsManager redisManager, Publisher pub)
        {
            _redisManager = redisManager;
            _pub = pub;
        }

        public Task<PunModel> AddPun(PunViewModel punVm)
        {
            var pun = PunModel.From(punVm);
            pun.Id = Guid.NewGuid();
            return Task.Run(() => {
                using (var redis = _redisManager.GetClient())
                {
                    redis.AddItemToList(LIST_ID, JsonSerializer.Serialize(pun));
                }
                return pun;
            });
        }

        public Task<IEnumerable<PunModel>> GetPuns()
        {
            return Task.Run(() => {
                using (var redis = _redisManager.GetClient())
                {
                    var list = redis.GetAllItemsFromList(LIST_ID);
                    return list.Select(pun => JsonSerializer.Deserialize<PunModel>(pun));
                }
            });
        }

        public bool SendPun(PunViewModel model)
        {
            using var redis = _redisManager.GetClient();

            if(redis.ContainsKey(model.Id.ToString()))
            {
                Console.WriteLine($"Already sent Sebastian pun {model.Id}, ignoring request");
                return false;
            }

            _pub.Publish(new EmailMessage
            {
                To = "patrik.nyman@axakon.se",
                Body = $"{model.Lead}\n.\n.\n.\n{model.Kicker}"
            });
            redis.Set(model.Id.ToString(), true);
            return true;
        }
    }
}