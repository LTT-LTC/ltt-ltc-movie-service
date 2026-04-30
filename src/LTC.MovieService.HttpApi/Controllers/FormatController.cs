using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Formats;
using LTC.MovieService.Formats.Dtos.Input;
using LTC.MovieService.Formats.Dtos.Output;

namespace LTC.MovieService.Controllers;

public abstract class FormatController : AbpControllerBase
{
    private readonly IFormatAppService _appService;
    public FormatController(IFormatAppService appService) { _appService = appService; }

    [HttpGet("format-all")]
    public async Task<PagedResultDto<FormatOutputDto>> GetFormatListAsync([FromQuery] GetFormatListInputDto input) { return await _appService.GetFormatListAsync(input); }

    [HttpGet("format/{id}")]
    public async Task<FormatDetailOutputDto> GetFormatAsync(Guid id) { return await _appService.GetFormatAsync(id); }

}

