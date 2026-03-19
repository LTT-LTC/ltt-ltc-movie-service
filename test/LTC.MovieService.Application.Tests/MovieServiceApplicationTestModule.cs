using Volo.Abp.Modularity;

namespace LTC.MovieService;

[DependsOn(
    typeof(MovieServiceApplicationModule),
    typeof(MovieServiceDomainTestModule)
)]
public class MovieServiceApplicationTestModule : AbpModule
{

}
