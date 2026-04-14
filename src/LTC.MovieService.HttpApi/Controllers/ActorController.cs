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
public class ActorController : AbpControllerBase {
 private readonly IActorAppService _appService;
 public ActorController(IActorAppService appService) { _appService = appService; }
 [HttpGet("actor-all")]
 public async Task<PagedResultDto<ActorOutputDto>> GetAllAsync([FromQuery] GetActorListInputDto input) { return await _appService.GetAllAsync(input); }
 [HttpGet("actor/{id}")]
 public async Task<ActorDetailOutputDto> GetAsync(Guid id) { return await _appService.GetAsync(id); }
}
