using LTC.MovieService.Ratings;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Manager;

[Route(MovieServiceSettingNames.DefaultRoute + "/manager/rating")]
public class RatingManagerController : RatingController
{
    public RatingManagerController(IRatingAppService appService) : base(appService)
    {
    }
}
