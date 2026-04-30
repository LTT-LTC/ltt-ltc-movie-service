using LTC.MovieService.MediaFiles;
using LTC.MovieService.MediaFiles.Dtos.Input;
using LTC.MovieService.MediaFiles.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LTC.MovieService.Controllers.Manager
{
    [Route(MovieServiceSettingNames.DefaultRoute + "/manager/media-files")]
    [Authorize(Roles = "Manager,manager")]
    public class MovieMediaFileManagerController : MovieMediaFileController
    {
        public MovieMediaFileManagerController(IMovieMediaFileAppService movieMediaFileAppService) : base(movieMediaFileAppService)
        {
        }

        [HttpPost("poster")]
        [Consumes("multipart/form-data")]
        public Task<UploadMoviePosterOutputDto> UploadPosterAsync([FromForm] UploadMoviePosterInputDto input) =>
            UploadPosterInternalAsync(input);
    }
}
