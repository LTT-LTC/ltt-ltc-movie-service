using Volo.Abp.Modularity;

namespace LTC.MovieService;

/* Inherit from this class for your domain layer tests. */
public abstract class MovieServiceDomainTestBase<TStartupModule> : MovieServiceTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
