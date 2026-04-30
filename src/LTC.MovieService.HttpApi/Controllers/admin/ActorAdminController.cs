using LTC.MovieService.Actors;
using LTC.MovieService.Actors.Dtos.Input;
using LTC.MovieService.Actors.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LTC.MovieService.Controllers.Admin;

[Route(MovieServiceSettingNames.DefaultRoute + "/admin/actor")]
[Authorize(Roles = "Admin,admin")]
public class ActorAdminController : ActorController
{
    private readonly IActorAppService _appService;

    public ActorAdminController(IActorAppService appService) : base(appService)
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
