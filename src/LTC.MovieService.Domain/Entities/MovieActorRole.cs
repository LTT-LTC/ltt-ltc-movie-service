using System;
using Volo.Abp.Domain.Entities;

namespace LTC.MovieService.Entities
{
    public class MovieActorRole : Entity<Guid>
    {
        public Guid MovieActorId { get; set; }
        public Guid RoleId { get; set; }
        public string? CharacterName { get; set; }
    }
}
