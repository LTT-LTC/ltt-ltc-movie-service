using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Formats.Dtos.Input;
using LTC.MovieService.Formats.Dtos.Output;

namespace LTC.MovieService.Formats;
public interface IFormatAppService : IApplicationService
{
Task<PagedResultDto<FormatOutputDto>> GetAllAsync(GetFormatListInputDto input);
Task<FormatDetailOutputDto> GetAsync(Guid id);
Task<FormatOutputDto> CreateAsync(CreateFormatInputDto input);
Task<FormatOutputDto> UpdateAsync(Guid id, UpdateFormatInputDto input);
Task DeleteAsync(Guid id);
}
