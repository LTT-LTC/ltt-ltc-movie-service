using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.MovieService;
using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;

namespace LTC.MovieService.Controllers;

[Route(MovieServiceSettingNames.DefaultRoute)]
public class FormatController : AbpControllerBase
{
    private readonly IFormatAppService _appService;
    public FormatController(IFormatAppService appService) { _appService = appService; }

    [HttpGet("format-all")]
    public async Task<PagedResultDto<FormatOutputDto>> GetAllAsync([FromQuery] GetFormatListInputDto input) { return await _appService.GetAllAsync(input); }

    [HttpGet("format/{id}")]
    public async Task<FormatDetailOutputDto> GetAsync(Guid id) { return await _appService.GetAsync(id); }

    [HttpPost("format")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<FormatOutputDto> CreateAsync(CreateFormatInputDto input) { return await _appService.CreateAsync(input); }

    [HttpPut("format/{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<FormatOutputDto> UpdateAsync(Guid id, UpdateFormatInputDto input) { return await _appService.UpdateAsync(id, input); }

    [HttpDelete("format/{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task DeleteAsync(Guid id) { await _appService.DeleteAsync(id); }
}

