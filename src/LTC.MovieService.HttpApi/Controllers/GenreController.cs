using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Genres;
using LTC.MovieService.Genres.Dtos.Input;
using LTC.MovieService.Genres.Dtos.Output;

namespace LTC.MovieService.Controllers;

public abstract class GenreController : AbpControllerBase
{
    private readonly IGenreAppService _appService;
    public GenreController(IGenreAppService appService) { _appService = appService; }

    [HttpGet("genre-all")]
    public async Task<PagedResultDto<GenreOutputDto>> GetGenreListAsync([FromQuery] GetGenreListInputDto input) { return await _appService.GetGenreListAsync(input); }

    [HttpGet("genre/{id}")]
    public async Task<GenreDetailOutputDto> GetGenreAsync(Guid id) { return await _appService.GetGenreAsync(id); }

}

