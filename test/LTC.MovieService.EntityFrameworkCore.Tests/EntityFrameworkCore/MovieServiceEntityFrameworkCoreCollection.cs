using Xunit;

namespace LTC.MovieService.EntityFrameworkCore;

[CollectionDefinition(MovieServiceTestConsts.CollectionDefinitionName)]
public class MovieServiceEntityFrameworkCoreCollection : ICollectionFixture<MovieServiceEntityFrameworkCoreFixture>
{

}
