using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Genres.Dtos.Input;
using LTC.MovieService.Genres.Dtos.Output;

namespace LTC.MovieService.Genres;
public interface IGenreAppService : IApplicationService
{
Task<PagedResultDto<GenreOutputDto>> GetAllAsync(GetGenreListInputDto input);
Task<GenreDetailOutputDto> GetAsync(Guid id);
Task<GenreOutputDto> CreateAsync(CreateGenreInputDto input);
Task<GenreOutputDto> UpdateAsync(Guid id, UpdateGenreInputDto input);
Task DeleteAsync(Guid id);
}
