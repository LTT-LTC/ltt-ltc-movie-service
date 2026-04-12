using System;
using Volo.Abp.Domain.Entities;

namespace LTC.MovieService.Entities
{
    public class MovieFormat : Entity<Guid>
    {
        public Guid MovieId { get; set; }
        public Guid FormatId { get; set; }
        public string? Language { get; set; }
        public string? Subtitle { get; set; }
    }
}
