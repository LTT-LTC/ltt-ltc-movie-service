using System;
using Volo.Abp.Domain.Entities;

namespace LTC.MovieService.Entities
{
    public class MovieActor : Entity<Guid>
    {
        public Guid MovieId { get; set; }
        public Guid ActorId { get; set; }
    }
}
