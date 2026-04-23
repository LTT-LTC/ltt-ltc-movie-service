using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Manager;

[Route(MovieServiceSettingNames.DefaultRoute + "/manager/movie")]
public class MovieManagerController : MovieController
{
    public MovieManagerController(IMovieAppService movieAppService) : base(movieAppService)
    {
    }
}
