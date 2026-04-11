using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace LTC.MovieService;

[DependsOn(
    typeof(MovieServiceDomainSharedModule),
    typeof(AbpObjectExtendingModule)
)]
public class MovieServiceApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        MovieServiceDtoExtensions.Configure();
    }
}
