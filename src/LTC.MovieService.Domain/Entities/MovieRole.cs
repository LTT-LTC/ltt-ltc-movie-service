using System;
using Volo.Abp.Domain.Entities;

namespace LTC.MovieService.Entities
{
    // Named MovieRole to avoid collision with ABP IdentityRole.
    // Maps to ERD table "Roles" which represents character roles (e.g. Director, Lead Actor).
    public class MovieRole : Entity<Guid>
    {
        public string Name { get; set; }
    }
}
