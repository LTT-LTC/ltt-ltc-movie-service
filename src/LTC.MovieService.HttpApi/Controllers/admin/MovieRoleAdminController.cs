using LTC.MovieService.Roles;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Admin;

[Route(MovieServiceSettingNames.DefaultRoute + "/admin/movie-role")]
public class MovieRoleAdminController : MovieRoleController
{
    public MovieRoleAdminController(IRoleAppService appService) : base(appService)
    {
    }
}
