using LTC.MovieService.Distributions;
using LTC.MovieService.Distributions.Dtos.Input;
using LTC.MovieService.Distributions.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LTC.MovieService.Controllers.Admin
{
    [Route(MovieServiceSettingNames.DefaultRoute + "/admin/distribution")]
    [Authorize(Roles = "Admin,admin")]
    public class DistributionAdminController : DistributionController
    {
        private readonly IDistributionAppService _distributionAppService;

        public DistributionAdminController(IDistributionAppService distributionAppService) : base(distributionAppService)
        {
            _distributionAppService = distributionAppService;
        }

        [HttpPost("movie-distribution")]
        public Task<DistributionOutputDto> CreateDistributionAsync(CreateDistributionInputDto input) =>
            _distributionAppService.CreateDistributionAsync(input);

        [HttpPut("movie-distribution/{id}")]
        public Task<DistributionOutputDto> UpdateDistributionAsync(Guid id, UpdateDistributionInputDto input) =>
            _distributionAppService.UpdateDistributionAsync(id, input);

        [HttpDelete("movie-distribution/{id}")]
        public Task DeleteDistributionAsync(Guid id) => _distributionAppService.DeleteDistributionAsync(id);
    }
}
