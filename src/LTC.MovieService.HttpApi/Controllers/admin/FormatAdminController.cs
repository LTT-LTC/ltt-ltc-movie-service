using LTC.MovieService.Formats;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Admin;

[Route(MovieServiceSettingNames.DefaultRoute + "/admin/format")]
public class FormatAdminController : FormatController
{
    public FormatAdminController(IFormatAppService appService) : base(appService)
    {
    }
}
