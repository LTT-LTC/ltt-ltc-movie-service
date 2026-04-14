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
public class AdminFormatController : AbpControllerBase {
 private readonly IFormatAppService _appService;
 public AdminFormatController(IFormatAppService appService) { _appService = appService; }
 [HttpGet("format-all")]
 public async Task<PagedResultDto<FormatOutputDto>> GetAllAsync([FromQuery] GetFormatListInputDto input) { return await _appService.GetAllAsync(input); }
 [HttpGet("format/{id}")]
 public async Task<FormatDetailOutputDto> GetAsync(Guid id) { return await _appService.GetAsync(id); }
 [HttpPost("format")]
 public async Task<FormatOutputDto> CreateAsync(CreateFormatInputDto input) { return await _appService.CreateAsync(input); }
 [HttpPut("format/{id}")]
 public async Task<FormatOutputDto> UpdateAsync(Guid id, UpdateFormatInputDto input) { return await _appService.UpdateAsync(id, input); }
 [HttpDelete("format/{id}")]
 public async Task DeleteAsync(Guid id) { await _appService.DeleteAsync(id); }
}
