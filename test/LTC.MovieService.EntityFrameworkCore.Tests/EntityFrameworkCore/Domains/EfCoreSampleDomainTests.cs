using LTC.MovieService.Samples;
using Xunit;

namespace LTC.MovieService.EntityFrameworkCore.Domains;

[Collection(MovieServiceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<MovieServiceEntityFrameworkCoreTestModule>
{

}
