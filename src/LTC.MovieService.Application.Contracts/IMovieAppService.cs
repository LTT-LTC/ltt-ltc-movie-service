using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LTC.MovieService.Dto;
using Volo.Abp.Application.Services;

namespace LTC.MovieService
{
    public interface IMovieAppService : IApplicationService
    {
        Task<List<MovieOutputDto>> GetAllAsync();
        Task<MovieOutputDto> GetAsync(Guid id);
    }
}