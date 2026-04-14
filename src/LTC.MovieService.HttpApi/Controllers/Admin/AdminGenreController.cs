using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.MovieService;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
namespace LTC.MovieService.Controllers.Admin;
[Route(MovieServiceSettingNames.DefaultRoute + "/admin")]
public class AdminGenreController : AbpControllerBase {
 private readonly IGenreAppService _appService;
 public AdminGenreController(IGenreAppService appService) { _appService = appService; }
 [HttpGet("genre-all")]
 public async Task<PagedResultDto<GenreOutputDto>> GetAllAsync([FromQuery] GetGenreListInputDto input) { return await _appService.GetAllAsync(input); }
 [HttpGet("genre/{id}")]
 public async Task<GenreDetailOutputDto> GetAsync(Guid id) { return await _appService.GetAsync(id); }
 [HttpPost("genre")]
 public async Task<GenreOutputDto> CreateAsync(CreateGenreInputDto input) { return await _appService.CreateAsync(input); }
 [HttpPut("genre/{id}")]
 public async Task<GenreOutputDto> UpdateAsync(Guid id, UpdateGenreInputDto input) { return await _appService.UpdateAsync(id, input); }
 [HttpDelete("genre/{id}")]
 public async Task DeleteAsync(Guid id) { await _appService.DeleteAsync(id); }
}
