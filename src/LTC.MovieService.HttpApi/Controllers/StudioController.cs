using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Studios;
using LTC.MovieService.Studios.Dtos.Input;
using LTC.MovieService.Studios.Dtos.Output;

namespace LTC.MovieService.Controllers;

public abstract class StudioController : AbpControllerBase
{
    private readonly IStudioAppService _appService;
    public StudioController(IStudioAppService appService) { _appService = appService; }

    [HttpGet("studio-all")]
    public async Task<PagedResultDto<StudioOutputDto>> GetStudioListAsync([FromQuery] GetStudioListInputDto input) { return await _appService.GetStudioListAsync(input); }

    [HttpGet("studio/{id}")]
    public async Task<StudioDetailOutputDto> GetStudioAsync(Guid id) { return await _appService.GetStudioAsync(id); }

}

