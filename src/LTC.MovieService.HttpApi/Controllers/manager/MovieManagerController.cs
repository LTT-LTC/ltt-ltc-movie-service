using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LTC.MovieService.Controllers.Manager;

[Route(MovieServiceSettingNames.DefaultRoute + "/manager/movie")]
[Authorize(Roles = "Manager,manager")]
public class MovieManagerController : MovieController
{
    private readonly IMovieAppService _movieAppService;

    public MovieManagerController(IMovieAppService movieAppService) : base(movieAppService)
    {
        _movieAppService = movieAppService;
    }

    [HttpPost("movie")]
    [Consumes("multipart/form-data")]
    public Task<MovieOutputDto> CreateMovieAsync([FromForm] CreateMovieInputDto input) =>
        _movieAppService.CreateMovieAsync(input);

    [HttpPut("movie/{id}")]
    [Consumes("multipart/form-data")]
    public Task<MovieOutputDto> UpdateMovieAsync(Guid id, [FromForm] UpdateMovieInputDto input) =>
        _movieAppService.UpdateMovieAsync(id, input);

    [HttpDelete("movie/{id}")]
    public Task DeleteMovieAsync(Guid id) => _movieAppService.DeleteMovieAsync(id);
}
