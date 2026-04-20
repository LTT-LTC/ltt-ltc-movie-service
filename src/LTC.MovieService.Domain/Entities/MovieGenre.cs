using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace LTC.MovieService.Entities
{
    public class MovieGenre : Entity<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        public Guid MovieId { get; set; }
        public Guid GenreId { get; set; }
    }
}
