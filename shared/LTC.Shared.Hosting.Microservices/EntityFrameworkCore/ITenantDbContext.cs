namespace LTC.Shared.Hosting.Microservices.EntityFrameworkCore;

/// <summary>
/// Marker interface for DbContexts that support schema-per-tenant isolation.
/// EF Core model caching uses this to cache a separate compiled model per schema.
/// </summary>
public interface ITenantDbContext
{
    string GetCurrentSchema();
}
