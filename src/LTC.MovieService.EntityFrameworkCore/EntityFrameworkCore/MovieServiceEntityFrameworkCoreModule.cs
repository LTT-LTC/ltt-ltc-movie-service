using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace LTC.MovieService.EntityFrameworkCore;

[DependsOn(
    typeof(MovieServiceDomainModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(Volo.Abp.TenantManagement.EntityFrameworkCore.AbpTenantManagementEntityFrameworkCoreModule)
    )]
public class MovieServiceEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        MovieServiceEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MovieServiceDbContext>(options =>
        {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        // Required for schema-per-tenant isolation: EF Core must cache a separate compiled
        // model for each schema name, otherwise all requests share the first cached model.
        context.Services.Replace(ServiceDescriptor.Singleton<IModelCacheKeyFactory, TenantModelCacheKeyFactory>());

        Configure<AbpDbContextOptions>(options =>
        {
                /* The main point to change your DBMS.
                 * See also MovieServiceMigrationsDbContextFactory for EF Core tooling. */
            options.UseSqlServer();
        });

    }
}
