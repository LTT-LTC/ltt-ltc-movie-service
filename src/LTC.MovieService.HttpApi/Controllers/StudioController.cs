using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Studio;
using LTC.MovieService.Studio.Dtos.Input;
using LTC.MovieService.Studio.Dtos.Output;

namespace LTC.MovieService.Controllers;

[Route(MovieServiceSettingNames.DefaultRoute)]
public class StudioController : AbpControllerBase
{
    private readonly IStudioAppService _appService;
    public StudioController(IStudioAppService appService) { _appService = appService; }

    [HttpGet("studio-all")]
    public async Task<PagedResultDto<StudioOutputDto>> GetAllAsync([FromQuery] GetStudioListInputDto input) { return await _appService.GetAllAsync(input); }

    [HttpGet("studio/{id}")]
    public async Task<StudioDetailOutputDto> GetAsync(Guid id) { return await _appService.GetAsync(id); }

    [HttpPost("studio")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<StudioOutputDto> CreateAsync(CreateStudioInputDto input) { return await _appService.CreateAsync(input); }

    [HttpPut("studio/{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<StudioOutputDto> UpdateAsync(Guid id, UpdateStudioInputDto input) { return await _appService.UpdateAsync(id, input); }

    [HttpDelete("studio/{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task DeleteAsync(Guid id) { await _appService.DeleteAsync(id); }
}

