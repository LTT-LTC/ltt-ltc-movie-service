using LTC.MovieService.Actors;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Manager;

[Route(MovieServiceSettingNames.DefaultRoute + "/manager/actor")]
public class ActorManagerController : ActorController
{
    public ActorManagerController(IActorAppService appService) : base(appService)
    {
    }
}
