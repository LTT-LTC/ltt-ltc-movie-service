using LTC.MovieService.Studios;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Admin;

[Route(MovieServiceSettingNames.DefaultRoute + "/admin/studio")]
public class StudioAdminController : StudioController
{
    public StudioAdminController(IStudioAppService appService) : base(appService)
    {
    }
}
