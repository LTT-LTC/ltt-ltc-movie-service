using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LTC.MovieService.EntityFrameworkCore;

/// <summary>
/// Custom IModelCacheKeyFactory that includes the tenant schema name in the EF Core model cache key.
/// EF Core caches the compiled model from the first request; without this factory every request
/// would use that cached model regardless of the active tenant, causing cross-tenant data leakage.
/// </summary>
public class TenantModelCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(DbContext context, bool designTime)
    {
        if (context is MovieServiceDbContext movieContext)
        {
            // Include the resolved schema so EF Core maintains a separate model per tenant schema.
            return (context.GetType(), movieContext.GetCurrentSchema(), designTime);
        }

        return (context.GetType(), designTime);
    }
}
