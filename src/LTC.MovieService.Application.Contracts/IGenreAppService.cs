using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
namespace LTC.MovieService;
public interface IGenreAppService : IApplicationService
{
Task<PagedResultDto<GenreOutputDto>> GetAllAsync(GetGenreListInputDto input);
Task<GenreDetailOutputDto> GetAsync(Guid id);
Task<GenreOutputDto> CreateAsync(CreateGenreInputDto input);
Task<GenreOutputDto> UpdateAsync(Guid id, UpdateGenreInputDto input);
Task DeleteAsync(Guid id);
}
