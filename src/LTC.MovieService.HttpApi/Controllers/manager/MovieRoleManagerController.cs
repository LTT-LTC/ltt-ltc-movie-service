using LTC.MovieService.Roles;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Manager;

[Route(MovieServiceSettingNames.DefaultRoute + "/manager/movie-role")]
public class MovieRoleManagerController : MovieRoleController
{
    public MovieRoleManagerController(IRoleAppService appService) : base(appService)
    {
    }
}
