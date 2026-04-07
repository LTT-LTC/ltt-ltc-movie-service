using System;
using Volo.Abp.Domain.Entities;

namespace LTC.MovieService.Entities
{
    public class MovieFormat : Entity<Guid>
    {
        public string Format { get; set; }
        public string? Language { get; set; }
        public string? Subtitle { get; set; }
    }
}
