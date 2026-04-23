using LTC.MovieService.MediaFiles;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Admin
{
    [Route(MovieServiceSettingNames.DefaultRoute + "/admin/media-files")]
    public class MovieMediaFileAdminController : MovieMediaFileController
    {
        public MovieMediaFileAdminController(IMovieMediaFileAppService movieMediaFileAppService) : base(movieMediaFileAppService)
        {
        }
    }
}
