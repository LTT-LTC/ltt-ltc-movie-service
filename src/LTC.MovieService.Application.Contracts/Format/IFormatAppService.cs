using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Format.Dtos.Input;
using LTC.MovieService.Format.Dtos.Output;
namespace LTC.MovieService.Format;
public interface IFormatAppService : IApplicationService
{
Task<PagedResultDto<FormatOutputDto>> GetAllAsync(GetFormatListInputDto input);
Task<FormatDetailOutputDto> GetAsync(Guid id);
Task<FormatOutputDto> CreateAsync(CreateFormatInputDto input);
Task<FormatOutputDto> UpdateAsync(Guid id, UpdateFormatInputDto input);
Task DeleteAsync(Guid id);
}
