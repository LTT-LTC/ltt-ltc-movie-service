using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LTC.MovieService
{
    public interface IMovieAppService : IApplicationService
    {
        Task<PagedResultDto<MovieOutputDto>> GetAllAsync(GetMovieListInputDto input);
        Task<MovieDetailOutputDto> GetAsync(Guid id);
        Task<MovieOutputDto> CreateAsync(CreateMovieInputDto input);
        Task<MovieOutputDto> UpdateAsync(Guid id, UpdateMovieInputDto input);
        Task DeleteAsync(Guid id);
    }
}
