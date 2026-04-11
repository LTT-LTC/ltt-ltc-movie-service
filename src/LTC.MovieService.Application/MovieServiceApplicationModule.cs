using Volo.Abp.Mapperly;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace LTC.MovieService;

[DependsOn(
    typeof(MovieServiceDomainModule),
    typeof(MovieServiceApplicationContractsModule)
    )]
public class MovieServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<MovieServiceApplicationModule>();
    }
}
