using System;
using Volo.Abp.Domain.Entities;

namespace LTC.MovieService.Entities
{
    public class Format : Entity<Guid>
    {
        public Guid MovieId { get; set; }
        public Guid MovieFormatId { get; set; }
    }
}
