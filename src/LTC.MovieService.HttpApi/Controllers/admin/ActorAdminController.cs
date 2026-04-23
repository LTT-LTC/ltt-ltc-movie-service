using LTC.MovieService.Actors;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Admin;

[Route(MovieServiceSettingNames.DefaultRoute + "/admin/actor")]
public class ActorAdminController : ActorController
{
    public ActorAdminController(IActorAppService appService) : base(appService)
    {
    }
}
