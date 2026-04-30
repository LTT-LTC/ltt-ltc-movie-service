using System;
using System.Threading.Tasks;
using LTC.MovieService.Dtos.Output;
using LTC.MovieService.Distributions.Dtos.Input;
using LTC.MovieService.Distributions.Dtos.Output;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;

namespace LTC.MovieService.Distributions
{
    public interface IDistributionAppService : IApplicationService
    {
        Task<PagedResultDto<DistributionOutputDto>> GetDistributionListAsync(GetDistributionListInputDto input);
        Task<DistributionOutputDto> CreateDistributionAsync(CreateDistributionInputDto input);
        Task<DistributionOutputDto> UpdateDistributionAsync(Guid id, UpdateDistributionInputDto input);
        Task DeleteDistributionAsync(Guid id);
    }
}