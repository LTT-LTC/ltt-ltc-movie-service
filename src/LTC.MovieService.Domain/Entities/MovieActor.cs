using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace LTC.MovieService.Entities
{
    public class MovieActor : Entity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public Guid MovieId { get; set; }
        public Guid ActorId { get; set; }
    }
}
