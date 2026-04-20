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
        Task<PagedResultDto<DistributionOutputDto>> GetListAsync(GetDistributionListInputDto input);
        Task<DistributionOutputDto> CreateAsync(CreateDistributionInputDto input);
        Task<DistributionOutputDto> UpdateAsync(Guid id, UpdateDistributionInputDto input);
        Task DeleteAsync(Guid id);
    }
}