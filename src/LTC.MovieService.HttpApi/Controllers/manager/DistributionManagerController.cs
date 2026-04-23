using LTC.MovieService.Distributions;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Manager
{
    [Route(MovieServiceSettingNames.DefaultRoute + "/manager/distribution")]
    public class DistributionManagerController : DistributionController
    {
        public DistributionManagerController(IDistributionAppService distributionAppService) : base(distributionAppService)
        {
        }
    }
}
