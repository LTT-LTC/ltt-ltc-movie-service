using LTC.MovieService.Genres;
using LTC.MovieService.Genres.Dtos.Input;
using LTC.MovieService.Genres.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LTC.MovieService.Controllers.Admin;

[Route(MovieServiceSettingNames.DefaultRoute + "/admin/genre")]
[Authorize(Roles = "Admin,admin")]
public class GenreAdminController : GenreController
{
    private readonly IGenreAppService _appService;

    public GenreAdminController(IGenreAppService appService) : base(appService)
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
