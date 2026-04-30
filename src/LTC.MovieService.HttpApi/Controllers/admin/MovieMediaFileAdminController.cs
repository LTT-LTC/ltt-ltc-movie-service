using LTC.MovieService.MediaFiles;
using LTC.MovieService.MediaFiles.Dtos.Input;
using LTC.MovieService.MediaFiles.Dtos.Output;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LTC.MovieService.Controllers.Admin
{
    [Route(MovieServiceSettingNames.DefaultRoute + "/admin/media-files")]
    [Authorize(Roles = "Admin,admin")]
    public class MovieMediaFileAdminController : MovieMediaFileController
    {
        public MovieMediaFileAdminController(IMovieMediaFileAppService movieMediaFileAppService) : base(movieMediaFileAppService)
        {
        }

        [HttpPost("poster")]
        [Consumes("multipart/form-data")]
        public Task<UploadMoviePosterOutputDto> UploadPosterAsync([FromForm] UploadMoviePosterInputDto input) =>
            UploadPosterInternalAsync(input);
    }
}
