using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace LTC.MovieService.Entities
{
    // Named MovieRole to avoid collision with ABP IdentityRole.
    // Maps to ERD table "Roles" which represents character roles (e.g. Director, Lead Actor).
    public class MovieRole : Entity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public string Name { get; set; }
    }
}
