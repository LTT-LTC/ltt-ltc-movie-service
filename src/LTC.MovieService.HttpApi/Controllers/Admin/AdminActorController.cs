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
public class AdminActorController : AbpControllerBase {
 private readonly IActorAppService _appService;
 public AdminActorController(IActorAppService appService) { _appService = appService; }
 [HttpGet("actor-all")]
 public async Task<PagedResultDto<ActorOutputDto>> GetAllAsync([FromQuery] GetActorListInputDto input) { return await _appService.GetAllAsync(input); }
 [HttpGet("actor/{id}")]
 public async Task<ActorDetailOutputDto> GetAsync(Guid id) { return await _appService.GetAsync(id); }
 [HttpPost("actor")]
 public async Task<ActorOutputDto> CreateAsync(CreateActorInputDto input) { return await _appService.CreateAsync(input); }
 [HttpPut("actor/{id}")]
 public async Task<ActorOutputDto> UpdateAsync(Guid id, UpdateActorInputDto input) { return await _appService.UpdateAsync(id, input); }
 [HttpDelete("actor/{id}")]
 public async Task DeleteAsync(Guid id) { await _appService.DeleteAsync(id); }
}
