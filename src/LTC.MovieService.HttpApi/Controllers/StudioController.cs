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
public class StudioController : AbpControllerBase {
 private readonly IStudioAppService _appService;
 public StudioController(IStudioAppService appService) { _appService = appService; }
 [HttpGet("studio-all")]
 public async Task<PagedResultDto<StudioOutputDto>> GetAllAsync([FromQuery] GetStudioListInputDto input) { return await _appService.GetAllAsync(input); }
 [HttpGet("studio/{id}")]
 public async Task<StudioDetailOutputDto> GetAsync(Guid id) { return await _appService.GetAsync(id); }
}
