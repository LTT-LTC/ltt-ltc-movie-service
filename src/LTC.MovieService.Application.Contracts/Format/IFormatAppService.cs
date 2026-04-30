using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Formats.Dtos.Input;
using LTC.MovieService.Formats.Dtos.Output;

namespace LTC.MovieService.Formats;
public interface IFormatAppService : IApplicationService
{
Task<PagedResultDto<FormatOutputDto>> GetFormatListAsync(GetFormatListInputDto input);
Task<FormatDetailOutputDto> GetFormatAsync(Guid id);
Task<FormatOutputDto> CreateFormatAsync(CreateFormatInputDto input);
Task<FormatOutputDto> UpdateFormatAsync(Guid id, UpdateFormatInputDto input);
Task DeleteFormatAsync(Guid id);
}
