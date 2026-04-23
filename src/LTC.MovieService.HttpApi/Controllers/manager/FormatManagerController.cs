using LTC.MovieService.Formats;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Manager;

[Route(MovieServiceSettingNames.DefaultRoute + "/manager/format")]
public class FormatManagerController : FormatController
{
    public FormatManagerController(IFormatAppService appService) : base(appService)
    {
    }
}
