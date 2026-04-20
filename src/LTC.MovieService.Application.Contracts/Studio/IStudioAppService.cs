using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Studio.Dtos.Input;
using LTC.MovieService.Studio.Dtos.Output;
namespace LTC.MovieService.Studio;
public interface IStudioAppService : IApplicationService
{
Task<PagedResultDto<StudioOutputDto>> GetAllAsync(GetStudioListInputDto input);
Task<StudioDetailOutputDto> GetAsync(Guid id);
Task<StudioOutputDto> CreateAsync(CreateStudioInputDto input);
Task<StudioOutputDto> UpdateAsync(Guid id, UpdateStudioInputDto input);
Task DeleteAsync(Guid id);
}
