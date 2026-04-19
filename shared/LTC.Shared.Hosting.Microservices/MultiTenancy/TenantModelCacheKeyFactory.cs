using LTC.Shared.Hosting.Microservices.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LTC.Shared.Hosting.Microservices.MultiTenancy;

/// <summary>
/// Custom IModelCacheKeyFactory that includes the tenant schema name in the EF Core model cache key.
/// Without this, EF Core caches the first compiled model for all requests, causing cross-tenant data leakage
/// when using schema-per-tenant isolation (e.g. queries hit 'dbo' instead of 'ltc').
/// </summary>
public class TenantModelCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(DbContext context, bool designTime)
    {
        if (context is ITenantDbContext tenantContext)
        {
            // Include the resolved schema name so EF Core builds a distinct model per tenant schema.
            return (context.GetType(), tenantContext.GetCurrentSchema(), designTime);
        }

        return (context.GetType(), designTime);
    }
}
