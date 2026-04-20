using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Studios.Dtos.Input;
using LTC.MovieService.Studios.Dtos.Output;

namespace LTC.MovieService.Studios;
public interface IStudioAppService : IApplicationService
{
Task<PagedResultDto<StudioOutputDto>> GetAllAsync(GetStudioListInputDto input);
Task<StudioDetailOutputDto> GetAsync(Guid id);
Task<StudioOutputDto> CreateAsync(CreateStudioInputDto input);
Task<StudioOutputDto> UpdateAsync(Guid id, UpdateStudioInputDto input);
Task DeleteAsync(Guid id);
}
