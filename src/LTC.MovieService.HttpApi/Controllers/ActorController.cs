using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Actors;
using LTC.MovieService.Actors.Dtos.Input;
using LTC.MovieService.Actors.Dtos.Output;

namespace LTC.MovieService.Controllers;

[Route(MovieServiceSettingNames.DefaultRoute)]
public class ActorController : AbpControllerBase
{
    private readonly IActorAppService _appService;
    public ActorController(IActorAppService appService) { _appService = appService; }

    [HttpGet("actor-all")]
    public async Task<PagedResultDto<ActorOutputDto>> GetAllAsync([FromQuery] GetActorListInputDto input) { return await _appService.GetAllAsync(input); }

    [HttpGet("actor/{id}")]
    public async Task<ActorDetailOutputDto> GetAsync(Guid id) { return await _appService.GetAsync(id); }

    [HttpPost("actor")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActorOutputDto> CreateAsync(CreateActorInputDto input) { return await _appService.CreateAsync(input); }

    [HttpPut("actor/{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<ActorOutputDto> UpdateAsync(Guid id, UpdateActorInputDto input) { return await _appService.UpdateAsync(id, input); }

    [HttpDelete("actor/{id}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task DeleteAsync(Guid id) { await _appService.DeleteAsync(id); }
}

