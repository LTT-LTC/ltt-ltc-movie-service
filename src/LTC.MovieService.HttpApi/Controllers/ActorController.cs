using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using LTC.MovieService.Actors;
using LTC.MovieService.Actors.Dtos.Input;
using LTC.MovieService.Actors.Dtos.Output;

namespace LTC.MovieService.Controllers;

public abstract class ActorController : AbpControllerBase
{
    private readonly IActorAppService _appService;
    public ActorController(IActorAppService appService) { _appService = appService; }

    [HttpGet("actor-all")]
    public async Task<PagedResultDto<ActorOutputDto>> GetActorListAsync([FromQuery] GetActorListInputDto input) { return await _appService.GetActorListAsync(input); }

    [HttpGet("actor/{id}")]
    public async Task<ActorDetailOutputDto> GetActorAsync(Guid id) { return await _appService.GetActorAsync(id); }

}

