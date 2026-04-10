using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LTC.MovieService.Dto;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace LTC.MovieService.Controllers;

[Area(MovieServiceRemoteServiceConsts.ModuleName)]
[RemoteService(Name = MovieServiceRemoteServiceConsts.RemoteServiceName)]
[Route($"{MovieServiceSettingNames.DefaultRoute}")]
public class MovieController : MovieServiceController, IMovieAppService
{
    private readonly IMovieAppService _movieAppService;

    public MovieController(IMovieAppService movieAppService)
    {
        _movieAppService = movieAppService;
    }

    [HttpGet("movie-all")]
    public virtual Task<List<MovieOutputDto>> GetAllAsync()
    {
        return _movieAppService.GetAllAsync();
    }

    [HttpGet("movie/{id}")]
    public virtual Task<MovieOutputDto> GetAsync(Guid id)
    {
        return _movieAppService.GetAsync(id);
    }
}