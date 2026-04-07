using System;
using Volo.Abp.Domain.Entities;

namespace LTC.MovieService.Entities
{
    public class Actor : Entity<Guid>
    {
        public string Name { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Bio { get; set; }
    }
}
