using LTC.MovieService.Actors;
using LTC.MovieService.Actors.Dtos.Input;
using LTC.MovieService.Actors.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LTC.MovieService.Controllers.Manager;

[Route(MovieServiceSettingNames.DefaultRoute + "/manager/actor")]
[Authorize(Roles = "Manager,manager")]
public class ActorManagerController : ActorController
{
    private readonly IActorAppService _appService;

    public ActorManagerController(IActorAppService appService) : base(appService)
    {
        _appService = appService;
    }

    [HttpPost("actor")]
    public Task<ActorOutputDto> CreateActorAsync(CreateActorInputDto input) => _appService.CreateActorAsync(input);

    [HttpPut("actor/{id}")]
    public Task<ActorOutputDto> UpdateActorAsync(Guid id, UpdateActorInputDto input) => _appService.UpdateActorAsync(id, input);

    [HttpDelete("actor/{id}")]
    public Task DeleteActorAsync(Guid id) => _appService.DeleteActorAsync(id);
}
