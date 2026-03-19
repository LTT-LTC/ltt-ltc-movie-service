using LTC.MovieService.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace LTC.MovieService.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MovieServiceEntityFrameworkCoreModule),
    typeof(MovieServiceApplicationContractsModule)
    )]
public class MovieServiceDbMigratorModule : AbpModule
{
}
