using Microsoft.AspNetCore.Http;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace LTC.MovieService.MultiTenancy;

public class TenantSchemaResolver : ITenantSchemaResolver, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantSchemaResolver(
        ICurrentTenant currentTenant,
        IHttpContextAccessor httpContextAccessor)
    {
        _currentTenant = currentTenant;
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetSchemaName()
    {
        // 1. Prefer explicit client-provided tenant key from header/cookie.
        //    Authenticated requests may still resolve as host at ABP level, so schema routing needs
        //    to read the raw tenant key directly from the request first.
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            var tenantKey = "X-Tenant";
            if (httpContext.Request.Headers.TryGetValue(tenantKey, out var headerValue))
            {
                var tenantName = headerValue.ToString().Trim();
                if (!string.IsNullOrWhiteSpace(tenantName))
                    return tenantName;
            }

            if (httpContext.Request.Cookies.TryGetValue(tenantKey, out var cookieValue))
            {
                var tenantName = cookieValue?.Trim();
                if (!string.IsNullOrWhiteSpace(tenantName))
                    return tenantName;
            }
        }

        // 2. Fall back to ABP-resolved tenant name (from AbpTenants table).
        if (!string.IsNullOrWhiteSpace(_currentTenant.Name))
            return _currentTenant.Name;

        // 3. Default schema
        return "dbo";
    }
}

public interface ITenantSchemaResolver
{
    string GetSchemaName();
}
