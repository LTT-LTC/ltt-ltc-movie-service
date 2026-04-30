using System;
using System.Threading.Tasks;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LTC.MovieService
{
    public interface IMovieAppService : IApplicationService
    {
        Task<PagedResultDto<MovieOutputDto>> GetMovieListAsync(GetMovieListInputDto input);
        Task<MovieDetailOutputDto> GetMovieAsync(Guid id);
        Task<MovieOutputDto> CreateMovieAsync(CreateMovieInputDto input);
        Task<MovieOutputDto> UpdateMovieAsync(Guid id, UpdateMovieInputDto input);
        Task DeleteMovieAsync(Guid id);
    }
}