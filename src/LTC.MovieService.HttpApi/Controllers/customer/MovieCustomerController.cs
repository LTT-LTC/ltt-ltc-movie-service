using System;
using System.Threading.Tasks;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace LTC.MovieService.Controllers.Customer;

[Route(MovieServiceSettingNames.DefaultRoute)]
public class MovieCustomerController : MovieServiceController
{
    private readonly IMovieAppService _movieAppService;

    public MovieCustomerController(IMovieAppService movieAppService)
    {
        _movieAppService = movieAppService;
    }

    [AllowAnonymous]
    [HttpGet("movie-all")]
    public Task<PagedResultDto<MovieOutputDto>> GetMovieListAsync(GetMovieListInputDto input)
    {
        return _movieAppService.GetMovieListAsync(input);
    }

    [AllowAnonymous]
    [HttpGet("movie/{id}")]
    public Task<MovieDetailOutputDto> GetMovieAsync(Guid id)
    {
        return _movieAppService.GetMovieAsync(id);
    }
}
