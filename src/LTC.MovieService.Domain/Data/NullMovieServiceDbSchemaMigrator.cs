using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace LTC.MovieService.Data;

/* This is used if database provider does't define
 * IMovieServiceDbSchemaMigrator implementation.
 */
public class NullMovieServiceDbSchemaMigrator : IMovieServiceDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
