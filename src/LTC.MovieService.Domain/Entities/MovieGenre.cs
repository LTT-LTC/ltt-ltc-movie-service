using System;
using Volo.Abp.Domain.Entities;

namespace LTC.MovieService.Entities
{
    public class MovieGenre : Entity<Guid>
    {
        public Guid MovieId { get; set; }
        public Guid GenreId { get; set; }
    }
}
