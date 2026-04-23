using LTC.MovieService.Genres;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Admin;

[Route(MovieServiceSettingNames.DefaultRoute + "/admin/genre")]
public class GenreAdminController : GenreController
{
    public GenreAdminController(IGenreAppService appService) : base(appService)
    {
    }
}
