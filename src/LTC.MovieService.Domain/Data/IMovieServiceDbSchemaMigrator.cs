using System.Threading.Tasks;

namespace LTC.MovieService.Data;

public interface IMovieServiceDbSchemaMigrator
{
    Task MigrateAsync();
}
