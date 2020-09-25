using System;

namespace puntman.Models
{
    public class PunModel
    {
        public Guid Id { get; set; }
        public string Lead { get; set; }
        public string Kicker { get; set; }

        public static PunModel From(PunViewModel pun)
        {
            return new PunModel
            {
                Id = pun.Id,
                Lead = pun.Lead,
                Kicker = pun.Kicker
            };
        }
    }
}