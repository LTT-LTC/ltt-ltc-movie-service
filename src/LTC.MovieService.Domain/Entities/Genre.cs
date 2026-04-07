using System;
using Volo.Abp.Domain.Entities;

namespace LTC.MovieService.Entities
{
    public class Genre : Entity<Guid>
    {
        public string Name { get; set; }
    }
}
