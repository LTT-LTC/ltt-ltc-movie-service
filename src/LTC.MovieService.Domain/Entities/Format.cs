using System;
using Volo.Abp.Domain.Entities;

namespace LTC.MovieService.Entities
{
    public class Format : Entity<Guid>
    {
        public string Name { get; set; }
    }
}
