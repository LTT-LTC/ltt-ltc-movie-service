using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Genres.Dtos.Input;
using LTC.MovieService.Genres.Dtos.Output;

namespace LTC.MovieService.Genres;
public interface IGenreAppService : IApplicationService
{
Task<PagedResultDto<GenreOutputDto>> GetGenreListAsync(GetGenreListInputDto input);
Task<GenreDetailOutputDto> GetGenreAsync(Guid id);
Task<GenreOutputDto> CreateGenreAsync(CreateGenreInputDto input);
Task<GenreOutputDto> UpdateGenreAsync(Guid id, UpdateGenreInputDto input);
Task DeleteGenreAsync(Guid id);
}
