using Volo.Abp.Modularity;

namespace LTC.MovieService;

[DependsOn(
    typeof(MovieServiceDomainModule),
    typeof(MovieServiceTestBaseModule)
)]
public class MovieServiceDomainTestModule : AbpModule
{

}
