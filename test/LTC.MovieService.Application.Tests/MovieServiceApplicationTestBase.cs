using Volo.Abp.Modularity;

namespace LTC.MovieService;

public abstract class MovieServiceApplicationTestBase<TStartupModule> : MovieServiceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
