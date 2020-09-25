using System;
using System.ComponentModel.DataAnnotations;

namespace puntman.Models
{
    public class PunViewModel
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Lead { get; set; }
        public string Kicker { get; set; }

        public static PunViewModel From(PunModel pun)
        {
            return new PunViewModel
            {
                Id = pun.Id,
                Lead = pun.Lead,
                Kicker = pun.Kicker
            };
        }
    }
}