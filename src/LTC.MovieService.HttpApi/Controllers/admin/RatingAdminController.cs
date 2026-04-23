using LTC.MovieService.Ratings;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Admin;

[Route(MovieServiceSettingNames.DefaultRoute + "/admin/rating")]
public class RatingAdminController : RatingController
{
    public RatingAdminController(IRatingAppService appService) : base(appService)
    {
    }
}
