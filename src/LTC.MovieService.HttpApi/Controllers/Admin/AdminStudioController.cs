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
public class AdminStudioController : AbpControllerBase {
 private readonly IStudioAppService _appService;
 public AdminStudioController(IStudioAppService appService) { _appService = appService; }
 [HttpGet("studio-all")]
 public async Task<PagedResultDto<StudioOutputDto>> GetAllAsync([FromQuery] GetStudioListInputDto input) { return await _appService.GetAllAsync(input); }
 [HttpGet("studio/{id}")]
 public async Task<StudioDetailOutputDto> GetAsync(Guid id) { return await _appService.GetAsync(id); }
 [HttpPost("studio")]
 public async Task<StudioOutputDto> CreateAsync(CreateStudioInputDto input) { return await _appService.CreateAsync(input); }
 [HttpPut("studio/{id}")]
 public async Task<StudioOutputDto> UpdateAsync(Guid id, UpdateStudioInputDto input) { return await _appService.UpdateAsync(id, input); }
 [HttpDelete("studio/{id}")]
 public async Task DeleteAsync(Guid id) { await _appService.DeleteAsync(id); }
}
