using LTC.MovieService.Dtos.Input;
using LTC.MovieService.Dtos.Output;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace LTC.MovieService.Controllers.Admin;

[Route(MovieServiceSettingNames.DefaultRoute + "/admin/movie")]
public class MovieAdminController : MovieController
{
    public MovieAdminController(IMovieAppService movieAppService) : base(movieAppService)
    {
    }
}
