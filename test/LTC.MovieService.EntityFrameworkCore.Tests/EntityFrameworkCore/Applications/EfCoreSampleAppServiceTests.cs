using LTC.MovieService.Samples;
using Xunit;

namespace LTC.MovieService.EntityFrameworkCore.Applications;

[Collection(MovieServiceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<MovieServiceEntityFrameworkCoreTestModule>
{

}
