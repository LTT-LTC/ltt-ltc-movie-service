using System;
using System.Threading.Tasks;
using LTC.MovieService.Distributions;
using LTC.MovieService.Distributions.Dtos.Input;
using LTC.MovieService.Distributions.Dtos.Output;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers
{
    public abstract class DistributionController(IDistributionAppService distributionAppService) : AbpControllerBase
    {
        [HttpGet("movie-distribution-all")]
        public Task<PagedResultDto<DistributionOutputDto>> GetDistributionListAsync([FromQuery] GetDistributionListInputDto input)
        {
            /// <summary>
            /// Get list of movie distributions.
            /// </summary>
            return distributionAppService.GetDistributionListAsync(input);
        }

    }
}
