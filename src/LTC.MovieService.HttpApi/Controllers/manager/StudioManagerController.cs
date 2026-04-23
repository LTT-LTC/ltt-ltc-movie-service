using LTC.MovieService.Studios;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Manager;

[Route(MovieServiceSettingNames.DefaultRoute + "/manager/studio")]
public class StudioManagerController : StudioController
{
    public StudioManagerController(IStudioAppService appService) : base(appService)
    {
    }
}
