using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Genre.Dtos.Input;
using LTC.MovieService.Genre.Dtos.Output;
namespace LTC.MovieService.Genre;
public interface IGenreAppService : IApplicationService
{
Task<PagedResultDto<GenreOutputDto>> GetAllAsync(GetGenreListInputDto input);
Task<GenreDetailOutputDto> GetAsync(Guid id);
Task<GenreOutputDto> CreateAsync(CreateGenreInputDto input);
Task<GenreOutputDto> UpdateAsync(Guid id, UpdateGenreInputDto input);
Task DeleteAsync(Guid id);
}
