using System;
using Volo.Abp.Domain.Entities;

namespace LTC.MovieService.Entities
{
    public class Rating : Entity<Guid>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
