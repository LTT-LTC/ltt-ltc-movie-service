using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace LTC.MovieService.Entities
{
    public class MovieActorRole : Entity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public Guid MovieActorId { get; set; }
        public Guid RoleId { get; set; }
        public string? CharacterName { get; set; }
    }
}
