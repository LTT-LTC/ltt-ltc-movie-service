using LTC.MovieService.Genres;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Manager;

[Route(MovieServiceSettingNames.DefaultRoute + "/manager/genre")]
public class GenreManagerController : GenreController
{
    public GenreManagerController(IGenreAppService appService) : base(appService)
    {
    }
}
