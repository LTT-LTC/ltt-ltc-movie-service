using System;
using System.Threading.Tasks;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace LTC.MovieService.Controllers;

[Area(MovieServiceRemoteServiceConsts.ModuleName)]
[RemoteService(Name = MovieServiceRemoteServiceConsts.RemoteServiceName)]
[Route(MovieServiceSettingNames.DefaultRoute)]
public class MovieController : MovieServiceController, IMovieAppService
{
    private readonly IMovieAppService _movieAppService;

    public MovieController(IMovieAppService movieAppService)
    {
        _movieAppService = movieAppService;
    }

    [HttpGet("movie-all")]
    public virtual Task<PagedResultDto<MovieOutputDto>> GetAllAsync([FromQuery] GetMovieListInputDto input)
    {
        return _movieAppService.GetAllAsync(input);
    }

    [HttpGet("movie/{id}")]
    public virtual Task<MovieDetailOutputDto> GetAsync(Guid id)
    {
        return _movieAppService.GetAsync(id);
    }

    [HttpPost("movie")]
    [Authorize(Roles = "Admin,Manager")]
    [Consumes("multipart/form-data")]
    public virtual Task<MovieOutputDto> CreateAsync([FromForm] CreateMovieInputDto input)
    {
        return _movieAppService.CreateAsync(input);
    }

    [HttpPut("movie/{id}")]
    [Authorize(Roles = "Admin,Manager")]
    [Consumes("multipart/form-data")]
    public virtual Task<MovieOutputDto> UpdateAsync(Guid id, [FromForm] UpdateMovieInputDto input)
    {
        return _movieAppService.UpdateAsync(id, input);
    }

    [HttpDelete("movie/{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public virtual Task DeleteAsync(Guid id)
    {
        return _movieAppService.DeleteAsync(id);
    }
}
