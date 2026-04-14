using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
namespace LTC.MovieService;
public interface IStudioAppService : IApplicationService
{
Task<PagedResultDto<StudioOutputDto>> GetAllAsync(GetStudioListInputDto input);
Task<StudioDetailOutputDto> GetAsync(Guid id);
Task<StudioOutputDto> CreateAsync(CreateStudioInputDto input);
Task<StudioOutputDto> UpdateAsync(Guid id, UpdateStudioInputDto input);
Task DeleteAsync(Guid id);
}
