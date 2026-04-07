using System;
using Volo.Abp.Domain.Entities;

namespace LTC.MovieService.Entities
{
    public class Studio : Entity<Guid>
    {
        public string Name { get; set; }
        public string? Country { get; set; }
    }
}
