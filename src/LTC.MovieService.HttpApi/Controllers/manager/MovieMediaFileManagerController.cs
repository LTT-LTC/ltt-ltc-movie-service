using LTC.MovieService.MediaFiles;
using Microsoft.AspNetCore.Mvc;

namespace LTC.MovieService.Controllers.Manager
{
    [Route(MovieServiceSettingNames.DefaultRoute + "/manager/media-files")]
    public class MovieMediaFileManagerController : MovieMediaFileController
    {
        public MovieMediaFileManagerController(IMovieMediaFileAppService movieMediaFileAppService) : base(movieMediaFileAppService)
        {
        }
    }
}
