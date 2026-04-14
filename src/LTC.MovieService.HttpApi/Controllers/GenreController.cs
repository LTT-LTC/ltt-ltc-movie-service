using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.MovieService;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
namespace LTC.MovieService.Controllers;
[Route(MovieServiceSettingNames.DefaultRoute)]
public class GenreController : AbpControllerBase {
 private readonly IGenreAppService _appService;
 public GenreController(IGenreAppService appService) { _appService = appService; }
 [HttpGet("genre-all")]
 public async Task<PagedResultDto<GenreOutputDto>> GetAllAsync([FromQuery] GetGenreListInputDto input) { return await _appService.GetAllAsync(input); }
 [HttpGet("genre/{id}")]
 public async Task<GenreDetailOutputDto> GetAsync(Guid id) { return await _appService.GetAsync(id); }
}
