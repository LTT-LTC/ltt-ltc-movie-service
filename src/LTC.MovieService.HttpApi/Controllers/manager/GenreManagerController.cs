using LTC.MovieService.Genres;
using LTC.MovieService.Genres.Dtos.Input;
using LTC.MovieService.Genres.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LTC.MovieService.Controllers.Manager;

[Route(MovieServiceSettingNames.DefaultRoute + "/manager/genre")]
[Authorize(Roles = "Manager,manager")]
public class GenreManagerController : GenreController
{
    private readonly IGenreAppService _appService;

    public GenreManagerController(IGenreAppService appService) : base(appService)
    {
        _appService = appService;
    }

    [HttpPost("genre")]
    public Task<GenreOutputDto> CreateGenreAsync(CreateGenreInputDto input) => _appService.CreateGenreAsync(input);

    [HttpPut("genre/{id}")]
    public Task<GenreOutputDto> UpdateGenreAsync(Guid id, UpdateGenreInputDto input) => _appService.UpdateGenreAsync(id, input);

    [HttpDelete("genre/{id}")]
    public Task DeleteGenreAsync(Guid id) => _appService.DeleteGenreAsync(id);
}
