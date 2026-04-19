using System;
using System.Threading.Tasks;
using LTC.MovieService.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers
{
    [Route(MovieServiceSettingNames.DefaultRoute)]
    public class DistributionController(IDistributionAppService distributionAppService) : AbpControllerBase
    {
        [HttpGet("movie-distribution-all")]
        public Task<PagedResultDto<DistributionOutputDto>> GetListAsync([FromQuery] GetDistributionListInputDto input)
        {
            /// <summary>
            /// Get list of movie distributions.
            /// </summary>
            return distributionAppService.GetListAsync(input);
        }

        [HttpPost("movie-distribution")]
        [Authorize(Roles = "Admin,Manager")]
        public Task<DistributionOutputDto> CreateAsync(CreateDistributionInputDto input)
        {
            /// <summary>
            /// Create a new movie distribution (Admin/Manager only).
            /// </summary>
            return distributionAppService.CreateAsync(input);
        }

        [HttpPut("movie-distribution/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public Task<DistributionOutputDto> UpdateAsync(Guid id, UpdateDistributionInputDto input)
        {
            /// <summary>
            /// Update a movie distribution (Admin/Manager only).
            /// </summary>
            return distributionAppService.UpdateAsync(id, input);
        }

        [HttpDelete("movie-distribution/{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public Task DeleteAsync(Guid id)
        {
            /// <summary>
            /// Delete a movie distribution (Admin/Manager only).
            /// </summary>
            return distributionAppService.DeleteAsync(id);
        }
    }
}
