using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Genre;
using LTC.MovieService.Genres.Dtos.Input;
using LTC.MovieService.Genres.Dtos.Output;

namespace LTC.MovieService.Controllers;

[Route(MovieServiceSettingNames.DefaultRoute)]
public class GenreController : AbpControllerBase
{
    private readonly IGenreAppService _appService;
    public GenreController(IGenreAppService appService) { _appService = appService; }

    [HttpGet("genre-all")]
    public async Task<PagedResultDto<GenreOutputDto>> GetAllAsync([FromQuery] GetGenreListInputDto input) { return await _appService.GetAllAsync(input); }

    [HttpGet("genre/{id}")]
    public async Task<GenreDetailOutputDto> GetAsync(Guid id) { return await _appService.GetAsync(id); }

    [HttpPost("genre")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<GenreOutputDto> CreateAsync(CreateGenreInputDto input) { return await _appService.CreateAsync(input); }

    [HttpPut("genre/{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<GenreOutputDto> UpdateAsync(Guid id, UpdateGenreInputDto input) { return await _appService.UpdateAsync(id, input); }

    [HttpDelete("genre/{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task DeleteAsync(Guid id) { await _appService.DeleteAsync(id); }
}

