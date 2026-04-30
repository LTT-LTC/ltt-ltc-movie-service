using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Studios.Dtos.Input;
using LTC.MovieService.Studios.Dtos.Output;

namespace LTC.MovieService.Studios;
public interface IStudioAppService : IApplicationService
{
Task<PagedResultDto<StudioOutputDto>> GetStudioListAsync(GetStudioListInputDto input);
Task<StudioDetailOutputDto> GetStudioAsync(Guid id);
Task<StudioOutputDto> CreateStudioAsync(CreateStudioInputDto input);
Task<StudioOutputDto> UpdateStudioAsync(Guid id, UpdateStudioInputDto input);
Task DeleteStudioAsync(Guid id);
}
