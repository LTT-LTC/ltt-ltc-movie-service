using LTC.MovieService.Distributions;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Admin
{
    [Route(MovieServiceSettingNames.DefaultRoute + "/admin/distribution")]
    public class DistributionAdminController : DistributionController
    {
        public DistributionAdminController(IDistributionAppService distributionAppService) : base(distributionAppService)
        {
        }
    }
}
